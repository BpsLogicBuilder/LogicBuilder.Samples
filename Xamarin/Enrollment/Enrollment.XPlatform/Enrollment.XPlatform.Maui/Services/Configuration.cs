using Enrollment.XPlatform;
using Enrollment.XPlatform.Flow.Rules;
using Enrollment.XPlatform.Services;

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
                .AddSingleton<ISearchSelectorBuilder, SearchSelectorBuilder>()
                .AddSingleton<IGetItemFilterBuilder, GetItemFilterBuilder>()
                .AddSingleton<IRulesLoader, RulesLoader>()
                .AddTransient<IScopedFlowManagerService, ScopedFlowManagerService>();
    }
}
