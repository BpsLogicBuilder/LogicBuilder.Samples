using AutoMapper;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels;
using System.Collections.Generic;

namespace Contoso.XPlatform.Utils
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
