using Enrollment.XPlatform.Validators;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Enrollment.XPlatform.ViewModels.Validatables
{
    public class DatePickerValidatableObject<T> : ValidatableObjectBase<T>
    {
        public DatePickerValidatableObject(IUiNotificationService uiNotificationService, string name, string templateName, IEnumerable<IValidationRule>? validations) 
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
