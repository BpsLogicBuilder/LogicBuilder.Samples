using Enrollment.XPlatform.Validators;
using System.Collections.Generic;

namespace Enrollment.XPlatform.ViewModels.Validatables
{
    public class HiddenValidatableObject<T> : ValidatableObjectBase<T>
    {
        public HiddenValidatableObject(string name, string templateName, IEnumerable<IValidationRule> validations, UiNotificationService uiNotificationService) : base(name, templateName, validations, uiNotificationService)
        {
        }
    }
}
