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
    abstract public class BaseDialogHandler : IDialogHandler
    {
        public virtual void Complete(IFlowManager flowManager, RequestBase request)
        {
            ((Director)flowManager.Director).FlowState = request.FlowState;
            flowManager.Director.SetSelection(request.CommandButtonRequest.NewSelection);
        }

        public static BaseDialogHandler Create(RequestBase response)
        {
            switch(response.ViewType)
            {
                case ViewType.Grid:
                case ViewType.Detail:
                    return (BaseDialogHandler)Activator.CreateInstance
                    (
                        typeof(BaseDialogHandler).Assembly.GetType
                        (
                            string.Format
                            (
                                "Contoso.Web.Flow.Dialogs.{0}DialogHandler",
                                Enum.GetName(typeof(ViewType), response.ViewType)
                            )
                        )
                    );
                default:
                    return new DefaultDialogHandler();
            }
        }

        IEnumerable<ValidationResult> IDialogHandler.GetErrors(RequestBase request) => new List<ValidationResult>();

        ScreenSettingsBase IDialogHandler.GetScreenSettings(RequestBase request, IEnumerable<ValidationResult> errors)
        {
            throw new NotImplementedException();
        }
    }
}
