using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    public class EditFormSettingsViewModel
    {
		public string Title { get; set; }
		public string DisplayField { get; set; }
		public RequestDetailsViewModel RequestDetails { get; set; }
		public Dictionary<string, Dictionary<string, string>> ValidationMessages { get; set; }
		public List<FormItemSettingViewModel> FieldSettings { get; set; }
		public FilterGroupViewModel FilterGroup { get; set; }
		public Dictionary<string, List<DirectiveViewModel>> ConditionalDirectives { get; set; }
    }
}