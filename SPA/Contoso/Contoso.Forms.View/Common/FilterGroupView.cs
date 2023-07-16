using System.Collections.Generic;

namespace Contoso.Forms.View.Common
{
    public class FilterGroupView
    {
		public string Logic { get; set; }
		public List<FilterDefinitionView> Filters { get; set; }
		public List<FilterGroupView> FilterGroups { get; set; }
    }
}