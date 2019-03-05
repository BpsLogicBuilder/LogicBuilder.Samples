using CheckMySymptoms.Flow;
using CheckMySymptoms.Flow.Cache;
using CheckMySymptoms.Forms.View;
using CheckMySymptoms.Forms.View.Common;
using CheckMySymptoms.Forms.View.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace CheckMySymptoms.Repositories
{
    public class FlowStateRepository : IFlowStateRepository
    {
        StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;
        readonly string fileName = "FlowState.txt";

        //JsonSerializerSettings Settings
        //{
        //    get
        //    {
        //        JsonSerializerSettings settings = new JsonSerializerSettings();
        //        settings.Converters.Add(new VariableInfoConverter());
        //        return settings;
        //    }
        //}

        readonly List<Type> KnownTypes = new List<Type>
        {
            typeof(MessageTemplateView),
            typeof(FlowCompleteView),
            typeof(HtmlPageSettingsView),
            typeof(InputFormView),
            typeof(Dictionary<int, object>),
            typeof(InputView<string>),
            typeof(InputView<int>),
            typeof(LookUpsViewModel),
            typeof(KeyValuePair<int, object>[]),
        };

        public async Task<FlowState> GetFlowState()
        {
            try
            {
                StorageFile file = await roamingFolder.GetFileAsync(fileName);
                string json = await FileIO.ReadTextAsync(file);

                //FlowStateForSerilization flowSate = JsonConvert.DeserializeObject<FlowStateForSerilization>(json, Settings);
                FlowStateForSerilization flowSate = JsonHelper.FromJson<FlowStateForSerilization>(json, KnownTypes);


                return new FlowState(flowSate);
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
        {
            string json = "";
            try
            {
                json = JsonHelper.ToJson(flowState, KnownTypes);
                //json = JsonConvert.SerializeObject(flowState);
            }
            catch (Exception)
            {
            }
            await FileIO.WriteTextAsync(file, json);
        }

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

    //class VariableInfoConverter : JsonConverter
    //{
    //    public override bool CanConvert(Type objectType)
    //    {
    //        return (objectType == typeof(LogicBuilder.RulesDirector.VariableInfo));
    //    }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        // Load the JSON for the Result into a JObject
    //        JObject jo = JObject.Load(reader);

    //        // Read the properties which will be used as constructor parameters
    //        string variableName = (string)jo["VariableName"];
    //        string variableType = (string)jo["VariableType"];
    //        Type type = Type.GetType(variableType);

    //        object variableValue = null;
    //        if (type != null && type.IsLiteralType())
    //            (jo["VariableValue"]).ToString().TryParse(type, out variableValue);
    //        else
    //            variableValue = (object)jo["VariableValue"];

    //        object tag = (object)jo["Tag"];

    //        // Construct the Result object using the non-default constructor
    //        LogicBuilder.RulesDirector.VariableInfo result = new LogicBuilder.RulesDirector.VariableInfo(variableName, variableType, variableValue, tag);

    //        // (If anything else needs to be populated on the result object, do that here)

    //        // Return the result
    //        return result;
    //    }

    //    public override bool CanWrite
    //    {
    //        get { return false; }
    //    }

    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public static class JsonHelper
    {
        public static string ToJson<T>(T instance, IEnumerable<Type> knownTypes)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T), knownTypes);
            using (MemoryStream tempStream = new MemoryStream())
            {
                serializer.WriteObject(tempStream, instance);
                return Encoding.UTF8.GetString(tempStream.ToArray(), 0, (int)tempStream.Length);
            }
        }

        public static T FromJson<T>(string json, IEnumerable<Type> knownTypes)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T), knownTypes);
            using (MemoryStream tempStream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                return (T)serializer.ReadObject(tempStream);
            }
        }
    }
}
