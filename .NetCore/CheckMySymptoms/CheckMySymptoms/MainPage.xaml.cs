using CheckMySymptoms.Flow.ScreenSettings;
using CheckMySymptoms.Flow.ScreenSettings.Views;
using CheckMySymptoms.Forms.View.Common;
using CheckMySymptoms.Forms.View.Input;
using CheckMySymptoms.Screens;
using CheckMySymptoms.Utils;
using CheckMySymptoms.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CheckMySymptoms
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            KeyboardAccelerator GoBack = new KeyboardAccelerator
            {
                Key = VirtualKey.GoBack
            };
            GoBack.Invoked += BackInvoked;

            KeyboardAccelerator AltLeft = new KeyboardAccelerator
            {
                Key = VirtualKey.Left
            };
            AltLeft.Invoked += BackInvoked;

            this.KeyboardAccelerators.Add(GoBack);
            this.KeyboardAccelerators.Add(AltLeft);
            // ALT routes here
            AltLeft.Modifiers = VirtualKeyModifiers.Menu;

            this.InitializeComponent();

            //Hide the tooltip for the accelerator keys.
            this.KeyboardAcceleratorPlacementMode = KeyboardAcceleratorPlacementMode.Hidden;
            this.DataContext = ViewModel;
            HandleAdFree(UiNotificationService.HasAdFree);
            UiNotificationService.FlowSettings.Subscribe(FlowSettingsChanged);
            UiNotificationService.AdFreeChanged.Subscribe(HandleAdFree);
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Start();
            } 
        }

        #region Fields
        private UiNotificationService _uiNotificationService;
        #endregion Fields

        #region Properties
        public MainPageViewModel ViewModel { get; set; } = new MainPageViewModel();

        public UiNotificationService UiNotificationService
        {
            get
            {
                if (_uiNotificationService == null)
                    _uiNotificationService = (Application.Current as App).ServiceProvider.GetRequiredService<UiNotificationService>();

                return _uiNotificationService;
            }
        }

        private bool CanGoBack => ViewModel.MenuItems?.Count > 1;
        #endregion Properties

        #region Methods
        private void HandleAdFree(bool hasAdFree)
        {
            if (hasAdFree)
            {
                UIElement element = navContentGrid.Children.FirstOrDefault(c => c is Microsoft.Advertising.WinRT.UI.AdControl);
                if (element != null)
                    navContentGrid.Children.Remove(element);
                svDialog.SetValue(Grid.RowSpanProperty, 2);
            }
        }

        private void FlowSettingsChanged(FlowSettings flowSettings)
        {
            if (flowSettings.ScreenSettings.ViewType != ViewType.Exception)
            {
                int stage = -1;
                flowSettings.FlowDataCache.DialogList.ForEach(item =>
                {
                    item.Stage = ++stage;
                    item.Last = item.Equals(flowSettings.FlowDataCache.DialogList.Last());
                    if (item.Text.Length > 20)
                        item.Text = string.Concat(item.Text.Substring(0, 20), "..."); 
                });

                UpdateNavigationMenu(flowSettings);
                ViewModel.BackButtonEnabled = ViewModel.MenuItems?.Count > 1;
            }

            if (flowSettings.NavigationType == NavigationType.Next || flowSettings.NavigationType == NavigationType.Start)
            {
                if (flowSettings.ScreenSettings.ViewType == ViewType.Exception)
                    SetExceptionDialog(flowSettings.ScreenSettings);
                else
                    SetNextDialog(flowSettings.ScreenSettings);
            }

            if (flowSettings.NavigationType == NavigationType.Previous)
                SetPreviousDialog(flowSettings.ScreenSettings);
        }

        private void UpdateNavigationMenu(FlowSettings flowSettings)
        {
            //ViewModel.MenuItems.Clear();
            navView.MenuItems.Clear();
            navView.SelectedItem = null;
            //Need to manually clear the navigation view because
            //of unpredictable behaviour in the Top Nav.  The left nav worked fine without clearing.

            if (flowSettings.NavigationType == NavigationType.Start)
                UpdateNavigationMenuOnStart(flowSettings);

            if (flowSettings.NavigationType == NavigationType.Next)
                UpdateNavigationMenuOnNext(flowSettings);

            if (flowSettings.NavigationType == NavigationType.Previous)
                UpdateNavigationMenuOnPrevious(flowSettings);
        }

        private void UpdateNavigationMenuOnStart(FlowSettings flowSettings)
        {
            ViewModel.MenuItems.Clear();
            ViewModel.MenuItems = new ObservableCollection<MenuItem>(flowSettings.FlowDataCache.DialogList);
        }

        private void UpdateNavigationMenuOnOpenSettings()
        {
            navView.SelectedItem = null;
            ViewModel.MenuItems.Clear();
            
            ViewModel.MenuItems = new ObservableCollection<MenuItem>
            (
                new System.Collections.Generic.List<MenuItem>
                {
                    new SettingsMenuItem
                    {
                        Icon = Enum.GetName(typeof(FontAwesome.UWP.FontAwesomeIcon), FontAwesome.UWP.FontAwesomeIcon.Home),
                        Text = ResourceStringNames.settingNavOnlyMenuItemText.GetResourceString(),
                        Last = false
                    }
                }
            );
        }

        private void UpdateNavigationMenuOnNext(FlowSettings flowSettings)
        {
            if (ViewModel.MenuItems.Count == 0)
                ViewModel.MenuItems = new ObservableCollection<MenuItem>(flowSettings.FlowDataCache.DialogList);
            else
            {
                for (int i = 0; i < ViewModel.MenuItems.Count; i++)
                {
                    if (ViewModel.MenuItems[i].Equals(flowSettings.FlowDataCache.DialogList[i]))
                        continue;

                    ViewModel.MenuItems[i] = flowSettings.FlowDataCache.DialogList[i];
                }

                ViewModel.MenuItems.Add(flowSettings.FlowDataCache.DialogList.Last());
            }
        }

        private void UpdateNavigationMenuOnPrevious(FlowSettings flowSettings)
        {
            if (ViewModel.MenuItems.Count == 0)//should never happen beacuse the Resume navigation type is Next
                ViewModel.MenuItems = new ObservableCollection<MenuItem>(flowSettings.FlowDataCache.DialogList);
            else
            {
                ViewModel.MenuItems
                    .Skip(flowSettings.FlowDataCache.DialogList.Count)
                    .ToList()
                    .ForEach(m => ViewModel.MenuItems.Remove(m));

                for (int i = 0; i < flowSettings.FlowDataCache.DialogList.Count; i++)
                {
                    if (ViewModel.MenuItems[i].Equals(flowSettings.FlowDataCache.DialogList[i]))
                        continue;

                    ViewModel.MenuItems[i] = flowSettings.FlowDataCache.DialogList[i];
                }
            }
        }

        private async void Start()
        {
            await UiNotificationService.Start();
        }

        private void SetPreviousDialog(ScreenSettingsBase screenSettings)
        {
            ChangeDialog(GetNewUserControl(screenSettings, -300));
            ElementSoundPlayer.Play(ElementSoundKind.MovePrevious);
        }

        private void SetNextDialog(ScreenSettingsBase screenSettings)
        {
            ChangeDialog(GetNewUserControl(screenSettings, 300));
            ElementSoundPlayer.Play(ElementSoundKind.MoveNext);
        }

        private void SetExceptionDialog(ScreenSettingsBase screenSettings)
        {
            ChangeDialog(GetNewUserControl(screenSettings, 0));
            ElementSoundPlayer.Play(ElementSoundKind.Invoke);
        }

        void ChangeDialog(UserControl control)
        {
            gridDialog.Children.Clear();
            gridDialog.Children.Add(control);

            //svDialog.ChangeView(null, -svDialog.VerticalOffset, null);
        }

        private UserControl GetNewUserControl(ScreenSettingsBase screenSettings, double transitionOffset)
        {
            switch (screenSettings.ViewType)
            {
                case ViewType.Exception:
                    return new Screens.Exception((ScreenSettings<ExceptionView>)screenSettings, transitionOffset);
                case ViewType.Message:
                    return new Message((ScreenSettings<MessageTemplateView>)screenSettings, UiNotificationService, transitionOffset);
                case ViewType.Select:
                    return new SelectButton((ScreenSettings<MessageTemplateView>)screenSettings, UiNotificationService, transitionOffset, svDialog);
                case ViewType.InputForm:
                    return new InputForm((ScreenSettings<InputFormView>)screenSettings, UiNotificationService, transitionOffset);
                case ViewType.FlowComplete:
                    return new FlowComplete((ScreenSettings<FlowCompleteView>)screenSettings, UiNotificationService, transitionOffset);
                default:
                    throw new ArgumentException("{7D11F37A-6B5D-4063-A232-445DFD417B75}");
            }
        }

        internal async Task<bool> On_BackRequested()
        {
            if (this.CanGoBack)
            {
                await UiNotificationService.Previous(UiNotificationService.CreateRequestForBackup());
                return true;
            }
            return false;
        }
        #endregion Methods

        #region Event Handlers
        private async void BackInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            await On_BackRequested();
            args.Handled = true;
        }

        private void NavView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
        }

        private async void NavView_BackRequested(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewBackRequestedEventArgs args)
        {
            await On_BackRequested();
            //UiNotificationService.Previous(UiNotificationService.CreateRequestForBackup());
        }

        private async void NavView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                UpdateNavigationMenuOnOpenSettings();
                this.UiNotificationService.NotifyExitCalled();
                ChangeDialog(new Settings(this.UiNotificationService));
                ElementSoundPlayer.Play(ElementSoundKind.Invoke);
                return;
            }
            if (args.InvokedItemContainer.Tag == null)
                return;

            if (args.InvokedItemContainer.Tag is SettingsMenuItem settingsMenuItem)
            {
                Start();
                return;
            }

            MenuItem menuItem = (MenuItem)args.InvokedItemContainer.Tag;
            await UiNotificationService.ToPreviousStep(UiNotificationService.CreateRequestForBackup(), menuItem.Stage);
        }
        #endregion Event Handlers
    }
}
