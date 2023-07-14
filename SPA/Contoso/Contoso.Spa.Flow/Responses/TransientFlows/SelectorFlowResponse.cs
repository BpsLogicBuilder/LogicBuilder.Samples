using Contoso.Common.Configuration.ExpressionDescriptors;

namespace Contoso.Spa.Flow.Responses.TransientFlows
{
    public class SelectorFlowResponse : BaseFlowResponse
    {
        public SelectorLambdaOperatorDescriptor? Selector { get; set; }
    }
}
