using Enrollment.Web.Flow.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Web.Flow.Dialogs
{
    public class DefaultDialogHandler : BaseDialogHandler
    {
       public override void Complete(IFlowManager flowManager, RequestBase request)
        {
            base.Complete(flowManager, request);
        }
    }
}
