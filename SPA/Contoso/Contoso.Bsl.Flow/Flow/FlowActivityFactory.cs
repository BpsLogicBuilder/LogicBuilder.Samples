using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Bsl.Flow
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
