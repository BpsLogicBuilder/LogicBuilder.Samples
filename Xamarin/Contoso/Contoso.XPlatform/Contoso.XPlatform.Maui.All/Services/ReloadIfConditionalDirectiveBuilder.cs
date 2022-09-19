using AutoMapper;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.Factories;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public class ReloadIfConditionalDirectiveBuilder<TModel> : BaseConditionalDirectiveBuilder<ReloadIf<TModel>, TModel>
    {
        public ReloadIfConditionalDirectiveBuilder(
            IDirectiveManagersFactory directiveManagersFactory,
            IMapper mapper,
            IFormGroupSettings formGroupSettings,
            IEnumerable<IFormField> properties,
            List<ReloadIf<TModel>>? parentList = null,
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
