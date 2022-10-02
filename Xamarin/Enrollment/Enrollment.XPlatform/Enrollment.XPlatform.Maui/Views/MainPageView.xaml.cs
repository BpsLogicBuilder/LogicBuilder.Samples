using AutoMapper;
using Enrollment.XPlatform.Flow;
using Enrollment.XPlatform.Flow.Settings;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels;
using Enrollment.XPlatform.Views.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Enrollment.XPlatform.Views;

public partial class MainPageView : FlyoutPage
{
    public MainPageView(
        IFlyoutDetailPageFactory detailPageFactory,
        IMapper mapper,
        MainPageViewModel mainPageViewModel, 
        IUiNotificationService uiNotificationService)
    {
        _detailPageFactory = detailPageFactory;
        _mapper = mapper;
        _uiNotificationService = uiNotificationService;
        InitializeComponent();
        //Visual = VisualMarker.Default;
        flyout.ListView.SelectionChanged += ListView_SelectionChanged;

        FlyoutLayoutBehavior = MainPageView.GetFlyoutLayoutBehavior();

        ViewModel = mainPageViewModel;
        this.BindingContext = ViewModel;
        flyout.BindingContext = ViewModel;

        _uiNotificationService.FlowSettingsSubject.Subscribe(FlowSettingsChanged);

        if (!DesignMode.IsDesignModeEnabled)
        {
            MainPageView.Start();
        }
    }

    #region Fields
    private readonly IFlyoutDetailPageFactory _detailPageFactory;
    private readonly IMapper _mapper;
    private readonly IUiNotificationService _uiNotificationService;
    #endregion Fields

    #region Properties
    public MainPageViewModel ViewModel { get; }
    private static bool IsPortrait => DeviceDisplay.MainDisplayInfo.Width < DeviceDisplay.MainDisplayInfo.Height;
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
        
        ChangePage(_detailPageFactory.CreatePage(flowSettings.ScreenSettings));

        UpdateNavigationMenu(flowSettings);
    }

    private void UpdateNavigationMenu(FlowSettings flowSettings)
    {
        List<FlyoutMenuItem> menuItems = _mapper.Map<List<FlyoutMenuItem>>(flowSettings.FlowDataCache.NavigationBar.MenuItems);

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
            flowManagerService.CopyPersistentFlowItems();
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