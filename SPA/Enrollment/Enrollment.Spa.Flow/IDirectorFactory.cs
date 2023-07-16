using LogicBuilder.RulesDirector;

namespace Enrollment.Spa.Flow
{
    public interface IDirectorFactory
    {
        DirectorBase Create(IFlowManager flowManager);
    }
}
