using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    public class MultiSelectTemplateViewModel
    {
		public string TemplateName { get; set; }
		public string PlaceHolderText { get; set; }
		public string TextField { get; set; }
		public string ValueField { get; set; }
		public DataRequestStateViewModel State { get; set; }
		public RequestDetailsViewModel RequestDetails { get; set; }
		public string ModelType { get; set; }
    }
}