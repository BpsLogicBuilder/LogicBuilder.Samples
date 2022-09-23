using Enrollment.XPlatform.Directives;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Services
{
    public interface IConditionalDirectiveBuilder<TConditionBase, TModel> : IConditionalDirectiveBuilder where TConditionBase : ConditionBase<TModel>, new()
    {
        List<TConditionBase> GetConditions();
    }

    public interface IConditionalDirectiveBuilder
    {
    }
}
