using AutoMapper;
using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Parameters.Expressions;
using Contoso.XPlatform.Constants;
using Contoso.XPlatform.Flow.Requests;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Contoso.XPlatform.Services
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
