using Contoso.Forms.Parameters.Common;
using Contoso.Web.Flow.Requests;

namespace Contoso.Web.Flow.Dialogs
{
    public class DetailDialogHandler : BaseDialogHandler
    {
        public override void Complete(IFlowManager flowManager, RequestBase request)
        {
            if (!request.CommandButtonRequest.Cancel)
            {
                flowManager.FlowDataCache.ParametersItems[typeof(FilterGroupParameters).FullName] = ((DetailRequest)request).FilterGroup;
            }

            base.Complete(flowManager, request);
        }
    }
}
