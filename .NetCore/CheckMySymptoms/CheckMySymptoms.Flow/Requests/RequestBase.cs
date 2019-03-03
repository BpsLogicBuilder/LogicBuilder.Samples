using CheckMySymptoms.Flow.Requests.Json;
using CheckMySymptoms.Flow.ScreenSettings.Views;
using CheckMySymptoms.Forms.View;
using Newtonsoft.Json;

namespace CheckMySymptoms.Flow.Requests
{
    [JsonConverter(typeof(RequestConverter))]
    abstract public class RequestBase
    {
        public CommandButtonRequest CommandButtonRequest { get; set; }
        abstract public ViewType ViewType { get; set; }
        abstract public ViewBase View { get; }
    }
}
