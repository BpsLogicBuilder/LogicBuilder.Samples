using Enrollment.Common.Configuration.ExpansionDescriptors;
using Enrollment.Common.Configuration.ExpressionDescriptors;

namespace Enrollment.Bsl.Business.Requests
{
    public class GetEntityRequest : BaseRequest
    {
        public FilterLambdaOperatorDescriptor Filter { get; set; }
        public SelectExpandDefinitionDescriptor SelectExpandDefinition { get; set; }
        public string ModelType { get; set; }
        public string DataType { get; set; }
    }
}
