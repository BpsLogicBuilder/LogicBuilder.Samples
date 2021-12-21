using AutoMapper;
using Enrollment.AutoMapperProfiles;
using Enrollment.XPlatform.AutoMapperProfiles;
using Enrollment.XPlatform.Flow;
using Enrollment.XPlatform.Flow.Cache;
using Enrollment.XPlatform.Flow.Rules;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Enrollment.XPlatform
{
    public class Startup
    {
        public static void Init(Action<IServiceCollection> nativeConfigurationServices)
        {
            var services = new ServiceCollection();

            nativeConfigurationServices(services);
            ConfigureServices(services);
            App.ServiceCollection = services;
            App.ServiceProvider = App.ServiceCollection.BuildServiceProvider();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services
                .AddSingleton<UiNotificationService, UiNotificationService>()
                .AddSingleton<IFieldsCollectionBuilder, FieldsCollectionBuilder>()
                .AddSingleton<ICollectionCellItemsBuilder, CollectionCellItemsBuilder>()
                .AddSingleton<IReadOnlyFieldsCollectionBuilder, ReadOnlyFieldsCollectionBuilder>()
                .AddSingleton<IConditionalValidationConditionsBuilder, ConditionalValidationConditionsBuilder>()
                .AddSingleton<IHideIfConditionalDirectiveBuilder, HideIfConditionalDirectiveBuilder>()
                .AddSingleton<IClearIfConditionalDirectiveBuilder, ClearIfConditionalDirectiveBuilder>()
                .AddSingleton<IReloadIfConditionalDirectiveBuilder, ReloadIfConditionalDirectiveBuilder>()
                .AddSingleton<IEntityStateUpdater, EntityStateUpdater>()
                .AddSingleton<IEntityUpdater, EntityUpdater>()
                .AddSingleton<IPropertiesUpdater, PropertiesUpdater>()
                .AddSingleton<IReadOnlyPropertiesUpdater, ReadOnlyPropertiesUpdater>()
                .AddSingleton<IReadOnlyCollectionCellPropertiesUpdater, ReadOnlyCollectionCellPropertiesUpdater>()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    new MapperConfiguration(cfg =>
                    {
                        cfg.AddMaps(typeof(DescriptorToOperatorMappingProfile), typeof(CommandButtonProfile));
                        cfg.AllowNullCollections = true;
                    })
                )
                .AddHttpClient()
                .AddSingleton<IHttpService, HttpService>()
                .AddSingleton<ISearchSelectorBuilder, SearchSelectorBuilder>()
                .AddSingleton<IGetItemFilterBuilder, GetItemFilterBuilder>()
                .AddSingleton<IContextProvider, ContextProvider>()
                .AddSingleton<IRulesLoader, RulesLoader>()
                .AddScoped<IFlowManager, FlowManager>()
                .AddScoped<FlowActivityFactory, FlowActivityFactory>()
                .AddScoped<DirectorFactory, DirectorFactory>()
                .AddScoped<FlowDataCache, FlowDataCache>()
                .AddScoped<ScreenData, ScreenData>()
                .AddScoped<IDialogFunctions, DialogFunctions>()
                .AddScoped<IActions, Actions>()
                .AddTransient<IScopedFlowManagerService, ScopedFlowManagerService>()
                .AddTransient<IMapper>
                (
                    sp => new Mapper
                    (
                        sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), 
                        sp.GetService
                    )
                )
                .AddTransient<MainPageViewModel, MainPageViewModel>()
                .AddTransient<EditFormViewModel, EditFormViewModel>()
                .AddTransient<DetailFormViewModel, DetailFormViewModel>()
                .AddTransient<SearchPageViewModel, SearchPageViewModel>()
                .AddTransient<ListPageViewModel, ListPageViewModel>()
                .AddTransient<TextPageViewModel, TextPageViewModel>()
                .AddTransient<ExtendedSplashViewModel, ExtendedSplashViewModel>();
        }
    }
}
