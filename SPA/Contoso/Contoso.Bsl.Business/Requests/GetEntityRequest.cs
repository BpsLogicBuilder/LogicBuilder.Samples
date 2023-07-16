using Contoso.Common.Configuration.ExpansionDescriptors;
using Contoso.Common.Configuration.ExpressionDescriptors;

namespace Contoso.Bsl.Business.Requests
{
    public class GetEntityRequest : BaseRequest
    {
        public FilterLambdaOperatorDescriptor Filter { get; set; }
        public SelectExpandDefinitionDescriptor SelectExpandDefinition { get; set; }
        public string ModelType { get; set; }
        public string DataType { get; set; }
    }
}
