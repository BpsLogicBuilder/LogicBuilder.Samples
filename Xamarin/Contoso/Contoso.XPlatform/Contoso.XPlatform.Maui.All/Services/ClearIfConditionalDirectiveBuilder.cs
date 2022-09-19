using AutoMapper;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Directives;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.Factories;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public class ClearIfConditionalDirectiveBuilder<TModel> : BaseConditionalDirectiveBuilder<ClearIf<TModel>, TModel>
    {
        public ClearIfConditionalDirectiveBuilder(
            IDirectiveManagersFactory directiveManagersFactory,
            IMapper mapper,
            IFormGroupSettings formGroupSettings,
            IEnumerable<IFormField> properties,
            List<ClearIf<TModel>>? parentList = null,
            string? parentName = null) : base(
                directiveManagersFactory,
                mapper,
                formGroupSettings,
                properties,
                parentList,
                parentName)
        {
        }
    }
}
