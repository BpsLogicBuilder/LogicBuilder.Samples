using Contoso.Utils;
using Contoso.Web.Flow.Requests;
using Contoso.Web.Flow.ScreenSettings.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Web.Flow.Requests.Json
{
    public class RequestConverter : JsonTypeConverter<RequestBase>
    {
        public override string TypePropertyName => "ViewType";

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);

            ViewType viewType = (ViewType)jo.GetValue(TypePropertyName, StringComparison.OrdinalIgnoreCase).Value<int>();

            Type GetType()
            {
                switch (viewType)
                {
                    case ViewType.Grid:
                    case ViewType.Detail:
                        return GetDerivedType(string.Format("Contoso.Web.Flow.Requests.{0}Request", Enum.GetName(typeof(ViewType), viewType)));
                    default:
                        return typeof(DefaultRequest);
                }
            }

            Type derivedType = GetType();
            return JsonConvert.DeserializeObject(jo.ToString(), derivedType, SpecifiedSubclassConversion);
        }

        protected override Type GetDerivedType(string typeName)
            => typeof(RequestBase).Assembly.GetType(typeName, true, false);
    }
}
