using Enrollment.Common.Configuration.ItemFilter;
using Enrollment.Parameters.Expressions;
using Enrollment.XPlatform.Utils;
using System;

namespace Enrollment.XPlatform.Services
{
    public class GetItemFilterBuilder : IGetItemFilterBuilder
    {
        public FilterLambdaOperatorParameters CreateFilter(ItemFilterGroupDescriptor descriptor, Type modelType, object entity) 
            => CreateItemFilterHelper.CreateFilter(descriptor, modelType, entity);
    }
}
