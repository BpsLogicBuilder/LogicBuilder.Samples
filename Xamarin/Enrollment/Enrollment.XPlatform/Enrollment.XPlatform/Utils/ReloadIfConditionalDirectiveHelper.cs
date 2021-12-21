using AutoMapper;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Validators;
using Enrollment.XPlatform.ViewModels;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Utils
{
    public class ReloadIfConditionalDirectiveHelper<TModel> : BaseConditionalDirectiveHelper<ReloadIf<TModel>, TModel>
    {
        public ReloadIfConditionalDirectiveHelper(IFormGroupSettings formGroupSettings,
                                                 IEnumerable<IFormField> properties,
                                                 IMapper mapper,
                                                 List<ReloadIf<TModel>> parentList = null,
                                                 string parentName = null)
            : base(formGroupSettings, properties, mapper, parentList, parentName)
        {
        }
    }
}
