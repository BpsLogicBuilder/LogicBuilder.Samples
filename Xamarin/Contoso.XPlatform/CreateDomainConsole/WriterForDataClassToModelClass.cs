using Contoso.Domain.Attributes;
using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Contoso.Domain
{
    static class WriterForDataClassToModelClass
    {
        #region Constants
        const string PROPERTIES = "#Properties#";
        const string DATA_NAMESPACE_DOT = "Contoso.Data.Entities.";
        const string MODEL_CLASS_SUFFIX = "Model";
        const string SAVE_PATH = @"C:\.github\BlaiseD\Contoso.XPlatform\Contoso.Domain";
        #endregion Constants

        internal static void Write(bool implementINotifyPropertyChanged = true)
        {
            using (CSharpCodeProvider compiler = new CSharpCodeProvider())
            {
                typeof(Data.BaseDataClass).Assembly.GetTypes().Where(p => typeof(Data.BaseDataClass).IsAssignableFrom(p)
                                && !p.GetTypeInfo().IsAbstract
                                && !p.GetTypeInfo().IsGenericTypeDefinition
                                && !p.GetTypeInfo().IsInterface)
                                .ToList()
                                .ForEach(t =>
                                {
                                    WriteClass(t, compiler, implementINotifyPropertyChanged);
                                });
            }
        }

        private static string GetAttributesString(this PropertyInfo pInfo)
        {
            return AttributeRewriter.GetParameterAttributes(pInfo)
                .Aggregate(new StringBuilder(), (sb, next) =>
                {
                    sb.Append("\t\t");
                    sb.Append(next);
                    sb.Append(Environment.NewLine);
                    return sb;
                }).ToString();
        }

        private static string GetBackingFieldName(PropertyInfo propertyInfo) 
            => $"_{propertyInfo.Name[0].ToString().ToLowerInvariant()}{propertyInfo.Name.Substring(1)}";

        private static string GetPropertyString(PropertyInfo propertyInfo, CSharpCodeProvider compiler, bool implementINotifyPropertyChanged)
        {
            if (!implementINotifyPropertyChanged)
                return $"{propertyInfo.GetAttributesString()}\t\tpublic {propertyInfo.PropertyType.GetNewPropertyClassName(compiler)} {propertyInfo.Name} {{ get; set; }}";

            List<string> stringBuilder = new List<string>();
            string backingFieldName = GetBackingFieldName(propertyInfo);
            string typeName = propertyInfo.PropertyType.GetNewPropertyClassName(compiler);
            stringBuilder.Add($"\t\tprivate {typeName} {backingFieldName};");
            stringBuilder.Add($"{propertyInfo.GetAttributesString()}\t\tpublic {typeName} {propertyInfo.Name}");
            stringBuilder.Add("\t\t{");
            stringBuilder.Add($"\t\t\tget {{ return {backingFieldName}; }}");
            stringBuilder.Add("\t\t\tset");
            stringBuilder.Add("\t\t\t{");
            stringBuilder.Add($"\t\t\t\tif ({backingFieldName} == value)");
            stringBuilder.Add($"\t\t\t\t\treturn;");
            stringBuilder.Add("");
            stringBuilder.Add($"\t\t\t\t{backingFieldName} = value;");
            stringBuilder.Add("\t\t\t\tOnPropertyChanged();");
            stringBuilder.Add("\t\t\t}");
            stringBuilder.Add("\t\t}");

            return string.Join(Environment.NewLine, stringBuilder);
        }

        private static string GettemplateFilePath(bool implementINotifyPropertyChanged, string directory) 
            => implementINotifyPropertyChanged
                ? $"{directory}\\INotifyPropertyChangedModelClassTemplate.txt"
                : $"{directory}\\ModelClassTemplate.txt";

        private static void WriteClass(Type type, CSharpCodeProvider compiler, bool implementINotifyPropertyChanged)
        {
            DoWrite
            (
                type.Name,
                type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanWrite && p.CanRead)
                    .Select(p => GetPropertyString(p, compiler, implementINotifyPropertyChanged))
            );

            void DoWrite(string name, IEnumerable<string> propertiesList)
            {
                using (StreamWriter sr = new StreamWriter(string.Format(CultureInfo.InvariantCulture, "{0}\\{1}Model.cs", SAVE_PATH, name), false, Encoding.UTF8))
                {
                    sr.Write
                    (
                        File.ReadAllText(GettemplateFilePath(implementINotifyPropertyChanged, Directory.GetCurrentDirectory()))
                            .Replace("#Name#", name)
                            .Replace(PROPERTIES, string.Join(string.Concat(Environment.NewLine, Environment.NewLine), propertiesList))
                    );
                }
            }
        }

        private static string GetNewPropertyClassName(this Type propertyType, CSharpCodeProvider compiler)
        {
            if (propertyType.GetTypeInfo().IsGenericType && propertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {//Nullable value type
                return string.Concat
                                (
                                    compiler.GetTypeOutput(new CodeTypeReference(Nullable.GetUnderlyingType(propertyType))), "?"
                                ).Replace(DATA_NAMESPACE_DOT, "");
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
                        sb.Append(string.Concat(next.ToString(), MODEL_CLASS_SUFFIX).Replace(DATA_NAMESPACE_DOT, ""));
                    }
                    else
                    {
                        sb.Append(next.GetNewPropertyClassName(compiler));
                    }
                    //System.Text.RegularExpressions.Regex.Replace("", "xxx$", "");//Replace at end of string
                    sb.Append((count < genericArguments.Length) ? ", " : ">");
                    return sb;
                });

                return declaration.ToString().Replace(DATA_NAMESPACE_DOT, "");
            }
            else if (!propertyType.GetTypeInfo().IsValueType && propertyType != typeof(string) && !typeof(System.Collections.IEnumerable).IsAssignableFrom(propertyType))
            {//other reference types
                return string.Concat(propertyType.ToString(), MODEL_CLASS_SUFFIX).Replace(DATA_NAMESPACE_DOT, "");
            }
            else
            {//value types
                return compiler.GetTypeOutput(new CodeTypeReference(propertyType)).Replace(DATA_NAMESPACE_DOT, "");
            }
        }
    }
}
