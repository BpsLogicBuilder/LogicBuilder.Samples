using CheckMySymptoms.Flow.ScreenSettings.Views;
using CheckMySymptoms.Forms.View;

namespace CheckMySymptoms.Flow.Requests
{
    public class DefaultRequest : RequestBase
    {
        public override ViewType ViewType { get; set; }

        public override ViewBase View => throw new System.NotImplementedException();
    }
}
