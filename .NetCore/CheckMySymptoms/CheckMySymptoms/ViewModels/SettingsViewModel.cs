using CheckMySymptoms.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckMySymptoms.ViewModels
{
    public class SettingsViewModel : ObservableObject
    {
        public SettingsViewModel(UiNotificationService uiNotificationService)
        {
            this.uiNotificationService = uiNotificationService;
            this.Voices = new ObservableCollection<VoiceModel>(Helpers.SupportedVoices);

        }

        private string _selectedVoice;
        private readonly UiNotificationService uiNotificationService;

        public string SelectedVoice
        {
            get { return _selectedVoice; }
            set
            {
                Set(ref _selectedVoice, value);
                SetRoamingValues(Windows.Storage.ApplicationData.Current.RoamingSettings);
                uiNotificationService.SelectedVoice = _selectedVoice;
            }
        }


        public ObservableCollection<VoiceModel> Voices { get; set; }

        private void SetRoamingValues(Windows.Storage.ApplicationDataContainer roamingSettings)
        {
            roamingSettings.Values[RoamingData.SelectedVoice] = SelectedVoice;
        }
    }
}
