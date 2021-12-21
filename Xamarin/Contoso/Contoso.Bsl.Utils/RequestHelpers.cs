using AutoMapper;
using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Common.Utils;
using Contoso.Domain;
using LogicBuilder.Data;
using LogicBuilder.Domain;
using LogicBuilder.EntityFrameworkCore.SqlServer.Repositories;
using LogicBuilder.Expressions.Utils.Expansions;
using LogicBuilder.Expressions.Utils.ExpressionBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleToAttribute("Contoso.Bsl.Flow.Integration.Tests")]
namespace Contoso.Bsl.Utils
{
    public static class RequestHelpers
    {
        public static async Task<GetListResponse> GetList(GetTypedListRequest request, IContextRepository contextRepository, IMapper mapper)
            => await (Task<GetListResponse>)"GetList".GetSelectMethod()
            .MakeGenericMethod
            (
                Type.GetType(request.ModelType),
                Type.GetType(request.DataType),
                Type.GetType(request.ModelReturnType),
                Type.GetType(request.DataReturnType)
            ).Invoke(null, new object[] { request, contextRepository, mapper });

        public static async Task<GetListResponse> GetList<TModel, TData, TModelReturn, TDataReturn>(GetTypedListRequest request, IContextRepository contextRepository, IMapper mapper)
            where TModel : BaseModel
            where TData : BaseData
            => new GetListResponse
            {
                List = (IEnumerable<EntityModelBase>)await Query<TModel, TData, TModelReturn, TDataReturn>
                (
                    contextRepository,
                    mapper.MapToOperator(request.Selector),
                    mapper.MapExpansion(request.SelectExpandDefinition)
                ),
                Success = true
            };

        public static async Task<GetEntityResponse> GetEntity(GetEntityRequest request, IContextRepository contextRepository, IMapper mapper)
            => await (Task<GetEntityResponse>)"GetEntity".GetSelectMethod()
            .MakeGenericMethod
            (
                Type.GetType(request.ModelType),
                Type.GetType(request.DataType)
            ).Invoke(null, new object[] { request, contextRepository, mapper });

        public static async Task<GetEntityResponse> GetEntity<TModel, TData>(GetEntityRequest request, IContextRepository contextRepository, IMapper mapper)
            where TModel : EntityModelBase
            where TData : BaseData
            => new GetEntityResponse
            {
                Entity = await QueryEntity<TModel, TData>
                (
                    contextRepository,
                    mapper.MapToOperator(request.Filter),
                    mapper.MapExpansion(request.SelectExpandDefinition)
                ),
                Success = true
            };

        private static Task<TModelReturn> Query<TModel, TData, TModelReturn, TDataReturn>(IContextRepository repository,
            IExpressionPart queryExpression,
            SelectExpandDefinition selectExpandDefinition = null)
            where TModel : BaseModel
            where TData : BaseData
            => repository.QueryAsync<TModel, TData, TModelReturn, TDataReturn>
            (
                (Expression<Func<IQueryable<TModel>, TModelReturn>>)queryExpression.Build(),
                selectExpandDefinition
            );

        private async static Task<TModel> QueryEntity<TModel, TData>(IContextRepository repository,
            IExpressionPart filterExpression, SelectExpandDefinition selectExpandDefinition = null)
            where TModel : BaseModel
            where TData : BaseData 
            => (
                    await repository.GetAsync<TModel, TData>
                    (
                        (Expression<Func<TModel, bool>>)filterExpression.Build(),
                        null,
                        selectExpandDefinition
                    )
               ).FirstOrDefault();

        private static MethodInfo GetSelectMethod(this string methodName)
           => typeof(RequestHelpers).GetMethods().Single(m => m.Name == methodName && m.IsGenericMethod);
    }
}
