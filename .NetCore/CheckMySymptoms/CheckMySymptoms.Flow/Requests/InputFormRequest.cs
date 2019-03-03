using CheckMySymptoms.Flow.ScreenSettings.Views;
using CheckMySymptoms.Forms.View;
using CheckMySymptoms.Forms.View.Input;

namespace CheckMySymptoms.Flow.Requests
{
    public class InputFormRequest : RequestBase
    {
        public InputFormView Form { get; set; }
        public override ViewType ViewType { get; set; }
        public override ViewBase View => Form;
    }
}
