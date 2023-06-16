using LogicBuilder.RulesDirector;

namespace Contoso.Spa.Flow
{
    public interface IDirectorFactory
    {
        DirectorBase Create(IFlowManager flowManager);
    }
}
