using AutoMapper;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public class ValidateIfConditionsBuilder : IValidateIfConditionsBuilder
    {
        private readonly IMapper mapper;

        public ValidateIfConditionsBuilder(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public List<ValidateIf<TModel>> GetConditions<TModel>(IFormGroupSettings formGroupSettings, IEnumerable<IValidatable> properties)
            => new ValidateIfConditionalDirectiveHelper<TModel>
            (
                formGroupSettings,
                properties,
                mapper
            ).GetConditions();
    }
}
