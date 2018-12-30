using LogicBuilder.RulesDirector;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

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
