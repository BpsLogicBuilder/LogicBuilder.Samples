using AutoMapper;
using Enrollment.Common.Utils;
using Enrollment.Parameters.Expansions;
using Enrollment.Parameters.Expressions;
using LogicBuilder.Attributes;
using LogicBuilder.Data;
using LogicBuilder.Domain;
using LogicBuilder.EntityFrameworkCore.SqlServer.Repositories;
using LogicBuilder.Expressions.Utils.ExpressionBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Enrollment.Bsl.Flow.Integration.Tests")]
namespace Enrollment.Bsl.Flow
{
    internal static class ProjectionOperations<TModel, TData> where TModel : BaseModel where TData : BaseData
    {
        [AlsoKnownAs("GetSingle")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static TModel Get(IContextRepository repository,
            IMapper mapper,
            IExpressionParameter filterExpression,
            IExpressionParameter queryFunc = null,
            SelectExpandDefinitionParameters expansion = null)
            => GetItems
            (
                repository,
                mapper,
                filterExpression,
                queryFunc,
                expansion
            ).SingleOrDefault();

        [AlsoKnownAs("GetList")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static ICollection<TModel> GetItems(IContextRepository repository,
            IMapper mapper,
            IExpressionParameter filterExpression = null,
            IExpressionParameter queryFunc = null,
            SelectExpandDefinitionParameters expansion = null) 
            => repository.GetAsync<TModel, TData>
            (
                GetFilter(mapper.MapToOperator(filterExpression)),
                GetQueryFunc(mapper.MapToOperator(queryFunc)),
                mapper.MapExpansion(expansion)
            ).Result;

        public static Expression<Func<TModel, bool>> GetFilter(IExpressionPart filterExpression)
            => (Expression<Func<TModel, bool>>)filterExpression?.Build();

        public static Expression<Func<IQueryable<TModel>, IQueryable<TModel>>> GetQueryFunc(IExpressionPart selectorExpression)
            => (Expression<Func<IQueryable<TModel>, IQueryable<TModel>>>)selectorExpression?.Build();
    }
}
