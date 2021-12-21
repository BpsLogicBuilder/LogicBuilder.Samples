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

namespace CreateExpressionDescriptorsFromOperators
{
    static class CreateExpressionDescriptorClasses
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
        const string PARAMETER_NAMESPACE_DOT = "LogicBuilder.Expressions.Utils.ExpressionBuilder.";
        static readonly string DESCRIPTOR_SAVE_PATH = $@"{Constants.BASEPATH}\ExpressionDescriptors";
        const string OPERATOR = "Operator";
        const string OPERATORDESCRIPTOR = "OperatorDescriptor";

        private static void WriteCommonClass(Type type, CSharpCodeProvider compiler)
        {
            string name = type.Name.Replace(OPERATOR, OPERATORDESCRIPTOR);

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

            SortedSet<string> nameSpaces = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .Select(p => p.PropertyType)
                .Aggregate(new SortedSet<string>(), (list, next) =>
                {
                    if (next == typeof(object)
                        || next == typeof(string)
                        || (next.IsLiteralType() && !typeof(Enum).IsAssignableFrom(next) && !(next.IsGenericType && next.GetGenericTypeDefinition() == typeof(Nullable<>))))
                        return list;

                    if (!next.Namespace.StartsWith("LogicBuilder.Expressions.Utils.ExpressionBuilder"))
                        list.Add($"using {next.Namespace};");

                    return list;
                });

            string baseClassString = type.BaseType != typeof(object) ? $" : {type.BaseType.Name.Replace(OPERATOR, OPERATORDESCRIPTOR)}" : " : OperatorDescriptorBase";

            string nameSpacesString = nameSpaces.Any()
                ? $"{string.Join(Environment.NewLine, nameSpaces.OrderBy(n => n))}{Environment.NewLine}{Environment.NewLine}"
                : "";

            string propertiestring = propertiesList.Any()
                ? $"{string.Join(Environment.NewLine, propertiesList)}"
                : "";

            string text = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\DescriptorClassTemplate.txt")
                .Replace("#Name#", name)
                .Replace(PROPERTIES, propertiestring)
                .Replace(NAMESPACES, nameSpacesString)
                .Replace("#Base#", baseClassString);

            text = text.Replace("#Modifier#", type.IsAbstract ? "abstract " : "");

            using (StreamWriter sr = new StreamWriter($"{DESCRIPTOR_SAVE_PATH}\\{name}.cs", false, Encoding.UTF8))
            {
                sr.Write(text);
            }
        }

        static readonly Func<string, string> replaceCommonTypeName = oldName =>
        {
            return Regex.Replace(oldName, "Operator$", OPERATORDESCRIPTOR)
                .Replace("IExpressionPart", "OperatorDescriptorBase")
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
            else if(propertyType == typeof(Type))
            {
                final = "string";
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
        internal const string BASEPATH = @"C:\.github\BlaiseD\Contoso.XPlatform\Contoso.Common.Configuration";
    }
}
