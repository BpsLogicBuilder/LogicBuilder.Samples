using Contoso.Common.Configuration.ExpressionDescriptors;

namespace Contoso.Forms.Configuration
{
    public class MultiSelectTemplateDescriptor
    {
        public string TemplateName { get; set; }
        public string PlaceholderText { get; set; }
        public string TextField { get; set; }
        public string ValueField { get; set; }
        public string ModelType { get; set; }
        public string LoadingIndicatorText { get; set; }
        public SelectorLambdaOperatorDescriptor TextAndValueSelector { get; set; }
        public RequestDetailsDescriptor RequestDetails { get; set; }
    }
}
