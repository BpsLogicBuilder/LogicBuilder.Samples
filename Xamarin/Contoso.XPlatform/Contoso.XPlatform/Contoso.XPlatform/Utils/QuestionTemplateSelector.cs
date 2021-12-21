using Contoso.XPlatform.ViewModels.Validatables;
using Xamarin.Forms;

namespace Contoso.XPlatform.Utils
{
    public class QuestionTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CheckboxTemplate { get; set; }
        public DataTemplate DateTemplate { get; set; }
        public DataTemplate FormGroupArrayTemplate { get; set; }
        public DataTemplate HiddenTemplate { get; set; }
        public DataTemplate LabelTemplate { get; set; }
        public DataTemplate MultiSelectTemplate { get; set; }
        public DataTemplate PasswordTemplate { get; set; }
        public DataTemplate PopupFormGroupTemplate { get; set; }
        public DataTemplate PickerTemplate { get; set; }
        public DataTemplate SwitchTemplate { get; set; }
        public DataTemplate TextTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            IValidatable input = (IValidatable)item;

            return input.TemplateName switch
            {
                nameof(CheckboxTemplate) => CheckboxTemplate,
                nameof(DateTemplate) => DateTemplate,
                nameof(HiddenTemplate) => HiddenTemplate,
                nameof(FormGroupArrayTemplate) => FormGroupArrayTemplate,
                nameof(LabelTemplate) => LabelTemplate,
                nameof(MultiSelectTemplate) => MultiSelectTemplate,
                nameof(PasswordTemplate) => PasswordTemplate,
                nameof(PopupFormGroupTemplate) => PopupFormGroupTemplate,
                nameof(PickerTemplate) => PickerTemplate,
                nameof(SwitchTemplate) => SwitchTemplate,
                _ => TextTemplate,
            };
        }
    }
}
