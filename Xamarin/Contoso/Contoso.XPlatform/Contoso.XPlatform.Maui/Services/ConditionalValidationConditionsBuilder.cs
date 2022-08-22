using AutoMapper;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public class ConditionalValidationConditionsBuilder : IConditionalValidationConditionsBuilder
    {
        private readonly IMapper mapper;

        public ConditionalValidationConditionsBuilder(IMapper mapper)
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
