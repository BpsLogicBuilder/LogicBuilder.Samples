using Enrollment.Common.Configuration.ExpansionDescriptors;
using Enrollment.Common.Configuration.ItemFilter;
using Enrollment.Forms.Configuration.Bindings;
using System.Collections.Generic;

namespace Enrollment.Forms.Configuration.SearchForm
{
    public class SearchFormSettingsDescriptor
    {
        public string Title { get; set; }
        public string ModelType { get; set; }
        public string LoadingIndicatorText { get; set; }
        public string ItemTemplateName { get; set; }
        public string FilterPlaceholder { get; set; }
        public string CreatePagingSelectorFlowName { get; set; }
        public Dictionary<string, ItemBindingDescriptor> Bindings { get; set; }
        public SortCollectionDescriptor SortCollection { get; set; }
        public SearchFilterGroupDescriptor SearchFilterGroup { get; set; }
        public ItemFilterGroupDescriptor ItemFilterGroup { get; set; }
        public RequestDetailsDescriptor RequestDetails { get; set; }
    }
}
