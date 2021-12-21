using LogicBuilder.RulesDirector;

namespace Contoso.XPlatform.Flow
{
    public class FlowActivityFactory
    {
        public IFlowActivity Create(IFlowManager flowManager)
            => new FlowActivity(flowManager);
    }
}
