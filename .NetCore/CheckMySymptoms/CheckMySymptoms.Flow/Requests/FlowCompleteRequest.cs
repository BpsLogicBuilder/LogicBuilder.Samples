using CheckMySymptoms.Flow.ScreenSettings.Views;
using CheckMySymptoms.Forms.View;
using CheckMySymptoms.Forms.View.Common;

namespace CheckMySymptoms.Flow.Requests
{
    public class FlowCompleteRequest : RequestBase
    {
        public FlowCompleteView Form { get; set; }
        public override ViewType ViewType { get; set; }
        public override ViewBase View => Form;
    }
}
