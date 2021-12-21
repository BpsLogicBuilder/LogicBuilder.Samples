using AutoMapper;
using Contoso.Common.Utils;
using Contoso.Parameters.Expansions;
using Contoso.Parameters.Expressions;
using LogicBuilder.Attributes;
using LogicBuilder.EntityFrameworkCore.SqlServer.Repositories;
using LogicBuilder.Expressions.Utils.ExpressionBuilder;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Contoso.Bsl.Flow
{
    public static class QueryOperations<TModel, TData, TModelReturn, TDataReturn> where TModel : LogicBuilder.Domain.BaseModel where TData : LogicBuilder.Data.BaseData
    {
        /// <summary>
        /// The expression (queryExpression) is of ttype Expression<Func<IQueryable<TModel>, TModelReturn>>.
        /// An expression of type Expression<Func<IQueryable<TData>, TDataReturn>> will be executed against the
        /// database. If TModelReturn and TDataReturn are IQueryables then the return value is aquired by projection
        /// otherwise it will be retrieved using mapping.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        /// <param name="queryExpression"></param>
        /// <param name="expansion">Can be used when TModelReturn and TDataReturn are IQueryables</param>
        /// <returns></returns>
        [AlsoKnownAs("Query")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static TModelReturn Query(IContextRepository repository,
            IMapper mapper,
            IExpressionParameter queryExpression,
            SelectExpandDefinitionParameters expansion = null) 
            => repository.QueryAsync<TModel, TData, TModelReturn, TDataReturn>
            (
                GetQueryFunc(mapper.MapToOperator(queryExpression)),
                mapper.MapExpansion(expansion)
            ).Result;

        public static Expression<Func<IQueryable<TModel>, TModelReturn>> GetQueryFunc(IExpressionPart selectorExpression)
           => (Expression<Func<IQueryable<TModel>, TModelReturn>>)selectorExpression.Build();
    }
}
