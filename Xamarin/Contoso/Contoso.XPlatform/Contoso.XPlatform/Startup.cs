using Akavache;
using Contoso.XPlatform.Constants;
using Contoso.XPlatform.Directives;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Contoso.XPlatform
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
            BlobCache.ApplicationName = AppConstants.ApplicationName;
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services
                .AddServices()
                .AddDirectiveServices()
                .AddFlowServices()
                /*To use the extended splash (useful for low powered devices)
                 * 1) Comment out .AddRulesCache()
                 * 2) In App(), replace MainPage = ServiceProvider.GetRequiredService<MainPageView>();
                 *          with MainPage = ServiceProvider.GetRequiredService<ExtendedSplashView>();
                 */
                .AddRulesCache()
                .AddViewModels()
                .AddViews();
        }
    }
}
