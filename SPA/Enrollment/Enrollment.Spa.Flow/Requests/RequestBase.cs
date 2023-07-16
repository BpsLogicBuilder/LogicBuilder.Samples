using Enrollment.Spa.Flow.Requests.Json;
using Enrollment.Spa.Flow.ScreenSettings;
using Enrollment.Spa.Flow.ScreenSettings.Views;
using System.Text.Json.Serialization;

namespace Enrollment.Spa.Flow.Requests
{
    [JsonConverter(typeof(RequestConverter))]
    abstract public class RequestBase
    {
        public int UserId { get; set; }
        public CommandButtonRequest? CommandButtonRequest { get; set; }
        public FlowState? FlowState { get; set; }
        abstract public ViewType ViewType { get; set; }
    }
}
