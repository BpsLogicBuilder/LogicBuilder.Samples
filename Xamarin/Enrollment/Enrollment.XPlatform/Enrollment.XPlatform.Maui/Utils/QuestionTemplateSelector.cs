using Enrollment.XPlatform.ViewModels.Validatables;
using Microsoft.Maui.Controls;
using System;

namespace Enrollment.XPlatform.Utils
{
    public class QuestionTemplateSelector : DataTemplateSelector
    {
        private DataTemplate? _checkboxTemplate;
        private DataTemplate? _dateTemplate;
        private DataTemplate? _formGroupArrayTemplate;
        private DataTemplate? _hiddenTemplate;
        private DataTemplate? _labelTemplate;
        private DataTemplate? _multiSelectTemplate;
        private DataTemplate? _passwordTemplate;
        private DataTemplate? _popupFormGroupTemplate;
        private DataTemplate? _pickerTemplate;
        private DataTemplate? _switchTemplate;
        private DataTemplate? _textTemplate;

        public DataTemplate CheckboxTemplate
        {
            get => _checkboxTemplate ?? throw new ArgumentException($"{nameof(_checkboxTemplate)}: {{FBEF04F4-DFAF-467F-A983-AA86D58B09DD}}"); 
            set => _checkboxTemplate = value;
        }

        public DataTemplate DateTemplate
        {
            get => _dateTemplate ?? throw new ArgumentException($"{nameof(_dateTemplate)}: {{76456A93-5C6F-487B-B1B9-CE1405011982}}"); 
            set => _dateTemplate = value;
        }
        public DataTemplate FormGroupArrayTemplate
        {
            get => _formGroupArrayTemplate ?? throw new ArgumentException($"{nameof(_formGroupArrayTemplate)}: {{C9089D9D-D484-444B-BB4F-78F7BB78A1AD}}"); 
            set => _formGroupArrayTemplate = value;
        }
        public DataTemplate HiddenTemplate
        {
            get => _hiddenTemplate ?? throw new ArgumentException($"{nameof(_hiddenTemplate)}: {{649E0F03-E1B6-490D-AF8F-3EF719D21D8C}}"); 
            set => _hiddenTemplate = value;
        }
        public DataTemplate LabelTemplate
        {
            get => _labelTemplate ?? throw new ArgumentException($"{nameof(_labelTemplate)}: D8590E1F-D029-405F-8E6C-EA98803004B8"); 
            set => _labelTemplate = value;
        }
        public DataTemplate MultiSelectTemplate
        {
            get => _multiSelectTemplate ?? throw new ArgumentException($"{nameof(_multiSelectTemplate)}: {{8DEA1FD1-FD54-4124-8B2A-A995C8C1CA8E}}");
            set => _multiSelectTemplate = value;
        }
        public DataTemplate PasswordTemplate
        {
            get => _passwordTemplate ?? throw new ArgumentException($"{nameof(_passwordTemplate)}: {{E8FCAA46-945F-49E4-B366-FDB6A8CAF193}}");
            set => _passwordTemplate = value;
        }
        public DataTemplate PopupFormGroupTemplate
        {
            get => _popupFormGroupTemplate ?? throw new ArgumentException($"{nameof(_popupFormGroupTemplate)}: {{6D88BE38-715D-4175-B979-094B1DFC53AE}}"); 
            set => _popupFormGroupTemplate = value;
        }
        public DataTemplate PickerTemplate
        {
            get => _pickerTemplate ?? throw new ArgumentException($"{nameof(_pickerTemplate)}: {{B4DAB9E0-4B39-4CBE-890B-7247FBA8C369}}"); 
            set => _pickerTemplate = value;
        }
        public DataTemplate SwitchTemplate
        {
            get => _switchTemplate ?? throw new ArgumentException($"{nameof(_switchTemplate)}: {{CF9079F1-84F9-4B3C-A930-2550A3D74DC7}}"); 
            set => _switchTemplate = value;
        }
        public DataTemplate TextTemplate
        {
            get => _textTemplate ?? throw new ArgumentException($"{nameof(_textTemplate)}: {{86F4DEE0-4C21-41D9-AED4-73F326E6211F}}"); 
            set => _textTemplate = value;
        }

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
