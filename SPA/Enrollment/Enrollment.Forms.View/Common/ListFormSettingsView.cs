using Enrollment.Common.Configuration.ExpressionDescriptors;
using System.Collections.Generic;

namespace Enrollment.Forms.View.Common
{
    public class ListFormSettingsView
    {
        public string Title { get; set; }
        public RequestDetailsView RequestDetails { get; set; }
        public SelectorLambdaOperatorDescriptor FieldsSelector { get; set; }
        public List<DetailItemView> FieldSettings { get; set; }
    }
}