using LogicBuilder.Expressions.Utils.ExpressionDescriptors;
using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CreateExpressionParametersFromDescriptors
{
    static class WriterForDescriptorClassToParameterClass
    {
        #region Constants
        const string DECLARATION = "#Declaration#";
        const string CONSTRUCTORS = "#Constructors#";
        const string PROPERTIES = "#Properties#";
        const string DESCRIPTORS_NAMESPACE_DOT = "LogicBuilder.Expressions.Utils.ExpressionDescriptors.";
        const string EXPRESSION_CLASS_SUFFIX = "OperatorParameter";
        const string DESCRIPTOR = "Descriptor";
        const string NEWINTERFACE = "IExpressionParameter";
        const string OLDINTERFACE = "IExpressionDescriptor";
        const string SAVE_PATH = @"C:\.github\BlaiseD\Contoso.XPlatform\Contoso.Parameters\Expressions";
        #endregion Constants

        internal static void Write()
        {
            using (CSharpCodeProvider compiler = new CSharpCodeProvider())
            {
                typeof(IExpressionDescriptor).Assembly.GetTypes().Where(p => typeof(IExpressionDescriptor).IsAssignableFrom(p)
                                && !p.GetTypeInfo().IsGenericTypeDefinition
                                && !p.GetTypeInfo().IsInterface)
                                .ToList()
                                .ForEach(t =>
                                {
                                    WriteClass(t, compiler);
                                });
            }
        }

        private static void WriteClass(Type type, CSharpCodeProvider compiler)
        {
            DoWrite
            (
                type.Name,
                type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanWrite && p.CanRead)
                    .Select(p => GetPropertyString(p, compiler))
            );

            void DoWrite(string name, IEnumerable<string> propertiesList)
            {
                string newName = name.Replace(DESCRIPTOR, EXPRESSION_CLASS_SUFFIX);

                using (StreamWriter sr = new StreamWriter($"{SAVE_PATH}\\{newName}.cs", false, Encoding.UTF8))
                {
                    sr.Write
                    (
                        File.ReadAllText(GetTemplateFilePath(Directory.GetCurrentDirectory()))
                            .Replace(DECLARATION, GetDeclaration(type, newName))
                            .Replace(CONSTRUCTORS, GetConstructors(type, compiler))
                            .Replace
                            (
                                PROPERTIES,
                                propertiesList.Any() 
                                    ? $"{Environment.NewLine}{Environment.NewLine}{string.Join(Environment.NewLine, propertiesList)}"
                                    : ""
                            )
                    );
                }
            }
        }

        private static string GetDeclaration(Type type, string newName) 
            => type.BaseType == typeof(object)
                ? $"{newName} : {NEWINTERFACE}"
                : $"{newName} : {type.BaseType.Name.Replace(DESCRIPTOR, EXPRESSION_CLASS_SUFFIX)}";

        private static string GetTemplateFilePath(string directory)
            => $"{directory}\\ParameterClassTemplate.txt";

        private static string GetPropertyString(PropertyInfo propertyInfo, CSharpCodeProvider compiler) 
            => $"\t\tpublic {propertyInfo.PropertyType.GetNewPropertyClassName(compiler)} {propertyInfo.Name} {{ get; set; }}";

        private static string GetConstructors(Type type, CSharpCodeProvider compiler)
        {
            string newTypeName = type.Name.Replace(DESCRIPTOR, EXPRESSION_CLASS_SUFFIX);
            return string.Join($"{Environment.NewLine}{Environment.NewLine}", type.GetConstructors().Select(con => WriteConstructor(con)));

            string WriteConstructor(ConstructorInfo constructor)
            {
                List<string> stringBuilder = new List<string>();

                if (type.BaseType == typeof(object))
                    stringBuilder.Add($"\t\tpublic {newTypeName}({GetConstructorArguments(constructor, compiler)})");
                else
                    stringBuilder.Add($"\t\tpublic {newTypeName}({GetConstructorArguments(constructor, compiler)}) : base({GetBaseArguments(constructor)})");

                stringBuilder.Add("\t\t{");
                if (type.BaseType == typeof(object))
                    stringBuilder.Add($"{GetConstructorAssignments(constructor, compiler)}");
                stringBuilder.Add("\t\t}");

                return string.Join(Environment.NewLine, stringBuilder);
            }
        }

        private static string GetConstructorArguments(ConstructorInfo constructor, CSharpCodeProvider compiler)
        {
            List<string> stringBuilder = new List<string>();
            foreach (ParameterInfo info in constructor.GetParameters())
                stringBuilder.Add($"{info.ParameterType.GetNewPropertyClassName(compiler)} {info.Name}");

            return string.Join(", ", stringBuilder);
        }

        private static string GetBaseArguments(ConstructorInfo constructor) 
            => string.Join(", ", constructor.GetParameters().Select(info => info.Name));

        private static string GetConstructorAssignments(ConstructorInfo constructor, CSharpCodeProvider compiler)
        {
            List<string> stringBuilder = new List<string>();
            foreach (ParameterInfo info in constructor.GetParameters())
                stringBuilder.Add($"\t\t\t{GetPropertyName(info.Name)} = {info.Name};");

            return string.Join(Environment.NewLine, stringBuilder);
        }

        private static string GetPropertyName(string parameterName)
            => $"{parameterName[0].ToString().ToUpperInvariant()}{parameterName.Substring(1)}";

        private static string GetNewPropertyClassName(this Type propertyType, CSharpCodeProvider compiler)
        {
            if (propertyType.GetTypeInfo().IsGenericType && propertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {//Nullable value type
                return string.Concat
                                (
                                    compiler.GetTypeOutput(new CodeTypeReference(Nullable.GetUnderlyingType(propertyType))), "?"
                                ).Replace(DESCRIPTORS_NAMESPACE_DOT, "");
            }
            else if (propertyType.GetTypeInfo().IsGenericType && typeof(System.Collections.IEnumerable).IsAssignableFrom(propertyType))
            {//other Generic Types that are enumerable i.e. lists, dictionaries, etc
                string init = propertyType.Name.Substring(0, propertyType.Name.IndexOf("`"));
                int count = 0;
                Type[] genericArguments = propertyType.GetGenericArguments();
                System.Text.StringBuilder declaration = genericArguments.Aggregate(new System.Text.StringBuilder(init + "<"), (sb, next) =>
                {
                    count++;
                    if (!next.GetTypeInfo().IsValueType && next != typeof(string) && !typeof(System.Collections.IEnumerable).IsAssignableFrom(next))
                    {
                        sb.Append(next.ToString().Replace(DESCRIPTORS_NAMESPACE_DOT, "").Replace(OLDINTERFACE, NEWINTERFACE).Replace(DESCRIPTOR, EXPRESSION_CLASS_SUFFIX));
                    }
                    else
                    {
                        sb.Append(next.GetNewPropertyClassName(compiler));
                    }
                    //System.Text.RegularExpressions.Regex.Replace("", "xxx$", "");//Replace at end of string
                    sb.Append((count < genericArguments.Length) ? ", " : ">");
                    return sb;
                });

                return declaration.ToString().Replace(DESCRIPTORS_NAMESPACE_DOT, "");
            }
            else if (!propertyType.GetTypeInfo().IsValueType && propertyType != typeof(string) && !typeof(System.Collections.IEnumerable).IsAssignableFrom(propertyType))
            {//other reference types
                return propertyType.ToString().Replace(DESCRIPTORS_NAMESPACE_DOT, "").Replace(OLDINTERFACE, NEWINTERFACE).Replace(DESCRIPTOR, EXPRESSION_CLASS_SUFFIX);
            }
            else
            {//value types
                return compiler.GetTypeOutput(new CodeTypeReference(propertyType)).Replace(DESCRIPTORS_NAMESPACE_DOT, "");
            }
        }
    }
}
