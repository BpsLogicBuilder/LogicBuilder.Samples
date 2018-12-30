using System.Collections.Generic;

namespace Enrollment.Forms.View.Common
{
    public class GroupView
    {
		public string Field { get; set; }
		public string Dir { get; set; }
		public List<AggregateDefinitionView> Aggregates { get; set; }
    }
}