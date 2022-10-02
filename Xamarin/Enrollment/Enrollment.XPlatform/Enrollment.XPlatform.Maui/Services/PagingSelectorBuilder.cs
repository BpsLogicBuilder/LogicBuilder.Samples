using AutoMapper;
using Enrollment.Common.Configuration.ExpressionDescriptors;
using Enrollment.Parameters.Expressions;
using Enrollment.XPlatform.Constants;
using Enrollment.XPlatform.Flow.Requests;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Enrollment.XPlatform.Services
{
    internal class PagingSelectorBuilder : IPagingSelectorBuilder
    {
        private readonly IMapper mapper;

        public PagingSelectorBuilder(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<SelectorLambdaOperatorDescriptor> CreateSelector(int skip, string? searchText, string createPagingSelectorFlowName)
        {
            using IScopedFlowManagerService flowManagerService = App.ServiceProvider.GetRequiredService<IScopedFlowManagerService>();
            flowManagerService.SetFlowDataCacheItem
            (
                FlowDataCacheItemKeys.SkipCount,
                skip
            );

            flowManagerService.SetFlowDataCacheItem
            (
                FlowDataCacheItemKeys.SearchText,
                searchText ?? ""
            );

            await flowManagerService.RunFlow
            (
                new NewFlowRequest
                {
                    InitialModuleName = createPagingSelectorFlowName
                }
            );

            return this.mapper.Map<SelectorLambdaOperatorDescriptor>
            (
                flowManagerService.GetFlowDataCacheItem(typeof(SelectorLambdaOperatorParameters).FullName!)/*FullName of known type*/
            );
        }
    }
}

