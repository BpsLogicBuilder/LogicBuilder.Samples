using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CreateAutoMapperProfilesForInputQuestions
{
    static class WriterForAutoMapperCommonParamatersProfiles
    {
        internal static void Write() => WriteAutoMapperValueProfiles();

        private static void WriteAutoMapperValueProfiles()
        {
            List<string> commonParameterToViewModelMaps = typeof(Enrollment.Forms.Parameters.Input.BaseDataParameters).Assembly.GetTypes().Where(p => p.Namespace == "Enrollment.Forms.Parameters.Common"
                               && !p.GetTypeInfo().IsEnum
                               && !p.GetTypeInfo().IsAbstract
                               && !p.GetTypeInfo().IsGenericTypeDefinition
                               && !p.GetTypeInfo().IsInterface
                               && Attribute.GetCustomAttribute(p, typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute)) == null)
                                .ToList()
                                .Aggregate(new List<string>(), (list, next) =>
                                {
                                    list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\tCreateMap<{0}, {1}>().ReverseMap();", next.Name, next.Name.Replace("Parameters", "ViewModel")));
                                    return list;
                                });

            string text = File.ReadAllText(string.Format(CultureInfo.InvariantCulture, "{0}\\CommonParametersMappingProfileFileTemplate.txt", Directory.GetCurrentDirectory()))
                .Replace("#CommonParameterToViewModelIncludes#", string.Join(Environment.NewLine, commonParameterToViewModelMaps));

            using (StreamWriter sr = new StreamWriter(@"C:\.NetCore\Samples\Enrollment\Enrollment.AutoMapperProfiles\CommonParametersMappingProfile.cs", false, Encoding.UTF8))
            {
                sr.Write(text);
                sr.Close();
            }
        }
    }
}
