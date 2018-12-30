using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    public class DetailFormSettingsViewModel
    {
		public string Title { get; set; }
		public string DisplayField { get; set; }
		public RequestDetailsViewModel RequestDetails { get; set; }
		public List<DetailItemViewModel> FieldSettings { get; set; }
		public FilterGroupViewModel FilterGroup { get; set; }
    }
}