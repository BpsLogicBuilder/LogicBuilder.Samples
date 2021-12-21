using Contoso.Parameters.Expressions;
using Contoso.Parameters.ItemFilter;
using LogicBuilder.Attributes;
using System;

namespace Contoso.Bsl.Flow.Services
{
    public interface IGetItemFilterBuilder
    {
        [AlsoKnownAs("CreateFilter")]
        FilterLambdaOperatorParameters CreateFilter
        (
            [Comments("Filter group definition.")]
            ItemFilterGroupParameters itemFilterGroup,

            [Comments("The filter parameter type for the lambda expression. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
            Type modelType,

            [Comments("Configuration details for the form.")]
            object entity
        );
    }
}
