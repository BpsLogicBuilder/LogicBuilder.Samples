using Contoso.Common.Configuration.ExpressionDescriptors;

namespace Contoso.Forms.View.Common
{
    public class DetailDropDownTemplateView
    {
		public string TemplateName { get; set; }
		public string PlaceHolderText { get; set; }
		public string TextField { get; set; }
		public string ValueField { get; set; }
		public DataRequestStateView State { get; set; }
        public SelectorLambdaOperatorDescriptor TextAndValueSelector { get; set; }
        public RequestDetailsView RequestDetails { get; set; }
    }
}