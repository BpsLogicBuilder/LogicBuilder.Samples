using CheckMySymptoms.Flow.Requests;
using CheckMySymptoms.Flow.ScreenSettings.Views;
using CheckMySymptoms.Forms.View;
using CheckMySymptoms.Forms.View.Common;
using CheckMySymptoms.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media.SpeechRecognition;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CheckMySymptoms.Screens
{
    public sealed partial class SelectButton : UserControl
    {
        private readonly ScrollViewer svDialog;

        private ScreenSettings<MessageTemplateView> ScreenSettings { get; set; }
        private UiNotificationService UiNotificationService { get; set; }
        private ObservableCollection<CommandButtonView> Buttons { get; set; }
        private Dictionary<string, CommandButtonView> RecognitionResults { get; set; }
        private double TransitionOffset { get; set; }
        private bool exited;
        private VoiceInformation voiceInformation;

        public SelectButton(ScreenSettings<MessageTemplateView> screenSettings, UiNotificationService uiNitificationService, double transitionOffset, ScrollViewer svDialog)
        {
            this.InitializeComponent();
            this.TransitionOffset = transitionOffset;
            this.svDialog = svDialog;
            SetSelectedButton(screenSettings);
            this.ScreenSettings = screenSettings;
            this.Buttons = new ObservableCollection<CommandButtonView>(this.ScreenSettings.CommandButtons);
            this.RecognitionResults = this.Buttons.ToDictionary(b => b.LongString);
            this.UiNotificationService = uiNitificationService;
            this.voiceInformation = Helpers.GetVoice(this.UiNotificationService.SelectedVoice);
            this.UiNotificationService.ExitCalledFromMain.Subscribe(ExitCalled);
            this.Loaded += SelectButton_Loaded;
        }

        private void ExitCalled(bool selected)
        {
            this.exited = true;
            media.StopMedia();
        }

        private void SetSelectedButton(ScreenSettings<MessageTemplateView> screenSettings)
        {
            if (string.IsNullOrEmpty(screenSettings.Settings.Selection))
                return;

            foreach (CommandButtonView button in screenSettings.CommandButtons)
                button.IsSelected = button.ShortString == screenSettings.Settings.Selection;
        }

        private MessageTemplateView GetMessageTemplateView(string shortString, MessageTemplateView view)
        {
            view.Selection = shortString;
            return view;
        }
#if (DEBUG)
        private async void FocusSelectedButton()
#else
        private void FocusSelectedButton()
#endif
        {
            if (string.IsNullOrEmpty(this.ScreenSettings.Settings.Selection))
                return;

            int index = GetIndex();

            if (index == -1)
            {
#if (DEBUG)
                await Helpers.ShowDialog(this.ScreenSettings.Settings.Selection);
#endif           
                return;
            }

            Button selectedButton = FindVisualChildren<Button>(buttonsList.ContainerFromIndex(index)).First();
            selectedButton.Focus(FocusState.Keyboard);
            this.svDialog.ScrollToElement(selectedButton);

            int GetIndex()
            {
                for (int i = 0; i < buttonsList.Items.Count; i++)
                {
                    if (((CommandButtonView)buttonsList.Items[i]).IsSelected)
                        return i;
                }

                return -1;
            }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            CommandButtonView button = (CommandButtonView)((Button)sender).Tag;
            await DoNext(button.ShortString, button.SymptomText, button.AddToSymptions, button.Cancel);
        }

        private async Task DoNext(string shortText, string symptomText, bool addToSymptoms, bool cancel)
        {
            if (exited)//DoNext may be called by the voice AND a click event
                return;

            this.exited = true;
            media.StopMedia();

            await UiNotificationService.Next
            (
                new SelectRequest
                {
                    AddToSymptoms = addToSymptoms,
                    MessageTemplateView = GetMessageTemplateView(shortText, ScreenSettings.Settings),
                    CommandButtonRequest = new CommandButtonRequest { NewSelection = shortText, SymtomText = symptomText, Cancel = cancel },
                    ViewType = ViewType.Select
                }
            );
        }

        private async void SelectButton_Loaded(object sender, RoutedEventArgs e)
        {
            FocusSelectedButton();
            await SpeakAndListen();
        }

        private async Task SpeakAndListen()
        {
            //Form can be complete by calling previous, button click or voice recognition
            //so we need a global flag to exit the recursive SpeakAndListen().
            if (exited)
                return;

            if (!UiNotificationService.IsVoiceOn)
                return;

            if (this.voiceInformation == null)
                return;

            Task speak = this.media.Speak(this.ScreenSettings.Settings.Message, voiceInformation);
            Task listen = RecordSpeechFromMicrophoneAsync(voiceInformation, DoNext);
            await Task.WhenAll(speak, listen);
        }

        async Task DoNext(SpeechRecognitionResult result)
        {
            if (!this.RecognitionResults.ContainsKey(result.Text))
            {
#if (DEBUG)
                await Helpers.ShowDialog(result.Text + " " + Enum.GetName(typeof(SpeechRecognitionConfidence), result.Confidence));
#endif
                return;
            }

            CommandButtonView button = this.RecognitionResults[result.Text];
            await DoNext(button.ShortString, button.SymptomText, button.AddToSymptions, button.Cancel);
        }

        async Task GoBack()
        {
            if (exited)//GoBack may be called by the voice AND a click event
                return;

            this.exited = true;
            media.StopMedia();
            await UiNotificationService.GoBack();
        }

        private ICollection<ISpeechRecognitionConstraint> SpeechRecognitionConstraints
            => new List<ISpeechRecognitionConstraint>
            {
                new SpeechRecognitionListConstraint(this.Buttons.Select(b => b.LongString), SpeechRecognitionConstants.OPTIONSTAG) { Probability = SpeechRecognitionConstraintProbability.Default },
                new SpeechRecognitionListConstraint(new string[] { ResourceStringNames.goBackVoiceCommand.GetResourceString()  }, SpeechRecognitionConstants.GOBACKTAG) { Probability = SpeechRecognitionConstraintProbability.Min}
            };

        async Task RecordSpeechFromMicrophoneAsync(VoiceInformation voiceInformation, Func<SpeechRecognitionResult, Task> doNext)
        {
            if (!await AudioCapturePermissions.RequestMicrophonePermission())
                return;

            if (voiceInformation == null)
                return;

            if (!await DoRecognition())
            {
                //media.StopMedia();
                //In some cases DoRecognition ends prematurely e.g. when
                //the user allows access to the microphone but there is no
                //microphone available so do not stop media.
                await SpeakAndListen();
            }

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
                            !(result.Status == SpeechRecognitionResultStatus.Success
                                && new HashSet<SpeechRecognitionConfidence>
                                {
                                    SpeechRecognitionConfidence.High,
                                    SpeechRecognitionConfidence.Medium,
                                    SpeechRecognitionConfidence.Low
                                }.Contains(result.Confidence)
                            )
                        )
                    {
                        return false;
                    }

                    if (result.Constraint.Tag == SpeechRecognitionConstants.GOBACKTAG)
                    {
                        if (UiNotificationService.CanGoBack)
                        {
                            await GoBack();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {//Options constraint succeeded
                        await doNext(result);
                        return true;
                    }
                }
            }
        }
    }
}
