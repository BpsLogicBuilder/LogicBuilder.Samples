using Akavache;
using Enrollment.XPlatform.Constants;
using Enrollment.XPlatform.Directives;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Hosting;
using System;

namespace Enrollment.XPlatform
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

        public static void Init(Action<IServiceCollection> platformConfigurationServices)
        {
            var services = new ServiceCollection();

            platformConfigurationServices(services);
            ConfigureServices(services);
            App.ServiceCollection = services;
            App.ServiceProvider = App.ServiceCollection.BuildServiceProvider();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services
                .AddServices()
                .AddDirectiveServices()
                .AddFlowServices()
                /*To use the extended splash (useful for low powered devices)
                 * 1) Comment out .AddRulesCache()
                 * 2) In App.CreateWindow(), replace return new Microsoft.Maui.Controls.Window(ServiceProvider.GetRequiredService<MainPageView>()); 
                 *          with return new Microsoft.Maui.Controls.Window(App.ServiceProvider.GetRequiredService<ExtendedSplashView>());
                 */
                .AddRulesCache()
                .AddViewModels()
                .AddViews();
        }
    }
}