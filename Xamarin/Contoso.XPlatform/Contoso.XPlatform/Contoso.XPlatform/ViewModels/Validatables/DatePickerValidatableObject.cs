using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Validators;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class DatePickerValidatableObject<T> : ValidatableObjectBase<T>
    {
        public DatePickerValidatableObject(string name, string templateName, IEnumerable<IValidationRule> validations, UiNotificationService uiNotificationService) 
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
