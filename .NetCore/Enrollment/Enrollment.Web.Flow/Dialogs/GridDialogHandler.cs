using Enrollment.Forms.Parameters.Common;
using Enrollment.Web.Flow.Requests;

namespace Enrollment.Web.Flow.Dialogs
{
    public class GridDialogHandler : BaseDialogHandler
    {
        public override void Complete(IFlowManager flowManager, RequestBase request)
        {
            if (!request.CommandButtonRequest.Cancel)
            {
                flowManager.FlowDataCache.ParametersItems[typeof(FilterGroupParameters).FullName] = ((GridRequest)request).FilterGroup;
            }

            base.Complete(flowManager, request);
        }
    }
}
