using LogicBuilder.RulesDirector;

namespace Enrollment.Web.Flow
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
