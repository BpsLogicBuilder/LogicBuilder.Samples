using LogicBuilder.RulesDirector;

namespace Contoso.Web.Flow
{
    public class FlowActivityFactory
    {
        public FlowActivityFactory()
        {
        }

        #region Variables
        #endregion Variables

        public IFlowActivity Create(IFlowManager flowManager) 
            => new FlowActivity(flowManager);
    }
}
