using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace CreateAutoMapperProfilesForViewAndParameters
{
    static class WriterForAutoMapperInputValueProfiles
    {
        internal static void Write()
        {
            List<string> mappingsList = ValueTypes.Aggregate(new List<string>(), (list, next) =>
            {
                list.Add(File.ReadAllText(string.Format(CultureInfo.InvariantCulture, "{0}\\InputMapperProfilesTemplate.txt", Directory.GetCurrentDirectory()))
                    .Replace("#TYPE#", next));
                return list;
            });

            mappingsList = ValueTypes.Aggregate(mappingsList, (list, next) =>
            {
                list.Add(File.ReadAllText(string.Format(CultureInfo.InvariantCulture, "{0}\\InputMapperProfilesTemplate.txt", Directory.GetCurrentDirectory()))
                    .Replace("#TYPE#", string.Concat(next, "?")));
                return list;
            });

            mappingsList = OtherTypes.Aggregate(mappingsList, (list, next) =>
            {
                list.Add(File.ReadAllText(string.Format(CultureInfo.InvariantCulture, "{0}\\InputMapperProfilesTemplate.txt", Directory.GetCurrentDirectory()))
                    .Replace("#TYPE#", next));
                return list;
            });

            string mappings = string.Join(string.Concat(Environment.NewLine, Environment.NewLine), mappingsList);

            string text = File.ReadAllText(string.Format(CultureInfo.InvariantCulture, "{0}\\InputVariablesMappingProfileFileTemplate.txt", Directory.GetCurrentDirectory()))
                .Replace("#Mappings#", mappings);

            List<string> includesViewToParametersList = ValueTypes.Aggregate(new List<string>(), (list, next) =>
            {
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputView<{0}>, InputQuestionParameters<{0}>>()", next));
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputView<{0}?>, InputQuestionParameters<{0}?>>()", next));
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputView<IEnumerable<{0}>>, InputQuestionParameters<IEnumerable<{0}>>>()", next));
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputView<IEnumerable<{0}?>>, InputQuestionParameters<IEnumerable<{0}?>>>()", next));
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputView<ICollection<{0}>>, InputQuestionParameters<ICollection<{0}>>>()", next));
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputView<ICollection<{0}?>>, InputQuestionParameters<ICollection<{0}?>>>()", next));
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputView<IList<{0}>>, InputQuestionParameters<IList<{0}>>>()", next));
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputView<IList<{0}?>>, InputQuestionParameters<IList<{0}?>>>()", next));
                return list;
            });

            includesViewToParametersList = OtherTypes.Aggregate(includesViewToParametersList, (list, next) =>
            {
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputView<{0}>, InputQuestionParameters<{0}>>()", next));
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputView<IEnumerable<{0}>>, InputQuestionParameters<IEnumerable<{0}>>>()", next));
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputView<ICollection<{0}>>, InputQuestionParameters<ICollection<{0}>>>()", next));
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputView<IList<{0}>>, InputQuestionParameters<IList<{0}>>>()", next));
                return list;
            });

            List<string> includesParametersToViewList = ValueTypes.Aggregate(new List<string>(), (list, next) =>
            {
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputQuestionParameters<{0}>, InputView<{0}>>()", next));
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputQuestionParameters<{0}?>, InputView<{0}?>>()", next));
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputQuestionParameters<IEnumerable<{0}>>, InputView<IEnumerable<{0}>>>()", next));
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputQuestionParameters<IEnumerable<{0}?>>, InputView<IEnumerable<{0}?>>>()", next));
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputQuestionParameters<ICollection<{0}>>, InputView<ICollection<{0}>>>()", next));
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputQuestionParameters<ICollection<{0}?>>, InputView<ICollection<{0}?>>>()", next));
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputQuestionParameters<IList<{0}>>, InputView<IList<{0}>>>()", next));
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputQuestionParameters<IList<{0}?>>, InputView<IList<{0}?>>>()", next));
                return list;
            });

            includesParametersToViewList = OtherTypes.Aggregate(includesParametersToViewList, (list, next) =>
            {
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputQuestionParameters<{0}>, InputView<{0}>>()", next));
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputQuestionParameters<IEnumerable<{0}>>, InputView<IEnumerable<{0}>>>()", next));
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputQuestionParameters<ICollection<{0}>>, InputView<ICollection<{0}>>>()", next));
                list.Add(string.Format(CultureInfo.CurrentCulture, "\t\t\t\t.Include<InputQuestionParameters<IList<{0}>>, InputView<IList<{0}>>>()", next));
                return list;
            });

            text = text.Replace("#ViewToParameterIncludes#", string.Join(Environment.NewLine, includesViewToParametersList));

            text = text.Replace("#ParameterToViewIncludes#", string.Join(Environment.NewLine, includesParametersToViewList));

            using (StreamWriter sr = new StreamWriter($@"{Constants.BASEPATH}\CheckMySymptoms.AutoMapperProfiles\InputVariablesMappingProfile.cs", false, Encoding.UTF8))
            {
                sr.Write(text);
            }
        }

        //We don't expect standard value types to change
        private static string[] ValueTypes => new string[] {
                "bool",
                "DateTime",
                "TimeSpan",
                "Guid",
                "decimal",
                "byte",
                "short",
                "int",
                "long",
                "float",
                "double",
                "char",
                "sbyte",
                "ushort",
                "uint",
                "ulong"
            };

        //The list of reference types IS likely to change
        //Alternativly ask questions populating members of the reference types.
        private static string[] OtherTypes => new string[] {
                "string"
            };
    }

    internal struct Constants
    {
        internal const string BASEPATH = @"C:\BLoB\Samples\.NetCore\CheckMySymptoms";
    }
}
