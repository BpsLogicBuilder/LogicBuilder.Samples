using LogicBuilder.RulesDirector;

namespace Contoso.Spa.Flow
{
    public interface IFlowActivityFactory
    {
        IFlowActivity Create(IFlowManager flowManager);
    }
}
