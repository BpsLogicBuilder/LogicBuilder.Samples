using Contoso.XPlatform.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;

namespace Contoso.XPlatform
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = ServiceProvider.GetRequiredService<MainPageView>();
            /*To use the extended splash (useful for low powered devices)
                 * 1) Replace MainPage = ServiceProvider.GetRequiredService<MainPageView>(); with MainPage = ServiceProvider.GetRequiredService<ExtendedSplashView>(); 
                 * 2) In MauiProgram.ConfigureServices(), comment out .AddRulesCache()
                 */
            //MainPage = ServiceProvider.GetRequiredService<ExtendedSplashView>(); 
        }

        public const string BASE_URL = "https://contosoapibps.azurewebsites.net/";


        #region Properties
        private static IServiceProvider? _serviceProvider;
        public static IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider == null)
                    throw new ArgumentException($"{nameof(ServiceProvider)}: {{2FF831E0-6EF6-4845-BC89-E96C31244248}}");

                return _serviceProvider;
            }
            set => _serviceProvider = value;
        }

        private static ServiceCollection? _serviceCollection;
        public static ServiceCollection ServiceCollection
        {
            get
            {
                if (_serviceCollection == null)
                    throw new ArgumentException($"{nameof(ServiceCollection)}: {{793DCDB9-3587-45CB-9EB4-A5508A575E02}}");

                return _serviceCollection;
            }
            set => _serviceCollection = value;
        }
        #endregion Properties

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
