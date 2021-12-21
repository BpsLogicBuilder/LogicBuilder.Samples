using AutoMapper;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.Validators;
using Enrollment.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Services
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
