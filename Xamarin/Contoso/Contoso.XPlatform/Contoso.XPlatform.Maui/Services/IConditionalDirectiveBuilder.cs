using Contoso.XPlatform.Directives;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public interface IConditionalDirectiveBuilder<TConditionBase, TModel> : IConditionalDirectiveBuilder where TConditionBase : ConditionBase<TModel>, new()
    {
        List<TConditionBase> GetConditions();
    }

    public interface IConditionalDirectiveBuilder
    {
    }
}
