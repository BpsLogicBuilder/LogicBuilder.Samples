using Contoso.XPlatform;
using Contoso.XPlatform.Flow.Rules;
using Contoso.XPlatform.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class Configuration
    {
        internal static IServiceCollection AddServices(this IServiceCollection services)
            => services
                .AddSingleton<UiNotificationService, UiNotificationService>()
                .AddSingleton<ICollectionCellManager, CollectionCellManager>()
                .AddSingleton<IConditionalValidationConditionsBuilder, ConditionalValidationConditionsBuilder>()
                .AddSingleton<IHideIfConditionalDirectiveBuilder, HideIfConditionalDirectiveBuilder>()
                .AddSingleton<IClearIfConditionalDirectiveBuilder, ClearIfConditionalDirectiveBuilder>()
                .AddSingleton<IReloadIfConditionalDirectiveBuilder, ReloadIfConditionalDirectiveBuilder>()
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
                .AddSingleton<IContextProvider, ContextProvider>()
                .AddSingleton<IRulesLoader, RulesLoader>()
                .AddTransient<IScopedFlowManagerService, ScopedFlowManagerService>();
    }
}
