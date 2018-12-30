using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    public class GroupViewModel
    {
		public string Field { get; set; }
		public string Dir { get; set; }
		public List<AggregateDefinitionViewModel> Aggregates { get; set; }
    }
}