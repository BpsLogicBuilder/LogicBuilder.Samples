using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Utils
{
    abstract public class JsonTypeConverter<T> : JsonConverter
    {
        #region Properties
        static protected JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings() { ContractResolver = new BaseSpecifiedConcreteClassConverter<T>() };
        abstract public string TypePropertyName { get; }
        #endregion Properties

        #region Methods
        abstract protected Type GetDerivedType(string typeName);

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(T));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            //Type derivedType = GetDerivedType(jo[TypePropertyName].Value<string>());
            //return jo.ToObject(derivedType, serializer);

            Type derivedType = GetDerivedType(jo.GetValue(TypePropertyName, StringComparison.OrdinalIgnoreCase)?.Value<string>());
            return JsonConvert.DeserializeObject(jo.ToString(), derivedType, SpecifiedSubclassConversion);
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
        #endregion Methods
    }

    public class BaseSpecifiedConcreteClassConverter<T> : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (typeof(T).IsAssignableFrom(objectType) && !objectType.IsAbstract)
                return null; // pretend TableSortRuleConvert is not specified (thus avoiding a stack overflow)
            return base.ResolveContractConverter(objectType);
        }
    }
}
