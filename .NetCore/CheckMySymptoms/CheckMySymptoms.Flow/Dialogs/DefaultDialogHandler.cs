using CheckMySymptoms.Flow;
using CheckMySymptoms.Flow.Requests;

namespace CheckMySymptoms.Flow.Dialogs
{
    public class DefaultDialogHandler : BaseDialogHandler
    {
        public DefaultDialogHandler(RequestBase request) : base(request)
        {
        }

        public override void Complete(IFlowManager flowManager)
        {
            base.Complete(flowManager);
        }
    }
}
