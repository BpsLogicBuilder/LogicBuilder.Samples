using AutoMapper;
using Contoso.XPlatform.Flow.Cache;
using Contoso.XPlatform.Flow.Rules;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Flow;
using Contoso.XPlatform.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Hosting;
using System;
using Contoso.XPlatform.Services;
using Contoso.AutoMapperProfiles;
using Contoso.XPlatform.AutoMapperProfiles;
using Contoso.XPlatform.ViewModels;
using Akavache;

namespace Contoso.XPlatform
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    const string FontAwesomeFontFile = "FontAwesome5Solid900.otf";
                    if (DeviceInfo.Platform == DevicePlatform.Android)
                        fonts.AddFont(FontAwesomeFontFile, FontAwesomeFontFamily.AndroidSolid);

                    if (DeviceInfo.Platform == DevicePlatform.iOS)
                        fonts.AddFont(FontAwesomeFontFile, FontAwesomeFontFamily.iOSSolid);

                    if (DeviceInfo.Platform == DevicePlatform.MacCatalyst)
                        fonts.AddFont(FontAwesomeFontFile, FontAwesomeFontFamily.MacCatalystSolid);

                    if (DeviceInfo.Platform == DevicePlatform.Tizen)
                        fonts.AddFont(FontAwesomeFontFile, FontAwesomeFontFamily.TizenSolid);

                    if (DeviceInfo.Platform == DevicePlatform.WinUI)
                        fonts.AddFont(FontAwesomeFontFile, FontAwesomeFontFamily.WinUISolid);

                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            BlobCache.ApplicationName = AppConstants.ApplicationName;

            return builder.Build();
        }

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
                .AddTransient<Func<ScreenSettingsBase, EditFormViewModel>>
                (
                    provider =>
                    screenSettings => ActivatorUtilities.CreateInstance<EditFormViewModel>
                    (
                        provider,
                        provider.GetRequiredService<IContextProvider>(),
                        screenSettings
                    )
                )
                .AddTransient<Func<ScreenSettingsBase, DetailFormViewModel>>
                (
                    provider =>
                    screenSettings => ActivatorUtilities.CreateInstance<DetailFormViewModel>
                    (
                        provider,
                        provider.GetRequiredService<IContextProvider>(),
                        screenSettings
                    )
                )
                .AddTransient<Func<ScreenSettingsBase, SearchPageViewModel>>
                (
                    provider =>
                    screenSettings => ActivatorUtilities.CreateInstance<SearchPageViewModel>
                    (
                        provider,
                        provider.GetRequiredService<IContextProvider>(),
                        screenSettings
                    )
                )
                .AddTransient<Func<ScreenSettingsBase, ListPageViewModel>>
                (
                    provider =>
                    screenSettings => ActivatorUtilities.CreateInstance<ListPageViewModel>
                    (
                        provider,
                        provider.GetRequiredService<IContextProvider>(),
                        screenSettings
                    )
                )
                .AddTransient<Func<ScreenSettingsBase, TextPageViewModel>>
                (
                    provider =>
                    screenSettings => ActivatorUtilities.CreateInstance<TextPageViewModel>
                    (
                        provider,
                        screenSettings
                    )
                )
                .AddTransient<ExtendedSplashViewModel, ExtendedSplashViewModel>();
        }
    }
}