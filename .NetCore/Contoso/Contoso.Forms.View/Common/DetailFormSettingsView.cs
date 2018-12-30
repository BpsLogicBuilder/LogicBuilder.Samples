using System.Collections.Generic;

namespace Contoso.Forms.View.Common
{
    public class DetailFormSettingsView
    {
		public string Title { get; set; }
		public string DisplayField { get; set; }
		public RequestDetailsView RequestDetails { get; set; }
		public List<DetailItemView> FieldSettings { get; set; }
		public FilterGroupView FilterGroup { get; set; }
    }
}