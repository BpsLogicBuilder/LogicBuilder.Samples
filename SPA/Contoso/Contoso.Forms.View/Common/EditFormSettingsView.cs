using System.Collections.Generic;

namespace Contoso.Forms.View.Common
{
    public class EditFormSettingsView
    {
		public string Title { get; set; }
		public string DisplayField { get; set; }
		public FormRequestDetailsView RequestDetails { get; set; }
		public Dictionary<string, Dictionary<string, string>> ValidationMessages { get; set; }
		public List<FormItemSettingView> FieldSettings { get; set; }
		public FilterGroupView FilterGroup { get; set; }
		public Dictionary<string, List<DirectiveView>> ConditionalDirectives { get; set; }
		public string ModelType { get; set; }
    }
}