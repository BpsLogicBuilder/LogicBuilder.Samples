using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Contoso.Domain
{
    static class WriterForEnums
    {
        #region Constants
        const string VALUES = "#Values#";
        //const string SAVE_PATH = @"C:\.NetCore\Samples\ContosoUniversity\DomainFiles";
        const string SAVE_PATH = @"C:\.github\BlaiseD\Contoso.XPlatform\Contoso.Domain";
        #endregion Constants

        internal static void Write()
        {
            typeof(Data.BaseDataClass).Assembly.GetTypes()
                .Where(p => p.IsEnum)
                .ToList()
                .ForEach(t => WriteEnum(t));
        }

        private static void WriteEnum(Type type)
        {
            void DoWrite(string name, IEnumerable<string> values, string path)
            {
                using (StreamWriter sr = new StreamWriter(string.Format(CultureInfo.InvariantCulture, "{0}\\{1}.cs", SAVE_PATH, name), false, Encoding.UTF8))
                {
                    sr.Write
                    (
                        File.ReadAllText(string.Format(CultureInfo.InvariantCulture, "{0}\\EnumTemplate.txt", path))
                            .Replace("#Name#", name)
                            .Replace(VALUES, string.Join(string.Concat(",", Environment.NewLine), values))
                    );
                }
            }

            DoWrite
            (
                type.Name, 
                type.GetEnumNames().Select(n => string.Format("\t\t{0}", n)), 
                Directory.GetCurrentDirectory()
            );
        }
    }
}
