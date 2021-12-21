using LogicBuilder.Expressions.Utils.ExpressionBuilder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace CreateExpressionParametersFromOperators
{
    static class CreateDescriptorToOperatorMappingProfile
    {
        internal static void Write()
        {
            List<Type> types = typeof(LogicBuilder.Expressions.Utils.ExpressionBuilder.ParameterOperator).Assembly.GetTypes()
                .Where
                (
                    p => p.Namespace != null &&
                    p.Namespace.StartsWith("LogicBuilder.Expressions.Utils.ExpressionBuilder")
                    && !p.IsEnum
                    && !p.IsGenericTypeDefinition
                    && !p.IsInterface
                    && p.FullName.EndsWith("Operator")
                    && Attribute.GetCustomAttribute(p, typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute)) == null
                )
                .OrderBy(type => type.Name)
                .ToList();

            WriteProfile(types);
        }

        static readonly string MAPPING_SAVE_PATH = @"C:\.github\BlaiseD\Contoso.XPlatform\Contoso.AutoMapperProfiles";

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
                    if (parameters.Length > 0 && parameters[0].Name == "parameters" && parameters[0].ParameterType == typeof(IDictionary<string, ParameterExpression>))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append($"\t\t\tCreateMap<{type.Name.Replace("Operator", "OperatorDescriptor")}, {type.Name}>()");
                        sb.Append($"{Environment.NewLine}\t\t\t\t.ConstructUsing");
                        sb.Append($"{Environment.NewLine}\t\t\t\t(");
                        sb.Append($"{Environment.NewLine}\t\t\t\t\t(src, context) => new {type.Name}");
                        sb.Append($"{Environment.NewLine}\t\t\t\t\t(");
                        sb.Append($"{Environment.NewLine}\t\t\t\t\t\t(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY]");
                        sb.Append(GetRemainingParameters(parameters.Skip(1)));
                        sb.Append($"{Environment.NewLine}\t\t\t\t\t)");
                        sb.Append($"{Environment.NewLine}\t\t\t\t)");
                        sb.Append($"{Environment.NewLine}\t\t\t\t.ForAllMembers(opt => opt.Ignore());");
                        sb.Append($"{Environment.NewLine}");

                        return sb.ToString();
                    }

                    
                    if (!hasTypeParameters)
                    {
                        return $"\t\t\tCreateMap<{type.Name.Replace("Operator", "OperatorDescriptor")}, {type.Name}>();";
                    }
                    else
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.Append($"\t\t\tCreateMap<{type.Name.Replace("Operator", "OperatorDescriptor")}, {type.Name}>()");
                        foreach (var parameter in parameters)
                        {
                            if (parameter.ParameterType == typeof(Type))
                            {
                                builder.Append($"{Environment.NewLine}\t\t\t\t.ForMember(dest => dest.{FirstCharToUpper(parameter.Name)}, opts => opts.Ignore())");
                                builder.Append($"{Environment.NewLine}\t\t\t\t.ForCtorParam(\"{parameter.Name}\", opts => opts.MapFrom(x => Type.GetType(x.{FirstCharToUpper(parameter.Name)})))");
                            }
                        }
                        builder.Append(";");
                        return builder.ToString();
                    }
                })
            .ToList();

            string GetRemainingParameters(IEnumerable<ParameterInfo> parameters)
            {
                if (!parameters.Any())
                    return string.Empty;

                StringBuilder sb = new StringBuilder();
                sb.Append($",{Environment.NewLine}\t\t\t\t\t\t");

                sb.Append
                (
                    string.Join
                    (
                        $",{Environment.NewLine}\t\t\t\t\t\t",
                        parameters.Select(p => GetParameterString(p))
                    )
                );

                return sb.ToString();
            }

            string GetParameterString(ParameterInfo parameter)
            {
                if (parameter.ParameterType == typeof(IExpressionPart))
                    return $"context.Mapper.Map<IExpressionPart>(src.{FirstCharToUpper(parameter.Name)})";
                else if (parameter.ParameterType == typeof(Type))
                    return $"Type.GetType(src.{FirstCharToUpper(parameter.Name)})";
                else
                    return $"src.{FirstCharToUpper(parameter.Name)}";
            }

            string FirstCharToUpper(string parameterName)
            {
                return $"{parameterName[0].ToString().ToUpperInvariant()}{parameterName.Substring(1)}";
            }

            List<string> includeMapStatements = types.Select
            (
                type => $"\t\t\t\t.Include<{type.Name.Replace("Operator", "OperatorDescriptor")}, {type.Name}>()"
            )
            .ToList();

            string text = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\DescriptorToOperatorMappingProfileTemplate.txt")
                .Replace("#Mappings#", string.Join(Environment.NewLine, createMapStatements))
                .Replace("#DescriptorToPartIncludes#", $"{string.Join(Environment.NewLine, includeMapStatements)};");

            using (StreamWriter sr = new StreamWriter($@"{MAPPING_SAVE_PATH}\DescriptorToOperatorMappingProfile.cs", false, Encoding.UTF8))
            {
                sr.Write(text);
                sr.Close();
            }

        }
    }
}
