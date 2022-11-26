using Contoso.XPlatform.Flow;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Contoso.XPlatform.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.UnhandledException += App_UnhandledException;
        }

        protected override MauiApp CreateMauiApp()
        {
            MauiApp mauiApp = MauiProgram.CreateMauiApp();
            MauiProgram.Init(ConfigureServices);
            return mauiApp;
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAppLogger, AppLogger>();
        }

        private async void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            await ShowDialog(e.Message);
            await ShowDialog(e.Exception.Message);
            await ShowDialog(e.Exception.ToString());
        }

        private static async Task ShowDialog(string message)
        {
            ContentDialog dialog = new()
            {
                Title = "Error",
                Content = message,
                CloseButtonText = "Ok"
            };

            await dialog.ShowAsync();
        }
    }
}