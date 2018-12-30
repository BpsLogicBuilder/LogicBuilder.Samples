using Enrollment.Web.Flow.Requests;
using Enrollment.Web.Flow.ScreenSettings.Views;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Enrollment.Web.Flow.Dialogs
{
    public interface IDialogHandler
    {
        IEnumerable<ValidationResult> GetErrors(RequestBase request);
        ScreenSettingsBase GetScreenSettings(RequestBase request, IEnumerable<ValidationResult> errors);
        void Complete(IFlowManager flowManager, RequestBase request);
    }
}
