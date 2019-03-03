using System.Collections.Generic;

namespace CheckMySymptoms.Forms.View.Common
{
    public class AboutFormSettingsView
    {
		public string Title { get; set; }
		public RequestDetailsView RequestDetails { get; set; }
		public DataRequestStateView State { get; set; }
		public List<DetailItemView> FieldSettings { get; set; }
    }
}