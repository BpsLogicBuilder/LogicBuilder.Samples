using Contoso.Web.Flow.Cache;
using Contoso.Web.Flow.Requests;
using Contoso.Web.Flow.ScreenSettings.Views;
using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Contoso.Web.Flow.Dialogs
{
    public interface IDialogHandler
    {
        IEnumerable<ValidationResult> GetErrors(RequestBase request);
        ScreenSettingsBase GetScreenSettings(RequestBase request, IEnumerable<ValidationResult> errors);
        void Complete(IFlowManager flowManager, RequestBase request);
    }
}
