using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Validators;
using Enrollment.XPlatform.ViewModels;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Services
{
    public interface IReloadIfConditionalDirectiveBuilder
    {
        List<ReloadIf<TModel>> GetConditions<TModel>(IFormGroupSettings formGroupSettings, IEnumerable<IFormField> properties);
    }
}
