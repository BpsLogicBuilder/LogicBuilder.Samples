using Contoso.Forms.Configuration.Navigation;
using Contoso.XPlatform.Flow;
using Contoso.XPlatform.Flow.Settings;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contoso.XPlatform.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageView : FlyoutPage
    {
        public MainPageView()
        {
            InitializeComponent();
            Visual = VisualMarker.Material;
            flyout.ListView.SelectionChanged += ListView_SelectionChanged;
            ViewModel = App.ServiceProvider.GetRequiredService<MainPageViewModel>();
            this.BindingContext = ViewModel;
            flyout.BindingContext = ViewModel;

            UiNotificationService.FlowSettingsSubject.Subscribe(FlowSettingsChanged);

            if (!DesignMode.IsDesignModeEnabled)
            {
                Start();
            }
        }

        #region Fields
        private UiNotificationService _uiNotificationService;
        private IAppLogger _appLogger;
        #endregion Fields

        #region Properties
        public MainPageViewModel ViewModel { get; }
        private bool IsPortrait => Width < Height;

        public UiNotificationService UiNotificationService
        {
            get
            {
                if (_uiNotificationService == null)
                {
                    DateTime dt = DateTime.Now;
                    _uiNotificationService = App.ServiceProvider.GetRequiredService<UiNotificationService>();
                    DateTime dt2 = DateTime.Now;

                    AppLogger.LogMessage(nameof(MainPageView), $"Get UiNotificationService (milliseconds) = {(dt2 - dt).TotalMilliseconds}");
                }

                return _uiNotificationService;
            }
        }

        public IAppLogger AppLogger
        {
            get
            {
                if (_appLogger == null)
                {
                    DateTime dt = DateTime.Now;
                    _appLogger = App.ServiceProvider.GetRequiredService<IAppLogger>();
                    DateTime dt2 = DateTime.Now;

                    AppLogger.LogMessage(nameof(MainPageView), $"Get AppLogger (milliseconds) = {(dt2 - dt).TotalMilliseconds}");
                }

                return _appLogger;
            }
        }
        #endregion Properties

        #region Methods
        private async void Start()
        {
            using (IScopedFlowManagerService flowManagerService = App.ServiceProvider.GetRequiredService<IScopedFlowManagerService>())
            {
                await flowManagerService.Start();
            }
        }

        private void FlowSettingsChanged(FlowSettings flowSettings)
        {
            flowSettings.FlowDataCache.NavigationBar.MenuItems
                .ForEach(item => item.Active = item.InitialModule == flowSettings.FlowDataCache.NavigationBar.CurrentModule);

            ChangePage(flowSettings.ScreenSettings.CreatePage());

            UpdateNavigationMenu(flowSettings);
        }

        private void UpdateNavigationMenu(FlowSettings flowSettings)
            => ViewModel.MenuItems = new ObservableCollection<NavigationMenuItemDescriptor>(flowSettings.FlowDataCache.NavigationBar.MenuItems);

        private void ChangePage(Page page)
        {
            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread
            (
                () => Detail = GetNavigationPage(page)
            );

            IsPresented = false;

            flyout.ListView.SelectedItem = null;
        }
        #endregion Methods

        private async void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count != 1)
                return;

            if (!(e.CurrentSelection.First() is NavigationMenuItemDescriptor item))
                return;

            if (item.Active)
                return;

            DisposeCurrentPageBindingContext(Detail);

            using (IScopedFlowManagerService flowManagerService = App.ServiceProvider.GetRequiredService<IScopedFlowManagerService>())
            {
                await flowManagerService.NewFlowStart
                (
                    new Flow.Requests.NewFlowRequest { InitialModuleName = item.InitialModule }
                );
            }

            void DisposeCurrentPageBindingContext(Page detail)
            {
                if (detail is not NavigationPage navigationPage)
                    return;

                if (navigationPage?.RootPage.BindingContext is not IDisposable disposable)
                    return;

                disposable.Dispose();
            }
        }

        private NavigationPage GetNavigationPage(Page page)
        {
            NavigationPage.SetHasBackButton(page, false);
            page.SetDynamicResource(Page.BackgroundColorProperty, "PageBackgroundColor");
            var navigationPage = new NavigationPage(page);
            navigationPage.SetDynamicResource(NavigationPage.BarBackgroundColorProperty, "PageBackgroundColor");
            navigationPage.SetDynamicResource(NavigationPage.BarTextColorProperty, "PrimaryTextColor");

            return navigationPage;
        }
    }
}