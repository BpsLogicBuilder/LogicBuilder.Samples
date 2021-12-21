using AutoMapper;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Validators;
using Enrollment.XPlatform.ViewModels;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Utils
{
    public class ClearIfConditionalDirectiveHelper<TModel> : BaseConditionalDirectiveHelper<ClearIf<TModel>, TModel>
    {
        public ClearIfConditionalDirectiveHelper(IFormGroupSettings formGroupSettings,
                                                 IEnumerable<IFormField> properties,
                                                 IMapper mapper,
                                                 List<ClearIf<TModel>> parentList = null,
                                                 string parentName = null)
            : base(formGroupSettings, properties, mapper, parentList, parentName)
        {
        }
    }
}
