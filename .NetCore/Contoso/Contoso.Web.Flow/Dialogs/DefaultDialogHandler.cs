using Contoso.Web.Flow.Cache;
using Contoso.Web.Flow.Requests;
using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Web.Flow.Dialogs
{
    public class DefaultDialogHandler : BaseDialogHandler
    {
        public override void Complete(IFlowManager flowManager, RequestBase request)
        {
            base.Complete(flowManager, request);
        }
    }
}
