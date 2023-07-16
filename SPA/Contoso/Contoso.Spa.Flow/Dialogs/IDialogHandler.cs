using Contoso.Spa.Flow.Requests;

namespace Contoso.Spa.Flow.Dialogs
{
    public interface IDialogHandler
    {
        void Complete(IFlowManager flowManager, RequestBase request);
    }
}
