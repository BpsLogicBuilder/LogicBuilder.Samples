using Contoso.Common.Configuration.ItemFilter;
using Contoso.Parameters.Expressions;
using Contoso.XPlatform.Utils;
using System;

namespace Contoso.XPlatform.Services
{
    public class GetItemFilterBuilder : IGetItemFilterBuilder
    {
        public FilterLambdaOperatorParameters CreateFilter(ItemFilterGroupDescriptor descriptor, Type modelType, object entity) 
            => CreateItemFilterHelper.CreateFilter(descriptor, modelType, entity);
    }
}
