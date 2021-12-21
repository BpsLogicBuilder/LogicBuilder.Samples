using AutoMapper;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Validators;
using Enrollment.XPlatform.ViewModels;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Utils
{
    public class HideIfConditionalDirectiveHelper<TModel> : BaseConditionalDirectiveHelper<HideIf<TModel>, TModel>
    {
        public HideIfConditionalDirectiveHelper(IFormGroupSettings formGroupSettings,
                                                 IEnumerable<IFormField> properties,
                                                 IMapper mapper,
                                                 List<HideIf<TModel>> parentList = null,
                                                 string parentName = null)
            : base(formGroupSettings, properties, mapper, parentList, parentName)
        {
        }
    }
}
