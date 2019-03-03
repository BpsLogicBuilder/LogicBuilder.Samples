using CheckMySymptoms.Flow.ScreenSettings.Views;
using CheckMySymptoms.Forms.View.Common;
using CheckMySymptoms.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media.SpeechRecognition;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CheckMySymptoms.Screens
{
    public sealed partial class FlowComplete : UserControl
    {
        private readonly ScreenSettings<FlowCompleteView> screenSettings;
        private readonly UiNotificationService uiNotificationService;

        public bool SymptomsVisible => this.screenSettings.Settings.Symptoms.Count > 0;
        public bool TreatmentsVisible => this.screenSettings.Settings.Treatments.Count > 0;
        public bool DiagnosesVisible => this.screenSettings.Settings.Diagnoses.Count > 0;
        private double TransitionOffset { get; set; }
        private bool exited;
        private VoiceInformation voiceInformation;

        public FlowComplete(ScreenSettings<FlowCompleteView> screenSettings, UiNotificationService uiNotificationService, double transitionOffset)
        {
            this.InitializeComponent();
            this.screenSettings = screenSettings;
            this.uiNotificationService = uiNotificationService;
            this.voiceInformation = Helpers.GetVoice(this.uiNotificationService.SelectedVoice);
            this.uiNotificationService.ExitCalledFromMain.Subscribe(ExitCalled);
            this.TransitionOffset = transitionOffset;
            this.Loaded += FlowComplete_Loaded;
        }

        private void ExitCalled(bool selected)
        {
            this.exited = true;
            media.StopMedia();
        }

        private async void FlowComplete_Loaded(object sender, RoutedEventArgs e)
        {
            await SpeakAndListen();
        }

        private async Task SpeakAndListen(bool listenOnly = false)
        {
            //Form can be complete by calling previous, button click or voice recognition
            //so we need a global flag to exit the recursive SpeakAndListen().
            if (exited)
                return;

            string GetText()
            {
                List<string> list = new List<string>();
                list.AddRange(this.screenSettings.Settings.Diagnoses.Select(d => string.Concat(" ", d)));
                list.AddRange(this.screenSettings.Settings.Treatments.Select(d => string.Concat(" ", d)));

                return string.Join(" ", list);
            }

            if (!uiNotificationService.IsVoiceOn)
                return;

            if (this.voiceInformation == null)
                return;

            if (!listenOnly)
            {
                Task speak = this.media.Speak(GetText(), voiceInformation);
                Task listen = RecordSpeechFromMicrophoneAsync(voiceInformation);
                await Task.WhenAll(speak, listen);
            }
            else
            {
                await RecordSpeechFromMicrophoneAsync(voiceInformation);
            }
        }

        async Task GoBack()
        {
            if (exited)//GoBack may be called by the voice AND a click event
                return;

            this.exited = true;
            media.StopMedia();
            await uiNotificationService.GoBack();
        }

        private ICollection<ISpeechRecognitionConstraint> SpeechRecognitionConstraints
            => new List<ISpeechRecognitionConstraint>
            {
                new SpeechRecognitionListConstraint(new string[] { ResourceStringNames.goBackVoiceCommand.GetResourceString() }, SpeechRecognitionConstants.GOBACKTAG) { Probability = SpeechRecognitionConstraintProbability.Default}
            };

        async Task RecordSpeechFromMicrophoneAsync(VoiceInformation voiceInformation)
        {
            if (!await AudioCapturePermissions.RequestMicrophonePermission())
                return;

            if (voiceInformation == null)
                return;

            if (!await DoRecognition())
                await SpeakAndListen(listenOnly: true);

            async Task<bool> DoRecognition()
            {
                using (SpeechRecognizer speechRecognizer = new SpeechRecognizer(new Windows.Globalization.Language(voiceInformation.Language)))
                {
                    SpeechRecognitionConstraints.ToList().ForEach(c => speechRecognizer.Constraints.Add(c));

                    speechRecognizer.Timeouts.InitialSilenceTimeout = TimeSpan.FromSeconds(SpeechRecognitionConstants.InitialSilenceTimeout);
                    speechRecognizer.Timeouts.EndSilenceTimeout = TimeSpan.FromSeconds(SpeechRecognitionConstants.EndSilenceTimeout);

                    await speechRecognizer.CompileConstraintsAsync();

                    SpeechRecognitionResult result = await speechRecognizer.RecognizeAsync();

                    if (
                        result.Status == SpeechRecognitionResultStatus.Success
                            && new HashSet<SpeechRecognitionConfidence>
                            {
                                SpeechRecognitionConfidence.High,
                                SpeechRecognitionConfidence.Medium,
                                SpeechRecognitionConfidence.Low
                            }.Contains(result.Confidence)
                            && uiNotificationService.CanGoBack
                        )
                    {
                        await GoBack();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
