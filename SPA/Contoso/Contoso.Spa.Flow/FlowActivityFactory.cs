using LogicBuilder.RulesDirector;

namespace Contoso.Spa.Flow
{
    public class FlowActivityFactory : IFlowActivityFactory
    {
        public IFlowActivity Create(IFlowManager flowManager)
            => new FlowActivity(flowManager);
    }
}
