using LogicBuilder.RulesDirector;

namespace Enrollment.Spa.Flow
{
    public class FlowActivityFactory : IFlowActivityFactory
    {
        public IFlowActivity Create(IFlowManager flowManager)
            => new FlowActivity(flowManager);
    }
}
