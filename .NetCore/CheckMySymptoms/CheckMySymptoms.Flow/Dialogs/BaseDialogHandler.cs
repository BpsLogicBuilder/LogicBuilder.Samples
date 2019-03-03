using CheckMySymptoms.Flow;
using CheckMySymptoms.Flow.Requests;
using CheckMySymptoms.Flow.ScreenSettings.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CheckMySymptoms.Flow.Dialogs
{
    abstract public class BaseDialogHandler : IDialogHandler
    {
        public BaseDialogHandler(RequestBase request)
        {
            this.Request = request;
        }

        protected RequestBase Request { get; }
        object IDialogHandler.FieldValues => null;

        public virtual void Complete(IFlowManager flowManager)
        {
            flowManager.Director.SetSelection(this.Request.CommandButtonRequest.NewSelection);
        }

        public static BaseDialogHandler Create(RequestBase request)
        {
            switch (request.ViewType)
            {
                case ViewType.Select:
                case ViewType.InputForm:
                    return (BaseDialogHandler)Activator.CreateInstance
                    (
                        typeof(BaseDialogHandler).Assembly.GetType
                        (
                            string.Format
                            (
                                "CheckMySymptoms.Flow.Dialogs.{0}DialogHandler",
                                Enum.GetName(typeof(ViewType), request.ViewType)
                            )
                        ),
                        new object[] { request }
                    );
                default:
                    return new DefaultDialogHandler(request);
            }
        }

        IEnumerable<ValidationResult> IDialogHandler.GetErrors(RequestBase request) => new List<ValidationResult>();

        ScreenSettingsBase IDialogHandler.GetScreenSettings(RequestBase request, IEnumerable<ValidationResult> errors)
        {
            throw new NotImplementedException();
        }
    }
}
