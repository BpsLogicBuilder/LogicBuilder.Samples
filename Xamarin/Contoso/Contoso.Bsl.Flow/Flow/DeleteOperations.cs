using AutoMapper;
using Contoso.Common.Utils;
using Contoso.Parameters.Expressions;
using LogicBuilder.Attributes;
using LogicBuilder.Data;
using LogicBuilder.Domain;
using LogicBuilder.EntityFrameworkCore.SqlServer.Repositories;
using LogicBuilder.Expressions.Utils.ExpressionBuilder;
using System;
using System.Linq.Expressions;

namespace Contoso.Bsl.Flow
{
    internal class DeleteOperations<TModel, TData> where TModel : BaseModel where TData : BaseData
    {
        [AlsoKnownAs("Delete")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static bool Delete(IContextRepository repository,
            IMapper mapper,
            FilterLambdaOperatorParameters filterExpression)
            => repository.DeleteAsync<TModel, TData>
            (
                GetFilter(mapper.MapToOperator(filterExpression))
            ).Result;

        public static Expression<Func<TModel, bool>> GetFilter(IExpressionPart filterExpression)
            => (Expression<Func<TModel, bool>>)filterExpression?.Build();
    }
}
