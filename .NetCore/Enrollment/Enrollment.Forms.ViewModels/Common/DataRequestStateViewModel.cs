using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    public class DataRequestStateViewModel
    {
		public int? Skip { get; set; }
		public int? Take { get; set; }
		public List<SortViewModel> Sort { get; set; }
		public List<GroupViewModel> Group { get; set; }
		public FilterGroupViewModel FilterGroup { get; set; }
		public List<AggregateDefinitionViewModel> Aggregates { get; set; }
    }
}