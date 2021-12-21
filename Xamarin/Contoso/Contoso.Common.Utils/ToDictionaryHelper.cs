using AutoMapper;
using Contoso.Parameters.Expressions;
using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Contoso.Common.Utils
{
    public static class ToDictionaryHelper<TSource, TKey, TValue>
    {
        [AlsoKnownAs("ToDictionary")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static IDictionary<TKey, TValue> ToDictionary(IMapper mapper, IEnumerable<TSource> enumerable, SelectorLambdaOperatorParameters keySelector, SelectorLambdaOperatorParameters valueSelector)
            => enumerable.ToDictionary
            (
                ((Expression<Func<TSource, TKey>>)mapper.MapToOperator(keySelector).Build()).Compile(),
                ((Expression<Func<TSource, TValue>>)mapper.MapToOperator(valueSelector).Build()).Compile()
            );
    }
}
