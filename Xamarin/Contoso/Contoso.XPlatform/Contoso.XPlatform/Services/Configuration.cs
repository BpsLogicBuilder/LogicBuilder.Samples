using Contoso.XPlatform;
using Contoso.XPlatform.Flow.Rules;
using Contoso.XPlatform.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class Configuration
    {
        internal static IServiceCollection AddServices(this IServiceCollection services)
            => services
                .AddSingleton<IUiNotificationService, UiNotificationService>()
                .AddSingleton<ICollectionCellManager, CollectionCellManager>()
                .AddSingleton<IEntityStateUpdater, EntityStateUpdater>()
                .AddSingleton<IEntityUpdater, EntityUpdater>()
                .AddSingleton<IPropertiesUpdater, PropertiesUpdater>()
                .AddSingleton<IReadOnlyPropertiesUpdater, ReadOnlyPropertiesUpdater>()
                .AddSingleton<IReadOnlyCollectionCellPropertiesUpdater, ReadOnlyCollectionCellPropertiesUpdater>()
                .AddAutoMapperServices()
                .AddHttpClient()
                .AddSingleton<IHttpService, HttpService>()
                .AddSingleton<IRulesLoader, RulesLoader>()
                .AddTransient<IPagingSelectorBuilder, PagingSelectorBuilder>()/*must be transient - should not create scoped service from a singleton context*/
                .AddTransient<IScopedFlowManagerService, ScopedFlowManagerService>();
    }
}
