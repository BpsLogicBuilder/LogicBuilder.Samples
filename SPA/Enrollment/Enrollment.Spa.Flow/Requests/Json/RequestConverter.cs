using Enrollment.Spa.Flow.ScreenSettings.Views;
using Enrollment.Utils;
using System;
using System.Linq;
using System.Text.Json;

namespace Enrollment.Spa.Flow.Requests.Json
{
    public class RequestConverter : JsonTypeConverter<RequestBase>
    {
        public override string TypePropertyName => nameof(RequestBase.ViewType);

        public override RequestBase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException();

            using var jsonDocument = JsonDocument.ParseValue(ref reader);
            JsonProperty jsonProperty = GetJsonProperty();
            if (jsonProperty.Equals(default(JsonProperty)))
                throw new JsonException();

            ViewType viewType = (ViewType)jsonProperty.Value.GetInt32();

            JsonProperty GetJsonProperty()
                => jsonDocument.RootElement.EnumerateObject().FirstOrDefault(e => e.Name.ToLowerInvariant() == TypePropertyName.ToLowerInvariant());

            Type GetType()
            {
                return viewType switch
                {
                    ViewType.Grid or ViewType.Detail => typeof(RequestBase).Assembly.GetType
                    (
                        $"Enrollment.Spa.Flow.Requests.{Enum.GetName(typeof(ViewType), viewType)}Request",
                        true,
                        false
                    ) ?? throw new ArgumentException(nameof(viewType)),
                    _ => typeof(DefaultRequest),
                };
            }

            return (RequestBase)(JsonSerializer.Deserialize
            (
                jsonDocument.RootElement.GetRawText(),
                GetType(),
                options
            ) ?? throw new ArgumentException(nameof(viewType)));
        }
    }
}
