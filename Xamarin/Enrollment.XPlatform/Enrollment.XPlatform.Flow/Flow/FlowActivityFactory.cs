using LogicBuilder.RulesDirector;

namespace Enrollment.XPlatform.Flow
{
    public class FlowActivityFactory
    {
        public IFlowActivity Create(IFlowManager flowManager)
            => new FlowActivity(flowManager);
    }
}
