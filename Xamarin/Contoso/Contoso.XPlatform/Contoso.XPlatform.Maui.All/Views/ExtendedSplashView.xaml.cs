using Contoso.XPlatform.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace Contoso.XPlatform.Views;

public partial class ExtendedSplashView : ContentPage
{
	public ExtendedSplashView(ExtendedSplashViewModel extendedSplashViewModel)
	{
		InitializeComponent();
        //Visual = VisualMarker.Default;
        this.BindingContext = extendedSplashViewModel;
    }

    protected async override void OnAppearing()
    {
        await ExtendedSplashView.LoadRules((ExtendedSplashViewModel)BindingContext);
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