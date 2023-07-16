using System.Collections.Generic;

namespace Enrollment.Forms.View.Common
{
    public class FormControlSettingsView : FormItemSettingView
    {
        public override AbstractControlEnum AbstractControlType { get; set; }
        public string Field { get; set; }
        public string DomElementId { get; set; }
        public string Title { get; set; }
        public string Placeholder { get; set; }
        public string Type { get; set; }
        public FormValidationSettingView ValidationSetting { get; set; }
        public FormValidationSettingView UnchangedValidationSetting => ValidationSetting;
        public TextFieldTemplateView TextTemplate { get; set; }
        public DropDownTemplateView DropDownTemplate { get; set; }
        public string ModelType { get; set; }
    }
}