using Contoso.XPlatform.Validators;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public interface IConditionalDirectiveBuilder<TConditionBase, TModel> where TConditionBase : ConditionBase<TModel>, new()
    {
        List<TConditionBase> GetConditions();
    }
}
