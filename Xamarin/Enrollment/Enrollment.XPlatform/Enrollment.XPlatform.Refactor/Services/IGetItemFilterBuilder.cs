using Enrollment.Common.Configuration.ItemFilter;
using Enrollment.Parameters.Expressions;
using System;

namespace Enrollment.XPlatform.Services
{
    public interface IGetItemFilterBuilder
    {
        FilterLambdaOperatorParameters CreateFilter(ItemFilterGroupDescriptor descriptor, Type modelType, object entity);
    }
}
