using AutoMapper;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Directives;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.Factories;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public class HideIfConditionalDirectiveBuilder<TModel> : BaseConditionalDirectiveBuilder<HideIf<TModel>, TModel>
    {
        public HideIfConditionalDirectiveBuilder(
            IDirectiveManagersFactory directiveManagersFactory,
            IMapper mapper,
            IFormGroupSettings formGroupSettings,
            IEnumerable<IFormField> properties,
            List<HideIf<TModel>>? parentList = null,
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
