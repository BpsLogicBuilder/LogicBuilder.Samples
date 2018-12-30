using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    public class FormGroupArraySettingsViewModel : FormItemSettingViewModel
    {
		public override AbstractControlEnum AbstractControlType { get; set; }
		public string Field { get; set; }
		public FormGroupTemplateViewModel FormGroupTemplate { get; set; }
		public List<FormItemSettingViewModel> FieldSettings { get; set; }
		public Dictionary<string, Dictionary<string, string>> ValidationMessages { get; set; }
		public List<string> KeyFields { get; set; }
		public Dictionary<string, List<DirectiveViewModel>> ConditionalDirectives { get; set; }
		public string Title { get; set; }
		public bool ShowTitle { get; set; }
		public string ArrayElementType { get; set; }
    }
}