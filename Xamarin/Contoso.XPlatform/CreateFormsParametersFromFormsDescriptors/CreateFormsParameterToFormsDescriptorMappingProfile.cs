using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CreateFormsParametersFromFormsDescriptors
{
    static class CreateFormsParameterToFormsDescriptorMappingProfile
    {
        internal static void Write()
        {
            List<Type> types = typeof(Contoso.Forms.Parameters.CommandButtonParameters).Assembly.GetTypes()
                .Where
                (
                    type => type.Namespace != null &&
                    type.Namespace.StartsWith("Contoso.Forms.Parameters")
                    && !type.IsEnum
                    && !type.IsGenericTypeDefinition
                    && !type.IsInterface
                    && !new HashSet<string>
                    {
                        "CommandButtonParameters",
                        "FormItemSettingsParameters",
                        "DetailItemSettingsParameters",
                        "ItemBindingParameters"
                    }.Contains(type.Name)
                    && type.FullName.EndsWith("Parameters")
                    && Attribute.GetCustomAttribute(type, typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute)) == null
                )
                .OrderBy(type => type.Name)
                .ToList();

            WriteProfile(types);
        }

        static readonly string MAPPING_SAVE_PATH = @"C:\.github\BlaiseD\Contoso.XPlatform\Contoso.XPlatform.AutoMapperProfiles";

        private static void WriteProfile(List<Type> types)
        {
            List<string> createMapStatements = types.Select
            (
                type =>
                {
                    var constructorInfo = type.GetConstructors()
                        .OrderByDescending(c => c.GetParameters().Length)
                        .First();

                    var parameters = constructorInfo.GetParameters();
                    bool hasTypeParameters = parameters.Any(p => p.ParameterType == typeof(Type));

                    StringBuilder sb = new StringBuilder();
                    sb.Append($"\t\t\tCreateMap<{type.Name}, {type.Name.Replace("Parameters", "Descriptor")}>()");
                    if (!hasTypeParameters)
                        sb.Append(";");
                    else
                    {
                        foreach (var parameter in parameters)
                        {
                            if (parameter.ParameterType == typeof(Type))
                                sb.Append($"{Environment.NewLine}\t\t\t\t.ForMember(dest => dest.{FirstCharToUpper(parameter.Name)}, opts => opts.MapFrom(x => x.{FirstCharToUpper(parameter.Name)}.AssemblyQualifiedName))");
                        }

                        sb.Append(";");
                    }

                    return sb.ToString();
                }).ToList();

            List<string> formItemSettingsIncludes = types.Where
            (
                t => t != typeof(Contoso.Forms.Parameters.DataForm.FormItemSettingsParameters)
                    && typeof(Contoso.Forms.Parameters.DataForm.FormItemSettingsParameters).IsAssignableFrom(t)
            )
            .Select
            (
                type => $"\t\t\t\t.Include<{type.Name}, {type.Name.Replace("Parameters", "Descriptor")}>()"
            )
            .ToList();

            List<string> searchFilterParametersBaseIncludes = types.Where
            (
                t => t != typeof(Contoso.Forms.Parameters.SearchForm.SearchFilterParametersBase)
                    && typeof(Contoso.Forms.Parameters.SearchForm.SearchFilterParametersBase).IsAssignableFrom(t)
            )
            .Select
            (
                type => $"\t\t\t\t.Include<{type.Name}, {type.Name.Replace("Parameters", "Descriptor")}>()"
            )
            .ToList();

            List<string> itemFilterParametersBaseIncludes = types.Where
            (
                t => t != typeof(Contoso.Parameters.ItemFilter.ItemFilterParametersBase)
                    && typeof(Contoso.Parameters.ItemFilter.ItemFilterParametersBase).IsAssignableFrom(t)
            )
            .Select
            (
                type => $"\t\t\t\t.Include<{type.Name}, {type.Name.Replace("Parameters", "Descriptor")}>()"
            )
            .ToList();

            List<string> labelItemParametersBaseIncludes = types.Where
            (
                t => t != typeof(Contoso.Forms.Parameters.TextForm.LabelItemParametersBase)
                    && typeof(Contoso.Forms.Parameters.TextForm.LabelItemParametersBase).IsAssignableFrom(t)
            )
            .Select
            (
                type => $"\t\t\t\t.Include<{type.Name}, {type.Name.Replace("Parameters", "Descriptor")}>()"
            )
            .ToList();

            List<string> spanItemParametersBaseIncludes = types.Where
            (
                t => t != typeof(Contoso.Forms.Parameters.TextForm.SpanItemParametersBase)
                    && typeof(Contoso.Forms.Parameters.TextForm.SpanItemParametersBase).IsAssignableFrom(t)
            )
            .Select
            (
                type => $"\t\t\t\t.Include<{type.Name}, {type.Name.Replace("Parameters", "Descriptor")}>()"
            )
            .ToList();

            List<string> itemBindingParametersBaseIncludes = types.Where
            (
                t => t != typeof(Contoso.Forms.Parameters.Bindings.ItemBindingParameters)
                    && typeof(Contoso.Forms.Parameters.Bindings.ItemBindingParameters).IsAssignableFrom(t)
            )
            .Select
            (
                type => $"\t\t\t\t.Include<{type.Name}, {type.Name.Replace("Parameters", "Descriptor")}>()"
            )
            .ToList();

            string text = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\FormsParameterToFormsDescriptorMappingProfileTemplate.txt")
                .Replace("#Mappings#", string.Join(Environment.NewLine, createMapStatements))
                .Replace("#FormItemSettingsIncludes#", $"{string.Join(Environment.NewLine, formItemSettingsIncludes)};")
                .Replace("#SearchFilterParametersBaseIncludes#", $"{string.Join(Environment.NewLine, searchFilterParametersBaseIncludes)};")
                .Replace("#ItemFilterDescriptorBaseIncludes#", $"{string.Join(Environment.NewLine, itemFilterParametersBaseIncludes)};")
                .Replace("#LabelItemParametersBaseIncludes#", $"{string.Join(Environment.NewLine, labelItemParametersBaseIncludes)};")
                .Replace("#SpanItemParametersBaseIncludes#", $"{string.Join(Environment.NewLine, spanItemParametersBaseIncludes)};")
                .Replace("#ItemBindingParametersBaseIncludes#", $"{string.Join(Environment.NewLine, itemBindingParametersBaseIncludes)};");

            using (StreamWriter sr = new StreamWriter($@"{MAPPING_SAVE_PATH}\FormsParameterToFormsDescriptorMappingProfile.cs", false, Encoding.UTF8))
            {
                sr.Write(text);
                sr.Close();
            }

            string FirstCharToUpper(string parameterName)
            {
                return $"{parameterName[0].ToString().ToUpperInvariant()}{parameterName.Substring(1)}";
            }
        }
    }
}
