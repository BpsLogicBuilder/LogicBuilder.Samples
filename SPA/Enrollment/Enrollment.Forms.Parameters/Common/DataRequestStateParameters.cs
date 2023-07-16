using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Enrollment.Forms.Parameters.Common
{
    public class DataRequestStateParameters
    {
        public DataRequestStateParameters()
        {
        }

        public DataRequestStateParameters
        (
            [Comments("Number of records to skip.")]
            int? skip = null,

            [Comments("Page size.")]
            int? take = null,

            [Comments("Sort descriptor.")]
            List<SortParameters> sort = null,

            [Comments("Group by.")]
            List<GroupParameters> group = null,

            [Comments("Filter descriptors.")]
            FilterGroupParameters filterGroup = null,

            [Comments("Aggregate functions.")]
            List<AggregateDefinitionParameters> aggregates = null
        )
        {
            Skip = skip;
            Take = take;
            Sort = sort;
            Group = group;
            FilterGroup = filterGroup;
            Aggregates = aggregates;
        }

        public int? Skip { get; set; }
        public int? Take { get; set; }
        public List<SortParameters> Sort { get; set; }
        public List<GroupParameters> Group { get; set; }
        public FilterGroupParameters FilterGroup { get; set; }
        public List<AggregateDefinitionParameters> Aggregates { get; set; }
    }
}