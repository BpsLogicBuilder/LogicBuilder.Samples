using CheckMySymptoms.Utils;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CheckMySymptoms
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.UnhandledException += App_UnhandledException;
            Windows.Storage.ApplicationData.Current.DataChanged += Current_DataChanged;
            GetSoundValues();
        }

        private void Current_DataChanged(Windows.Storage.ApplicationData sender, object args)
        {
            GetSoundValues();
        }

        private void GetSoundValues()
        {
            Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            if (roamingSettings.Values[RoamingData.SoundPlayerState] is int soundPlayerState)
                ElementSoundPlayer.State = (ElementSoundPlayerState)soundPlayerState;

            if (roamingSettings.Values[RoamingData.SpatialAudioMode] is int spatialAudioMode)
                ElementSoundPlayer.SpatialAudioMode = (ElementSpatialAudioMode)spatialAudioMode;
        }

        private async void App_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            if (!(((Windows.UI.Xaml.Controls.ContentControl)(Window.Current.Content)).Content is MainPage mainPage))
                return;

            e.Handled = await mainPage.On_BackRequested();
        }

        async System.Threading.Tasks.Task ShowDialog(string message)
        {
            var messageDialog = new Windows.UI.Popups.MessageDialog(message);

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new Windows.UI.Popups.UICommand(
                "Try again",
                new Windows.UI.Popups.UICommandInvokedHandler(this.CommandInvokedHandler)));
            messageDialog.Commands.Add(new Windows.UI.Popups.UICommand(
                "Close",
                new Windows.UI.Popups.UICommandInvokedHandler(this.CommandInvokedHandler)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();
        }

        private void CommandInvokedHandler(Windows.UI.Popups.IUICommand command)
        {
            //throw new NotImplementedException();
        }
#if (DEBUG)
        private async void App_UnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {

            await ShowDialog(e.Message);
            await ShowDialog(e.Exception.Message);
            await ShowDialog(e.Exception.ToString());

        }
#else
        private void App_UnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {

        }
#endif

        #region Properties
        public IServiceProvider ServiceProvider { get; set; }
        #endregion Properties

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {//Called when an app is initially invoked from a tile
         //or after the app was supended then teminated or forcibly shut down

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                TilesHelpers.CreateTiles();
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame
                {
                    // Set the default language
                    Language = Windows.Globalization.ApplicationLanguages.Languages[0]
                };

                rootFrame.NavigationFailed += OnNavigationFailed;

                //  Display an extended splash screen if app was not previously running.
                if (e.PreviousExecutionState != ApplicationExecutionState.Running)
                {
                    bool loadState = (e.PreviousExecutionState == ApplicationExecutionState.Terminated);
                    ExtendedSplash extendedSplash = new ExtendedSplash(e.SplashScreen, loadState);
                    rootFrame.Content = extendedSplash;
                    Window.Current.Content = rootFrame;
                }
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();

            // IRulesCache cache = ServiceProvider.GetRequiredService<IRulesCache>();
            //IRulesCache cache = ServiceProvider.GetRequiredService<IRulesCache>();
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;

            rootFrame.PointerPressed += RootFrame_PointerPressed;
        }

        private async void RootFrame_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            bool isXButton1Pressed = e.GetCurrentPoint(sender as UIElement).Properties.PointerUpdateKind == Windows.UI.Input.PointerUpdateKind.XButton1Pressed;

            if (isXButton1Pressed)
            {
                if (!(((Windows.UI.Xaml.Controls.ContentControl)(Window.Current.Content)).Content is MainPage mainPage))
                    return;

                e.Handled = await mainPage.On_BackRequested();
            }
        }

        /// <summary>
        /// When a user launches your app normally (for example, by tapping the app tile), only the OnLaunched method is called.
        /// Override the OnActivated method to perform any general app initialization that should occur only when the app is not 
        /// launched normally (for example, from another app through the Search contract).
        /// You can determine how the app was activated through the IActivatedEventArgs.Kind property.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnActivated(IActivatedEventArgs args)
        {
            Configure(args);
            base.OnActivated(args);
        }

        private void Configure(IActivatedEventArgs e)
        {
            //ServiceProvider = Task.Run(async () => await Startup.ConfigureServiceProvider()).Result;
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
