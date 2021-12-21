using AutoMapper;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.Validators;
using Enrollment.XPlatform.ViewModels;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Services
{
    public class HideIfConditionalDirectiveBuilder : IHideIfConditionalDirectiveBuilder
    {
        private readonly IMapper mapper;

        public HideIfConditionalDirectiveBuilder(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public List<HideIf<TModel>> GetConditions<TModel>(IFormGroupSettings formGroupSettings, IEnumerable<IFormField> properties)
            => new HideIfConditionalDirectiveHelper<TModel>
            (
                formGroupSettings,
                properties,
                mapper
            ).GetConditions();
    }
}
