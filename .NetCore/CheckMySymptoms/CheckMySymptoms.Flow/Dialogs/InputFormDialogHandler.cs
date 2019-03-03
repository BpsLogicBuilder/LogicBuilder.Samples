using CheckMySymptoms.Flow;
using CheckMySymptoms.Flow.Requests;
using CheckMySymptoms.Flow.ScreenSettings.Views;
using CheckMySymptoms.Forms.View.Input;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CheckMySymptoms.Flow.Dialogs
{
    public class InputFormDialogHandler : BaseDialogHandler, IDialogHandler
    {
        public InputFormDialogHandler(RequestBase request) : base(request)
        {
        }

        object IDialogHandler.FieldValues => ((InputFormRequest)this.Request).Form.GetFields();


        IEnumerable<ValidationResult> IDialogHandler.GetErrors(RequestBase request)
        {
            if (request.CommandButtonRequest.Cancel)
                return new List<ValidationResult>();

            return DoValidation(((InputFormRequest)request).Form);
        }

        IEnumerable<ValidationResult> DoValidation(InputFormView vm) => vm.Validate(new ValidationContext(vm, null, null));

        ScreenSettingsBase IDialogHandler.GetScreenSettings(RequestBase request, IEnumerable<ValidationResult> errors)
            => new ScreenSettings<InputFormView>(((InputFormRequest)request).Form, errors, ViewType.InputForm);

        public override void Complete(IFlowManager flowManager)
        {
            if (!this.Request.CommandButtonRequest.Cancel)
            {
                flowManager.Director.AnswerInputQuestions
                (
                    ((InputFormRequest)this.Request).Form.GetAllQuestions()
                                                        .Select(q => new { q.Id, InputResponse = q.GetInputResponse() })
                                                        .ToDictionary(n => n.Id, n => n.InputResponse)
                );
            }

            base.Complete(flowManager);
        }
    }
}
