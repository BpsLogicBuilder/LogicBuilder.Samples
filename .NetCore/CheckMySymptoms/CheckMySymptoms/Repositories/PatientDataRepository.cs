using CheckMySymptoms.Forms.View;
using CheckMySymptoms.Utils;
using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;

namespace CheckMySymptoms.Repositories
{
    public class PatientDataRepository : IPatientDataRepository
    {
        public PatientDataRepository(IVariableMetadataRepository variableMetadataRepository)
        {
            this.variableMetadataRepository = variableMetadataRepository;
        }

        #region Constants
        private const string PATIENTTABLE = "Patient";
        #endregion Constants

        StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;
        readonly string ext = ".xml";
        private readonly IVariableMetadataRepository variableMetadataRepository;

        public async Task<Dictionary<string, VariableInfo>> GetData(Dictionary<string, VariableInfo> variables)
        {
            try
            {
                Dictionary<string, VariableInfo> data = await GetDictionary(variables);

                const string AGE = "Patient.Age";
                const string SEX = "Patient.Sex";

                if (!data.ContainsKey(SEX) || !data.ContainsKey(AGE))
                {
                    data.Add(AGE, new VariableInfo(AGE, typeof(int).FullName, 30));
                    data.Add(SEX, new VariableInfo(SEX, typeof(char).FullName, 'F'));
                    await SaveData(data);
                }

                return data;
            }
            catch
            {
                return variables;
            }
        }

        private async Task<Dictionary<string, VariableInfo>> GetDictionary(Dictionary<string, VariableInfo> variables)
        {
            try
            {
                StorageFile file = await roamingFolder.GetFileAsync(string.Concat(PATIENTTABLE, ext));
                string xml = await FileIO.ReadTextAsync(file);
                XDocument doc = XDocument.Parse(xml);
                return doc.Root.Elements(XmlDataConstants.DATAELEMENT).Select
                (
                    e =>
                    {
                        string name = e.Attribute(XmlDataConstants.NAMEATTRIBUTE).Value;
                        string type = e.Attribute(XmlDataConstants.DATATYPEATTRIBUTE).Value;
                        string value = e.Element(XmlDataConstants.VALUEELEMENT).Value;
                        value.TryParse(Type.GetType(type), out object result);
                        return new VariableInfo
                         (
                             name,
                             type,
                             result
                         );
                    }
                ).Aggregate(variables, (dict, next) =>
                {
                    if (dict.TryGetValue(next.VariableName, out VariableInfo info))
                        info = next;
                    else
                        dict.Add(next.VariableName, next);

                    return dict;
                });
            }
            catch (Exception)
            {
                return variables;
            }
        }

        public async Task SaveData(Dictionary<string, VariableInfo> variables)
        {
            Dictionary<string, VariableMetadata> metaData = this.variableMetadataRepository.GetMetadata().ToDictionary(m => m.VariableName);
            StorageFile file = await roamingFolder.CreateFileAsync(string.Concat(PATIENTTABLE, ext), CreationCollisionOption.ReplaceExisting);

            try
            {
                XDocument xmlDocument = new XDocument
                    (
                        new XElement
                        (
                            XmlDataConstants.ROOTELEMENT,
                            variables.Where(a => metaData.ContainsKey(a.Key) && metaData[a.Key].Table == PATIENTTABLE).Select
                            (
                                item => new XElement
                                (
                                    XmlDataConstants.DATAELEMENT,
                                    new XAttribute(XmlDataConstants.NAMEATTRIBUTE, item.Value.VariableName),
                                    new XAttribute(XmlDataConstants.DATATYPEATTRIBUTE, item.Value.VariableType),
                                    new XElement(XmlDataConstants.VALUEELEMENT, item.Value.VariableValue)
                                )
                            )
                        )
                    );

                await FileIO.WriteTextAsync(file, xmlDocument.ToString());
            }
            catch (Exception)
            {
                if (file != null)
                    await file.DeleteAsync();
            }
        }

        public async Task DeleteData()
        {
            try
            {
                StorageFile file = await roamingFolder.CreateFileAsync(string.Concat(PATIENTTABLE, ext), CreationCollisionOption.ReplaceExisting);
                if (file != null)
                    await file.DeleteAsync();
            }
            catch (Exception)
            {
            }
        }
    }

    internal struct XmlDataConstants
    {
        internal const string METADATAELEMENT = "metadata";
        internal const string DATATYPEATTRIBUTE = "dataType";
        internal const string NAMEATTRIBUTE = "name";
        internal const string ROOTELEMENT = "root";
        internal const string DATAELEMENT = "data";
        internal const string VALUEELEMENT = "value";
    }
}
