using System.Collections.Generic;

namespace Enrollment.Forms.Configuration.SearchForm
{
    public class SearchFilterGroupDescriptor : SearchFilterDescriptorBase
    {
        public ICollection<SearchFilterDescriptorBase> Filters { get; set; }
    }
}
