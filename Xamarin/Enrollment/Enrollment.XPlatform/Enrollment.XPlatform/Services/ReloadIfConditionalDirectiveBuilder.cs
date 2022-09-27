using AutoMapper;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Directives;
using Enrollment.XPlatform.Directives.Factories;
using Enrollment.XPlatform.ViewModels;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Services
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
