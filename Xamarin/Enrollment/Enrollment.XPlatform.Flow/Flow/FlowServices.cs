using Enrollment.XPlatform.Flow;
using Enrollment.XPlatform.Flow.Cache;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FlowServices
    {
        public static IServiceCollection AddFlowServices(this IServiceCollection services)
            => services
                .AddScoped<IFlowManager, FlowManager>()
                .AddScoped<FlowActivityFactory, FlowActivityFactory>()
                .AddScoped<DirectorFactory, DirectorFactory>()
                .AddScoped<FlowDataCache, FlowDataCache>()
                .AddScoped<ScreenData, ScreenData>()
                .AddScoped<IDialogFunctions, DialogFunctions>()
                .AddScoped<IActions, Actions>();
    }
}
