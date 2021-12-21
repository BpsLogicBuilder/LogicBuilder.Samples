using Contoso.Forms.Configuration;
using Xamarin.Forms;

namespace Contoso.XPlatform.Utils
{
    public class CommandButtonSelector : DataTemplateSelector
    {
        public DataTemplate SubmitButtonTemplate { get; set; }
        public DataTemplate NavigateButtonTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object buttonDescriptor, BindableObject container)
           => ((CommandButtonDescriptor)buttonDescriptor).Command == "SubmitCommand"
            ? SubmitButtonTemplate
            : NavigateButtonTemplate;
    }
}
