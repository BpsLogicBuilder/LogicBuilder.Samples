using Contoso.Forms.View.Input;
using Contoso.Web.Flow.ScreenSettings.Views;

namespace Contoso.Web.Flow.Requests
{
    public class InputFormRequest : RequestBase
    {
        public InputFormView Form { get; set; }
        public override ViewType ViewType { get; set; }
    }
}
