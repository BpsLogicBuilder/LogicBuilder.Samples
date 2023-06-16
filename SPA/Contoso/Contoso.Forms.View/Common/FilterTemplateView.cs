using Contoso.Common.Configuration.ExpressionDescriptors;

namespace Contoso.Forms.View.Common
{
    public class FilterTemplateView
    {
		public string TemplateName { get; set; }
		public bool IsPrimitive { get; set; }
		public string TextField { get; set; }
		public string ValueField { get; set; }
        public SelectorLambdaOperatorDescriptor TextAndValueSelector { get; set; }
        public RequestDetailsView RequestDetails { get; set; }
    }
}