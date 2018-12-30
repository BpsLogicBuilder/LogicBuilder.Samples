using System.Collections.Generic;

namespace Enrollment.Forms.View.Common
{
    public class FilterGroupView
    {
		public string Logic { get; set; }
		public List<FilterDefinitionView> Filters { get; set; }
		public List<FilterGroupView> FilterGroups { get; set; }
    }
}