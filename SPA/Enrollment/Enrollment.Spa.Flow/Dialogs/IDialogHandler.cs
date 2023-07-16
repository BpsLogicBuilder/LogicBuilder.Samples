using Enrollment.Spa.Flow.Requests;

namespace Enrollment.Spa.Flow.Dialogs
{
    public interface IDialogHandler
    {
        void Complete(IFlowManager flowManager, RequestBase request);
    }
}
