using AutoMapper;
using Contoso.Common.Utils;
using Contoso.Data;
using Contoso.Domain;
using Contoso.KendoGrid.Bsl.Business.Requests;
using Kendo.Mvc;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.UI;
using LogicBuilder.Data;
using LogicBuilder.Domain;
using LogicBuilder.EntityFrameworkCore.SqlServer.Repositories;
using LogicBuilder.Expressions.Utils.Expansions;
using LogicBuilder.Kendo.ExpressionExtensions.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Contoso.KendoGrid.Bsl.Utils
{
    public static class RequestHelpers
    {
        public static Task<DataSourceResult> GetData(this KendoGridDataRequest request, IContextRepository contextRepository, IMapper mapper)
            => (Task<DataSourceResult>)nameof(GetData).GetSelectMethod()
                .MakeGenericMethod
                (
                    request.ModelType.Contains(',') ? Type.GetType(request.ModelType) : typeof(BaseModelClass).Assembly.GetType(request.ModelType),
                    request.DataType.Contains(',') ? Type.GetType(request.DataType) : typeof(BaseDataClass).Assembly.GetType(request.DataType)
                ).Invoke(null, new object[] { request, contextRepository, mapper });

        public static Task<DataSourceResult> GetData<TModel, TData>(this KendoGridDataRequest request, IContextRepository contextRepository, IMapper mapper)
            where TModel : BaseModelClass
            where TData : BaseDataClass 
            => CreateDataSourceRequest(request.Options).GetDataSourceResult<TModel, TData>
            (
                contextRepository,
                mapper.MapExpansion(request.SelectExpandDefinition)
            );

        private static DataSourceRequest CreateDataSourceRequest(KendoGridDataSourceRequestOptions requestOptions)
        {
            var request = new DataSourceRequest();

            if (requestOptions.Sort != null)
                request.Sorts = DataSourceDescriptorSerializer.Deserialize<SortDescriptor>(requestOptions.Sort);

            request.Page = requestOptions.Page;

            request.PageSize = requestOptions.PageSize;

            if (requestOptions.Filter != null)
                request.Filters = FilterDescriptorFactory.Create(requestOptions.Filter);

            if (requestOptions.Group != null)
                request.Groups = DataSourceDescriptorSerializer.Deserialize<GroupDescriptor>(requestOptions.Group);

            if (requestOptions.Aggregate != null)
                request.Aggregates = DataSourceDescriptorSerializer.Deserialize<AggregateDescriptor>(requestOptions.Aggregate);

            return request;
        }

        private static Task<DataSourceResult> GetDataSourceResult<TModel, TData>(this DataSourceRequest request, IContextRepository contextRepository, SelectExpandDefinition selectExpandDefinition = null)
           where TModel : BaseModelClass
           where TData : BaseDataClass
           => request.Groups != null && request.Groups.Count > 0
               ? request.GetGroupedDataSourceResult<TModel, TData>(contextRepository, selectExpandDefinition)
               : request.GetUngroupedDataSourceResult<TModel, TData>(contextRepository, selectExpandDefinition);

        private static async Task<DataSourceResult> GetUngroupedDataSourceResult<TModel, TData>(this DataSourceRequest request, IContextRepository contextRepository, SelectExpandDefinition selectExpandDefinition = null)
            where TModel : BaseModel
            where TData : BaseData
        {
            bool getAggregates = request.Aggregates?.Count > 0;
            Expression<Func<IQueryable<TModel>, AggregateFunctionsGroup>> aggregatesExp = getAggregates ? QueryableExtensionsEx.CreateAggregatesExpression<TModel>(request) : null;
            Expression<Func<IQueryable<TModel>, int>> totalExp = QueryableExtensionsEx.CreateTotalExpression<TModel>(request);
            Expression<Func<IQueryable<TModel>, IQueryable<TModel>>> ungroupedExp = QueryableExtensionsEx.CreateUngroupedQueryableExpression<TModel>(request);

            return new DataSourceResult
            {
                Data = await contextRepository.QueryAsync<TModel, TData, IQueryable<TModel>, IQueryable<TData>>(ungroupedExp, selectExpandDefinition),
                AggregateResults = getAggregates
                                    ? (await GetAggregateFunctionsGroup()).GetAggregateResults(request.Aggregates.SelectMany(a => a.Aggregates))
                                    : null,
                Total = await contextRepository.QueryAsync<TModel, TData, int, int>(totalExp, selectExpandDefinition)
            };

            async Task<AggregateFunctionsGroup> GetAggregateFunctionsGroup()
            {
                var aggrewgatexpressions = request.CreateAggregatesQueryExpressions<TModel>();
                IQueryable<TModel> pagedQuery = await contextRepository.QueryAsync<TModel, TData, IQueryable<TModel>, IQueryable<TData>>(aggrewgatexpressions.QueryableExpression, selectExpandDefinition);
                return aggrewgatexpressions.AggregateExpression.Compile()(pagedQuery);
            }
        }

        private static async Task<DataSourceResult> GetGroupedDataSourceResult<TModel, TData>(this DataSourceRequest request, IContextRepository contextRepository, SelectExpandDefinition selectExpandDefinition = null)
            where TModel : BaseModel
            where TData : BaseData
        {
            bool getAggregates = request.Aggregates?.Count > 0;
            Expression<Func<IQueryable<TModel>, AggregateFunctionsGroup>> aggregatesExp = getAggregates ? QueryableExtensionsEx.CreateAggregatesExpression<TModel>(request) : null;
            Expression<Func<IQueryable<TModel>, int>> totalExp = QueryableExtensionsEx.CreateTotalExpression<TModel>(request);
            Expression<Func<IQueryable<TModel>, IEnumerable<AggregateFunctionsGroup>>> groupedExp = QueryableExtensionsEx.CreateGroupedDataExpression<TModel>(request);

            return new DataSourceResult
            {
                Data = await GetData(),
                AggregateResults = getAggregates
                                    ? (await GetAggregateFunctionsGroup()).GetAggregateResults(request.Aggregates.SelectMany(a => a.Aggregates))
                                    : null,
                Total = await contextRepository.QueryAsync<TModel, TData, int, int>(totalExp, selectExpandDefinition)
            };

            async Task<IEnumerable> GetData()
            {
                var groupByExpressions = request.CreateGroupedByQueryExpressions<TModel>();
                IQueryable<TModel> pagedQuery = await contextRepository.QueryAsync<TModel, TData, IQueryable<TModel>, IQueryable<TData>>(groupByExpressions.PagingExpression, selectExpandDefinition);
                return groupByExpressions.GroupByExpression.Compile()(pagedQuery);
            }

            async Task<AggregateFunctionsGroup> GetAggregateFunctionsGroup()
            {
                var aggrewgatexpressions = request.CreateAggregatesQueryExpressions<TModel>();
                IQueryable<TModel> pagedQuery = await contextRepository.QueryAsync<TModel, TData, IQueryable<TModel>, IQueryable<TData>>(aggrewgatexpressions.QueryableExpression, selectExpandDefinition);
                return aggrewgatexpressions.AggregateExpression.Compile()(pagedQuery);
            }
        }

        private static MethodInfo GetSelectMethod(this string methodName)
           => typeof(RequestHelpers).GetMethods().Single(m => m.Name == methodName && m.IsGenericMethod);
    }
}
