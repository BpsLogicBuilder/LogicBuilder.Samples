using Enrollment.Common.Configuration.ExpressionDescriptors;

namespace Enrollment.Spa.Flow.Responses.TransientFlows
{
    public class SelectorFlowResponse : BaseFlowResponse
    {
        public SelectorLambdaOperatorDescriptor? Selector { get; set; }
    }
}
