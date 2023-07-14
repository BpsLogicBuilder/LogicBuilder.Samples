using Contoso.Spa.Flow.Requests.TransientFlows;
using Contoso.Spa.Flow.Responses.TransientFlows;

namespace Contoso.Spa.Flow
{
    public interface ITransientFlowHelper
    {
        BaseFlowResponse RunSelectorFlow(SelectorFlowRequest selectorFlowRequest);
    }
}
