using Contoso.Web.Flow.Requests.Json;
using Contoso.Web.Flow.ScreenSettings;
using Contoso.Web.Flow.ScreenSettings.Views;
using Newtonsoft.Json;

namespace Contoso.Web.Flow.Requests
{
    [JsonConverter(typeof(RequestConverter))]
    abstract public class RequestBase
    {
        public CommandButtonRequest CommandButtonRequest { get; set; }
        public FlowState FlowState { get; set; }
        abstract public ViewType ViewType { get; set; }
    }
}
