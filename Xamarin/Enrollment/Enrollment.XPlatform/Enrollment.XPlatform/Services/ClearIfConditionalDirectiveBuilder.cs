using AutoMapper;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.Validators;
using Enrollment.XPlatform.ViewModels;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Services
{
    public class ClearIfConditionalDirectiveBuilder : IClearIfConditionalDirectiveBuilder
    {
        private readonly IMapper mapper;

        public ClearIfConditionalDirectiveBuilder(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public List<ClearIf<TModel>> GetConditions<TModel>(IFormGroupSettings formGroupSettings, IEnumerable<IFormField> properties)
            => new ClearIfConditionalDirectiveHelper<TModel>
            (
                formGroupSettings,
                properties,
                mapper
            ).GetConditions();
    }
}
