using Contoso.Common.Configuration.ExpressionDescriptors;

namespace Contoso.Forms.Configuration
{
    public class DropDownTemplateDescriptor
    {
        public string TemplateName { get; set; }
        public string TitleText { get; set; }
        public string TextField { get; set; }
        public string ValueField { get; set; }
        public string LoadingIndicatorText { get; set; }
        public SelectorLambdaOperatorDescriptor TextAndValueSelector { get; set; }
        public RequestDetailsDescriptor RequestDetails { get; set; }
        public string ReloadItemsFlowName { get; set; }
    }
}
