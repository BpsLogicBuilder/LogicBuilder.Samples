using LogicBuilder.Expressions.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CreateFormsParametersFromFormsDescriptors
{
    static class WriterForEnums
    {
        #region Constants
        const string VALUES = "#Values#";
        const string NAMESPACES = "#NameSpaces#";
        #endregion Constants

        internal static void Write()
        {
            typeof(Enrollment.Forms.Configuration.CommandButtonDescriptor).Assembly.GetTypes()
                .Where(p => p.IsEnum)
                .ToList()
                .ForEach(t => WriteEnum(t));
        }

        private static void WriteEnum(Type type)
        {
            string subFolder = GetSubfolder();

            HashSet<string> nameSpaces = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead && p.CanWrite)
                .Select(p => p.PropertyType)
                .Aggregate(new HashSet<string>(), (list, next) =>
                {
                    if (next == typeof(object)
                        || next == typeof(string)
                        || (next.IsLiteralType() && !typeof(Enum).IsAssignableFrom(next) && !(next.IsGenericType && next.GetGenericTypeDefinition() == typeof(Nullable<>))))
                        return list;

                    if (!next.Namespace.Equals("Enrollment.Forms.Configuration")
                    && !next.Namespace.Equals($"Enrollment.Forms.Configuration.{subFolder}")
                    && next.Namespace.StartsWith("Enrollment.Forms.Configuration"))
                        list.Add($"using {next.Namespace.Replace("Enrollment.Forms.Configuration", "Enrollment.Forms.Parameters")};");
                    else if (next.Namespace.StartsWith("Enrollment.Common.Configuration.ExpressionDescriptors"))
                        list.Add($"using {next.Namespace.Replace("Enrollment.Common.Configuration.ExpressionDescriptors", "Enrollment.Parameters.Expressions")};");
                    else if (next.Namespace.StartsWith("Enrollment.Common.Configuration.ExpansionDescriptors"))
                        list.Add($"using {next.Namespace.Replace("Enrollment.Common.Configuration.ExpansionDescriptors", "Enrollment.Parameters.Expansions")};");
                    else if (!next.Namespace.StartsWith("Enrollment.Forms.Configuration"))
                        list.Add($"using {next.Namespace};");

                    return list;
                });

            string nameSpacesString = nameSpaces.Any()
                ? $"{string.Join(Environment.NewLine, nameSpaces.OrderBy(n => n))}{Environment.NewLine}{Environment.NewLine}"
                : "";

            string savePath = string.IsNullOrEmpty(subFolder) ? Constants.BASEPATH : $@"{Constants.BASEPATH}\{subFolder}";

            DoWrite
            (
                type.Name,
                type.GetEnumNames().Select(n => string.Format("\t\t{0}", n)),
                Directory.GetCurrentDirectory()
            );

            void DoWrite(string name, IEnumerable<string> values, string path)
            {
                using (StreamWriter sr = new StreamWriter(string.Format(CultureInfo.InvariantCulture, "{0}\\{1}.cs", savePath, name), false, Encoding.UTF8))
                {
                    //string text = text.Replace("#Folder#", string.IsNullOrEmpty(subFolder) ? "" : $@".{subFolder}");
                    //text = text.Replace("#Folder#", string.IsNullOrEmpty(subFolder) ? "" : $@".{subFolder}");
                    sr.Write
                    (
                        File.ReadAllText(string.Format(CultureInfo.InvariantCulture, "{0}\\EnumTemplate.txt", path))
                            .Replace("#Folder#", string.IsNullOrEmpty(subFolder) ? "" : $@".{subFolder}")
                            .Replace("#Name#", name)
                            .Replace(NAMESPACES, nameSpacesString)
                            .Replace(VALUES, string.Join(string.Concat(",", Environment.NewLine), values))
                    );
                }
            }

            string GetSubfolder()
            {
                if (type.Namespace == "Enrollment.Forms.Configuration")
                    return "";

                return type.Namespace.Replace("Enrollment.Forms.Configuration.", "");
            }
        }
    }
}
