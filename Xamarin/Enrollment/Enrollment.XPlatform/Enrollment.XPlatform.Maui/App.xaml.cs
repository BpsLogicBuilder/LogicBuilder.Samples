using Enrollment.XPlatform.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;

namespace Enrollment.XPlatform
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Microsoft.Maui.Controls.Window(ServiceProvider.GetRequiredService<MainPageView>());
            /*To use the extended splash (useful for low powered devices)
                 * 1) Replace return new Microsoft.Maui.Controls.Window(ServiceProvider.GetRequiredService<MainPageView>()); with return new Microsoft.Maui.Controls.Window(ServiceProvider.GetRequiredService<ExtendedSplashView>());
                 * 2) In MauiProgram.ConfigureServices(), comment out .AddRulesCache()
                 */
            //return new Microsoft.Maui.Controls.Window(App.ServiceProvider.GetRequiredService<ExtendedSplashView>()); 
        }

        public const string BASE_URL = "https://enrollmentapibps.azurewebsites.net/";


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
    }
}