using CheckMySymptoms.Flow;
using CheckMySymptoms.Flow.Requests;
using CheckMySymptoms.Flow.ScreenSettings.Views;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CheckMySymptoms.Flow.Dialogs
{
    public interface IDialogHandler
    {
        IEnumerable<ValidationResult> GetErrors(RequestBase request);
        ScreenSettingsBase GetScreenSettings(RequestBase request, IEnumerable<ValidationResult> errors);
        void Complete(IFlowManager flowManager);
        object FieldValues { get; }
    }
}
