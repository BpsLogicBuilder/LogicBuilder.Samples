using Enrollment.XPlatform.Validators;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Windows.Input;

namespace Enrollment.XPlatform.ViewModels.Validatables
{
    public class DatePickerValidatableObject<T> : ValidatableObjectBase<T>
    {
        public DatePickerValidatableObject(UiNotificationService uiNotificationService, string name, string templateName, IEnumerable<IValidationRule>? validations) 
            : base(name, templateName, validations, uiNotificationService)
        {
        }

        public ICommand DateChangedCommand => new Command
        (
            () =>
            {
                IsDirty = true;
                IsValid = Validate();
            }
        );
    }
}
