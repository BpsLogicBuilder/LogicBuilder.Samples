using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    public class FilterTemplateViewModel
    {
		public string TemplateName { get; set; }
		public bool IsPrimitive { get; set; }
		public string TextField { get; set; }
		public string ValueField { get; set; }
		public DataRequestStateViewModel State { get; set; }
		public RequestDetailsViewModel RequestDetails { get; set; }
    }
}