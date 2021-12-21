using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.SearchForm
{
    public class SearchFilterGroupParameters : SearchFilterParametersBase
    {
		public SearchFilterGroupParameters
		(
			[Comments("List of filters - maximum of 2.  For additional filters use add a filter group instead of a filter.")]
			ICollection<SearchFilterParametersBase> filters
		)
		{
			Filters = filters;
		}

		public ICollection<SearchFilterParametersBase> Filters { get; set; }
    }
}