using Contoso.Domain;

namespace Contoso.Spa.Flow.Requests.TransientFlows
{
    public class SelectorFlowRequest
    {
        public EntityModelBase? Entity { get; set; }
        public string? ReloadItemsFlowName { get; set; }
    }
}
