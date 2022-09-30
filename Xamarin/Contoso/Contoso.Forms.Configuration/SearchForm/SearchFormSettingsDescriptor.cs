using Contoso.Forms.Configuration.Bindings;
using System.Collections.Generic;

namespace Contoso.Forms.Configuration.SearchForm
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
        public RequestDetailsDescriptor RequestDetails { get; set; }
    }
}
