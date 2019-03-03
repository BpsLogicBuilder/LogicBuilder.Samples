using CheckMySymptoms.Utils;
using CheckMySymptoms.ViewModels;
using CheckMySymptoms.WindowsStore;
using System;
using System.Globalization;
using System.Linq;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CheckMySymptoms.Screens
{
    public sealed partial class Settings : UserControl
    {
        private readonly UiNotificationService uiNotificationService;
        private SettingsViewModel SettingsViewModel { get; set; }

        public Settings(UiNotificationService uiNotificationService)
        {
            this.InitializeComponent();

            if (ElementSoundPlayer.State == ElementSoundPlayerState.On)
                soundToggle.IsOn = true;
            if (ElementSoundPlayer.SpatialAudioMode == ElementSpatialAudioMode.On)
                spatialSoundBox.IsChecked = true;

            this.uiNotificationService = uiNotificationService;
            if (this.uiNotificationService.IsVoiceOn)
                voiceToggle.IsOn = true;

            this.SettingsViewModel = new SettingsViewModel(this.uiNotificationService);
            this.Loaded += Settings_Loaded;
        }

        private void Settings_Loaded(object sender, RoutedEventArgs e)
        {//Must wait until the page loads before setting the selected value otherwise the combo box does not get set on first load
            this.SettingsViewModel.SelectedVoice = this.uiNotificationService.SelectedVoice;
            this.listboxVoiceChooser.SelectionChanged += ListboxVoiceChooser_SelectionChanged;
        }

        public string Version
        {
            get
            {
                var version = Windows.ApplicationModel.Package.Current.Id.Version;
                return String.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
            }
        }

        private void SpatialSoundBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (soundToggle.IsOn == true)
            {
                ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.Off;
                SetRoamingValues(Windows.Storage.ApplicationData.Current.RoamingSettings);
            }
        }

        private void SpatialSoundBox_Checked(object sender, RoutedEventArgs e)
        {
            if (soundToggle.IsOn == true)
            {
                ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.On;
                SetRoamingValues(Windows.Storage.ApplicationData.Current.RoamingSettings);
            }
        }

        private async void FeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("feedback-hub:"));
        }

        private void SoundToggle_Toggled(object sender, RoutedEventArgs e)
        {
            if (soundToggle.IsOn == true)
            {
                spatialSoundBox.IsEnabled = true;
                ElementSoundPlayer.State = ElementSoundPlayerState.On;
            }
            else
            {
                spatialSoundBox.IsEnabled = false;
                spatialSoundBox.IsChecked = false;

                ElementSoundPlayer.State = ElementSoundPlayerState.Off;
                ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.Off;
            }

            SetRoamingValues(Windows.Storage.ApplicationData.Current.RoamingSettings);
        }

        private void SetRoamingValues(Windows.Storage.ApplicationDataContainer roamingSettings)
        {
            roamingSettings.Values[RoamingData.SoundPlayerState] = (int)ElementSoundPlayer.State;
            roamingSettings.Values[RoamingData.SpatialAudioMode] = (int)ElementSoundPlayer.SpatialAudioMode;
        }

        private async void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            await uiNotificationService.DeleteFlowState();
            await uiNotificationService.DeletePatientData();
            txtReset.Text = ResourceStringNames.symptomCheckerResetNotificationText.GetResourceString();
        }

        private async void BtnContinue_Click(object sender, RoutedEventArgs e)
        {
            await uiNotificationService.Start();
        }

        private void ListboxVoiceChooser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtVoice.Text = String.Format(CultureInfo.CurrentCulture, ResourceStringNames.voiceChangedTextFormat.GetResourceString(), this.SettingsViewModel.Voices.Single(v => v.Id == (string)listboxVoiceChooser.SelectedValue).DisplayName);
        }

        private void Voice_Toggled(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            roamingSettings.Values[RoamingData.VoiceOn] = voiceToggle.IsOn;
            uiNotificationService.IsVoiceOn = voiceToggle.IsOn;
        }

        private async void BtnGetAdFree_Click(object sender, RoutedEventArgs e)
        {
            await StoreAccessHelper.Instance.SetupAdFreeSubscriptionInfoAsync();
            uiNotificationService.HasAdFree = await StoreAccessHelper.Instance.CheckIfUserHasAdFreeSubscriptionAsync();
            uiNotificationService.NotifyAdFreeChanged(uiNotificationService.HasAdFree);
        }
    }
}
