using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckMySymptoms.Utils
{
    public struct ResourceStringNames
    {
        public const string settingNavOnlyMenuItemText = "settingNavOnlyMenuItemText";
        public const string symptomCheckerResetNotificationText = "symptomCheckerResetNotificationText";
        public const string voiceChangedTextFormat = "voiceChangedTextFormat";
        public const string goBackVoiceCommand = "goBackVoiceCommand";
        public const string notificationText01 = "notificationText01";
        public const string notificationText02 = "notificationText02";
        public const string notificationText03 = "notificationText03";
        public const string errorGettingAddOnsFormat = "errorGettingAddOnsFormat";
        public const string subscriptionToPurchaseNotFound = "subscriptionToPurchaseNotFound";
        public const string adFreePurchaseSuccessful = "adFreePurchaseSuccessful";
        public const string addOnNotPurchasedFormat = "addOnNotPurchasedFormat";
        public const string addOnErrorDuringPurchaseFormat = "addOnErrorDuringPurchaseFormat";
        public const string addOnAlreadyPurchasedFormat = "addOnAlreadyPurchasedFormat";
    }

    public struct RoamingData
    {
        public const string SoundPlayerState = "SoundPlayerState";
        public const string SpatialAudioMode = "SpatialAudioMode";
        public const string SelectedVoice = "SelectedVoice";
        public const string VoiceOn = "VoiceOn";
    }

    public struct SpeechRecognitionConstants
    {
        public const string GOBACKTAG = "goBack";
        public const string OPTIONSTAG = "options";
        public const double InitialSilenceTimeout = 12;
        public const double EndSilenceTimeout = 5;
    }
}
