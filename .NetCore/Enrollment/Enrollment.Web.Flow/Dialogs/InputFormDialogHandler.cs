using Enrollment.Forms.View.Input;
using Enrollment.Web.Flow.Requests;
using Enrollment.Web.Flow.ScreenSettings.Views;
using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Enrollment.Web.Flow.Dialogs
{
    public class InputFormDialogHandler : BaseDialogHandler, IDialogHandler
    {
        IEnumerable<ValidationResult> IDialogHandler.GetErrors(RequestBase request)
        {
            if (request.CommandButtonRequest.Cancel)
                return new List<ValidationResult>();

            return DoValidation(((InputFormRequest)request).Form);
        }

        IEnumerable<ValidationResult> DoValidation(InputFormView vm) => vm.Validate(new ValidationContext(vm, null, null));

        ScreenSettingsBase IDialogHandler.GetScreenSettings(RequestBase request, IEnumerable<ValidationResult> errors) 
            => new ScreenSettings<InputFormView>(((InputFormRequest)request).Form, errors, ViewType.InputForm);

        public override void Complete(IFlowManager flowManager, RequestBase request)
        {
            if (!request.CommandButtonRequest.Cancel)
            {
                flowManager.Director.AnswerInputQuestions
                (
                    ((InputFormRequest)request).Form.GetAllQuestions()
                                                        .Select(q => new { q.Id, InputResponse = q.GetInputResponse() })
                                                        .ToDictionary(n => n.Id, n => n.InputResponse)
                );
            }

            base.Complete(flowManager, request);
        }
    }
}
