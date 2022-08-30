using AutoMapper;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
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
