using Contoso.Spa.Flow.Requests.Json;
using Contoso.Spa.Flow.ScreenSettings;
using Contoso.Spa.Flow.ScreenSettings.Views;
using System.Text.Json.Serialization;

namespace Contoso.Spa.Flow.Requests
{
    [JsonConverter(typeof(RequestConverter))]
    abstract public class RequestBase
    {
        public CommandButtonRequest? CommandButtonRequest { get; set; }
        public FlowState? FlowState { get; set; }
        abstract public ViewType ViewType { get; set; }
    }
}
