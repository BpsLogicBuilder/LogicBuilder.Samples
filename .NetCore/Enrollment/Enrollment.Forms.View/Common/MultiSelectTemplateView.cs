using System.Collections.Generic;

namespace Enrollment.Forms.View.Common
{
    public class MultiSelectTemplateView
    {
		public string TemplateName { get; set; }
		public string PlaceHolderText { get; set; }
		public string TextField { get; set; }
		public string ValueField { get; set; }
		public DataRequestStateView State { get; set; }
		public RequestDetailsView RequestDetails { get; set; }
		public string ModelType { get; set; }
    }
}