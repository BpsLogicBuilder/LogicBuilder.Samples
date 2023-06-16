using Contoso.Common.Configuration.ExpressionDescriptors;
using System.Collections.Generic;

namespace Contoso.Forms.View.Common
{
    public class ListFormSettingsView
    {
		public string Title { get; set; }
		public RequestDetailsView RequestDetails { get; set; }
        public SelectorLambdaOperatorDescriptor FieldsSelector { get; set; }
        public DataRequestStateView State { get; set; }
		public List<DetailItemView> FieldSettings { get; set; }
    }
}