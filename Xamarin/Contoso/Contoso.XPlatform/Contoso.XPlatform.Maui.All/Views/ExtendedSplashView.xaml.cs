using Contoso.XPlatform.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace Contoso.XPlatform.Views;

public partial class ExtendedSplashView : ContentPage
{
	public ExtendedSplashView()
	{
		InitializeComponent();
        //Visual = VisualMarker.Default;
        this.BindingContext = App.ServiceProvider.GetRequiredService<ExtendedSplashViewModel>();
    }

    protected async override void OnAppearing()
    {
        await LoadRules((ExtendedSplashViewModel)BindingContext);
        base.OnAppearing();
    }

    async Task LoadRules(ExtendedSplashViewModel viewModel)
    {
        await viewModel.AddRulesCacheService();
        App.ServiceProvider = App.ServiceCollection.BuildServiceProvider();
        MainThread.BeginInvokeOnMainThread
        (
            () => ((App)Application.Current!).MainPage = new MainPageView()/*Application.Current not null here*/
        );
    }
}