using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Validators;
using Enrollment.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Services
{
    public interface IConditionalValidationConditionsBuilder
    {
        List<ValidateIf<TModel>> GetConditions<TModel>(IFormGroupSettings formGroupSettings, IEnumerable<IValidatable> properties);
    }
}
