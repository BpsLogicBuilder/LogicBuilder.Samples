using Contoso.XPlatform.Validators;
using System.Collections.Generic;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class HiddenValidatableObject<T> : ValidatableObjectBase<T>
    {
        public HiddenValidatableObject(string name, string templateName, IEnumerable<IValidationRule> validations, UiNotificationService uiNotificationService) : base(name, templateName, validations, uiNotificationService)
        {
        }
    }
}
