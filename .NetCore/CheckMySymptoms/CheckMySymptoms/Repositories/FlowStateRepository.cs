using CheckMySymptoms.Flow;
using CheckMySymptoms.Flow.Cache;
using CheckMySymptoms.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace CheckMySymptoms.Repositories
{
    public class FlowStateRepository : IFlowStateRepository
    {
        StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;
        readonly string fileName = "FlowState.txt";

        JsonSerializerSettings Settings
        {
            get
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.Converters.Add(new VariableInfoConverter());
                return settings;
            }
        }

        public async Task<FlowState> GetFlowState()
        {
            try
            {
                StorageFile file = await roamingFolder.GetFileAsync(fileName);
                string json = await FileIO.ReadTextAsync(file);

                return new FlowState(JsonConvert.DeserializeObject<FlowStateForSerilization>(json, Settings));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task SaveFlowState(FlowState flowState) 
            => await SavePersistedFlowState
            (
                await roamingFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting),
                new FlowStateForSerilization(flowState)
            );

        private async Task SavePersistedFlowState(StorageFile file, FlowStateForSerilization flowState) 
            => await FileIO.WriteTextAsync(file, JsonConvert.SerializeObject(flowState));

        public async Task DeleteFlowState()
        {
            try
            {
                StorageFile file = await roamingFolder.GetFileAsync(fileName);
                if (file != null)
                    await file.DeleteAsync();
            }
            catch (Exception)
            {
            }
        }
    }

    class VariableInfoConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(LogicBuilder.RulesDirector.VariableInfo));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load the JSON for the Result into a JObject
            JObject jo = JObject.Load(reader);

            // Read the properties which will be used as constructor parameters
            string variableName = (string)jo["VariableName"];
            string variableType = (string)jo["VariableType"];
            Type type = Type.GetType(variableType);

            object variableValue = null;
            if (type != null && type.IsLiteralType())
                (jo["VariableValue"]).ToString().TryParse(type, out variableValue);
            else
                variableValue = (object)jo["VariableValue"];

            object tag = (object)jo["Tag"];

            //string variableName = GetJObjectValue<string>(jo, "VariableName");
            //string variableType = GetJObjectValue<string>(jo, "VariableType");
            //Type type = Type.GetType(variableType);

            //object variableValue = null;
            //if (type != null && type.IsLiteralType())
            //    GetJObjectValue<object>(jo, "VariableValue").ToString().TryParse(type, out variableValue); 
            //else
            //    variableValue = GetJObjectValue<object>(jo, "VariableValue");

            //object tag = GetJObjectValue<object>(jo, "Tag");

            // Construct the Result object using the non-default constructor
            LogicBuilder.RulesDirector.VariableInfo result = new LogicBuilder.RulesDirector.VariableInfo(variableName, variableType, variableValue, tag);

            // (If anything else needs to be populated on the result object, do that here)

            // Return the result
            return result;
        }

        //private T GetJObjectValue<T>(JObject jo, string propertyName)
        //{
        //    return jo.GetValue(propertyName, StringComparison.OrdinalIgnoreCase).Value<T>();
        //}

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
