using Enrollment.Forms.ViewModels.Properties;
using Enrollment.Forms.ViewModels.Validators;
using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Forms.ViewModels.Input
{
    public class InputViewModel<T> : BaseInputViewModel
    {
        public T CurrentValue { get; set; }
        public override string TypeString => typeof(InputViewModel<T>).ToTypeName();
        public override Type Type => typeof(T);
        public override object Input => null;

        public override InputResponse GetInputResponse() => new InputResponse(CurrentValue, this.HasErrors, Type);

        public override void Validate(Dictionary<string, Dictionary<string, string>> validationMessages)
        {
            if (this.ValidationSetting == null 
                || this.ValidationSetting.Validators == null 
                || this.ValidationSetting.Validators.Count == 0)
                return;

            if (validationMessages == null || !validationMessages.TryGetValue(this.VariableId, out Dictionary<string, string> messages))
                throw new InvalidOperationException(Resources.validationMessagesRequiredFormat.FormatString(this.VariableId));

            this.Errors = ValidationManager.DoValidation(CurrentValue, this.VariableId, this.ValidationSetting, messages);
        }
    }
}
