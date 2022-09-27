using AutoMapper;
using Contoso.XPlatform.Flow.Settings;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.Views.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contoso.XPlatform.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageView : FlyoutPage
    {
        public MainPageView(
            IFlyoutDetailPageFactory detailPageFactory,
            IMapper mapper,
            MainPageViewModel mainPageViewModel,
            UiNotificationService uiNotificationService)
        {
            _detailPageFactory = detailPageFactory;
            _mapper = mapper;
            _uiNotificationService = uiNotificationService;
            InitializeComponent();
            Visual = VisualMarker.Material;
            flyout.ListView.SelectionChanged += ListView_SelectionChanged;
            ViewModel = mainPageViewModel;
            this.BindingContext = ViewModel;
            flyout.BindingContext = ViewModel;

            _uiNotificationService.FlowSettingsSubject.Subscribe(FlowSettingsChanged);

            if (!DesignMode.IsDesignModeEnabled)
            {
                Start();
            }
        }

        #region Fields
        private readonly IFlyoutDetailPageFactory _detailPageFactory;
        private readonly IMapper _mapper;
        private readonly UiNotificationService _uiNotificationService;
        #endregion Fields

        #region Properties
        public MainPageViewModel ViewModel { get; }
        private bool IsPortrait => Width < Height;
        #endregion Properties

        #region Methods
        private async void Start()
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

        private void ChangePage(Page page)
        {
            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread
            (
                () => Detail = GetNavigationPage(page)
            );

            CloseFlyout();

            flyout.ListView.SelectedItem = null;
        }

        private void CloseFlyout()
        {
            if (!IsPortrait)
                return;

            IsPresented = false;
        }
        #endregion Methods

        private async void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private NavigationPage GetNavigationPage(Page page)
        {
            NavigationPage.SetHasBackButton(page, false);
            return new NavigationPage(page);
        }
    }
}