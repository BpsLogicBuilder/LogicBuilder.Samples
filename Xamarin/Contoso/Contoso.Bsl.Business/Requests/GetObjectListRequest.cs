using Contoso.Common.Configuration.ExpansionDescriptors;
using Contoso.Common.Configuration.ExpressionDescriptors;

namespace Contoso.Bsl.Business.Requests
{
    public class GetObjectListRequest : BaseRequest
    {
        public SelectorLambdaOperatorDescriptor Selector { get; set; }
        public SelectExpandDefinitionDescriptor SelectExpandDefinition { get; set; }
        public string ModelType { get; set; }
        public string DataType { get; set; }
    }
}
