using CheckMySymptoms.Flow.Requests;
using CheckMySymptoms.Flow.ScreenSettings.Views;
using CheckMySymptoms.Forms.View;
using CheckMySymptoms.Forms.View.Input;
using CheckMySymptoms.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Windows.Media.SpeechRecognition;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CheckMySymptoms.Screens
{
    public sealed partial class InputForm : UserControl
    {
        private ScreenSettings<InputFormView> ScreenSettings { get; set; }
        private UiNotificationService UiNotificationService { get; set; }
        private ObservableCollection<CommandButtonView> Buttons { get; set; }
        private Dictionary<string, object> RecognitionResults { get; set; }
        private double TransitionOffset { get; set; }
        private bool exited;
        private VoiceInformation voiceInformation;

        public InputForm()
        {
            this.InitializeComponent();
        }

        public InputForm(ScreenSettings<InputFormView> screenSettings, UiNotificationService uiNitificationService, double transitionOffset)
        {
            this.InitializeComponent();
            this.TransitionOffset = transitionOffset;
            this.ScreenSettings = screenSettings;
            Buttons = new ObservableCollection<CommandButtonView>(this.ScreenSettings.CommandButtons);
            this.RecognitionResults = GetRecognitionResults(this.ScreenSettings.Settings.GetAllQuestions().First());
            this.UiNotificationService = uiNitificationService;
            this.voiceInformation = Helpers.GetVoice(this.UiNotificationService.SelectedVoice);
            this.UiNotificationService.ExitCalledFromMain.Subscribe(ExitCalled);
            this.Loaded += InputForm_Loaded;
        }

        private Dictionary<string, object> GetRecognitionResults(BaseInputView inputView)
        {
            Type itemType = inputView.ItemsSource.GetType().GenericTypeArguments[0];
            return inputView.ItemsSource
                            .Select(o =>
                            {
                                return new
                                {
                                    val = itemType.GetProperty(inputView.DropDownTemplate.ValueField).GetValue(o),
                                    text = (string)itemType.GetProperty(inputView.DropDownTemplate.TextField).GetValue(o)
                                };
                            }).ToDictionary(k => k.text, v => v.val);
        }

        private async void InputForm_Loaded(object sender, RoutedEventArgs e)
        {
            await SpeakAndListen();
        }

        private async Task SpeakAndListen()
        {
            //Form can be complete by calling previous, button click or voice recognition
            //so we need a global flag to exit the recursive SpeakAndListen().
            if (exited)
                return;

            string GetText()
                => this.ScreenSettings.Settings.GetAllQuestions().First().Text;

            if (!UiNotificationService.IsVoiceOn)
                return;

            if (this.voiceInformation == null)
                return;

            Task speak = this.media.Speak(GetText(), voiceInformation);
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

            BaseInputView inputView = this.ScreenSettings.Settings.GetAllQuestions().First();
            PropertyInfo prop = inputView.GetType().GetProperty("CurrentValue", BindingFlags.Public | BindingFlags.Instance);
            if (null != prop && prop.CanWrite)
            {
                prop.SetValue(inputView, Convert.ChangeType(this.RecognitionResults[result.Text], inputView.Type), null);
            }
            else
            {
                return;
            }

            await DoNext(Buttons[0].ShortString, Buttons[0].Cancel);
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
                new SpeechRecognitionListConstraint(this.RecognitionResults.Select(b => b.Key), SpeechRecognitionConstants.OPTIONSTAG) { Probability = SpeechRecognitionConstraintProbability.Default },
                new SpeechRecognitionListConstraint(new string[] { ResourceStringNames.goBackVoiceCommand.GetResourceString() }, SpeechRecognitionConstants.GOBACKTAG) { Probability = SpeechRecognitionConstraintProbability.Min}
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

        private void ExitCalled(bool selected)
        {
            this.exited = true;
            media.StopMedia();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            CommandButtonView button = (CommandButtonView)((Button)sender).Tag;
            await DoNext(button.ShortString, button.Cancel);
        }

        private async Task DoNext(string shortText, bool cancel)
        {
            if (exited)//DoNext may be called by the voice AND a click event
                return;

            this.exited = true;
            media.StopMedia();

            await UiNotificationService.Next
            (
                new InputFormRequest
                {
                    Form = ScreenSettings.Settings,
                    CommandButtonRequest = new CommandButtonRequest { NewSelection = shortText, Cancel = cancel },
                    ViewType = ViewType.InputForm
                }
            );
        }
    }
}
