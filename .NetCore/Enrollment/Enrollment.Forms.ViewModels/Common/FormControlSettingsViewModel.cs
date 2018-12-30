using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    public class FormControlSettingsViewModel : FormItemSettingViewModel
    {
		public override AbstractControlEnum AbstractControlType { get; set; }
		public string Field { get; set; }
		public string DomElementId { get; set; }
		public string Title { get; set; }
		public string Placeholder { get; set; }
		public string Type { get; set; }
		public FormValidationSettingViewModel ValidationSetting { get; set; }
        public FormValidationSettingViewModel UnchangedValidationSetting => ValidationSetting;
        public TextFieldTemplateViewModel TextTemplate { get; set; }
		public DropDownTemplateViewModel DropDownTemplate { get; set; }
    }
}