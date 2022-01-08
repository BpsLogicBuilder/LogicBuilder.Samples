using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CreateAutoMapperProfilesForViewAndParameters
{
    static class WriterForAutoMapperCommonParamatersProfiles
    {
        internal static void Write() => WriteAutoMapperValueProfiles();

        private static void WriteAutoMapperValueProfiles()
        {
            List<string> commonParameterToViewMaps = typeof(Enrollment.Forms.Parameters.Common.AbstractControlEnum).Assembly.GetTypes().Where(p => p.Namespace == "Enrollment.Forms.Parameters.Common"
                               && !p.GetTypeInfo().IsEnum
                               && !p.GetTypeInfo().IsAbstract
                               && !p.GetTypeInfo().IsGenericTypeDefinition
                               && !p.GetTypeInfo().IsInterface
                               && Attribute.GetCustomAttribute(p, typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute)) == null)
                                .ToList()
                                .Aggregate(new List<string>(), (list, next) =>
                                {
                                    list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\tCreateMap<{0}, {1}>().ReverseMap();", next.Name, next.Name.Replace("Parameters", "View")));
                                    return list;
                                });
            
            string text = File.ReadAllText(string.Format(CultureInfo.InvariantCulture, "{0}\\CommonParametersMappingProfileFileTemplate.txt", Directory.GetCurrentDirectory()))
                .Replace("#CommonParameterToViewIncludes#", string.Join(Environment.NewLine, commonParameterToViewMaps));

            using (StreamWriter sr = new StreamWriter(@"C:\.github\BlaiseD\LogicBuilder.Samples\.NetCore\Enrollment\Enrollment.AutoMapperProfiles\CommonParametersMappingProfile.cs", false, Encoding.UTF8))
            {
                sr.Write(text);
                sr.Close();
            }
        }
    }
}
