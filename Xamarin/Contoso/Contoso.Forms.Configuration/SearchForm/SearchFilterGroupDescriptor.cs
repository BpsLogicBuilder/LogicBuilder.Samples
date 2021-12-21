using System.Collections.Generic;

namespace Contoso.Forms.Configuration.SearchForm
{
    public class SearchFilterGroupDescriptor : SearchFilterDescriptorBase
    {
        public ICollection<SearchFilterDescriptorBase> Filters { get; set; }
    }
}
