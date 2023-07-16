using Enrollment.Spa.Flow.Requests.TransientFlows;
using Enrollment.Spa.Flow.Responses.TransientFlows;

namespace Enrollment.Spa.Flow
{
    public interface ITransientFlowHelper
    {
        BaseFlowResponse RunSelectorFlow(SelectorFlowRequest selectorFlowRequest);
    }
}
