using System.Collections.Generic;

namespace Enrollment.Forms.View.Common
{
    public class DataRequestStateView
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public List<SortView> Sort { get; set; }
        public List<GroupView> Group { get; set; }
        public FilterGroupView FilterGroup { get; set; }
        public List<AggregateDefinitionView> Aggregates { get; set; }
    }
}