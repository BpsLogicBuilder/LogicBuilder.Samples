using Enrollment.Common.Configuration.ExpansionDescriptors;
using Enrollment.Common.Configuration.ExpressionDescriptors;

namespace Enrollment.Bsl.Business.Requests
{
    public class GetObjectListRequest : BaseRequest
    {
        public SelectorLambdaOperatorDescriptor Selector { get; set; }
        public SelectExpandDefinitionDescriptor SelectExpandDefinition { get; set; }
        public string ModelType { get; set; }
        public string DataType { get; set; }
    }
}
