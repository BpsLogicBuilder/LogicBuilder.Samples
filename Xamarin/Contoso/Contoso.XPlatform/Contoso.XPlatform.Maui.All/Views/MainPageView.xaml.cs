using AutoMapper;
using Contoso.XPlatform.Flow;
using Contoso.XPlatform.Flow.Settings;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Views;

public partial class MainPageView : FlyoutPage
{
    public MainPageView()
    {
        InitializeComponent();
        //Visual = VisualMarker.Default;
        flyout.ListView.SelectionChanged += ListView_SelectionChanged;

        FlyoutLayoutBehavior = MainPageView.GetFlyoutLayoutBehavior();

        ViewModel = App.ServiceProvider.GetRequiredService<MainPageViewModel>();
        this.BindingContext = ViewModel;
        flyout.BindingContext = ViewModel;

        UiNotificationService.FlowSettingsSubject.Subscribe(FlowSettingsChanged);

        if (!DesignMode.IsDesignModeEnabled)
        {
            MainPageView.Start();
        }
    }

    #region Fields
    private UiNotificationService? _uiNotificationService;
    private IAppLogger? _appLogger;
    private IMapper? _mapper;
    #endregion Fields

    #region Properties
    public MainPageViewModel ViewModel { get; }
    private static bool IsPortrait => DeviceDisplay.MainDisplayInfo.Width < DeviceDisplay.MainDisplayInfo.Height;

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

    public IMapper Mapper
    {
        get
        {
            _mapper ??= App.ServiceProvider.GetRequiredService<IMapper>();

            return _mapper;
        }
    }
    #endregion Properties

    #region Methods
    private static async void Start()
    {
        using IScopedFlowManagerService flowManagerService = App.ServiceProvider.GetRequiredService<IScopedFlowManagerService>();
        await flowManagerService.Start();
    }

    private void FlowSettingsChanged(FlowSettings flowSettings)
    {
        flowSettings.FlowDataCache.NavigationBar.MenuItems
            .ForEach(item => item.Active = item.InitialModule == flowSettings.FlowDataCache.NavigationBar.CurrentModule);

        ChangePage(flowSettings.ScreenSettings.CreatePage());

        UpdateNavigationMenu(flowSettings);
    }

    private void UpdateNavigationMenu(FlowSettings flowSettings)
    {
        List<FlyoutMenuItem> menuItems = Mapper.Map<List<FlyoutMenuItem>>(flowSettings.FlowDataCache.NavigationBar.MenuItems);

        if (menuItems.Count == ViewModel.MenuItems.Count)
        {
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (!menuItems[i].Equals(ViewModel.MenuItems[i]))
                {
                    ViewModel.MenuItems[i].Text = menuItems[i].Text;
                    ViewModel.MenuItems[i].Active = menuItems[i].Active;
                }
            }

            return;//Updating the active state prevents flicker on WinUI
        }

        ViewModel.MenuItems = new ObservableCollection<FlyoutMenuItem>
        (
            menuItems
        );
    }

    private async void ChangePage(Page page)
    {
        await MainThread.InvokeOnMainThreadAsync
        (
            () => Detail = MainPageView.GetNavigationPage(page)
        );

        CloseFlyout();

        flyout.ListView.SelectedItem = null;
    }

    private void CloseFlyout()
    {
        if (!MainPageView.IsPortrait)
            return;

        IsPresented = false;
    }

    private static FlyoutLayoutBehavior GetFlyoutLayoutBehavior()
    {
        return DeviceInfo.Platform == DevicePlatform.WinUI
            ? FlyoutLayoutBehavior.Default
            : FlyoutLayoutBehavior.Popover;/*Flyout page issues on ios and Android https://github.com/dotnet/maui/issues/7520 */
    }
    #endregion Methods

    private async void ListView_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 1)
            return;

        if (e.CurrentSelection[0] is not FlyoutMenuItem item)
            return;

        if (item.Active)
        {
            CloseFlyout();
            return;
        }

        DisposeCurrentPageBindingContext(Detail);

        using (IScopedFlowManagerService flowManagerService = App.ServiceProvider.GetRequiredService<IScopedFlowManagerService>())
        {
            await flowManagerService.NewFlowStart
            (
                new Flow.Requests.NewFlowRequest { InitialModuleName = item.InitialModule }
            );
        }

        static void DisposeCurrentPageBindingContext(Page detail)
        {
            if (detail is not NavigationPage navigationPage)
                return;

            if (navigationPage?.RootPage.BindingContext is not IDisposable disposable)
                return;

            disposable.Dispose();
        }
    }

    private static NavigationPage GetNavigationPage(Page page)
    {
        NavigationPage.SetHasBackButton(page, false);
        return new NavigationPage(page);
    }
}