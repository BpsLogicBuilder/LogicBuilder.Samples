using LogicBuilder.RulesDirector;

namespace Enrollment.Spa.Flow
{
    public interface IFlowActivityFactory
    {
        IFlowActivity Create(IFlowManager flowManager);
    }
}
