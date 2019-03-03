using System.Collections.Generic;

namespace CheckMySymptoms.Forms.View.Common
{
    public class FilterTemplateView
    {
		public string TemplateName { get; set; }
		public bool IsPrimitive { get; set; }
		public string TextField { get; set; }
		public string ValueField { get; set; }
		public DataRequestStateView State { get; set; }
		public RequestDetailsView RequestDetails { get; set; }
    }
}