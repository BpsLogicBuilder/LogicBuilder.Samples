using AutoMapper;
using Enrollment.Data;
using Enrollment.Domain;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using LogicBuilder.EntityFrameworkCore.SqlServer.Repositories;
using LogicBuilder.Expressions.EntityFrameworkCore;
using LogicBuilder.Expressions.Utils;
using LogicBuilder.Kendo.ExpressionExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Enrollment.Web.Utils
{
    #region Helpers
    public static class GenericHelpers
    {
        public static async Task<DataSourceResult> GetData<TModel, TData>(this DataRequest request, IContextRepository contextRepository, IMapper mapper)
            where TModel : BaseModelClass
            where TData : BaseDataClass
        {
            return await request.Options.CreateDataSourceRequest().GetDataSourceResult<TModel, TData>
            (
                contextRepository,
                mapper.MapExpansion(request.SelectExpandDefinition),
                request.Includes.BuildIncludesExpressionCollection<TModel>()
            );
        }

        public static async Task<IEnumerable<dynamic>> GetDynamicSelect<TModel, TData>(this DataRequest request, IContextRepository contextRepository, IMapper mapper)
            where TModel : BaseModelClass
            where TData : BaseDataClass
        {
            Expression<Func<IQueryable<TModel>, IEnumerable<dynamic>>> exp = Expression.Parameter(typeof(IQueryable<TModel>), "q").BuildLambdaExpression<IQueryable<TModel>, IEnumerable<dynamic>>
            (
                p => request.Distinct == true
                    ? request.Options.CreateDataSourceRequest()
                        .CreateUngroupedMethodExpression(p.GetSelectNew<TModel>(request.Selects))
                        .GetDistinct()
                    : request.Options.CreateDataSourceRequest()
                        .CreateUngroupedMethodExpression(p.GetSelectNew<TModel>(request.Selects))
            );

            return await contextRepository.QueryAsync<TModel, TData, IEnumerable<dynamic>, IEnumerable<dynamic>>
            (
                exp,
                mapper.MapExpansion(request.SelectExpandDefinition)
            );
        }

        public static async Task<BaseModelClass> GetSingle<TModel, TData>(this DataRequest request, IContextRepository contextRepository, IMapper mapper)
            where TModel : BaseModelClass
            where TData : BaseDataClass
        {
            Expression<Func<IQueryable<TModel>, IQueryable<TModel>>> exp = request.Options.CreateDataSourceRequest().CreateUngroupedQueryableExpression<TModel>();

            return
            (
                await contextRepository.QueryAsync<TModel, TData, IQueryable<TModel>, IQueryable<TData>>
                (
                    exp,
                    mapper.MapExpansion(request.SelectExpandDefinition)
                )
            ).Single();
        }

        public static async Task<BaseModelClass> GetSingleOrDefault<TModel, TData>(this DataRequest request, IContextRepository contextRepository, IMapper mapper)
            where TModel : BaseModelClass
            where TData : BaseDataClass
        {
            Expression<Func<IQueryable<TModel>, IQueryable<TModel>>> exp = request.Options.CreateDataSourceRequest().CreateUngroupedQueryableExpression<TModel>();

            return
            (
                await contextRepository.QueryAsync<TModel, TData, IQueryable<TModel>, IQueryable<TData>>
                (
                    exp,
                    mapper.MapExpansion(request.SelectExpandDefinition)
                )
            ).SingleOrDefault();
        }

        public static async Task<bool> Save<TModel, TData>(TModel item, IContextRepository contextRepository)
            where TModel : BaseModelClass
            where TData : BaseDataClass
            => await contextRepository.SaveGraphAsync<TModel, TData>(item);

        public static async Task<bool> Delete<TModel, TData>(this DataRequest request, IContextRepository contextRepository)
            where TModel : BaseModelClass
            where TData : BaseDataClass
            => await contextRepository.DeleteAsync<TModel, TData>(ExpressionBuilder.Expression<TModel>(request.Options.CreateDataSourceRequest().Filters));

        public static Task<bool> InvokeGenericSaveMethod(this PostModelRequest request, string methodName, BaseModelClass item, IContextRepository contextRepository)
            => (Task<bool>)request
                .GetGenericMethod(methodName)
                .Invoke(null, new object[] { item, contextRepository });

        public static Task<bool> InvokeGenericDeleteMethod(this DataRequest request, string methodName, IContextRepository contextRepository)
            => (Task<bool>)request
                .GetGenericMethod(methodName)
                .Invoke(null, new object[] { request, contextRepository });

        public static Task<TReturn> InvokeGenericMethod<TReturn>(this DataRequest request, string methodName, IContextRepository contextRepository, IMapper mapper)
            => (Task<TReturn>)request
                .GetGenericMethod(methodName)
                .Invoke(null, new object[] { request, contextRepository, mapper });

        private static MethodInfo GetMethod(this string methodName)
            => typeof(GenericHelpers).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);

        private static MethodInfo GetGenericMethod(this DataRequest request, string methodName)
            => methodName.GetMethod().MakeGenericMethod
            (
                typeof(BaseModelClass).GetTypeInfo().Assembly.GetType(request.ModelType),
                typeof(BaseDataClass).GetTypeInfo().Assembly.GetType(request.DataType)
            );

        private static MethodInfo GetGenericMethod(this PostModelRequest request, string methodName)
            => methodName.GetMethod().MakeGenericMethod
            (
                typeof(BaseModelClass).GetTypeInfo().Assembly.GetType(request.ModelType),
                typeof(BaseDataClass).GetTypeInfo().Assembly.GetType(request.DataType)
            );
    }
    #endregion Helpers
}
