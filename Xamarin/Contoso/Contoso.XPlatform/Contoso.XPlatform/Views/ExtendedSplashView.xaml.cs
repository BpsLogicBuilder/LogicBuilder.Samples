using Contoso.XPlatform.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contoso.XPlatform.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExtendedSplashView : ContentPage
    {
        public ExtendedSplashView(ExtendedSplashViewModel extendedSplashViewModel)
        {
            InitializeComponent();
            Visual = VisualMarker.Material;
            this.BindingContext = extendedSplashViewModel;
        }

        protected async override void OnAppearing()
        {
            await LoadRules((ExtendedSplashViewModel)BindingContext);
            base.OnAppearing();
        }

        static async Task LoadRules(ExtendedSplashViewModel viewModel)
        {
            await viewModel.AddRulesCacheService();
            App.ServiceProvider = App.ServiceCollection.BuildServiceProvider();
            MainThread.BeginInvokeOnMainThread
            (
                () => ((App)Application.Current!).MainPage = App.ServiceProvider.GetRequiredService<MainPageView>()/*Application.Current not null here*/
                                                                                        /*Can't inject MainPageView since we're rebuilding the service provider*/
            );
        }
    }
}