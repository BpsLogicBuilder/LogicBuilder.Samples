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
        public ExtendedSplashView()
        {
            InitializeComponent();
            Visual = VisualMarker.Material;
            this.BindingContext = App.ServiceProvider.GetRequiredService<ExtendedSplashViewModel>();
        }

        protected async override void OnAppearing()
        {
            await LoadRules(BindingContext as ExtendedSplashViewModel);
            base.OnAppearing();
        }

        async Task LoadRules(ExtendedSplashViewModel viewModel)
        {
            await viewModel.AddRulesCacheService();
            App.ServiceProvider = App.ServiceCollection.BuildServiceProvider();
            MainThread.BeginInvokeOnMainThread
            (
                () => (Application.Current as App).MainPage = new MainPageView()
            );
        }
    }
}