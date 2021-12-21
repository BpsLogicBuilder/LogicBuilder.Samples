using LogicBuilder.Expressions.Utils;
using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace CreateExpressionParametersFromOperators
{
    static class CreateExpressionParameterClasses
    {
        internal static void Write()
        {
            using (CSharpCodeProvider compiler = new CSharpCodeProvider())
            {
                typeof(LogicBuilder.Expressions.Utils.ExpressionBuilder.ParameterOperator).Assembly.GetTypes().Where
                                (
                                    p => p.Namespace != null &&
                                    p.Namespace.StartsWith("LogicBuilder.Expressions.Utils.ExpressionBuilder")
                                    && !p.IsEnum
                                    && !p.IsGenericTypeDefinition
                                    && !p.IsInterface
                                    && (p.FullName.EndsWith(OPERATOR) || p.FullName.EndsWith("OperatorBase"))
                                    && Attribute.GetCustomAttribute(p, typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute)) == null
                                )
                                .ToList()
                                .ForEach(t =>
                                {
                                    WriteCommonClass(t, compiler);
                                });
            }
        }

        const string PROPERTIES = "#Properties#";
        const string NAMESPACES = "#NameSpaces#";
        const string CONSTRUCTORS = "#Constructors#";
        const string PARAMETER_NAMESPACE_DOT = "LogicBuilder.Expressions.Utils.ExpressionBuilder.";
        static readonly string DESCRIPTOR_SAVE_PATH = $@"{Constants.BASEPATH}\Expressions";
        const string OPERATOR = "Operator";
        const string OPERATORPARAMETERS = "OperatorParameters";
        const string VIEW_CLASS_SUFFIX = "View";

        private static void WriteCommonClass(Type type, CSharpCodeProvider compiler)
        {
            string name = type.Name.Replace(OPERATOR, OPERATORPARAMETERS );

            List<string> propertiesList = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .Aggregate(new List<string>(), (list, p) =>
                {
                    if (p.Name == "Parameters")
                        return list;

                    if (p.Name == "Operator" && p.PropertyType.Name == "FilterFunction")
                        return list;

                    if (p.GetMethod.IsAbstract)
                        list.Add(string.Format("\t\tabstract public {0} {1} {{ get; set; }}", p.PropertyType.GetNewPropertyClassName(compiler, replaceCommonTypeName), p.Name));
                    else if (p.GetMethod.IsVirtual && p.GetMethod.GetBaseDefinition() == p.GetMethod)
                        list.Add(string.Format("\t\tvirtual public {0} {1} {{ get; set; }}", p.PropertyType.GetNewPropertyClassName(compiler, replaceCommonTypeName), p.Name));
                    else if (p.GetMethod.GetBaseDefinition() != p.GetMethod)
                        list.Add(string.Format("\t\tpublic override {0} {1} {{ get; set; }}", p.PropertyType.GetNewPropertyClassName(compiler, replaceCommonTypeName), p.Name));
                    else
                        list.Add(string.Format("\t\tpublic {0} {1} {{ get; set; }}", p.PropertyType.GetNewPropertyClassName(compiler, replaceCommonTypeName), p.Name));

                    return list;
                });

            List<string> propertyNameList = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .Aggregate(new List<string>(), (list, p) =>
                {
                    if (p.Name == "Parameters")
                        return list;

                    if (p.Name == "Operator" && p.PropertyType.Name == "FilterFunction")
                        return list;

                    list.Add(p.Name);

                    return list;
                });

            HashSet<string> nameSpaces = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .Select(p => p.PropertyType)
                .Aggregate(new HashSet<string>(), (list, next) =>
                {
                    if (next == typeof(object)
                        || next == typeof(string)
                        || (next.IsLiteralType() && !typeof(Enum).IsAssignableFrom(next) && !(next.IsGenericType && next.GetGenericTypeDefinition() == typeof(Nullable<>))))
                        return list;

                    if (!next.Namespace.StartsWith("LogicBuilder.Expressions.Utils.ExpressionBuilder"))
                        list.Add($"using {next.Namespace};");

                    return list;
                });

            string constructorString = GetConstructors(type, compiler, propertyNameList);


            string baseClassString = type.BaseType != typeof(object) ? $" : {type.BaseType.Name}" : " : IExpressionParameter";

            string nameSpacesString = nameSpaces.Any()
                ? $"{string.Join(Environment.NewLine, nameSpaces.OrderBy(n => n))}{Environment.NewLine}{Environment.NewLine}"
                : "";

            string propertiestring = propertiesList.Any()
                ? $"{Environment.NewLine}{Environment.NewLine}{string.Join(Environment.NewLine, propertiesList)}"
                : "";

            string text = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\ParameterClassTemplate.txt")
                .Replace("#Name#", name)
                .Replace(PROPERTIES, propertiestring)
                .Replace(NAMESPACES, nameSpacesString)
                .Replace(CONSTRUCTORS, constructorString)
                .Replace("#Base#", baseClassString.Replace(OPERATOR, OPERATORPARAMETERS ));

            text = text.Replace("#Modifier#", type.IsAbstract ? "abstract " : "");

            using (StreamWriter sr = new StreamWriter($"{DESCRIPTOR_SAVE_PATH}\\{name}.cs", false, Encoding.UTF8))
            {
                sr.Write(text);
            }
        }

        private static string GetConstructors(Type type, CSharpCodeProvider compiler, List<string> propertiesList)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetParameterlessConstructor(type));
            sb.Append(GetConstructor(type, compiler, propertiesList));

            return sb.ToString();
        }

        private static string GetParameterlessConstructor(Type type)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"\t\tpublic {type.Name.Replace(OPERATOR, OPERATORPARAMETERS )}()");
            sb.Append($"{Environment.NewLine}\t\t{{");
            sb.Append($"{Environment.NewLine}\t\t}}");
            return sb.ToString();
        }

        private static string GetConstructor(Type type, CSharpCodeProvider compiler, List<string> propertiesList)
        {
            Dictionary<ConstructorInfo, ParameterInfo[]> constructors = type.GetConstructors().ToDictionary
            (
                c => c,
                c => c.GetParameters()
            );

            ConstructorInfo constructor = constructors.OrderByDescending(c => c.Value.Length).First().Key;
            Dictionary<ConstructorInfo, HashSet<string>> constructorsWithFewerParameters = constructors
                .Where(c => c.Key != constructor)
                .ToDictionary(i => i.Key, i => new HashSet<string>(i.Value.Select(p => p.Name)));

            ParameterInfo[] parameters = constructor.GetParameters().Where(p => p.Name != "parameters").OrderBy(p => (p.IsOptional || ConsiderOptional(p))).ToArray();
            if (!parameters.Any())
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append($"\t\tpublic {constructor.DeclaringType.Name.Replace(OPERATOR, OPERATORPARAMETERS )}(");
            IEnumerable<string> parameterStrings = parameters.Select(p => GetParameterString(p));
            if (constructor.DeclaringType.BaseType == typeof(object))
            {
                sb.Append(string.Join(", ", parameters.Select(p => GetParameterString(p))));
                sb.Append(")");
            }
            else
            {
                sb.Append(string.Join(", ", parameters.Select(p => GetParameterString(p))));
                sb.Append(")");

                IEnumerable<ParameterInfo> baseConstructorParameters = parameters.Where(p => !propertiesList.Contains(FirstCharToUpper(p.Name)));
                sb.Append($" : base({string.Join(", ", baseConstructorParameters.Select(p => p.Name))})");
            }

            sb.Append($"{Environment.NewLine}\t\t{{");
            if (constructor.DeclaringType.BaseType == typeof(object))
            {
                foreach (ParameterInfo parameter in parameters)
                    sb.Append($"{Environment.NewLine}\t\t\t{FirstCharToUpper(parameter.Name)} = {parameter.Name};");
            }
            else
            {
                IEnumerable<ParameterInfo> declaringClassConstructorParameters = parameters.Where(p => propertiesList.Contains(FirstCharToUpper(p.Name)));
                foreach (ParameterInfo parameter in declaringClassConstructorParameters)
                    sb.Append($"{Environment.NewLine}\t\t\t{FirstCharToUpper(parameter.Name)} = {parameter.Name};");
            }

            sb.Append($"{Environment.NewLine}\t\t}}");
            return sb.ToString();

            string FirstCharToUpper(string parameterName)
                => $"{parameterName[0].ToString().ToUpperInvariant()}{parameterName.Substring(1)}";

            bool ConsiderOptional(ParameterInfo parameter)
            {
                foreach(var kvp in constructorsWithFewerParameters)
                {
                    if (!kvp.Value.Contains(parameter.Name))
                        return true;
                }

                return false;
            }

            string GetParameterString(ParameterInfo parameter)
            {
                string paramsString = Attribute.GetCustomAttribute(parameter, typeof(ParamArrayAttribute)) != null
                    ? "params "
                    : "";

                return $"{paramsString}{parameter.ParameterType.GetNewPropertyClassName(compiler, replaceCommonTypeName)} {parameter.Name}{GetDefaultValue(parameter)}";
            }

            string GetDefaultValue(ParameterInfo parameter)
            {
                if (!parameter.IsOptional && !ConsiderOptional(parameter))
                    return "";

                if (parameter.ParameterType == typeof(string))
                    return " = null";
                //return $" = {parameter.DefaultValue?.ToString() ?? "null"}";
                else if (parameter.ParameterType.IsValueType)
                    return $" = {parameter.DefaultValue.ToString() ?? Activator.CreateInstance(parameter.ParameterType).ToString()}";
                else
                    return " = null";
            }
        }

        //private static string GetDefaultValue(ParameterInfo parameter)
        //{
        //    if (!parameter.IsOptional)
        //        return "";

        //    if (parameter.ParameterType == typeof(string))
        //        return $" = {parameter.DefaultValue?.ToString() ?? "null"}";
        //    else if (parameter.ParameterType.IsValueType)
        //        return $" = {parameter.DefaultValue.ToString() ?? Activator.CreateInstance(parameter.ParameterType).ToString()}";
        //    else
        //        return " =  null";
        //}

        //private static string GetParameterString(ParameterInfo parameter, CSharpCodeProvider compiler)
        //{

        //    string paramsString = Attribute.GetCustomAttribute(parameter, typeof(ParamArrayAttribute)) != null
        //        ? "params "
        //        : "";

        //    return $"{paramsString}{parameter.ParameterType.GetNewPropertyClassName(compiler, replaceCommonTypeName)} {parameter.Name}{GetDefaultValue(parameter)}";
        //}

        //list.Add(string.Format("\t\tvirtual public {0} {1} {{ get; set; }}", p.PropertyType.GetNewPropertyClassName(compiler, replaceCommonTypeName), p.Name));

        static readonly Func<string, string> replaceCommonTypeName = oldName =>
        {
            return Regex.Replace(oldName, "Operator$", OPERATORPARAMETERS )
                .Replace("IExpressionPart", "IExpressionParameter")
                .Replace(PARAMETER_NAMESPACE_DOT, "")
                .Replace("LogicBuilder.Expressions.Utils.Strutures.", "")
                .Replace("System.", "");
        };

        private static string GetNewPropertyClassName(this Type propertyType, CSharpCodeProvider compiler, Func<string, string> replace)
        {
            string final = "";
            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {//Nullable value type
                final = replace(string.Concat
                                (
                                    compiler.GetTypeOutput(new CodeTypeReference(Nullable.GetUnderlyingType(propertyType))), "?"
                                ));
            }
            else if (propertyType.IsGenericType && typeof(System.Collections.IEnumerable).IsAssignableFrom(propertyType))
            {//other Generic Types that are enumerable i.e. lists, dictionaries, etc
                string init = propertyType.Name.Substring(0, propertyType.Name.IndexOf("`"));
                int count = 0;
                Type[] genericArguments = propertyType.GetGenericArguments();
                System.Text.StringBuilder declaration = genericArguments.Aggregate(new System.Text.StringBuilder(init + "<"), (sb, next) =>
                {
                    count++;
                    if (!next.IsValueType && next != typeof(object) && next != typeof(string) && !typeof(System.Collections.IEnumerable).IsAssignableFrom(next))
                    {
                        sb.Append(replace(next.Name));
                    }
                    else
                    {
                        sb.Append(next.GetNewPropertyClassName(compiler, replace));
                    }

                    sb.Append((count < genericArguments.Length) ? ", " : ">");
                    return sb;
                });

                final = replace(declaration.ToString());
            }
            else if (!propertyType.IsValueType && propertyType != typeof(object) && propertyType != typeof(string) && !typeof(System.Collections.IEnumerable).IsAssignableFrom(propertyType))
            {//other reference types
                final = replace(propertyType.Name);
            }
            else
            {//value types
                final = replace(compiler.GetTypeOutput(new CodeTypeReference(propertyType)));
            }

            return final;
        }
    }

    internal struct Constants
    {
        internal const string BASEPATH = @"C:\.github\BlaiseD\Contoso.XPlatform\Contoso.Parameters";
    }
}
