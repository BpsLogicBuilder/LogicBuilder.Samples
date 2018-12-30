using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;

namespace Contoso.Domain.Json
{
    abstract public class JsonTypeConverter<T> : JsonConverter
    {
        static JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings() { ContractResolver = new BaseSpecifiedConcreteClassConverter<T>() };

        abstract public string TypePropertyName { get; }
        abstract public Type GetDerivedType(string typeName);
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(T));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            //Dictionary<string, object> caseInsensitiveDictionary = new Dictionary<string, object>(jo.ToObject<IDictionary<string, object>>(), StringComparer.CurrentCultureIgnoreCase);
            //Type type = GetDerivedType((string)caseInsensitiveDictionary[TypePropertyName]);
            Type type = GetDerivedType(jo.GetValue(TypePropertyName, StringComparison.OrdinalIgnoreCase)?.Value<string>());
            //return jo.ToObject(type, serializer);
            return JsonConvert.DeserializeObject(jo.ToString(), type, SpecifiedSubclassConversion);
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
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

    internal static class TypeHelpers
    {
        internal static string ToTypeString(this Type type)
            => type.IsGenericType && !type.IsGenericTypeDefinition
                ? type.AssemblyQualifiedName
                : type.FullName;
    }
}
