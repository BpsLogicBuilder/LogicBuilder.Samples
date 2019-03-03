using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace CreateCommonView
{
    static class CreateViewClassesFromParametersClasses
    {
        internal static void Write()
        {
            HashSet<Type> excludes = new HashSet<Type>() { typeof(CheckMySymptoms.Forms.Parameters.Input.BaseDataParameters) };
            using (CSharpCodeProvider compiler = new CSharpCodeProvider())
            {
                typeof(CheckMySymptoms.Forms.Parameters.Input.BaseDataParameters).Assembly.GetTypes().Where
                                (
                                    p => p.Namespace == "CheckMySymptoms.Forms.Parameters.Common"
                                    && !p.GetTypeInfo().IsEnum
                                    && !excludes.Contains(p)
                                    && !p.GetTypeInfo().IsGenericTypeDefinition
                                    && !p.GetTypeInfo().IsInterface
                                    && p.FullName.EndsWith("Parameters")
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
        const string COMMON_PARAMETER_NAMESPACE_DOT = "CheckMySymptoms.Forms.Parameters.Common.";
        static readonly string COMMON_SAVE_PATH = $@"{Constants.BASEPATH}\CheckMySymptoms.Forms.View\Common\";
        const string VIEW_CLASS_SUFFIX = "View";

        private static void WriteCommonClass(Type type, CSharpCodeProvider compiler)
        {
            string name = type.Name.Replace("Parameters", "");
            List<string> propertiesList = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .Select(p =>
                {
                    if (p.GetMethod.IsAbstract)
                        return string.Format("\t\tabstract public {0} {1} {{ get; set; }}", p.PropertyType.GetNewPropertyClassName(compiler, replaceCommonTypeName), p.Name);
                    else if (p.GetMethod.GetBaseDefinition() != p.GetMethod)
                        return string.Format("\t\tpublic override {0} {1} {{ get; set; }}", p.PropertyType.GetNewPropertyClassName(compiler, replaceCommonTypeName), p.Name);
                    else
                        return string.Format("\t\tpublic {0} {1} {{ get; set; }}", p.PropertyType.GetNewPropertyClassName(compiler, replaceCommonTypeName), p.Name);
                })
                .ToList();

            string baseClassString = type.BaseType != typeof(object) ? string.Format(CultureInfo.InvariantCulture, " : {0}", type.BaseType.Name) : "";

            string text = File.ReadAllText(string.Format(CultureInfo.InvariantCulture, "{0}\\CommonTemplate.txt", Directory.GetCurrentDirectory()))
                .Replace("#Name#", name).Replace(PROPERTIES, string.Join(Environment.NewLine, propertiesList))
                .Replace("#Base#", baseClassString.Replace("Parameters", "View"));

            text = text.Replace("#Modifier#", type.IsAbstract ? "abstract " : "");

            using (StreamWriter sr = new StreamWriter(string.Format(CultureInfo.InvariantCulture, "{0}\\{1}View.cs", COMMON_SAVE_PATH, name), false, Encoding.UTF8))
            {
                sr.Write(text);
            }
        }

        static readonly Func<string, string> replaceCommonTypeName = oldName =>
        {
            return Regex.Replace(oldName, "Parameters$", VIEW_CLASS_SUFFIX)
                .Replace(COMMON_PARAMETER_NAMESPACE_DOT, "").Replace("System.", "");
        };

        private static string GetNewPropertyClassName(this Type propertyType, CSharpCodeProvider compiler, Func<string, string> replace)
        {
            string final = "";
            if (propertyType.GetTypeInfo().IsGenericType && propertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {//Nullable value type
                final = replace(string.Concat
                                (
                                    compiler.GetTypeOutput(new CodeTypeReference(Nullable.GetUnderlyingType(propertyType))), "?"
                                ));
            }
            else if (propertyType.GetTypeInfo().IsGenericType && typeof(System.Collections.IEnumerable).IsAssignableFrom(propertyType))
            {//other Generic Types that are enumerable i.e. lists, dictionaries, etc
                string init = propertyType.Name.Substring(0, propertyType.Name.IndexOf("`"));
                int count = 0;
                Type[] genericArguments = propertyType.GetGenericArguments();
                System.Text.StringBuilder declaration = genericArguments.Aggregate(new System.Text.StringBuilder(init + "<"), (sb, next) =>
                {
                    count++;
                    if (!next.GetTypeInfo().IsValueType && next != typeof(object) && next != typeof(string) && !typeof(System.Collections.IEnumerable).IsAssignableFrom(next))
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
            else if (!propertyType.GetTypeInfo().IsValueType && propertyType != typeof(object) && propertyType != typeof(string) && !typeof(System.Collections.IEnumerable).IsAssignableFrom(propertyType))
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
}
