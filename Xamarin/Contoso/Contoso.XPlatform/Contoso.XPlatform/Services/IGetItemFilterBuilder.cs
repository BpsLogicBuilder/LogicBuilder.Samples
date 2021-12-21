using Contoso.Common.Configuration.ItemFilter;
using Contoso.Parameters.Expressions;
using System;

namespace Contoso.XPlatform.Services
{
    public interface IGetItemFilterBuilder
    {
        FilterLambdaOperatorParameters CreateFilter(ItemFilterGroupDescriptor descriptor, Type modelType, object entity);
    }
}
