using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    public class FilterGroupViewModel
    {
		public string Logic { get; set; }
		public List<FilterDefinitionViewModel> Filters { get; set; }
		public List<FilterGroupViewModel> FilterGroups { get; set; }
    }
}