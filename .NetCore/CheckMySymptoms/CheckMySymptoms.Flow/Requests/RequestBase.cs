using CheckMySymptoms.Flow.ScreenSettings.Views;
using CheckMySymptoms.Forms.View;

namespace CheckMySymptoms.Flow.Requests
{
    //[JsonConverter(typeof(RequestConverter))]
    abstract public class RequestBase
    {
        public CommandButtonRequest CommandButtonRequest { get; set; }
        abstract public ViewType ViewType { get; set; }
        abstract public ViewBase View { get; }
    }
}
