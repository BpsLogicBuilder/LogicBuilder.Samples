using Contoso.XPlatform.ViewModels.ReadOnlys;
using Microsoft.Maui.Controls;
using System;

namespace Contoso.XPlatform.Utils
{
    public class ReadOnlyControlTemplateSelector : DataTemplateSelector
    {
        private DataTemplate? _checkboxTemplate;
        private DataTemplate? _dateTemplate;
        private DataTemplate? _formGroupArrayTemplate;
        private DataTemplate? _hiddenTemplate;
        private DataTemplate? _multiSelectTemplate;
        private DataTemplate? _passwordTemplate;
        private DataTemplate? _popupFormGroupTemplate;
        private DataTemplate? _pickerTemplate;
        private DataTemplate? _switchTemplate;
        private DataTemplate? _textTemplate;

        public DataTemplate CheckboxTemplate
        {
            get => _checkboxTemplate ?? throw new ArgumentException($"{nameof(_textTemplate)}: {{44D85B68-015A-42AC-85FA-21D9E9534E5F}}");
            set => _checkboxTemplate = value;
        }
        public DataTemplate DateTemplate
        {
            get => _dateTemplate ?? throw new ArgumentException($"{nameof(_textTemplate)}: {{2C9F9352-741C-4159-92D3-77F9DE953CC4}}");
            set => _dateTemplate = value;
        }
        public DataTemplate FormGroupArrayTemplate
        {
            get => _formGroupArrayTemplate ?? throw new ArgumentException($"{nameof(_textTemplate)}: {{34859BA3-6F5E-4D20-A05F-E67D0DDE968C}}");
            set => _formGroupArrayTemplate = value;
        }
        public DataTemplate HiddenTemplate
        {
            get => _hiddenTemplate ?? throw new ArgumentException($"{nameof(_textTemplate)}: {{86F4DEE0-4C21-41D9-AED4-73F326E6211F}}");
            set => _hiddenTemplate = value;
        }
        public DataTemplate MultiSelectTemplate
        {
            get => _multiSelectTemplate ?? throw new ArgumentException($"{nameof(_textTemplate)}: {{A4FC56F6-A177-4F90-BD81-D2B48AFFACE8}}");
            set => _multiSelectTemplate = value;
        }
        public DataTemplate PasswordTemplate
        {
            get => _passwordTemplate ?? throw new ArgumentException($"{nameof(_textTemplate)}: {{1A6115A3-5D7D-42A5-BBED-6F79E34E0FEA}}");
            set => _passwordTemplate = value;
        }
        public DataTemplate PopupFormGroupTemplate
        {
            get => _popupFormGroupTemplate ?? throw new ArgumentException($"{nameof(_textTemplate)}: {{D71A88FD-23FA-4E7F-8156-3E3E5CCF3728}}");
            set => _popupFormGroupTemplate = value;
        }
        public DataTemplate PickerTemplate
        {
            get => _pickerTemplate ?? throw new ArgumentException($"{nameof(_textTemplate)}: {{86F4DEE0-4C21-41D9-AED4-73F326E6211F}}");
            set => _pickerTemplate = value;
        }
        public DataTemplate SwitchTemplate
        {
            get => _switchTemplate ?? throw new ArgumentException($"{nameof(_textTemplate)}: {{97D24544-D5EF-4045-9E9E-CDE287A2236E}}");
            set => _switchTemplate = value;
        }
        public DataTemplate TextTemplate
        {
            get => _textTemplate ?? throw new ArgumentException($"{nameof(_textTemplate)}: {{6C59F1DB-3130-4056-A9C9-26F6EA19F6BA}}");
            set => _textTemplate = value;
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            IReadOnly input = (IReadOnly)item;

            return input.TemplateName switch
            {
                nameof(CheckboxTemplate) => CheckboxTemplate,
                nameof(DateTemplate) => DateTemplate,
                nameof(HiddenTemplate) => HiddenTemplate,
                nameof(FormGroupArrayTemplate) => FormGroupArrayTemplate,
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
