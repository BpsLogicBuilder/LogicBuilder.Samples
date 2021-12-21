using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Forms.Configuration.Bindings;
using System.Collections.Generic;

namespace Contoso.Forms.Configuration.ListForm
{
    public class ListFormSettingsDescriptor
    {
        public string Title { get; set; }
        public string ModelType { get; set; }
        public string LoadingIndicatorText { get; set; }
        public string ItemTemplateName { get; set; }
        public Dictionary<string, ItemBindingDescriptor> Bindings { get; set; }
        public SelectorLambdaOperatorDescriptor FieldsSelector { get; set; }
        public RequestDetailsDescriptor RequestDetails { get; set; }
    }
}
