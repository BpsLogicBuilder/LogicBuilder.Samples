using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Enrollment.Kendo.ViewModels;
using Kendo.Mvc;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.UI;
using LogicBuilder.Data;
using LogicBuilder.Domain;
using LogicBuilder.EntityFrameworkCore.SqlServer.Repositories;
using LogicBuilder.Expressions.Utils.Expansions;
using LogicBuilder.Kendo.ExpressionExtensions.Extensions;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Enrollment.Web.Utils
{
    public static class KendoUIHelpers
    {
        /// <summary>
        /// Get DataSource Result
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <param name="request"></param>
        /// <param name="contextRepository"></param>
        /// <param name="selectExpandDefinition"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public static async Task<DataSourceResult> GetDataSourceResult<TModel, TData>(this DataSourceRequest request, IContextRepository contextRepository, SelectExpandDefinition selectExpandDefinition = null, ICollection<Expression<Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>>> includeProperties = null)
            where TModel : BaseModel
            where TData : BaseData
            => request.Groups != null && request.Groups.Count > 0
                ? await request.GetGroupedDataSourceResult<TModel, TData>(contextRepository, request.Aggregates != null && request.Aggregates.Count > 0, selectExpandDefinition, includeProperties)
                : await request.GetUngroupedDataSourceResult<TModel, TData>(contextRepository, request.Aggregates != null && request.Aggregates.Count > 0, selectExpandDefinition);

        private static async Task<DataSourceResult> GetUngroupedDataSourceResult<TModel, TData>(this DataSourceRequest request, IContextRepository contextRepository, bool getAggregates, SelectExpandDefinition selectExpandDefinition = null)
            where TModel : BaseModel
            where TData : BaseData
        {
            Expression<Func<IQueryable<TModel>, AggregateFunctionsGroup>> aggregatesExp = getAggregates ? QueryableExtensionsEx.CreateAggregatesExpression<TModel>(request) : null;
            Expression<Func<IQueryable<TModel>, int>> totalExp = QueryableExtensionsEx.CreateTotalExpression<TModel>(request);
            Expression<Func<IQueryable<TModel>, IQueryable<TModel>>> ungroupedExp = QueryableExtensionsEx.CreateUngroupedQueryableExpression<TModel>(request);

            return new DataSourceResult
            {
                Data = await contextRepository.QueryAsync<TModel, TData, IQueryable<TModel>, IQueryable<TData>>(ungroupedExp, selectExpandDefinition),
                AggregateResults = getAggregates
                                    ? (await contextRepository.QueryAsync<TModel, TData, AggregateFunctionsGroup, AggregateFunctionsGroup, AggregateFunctionsGroupModel<TModel>>(aggregatesExp, selectExpandDefinition))
                                                                ?.GetAggregateResults(request.Aggregates.SelectMany(a => a.Aggregates))
                                    : null,
                Total = await contextRepository.QueryAsync<TModel, TData, int, int>(totalExp, selectExpandDefinition)
            };
        }

        private static async Task<DataSourceResult> GetGroupedDataSourceResult<TModel, TData>(this DataSourceRequest request, IContextRepository contextRepository, bool getAggregates, SelectExpandDefinition selectExpandDefinition = null, ICollection<Expression<Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>>> includeProperties = null)
            where TModel : BaseModel
            where TData : BaseData
        {
            Expression<Func<IQueryable<TModel>, AggregateFunctionsGroup>> aggregatesExp = getAggregates ? QueryableExtensionsEx.CreateAggregatesExpression<TModel>(request) : null;
            Expression<Func<IQueryable<TModel>, int>> totalExp = QueryableExtensionsEx.CreateTotalExpression<TModel>(request);
            Expression<Func<IQueryable<TModel>, IEnumerable<AggregateFunctionsGroup>>> groupedExp = QueryableExtensionsEx.CreateGroupedDataExpression<TModel>(request);

            return new DataSourceResult
            {
                Data = await contextRepository.QueryAsync<TModel, TData, IEnumerable<AggregateFunctionsGroup>, IEnumerable<AggregateFunctionsGroup>, IEnumerable<AggregateFunctionsGroupModel<TModel>>>(groupedExp, selectExpandDefinition, includeProperties),
                AggregateResults = getAggregates
                                    ? (await contextRepository.QueryAsync<TModel, TData, AggregateFunctionsGroup, AggregateFunctionsGroup, AggregateFunctionsGroupModel<TModel>>(aggregatesExp, selectExpandDefinition))
                                                                ?.GetAggregateResults(request.Aggregates.SelectMany(a => a.Aggregates))
                                    : null,
                Total = await contextRepository.QueryAsync<TModel, TData, int, int>(totalExp, selectExpandDefinition)
            };
        }

        /// <summary>
        /// Get DataSource Result
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <param name="request"></param>
        /// <param name="query"></param>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public static DataSourceResult GetDataSourceResult<TModel, TData>(this DataSourceRequest request, IQueryable<TData> query, IMapper mapper, ICollection<Expression<Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>>> includeProperties = null)
            where TModel : BaseModel
            where TData : BaseData
            => request.Groups != null && request.Groups.Count > 0
                ? request.GetGroupedDataSourceResult<TModel, TData>(query, mapper, request.Aggregates != null && request.Aggregates.Count > 0, includeProperties)
                : request.GetUngroupedDataSourceResult<TModel, TData>(query, mapper, request.Aggregates != null && request.Aggregates.Count > 0, includeProperties);

        private static DataSourceResult GetUngroupedDataSourceResult<TModel, TData>(this DataSourceRequest request, IQueryable<TData> query, IMapper mapper, bool getAggregates, ICollection<Expression<Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>>> includeProperties = null)
            where TModel : BaseModel
            where TData : BaseData
        {
            Expression<Func<IQueryable<TModel>, AggregateFunctionsGroup>> aggregatesExp = getAggregates ? QueryableExtensionsEx.CreateAggregatesExpression<TModel>(request) : null;
            Expression<Func<IQueryable<TModel>, int>> totalExp = QueryableExtensionsEx.CreateTotalExpression<TModel>(request);
            Expression<Func<IQueryable<TModel>, IEnumerable<TModel>>> ungroupedExp = QueryableExtensionsEx.CreateUngroupedDataExpression<TModel>(request);

            return new DataSourceResult
            {
                Data = query.Query<TModel, TData, IEnumerable<TModel>, IEnumerable<TData>>(mapper, ungroupedExp, includeProperties),
                AggregateResults = getAggregates
                                    ? query.Query<TModel, TData, AggregateFunctionsGroup, AggregateFunctionsGroup, AggregateFunctionsGroupModel<TModel>>(mapper, aggregatesExp, includeProperties)
                                                                ?.GetAggregateResults(request.Aggregates.SelectMany(a => a.Aggregates))
                                    : null,
                Total = query.Query<TModel, TData, int, int>(mapper, totalExp, includeProperties)
            };
        }

        private static DataSourceResult GetGroupedDataSourceResult<TModel, TData>(this DataSourceRequest request, IQueryable<TData> query, IMapper mapper, bool getAggregates, ICollection<Expression<Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>>> includeProperties = null)
            where TModel : BaseModel
            where TData : BaseData
        {
            Expression<Func<IQueryable<TModel>, AggregateFunctionsGroup>> aggregatesExp = getAggregates ? QueryableExtensionsEx.CreateAggregatesExpression<TModel>(request) : null;
            Expression<Func<IQueryable<TModel>, int>> totalExp = QueryableExtensionsEx.CreateTotalExpression<TModel>(request);
            Expression<Func<IQueryable<TModel>, IEnumerable<AggregateFunctionsGroup>>> groupedExp = QueryableExtensionsEx.CreateGroupedDataExpression<TModel>(request);

            return new DataSourceResult
            {
                Data = query.Query<TModel, TData, IEnumerable<AggregateFunctionsGroup>, IEnumerable<AggregateFunctionsGroup>, IEnumerable<AggregateFunctionsGroupModel<TModel>>>(mapper, groupedExp, includeProperties),
                AggregateResults = getAggregates
                                    ? query.Query<TModel, TData, AggregateFunctionsGroup, AggregateFunctionsGroup, AggregateFunctionsGroupModel<TModel>>(mapper, aggregatesExp, includeProperties)
                                                                ?.GetAggregateResults(request.Aggregates.SelectMany(a => a.Aggregates))
                                    : null,
                Total = query.Query<TModel, TData, int, int>(mapper, totalExp, includeProperties)
            };
        }

        /// <summary>
        /// Query
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <typeparam name="TModelResult"></typeparam>
        /// <typeparam name="TDataResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="mapper"></param>
        /// <param name="queryFunc"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public static TModelResult Query<TModel, TData, TModelResult, TDataResult>(this IQueryable<TData> query, IMapper mapper,
            Expression<Func<IQueryable<TModel>, TModelResult>> queryFunc, ICollection<Expression<Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>>> includeProperties = null) where TData : class
        {
            ICollection<Expression<Func<IQueryable<TData>, IIncludableQueryable<TData, object>>>> includes = mapper.MapIncludesList<Expression<Func<IQueryable<TData>, IIncludableQueryable<TData, object>>>>(includeProperties);

            if (includes != null)
                query = includes
                    .Select(i => i.Compile())
                    .Aggregate(query, (q, next) => q = next(q));

            //Map the expressions
            Expression<Func<IQueryable<TData>, TDataResult>> mappedQueryExpression = mapper.MapExpression<Expression<Func<IQueryable<TData>, TDataResult>>>(queryFunc);
            Func<IQueryable<TData>, TDataResult> mappedQueryFunc = mappedQueryExpression.Compile();

            TDataResult result = mappedQueryFunc(query);//await Task.Run(() => { return mappedQueryFunc(query); });
            //execute the query
            return typeof(TModelResult) == typeof(TDataResult) ? (TModelResult)(object)result : mapper.Map<TDataResult, TModelResult>(result);
        }

        /// <summary>
        /// Query
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <typeparam name="TModelResult"></typeparam>
        /// <typeparam name="TDataResult"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="query"></param>
        /// <param name="mapper"></param>
        /// <param name="queryFunc"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public static TReturn Query<TModel, TData, TModelResult, TDataResult, TReturn>(this IQueryable<TData> query, IMapper mapper,
            Expression<Func<IQueryable<TModel>, TModelResult>> queryFunc, ICollection<Expression<Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>>> includeProperties = null) where TData : class
        {
            ICollection<Expression<Func<IQueryable<TData>, IIncludableQueryable<TData, object>>>> includes = mapper.MapIncludesList<Expression<Func<IQueryable<TData>, IIncludableQueryable<TData, object>>>>(includeProperties);

            if (includes != null)
                query = includes
                    .Select(i => i.Compile())
                    .Aggregate(query, (q, next) => q = next(q));

            //Map the expressions
            Expression<Func<IQueryable<TData>, TDataResult>> mappedQueryExpression = mapper.MapExpression<Expression<Func<IQueryable<TData>, TDataResult>>>(queryFunc);
            Func<IQueryable<TData>, TDataResult> mappedQueryFunc = mappedQueryExpression.Compile();

            TDataResult result = mappedQueryFunc(query);//await Task.Run(() => { return mappedQueryFunc(query); });
            //execute the query
            return typeof(TReturn) == typeof(TDataResult) ? (TReturn)(object)result : mapper.Map<TDataResult, TReturn>(result);
        }

        public static Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>>[] GetIncludes<T>(params Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>>[] expressions)
            => expressions;

        /// <summary>
        /// Create DataSource Request
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static DataSourceRequest CreateDataSourceRequest(this DataSourceRequestOptions req)
        {
            var request = new DataSourceRequest();

            if (req.Sort != null)
                request.Sorts = DataSourceDescriptorSerializer.Deserialize<SortDescriptor>(req.Sort);


            request.Page = req.Page;

            request.PageSize = req.PageSize;

            if (req.Filter != null)
                request.Filters = FilterDescriptorFactory.Create(req.Filter);

            if (req.Group != null)
                request.Groups = DataSourceDescriptorSerializer.Deserialize<GroupDescriptor>(req.Group);

            if (req.Aggregate != null)
                request.Aggregates = DataSourceDescriptorSerializer.Deserialize<AggregateDescriptor>(req.Aggregate);

            return request;
        }
    }
}
