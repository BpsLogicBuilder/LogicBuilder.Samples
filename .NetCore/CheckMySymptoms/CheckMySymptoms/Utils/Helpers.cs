using CheckMySymptoms.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace CheckMySymptoms.Utils
{
    public static class Helpers
    {
        public static async void ScrollToElement(this ScrollViewer scrollViewer, UIElement element, bool isVerticalScrolling = true, bool smoothScrolling = true, float? zoomFactor = null)
        {
            //if (element)
            GeneralTransform transform = element.TransformToVisual((UIElement)scrollViewer.Content);
            Point position = transform.TransformPoint(new Point(0, 0));
            //var position = transform.TransformPoint(new Point(0, Math.Floor(scrollViewer.ActualHeight / 2)));
            //var halfScreen = Math.Floor(scrollViewer.ActualHeight / 2);
            if (position.Y < scrollViewer.ActualHeight)
                return;

            await Task.Delay(1000);
            if (isVerticalScrolling)
            {
                scrollViewer.ChangeView(null, position.Y, zoomFactor, !smoothScrolling);
            }
            else
            {
                scrollViewer.ChangeView(position.X, null, zoomFactor, !smoothScrolling);
            }
        }

        public static string GetResourceString(this string id)
        {
            ResourceLoader resourceLoader = ResourceLoader.GetForCurrentView();
            return resourceLoader.GetString(id);
        }

        public static async Task<SpeechSynthesisStream> SynthesizeTextToSpeechAsync(string text, VoiceInformation voiceInformation)
        {
            // Windows.Storage.Streams.IRandomAccessStream
            SpeechSynthesisStream stream = null;

            // Windows.Media.SpeechSynthesis.SpeechSynthesizer
            using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
            {
                ResourceContext speechContext = ResourceContext.GetForCurrentView();
                speechContext.Languages = Helpers.SupportedLanguages.ToArray();

                if (voiceInformation == null)
                    return null;

                text = text.ReplaceUpperCaseWords(voiceInformation.Language);
                text = text.Replace("/", "<break time=\"100ms\"/>");
                text = System.Text.RegularExpressions.Regex.Replace(text, "([A-Z]{2,})", "<say-as interpret-as=\"spell-out\">$1</say-as> <break time=\"50ms\"/>");

                text = $"<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\"{voiceInformation.Language}\">{text}</speak>";

                synthesizer.Voice = voiceInformation;

                // Windows.Media.SpeechSynthesis.SpeechSynthesisStream
                stream = await synthesizer.SynthesizeSsmlToStreamAsync(text);
                //stream = await synthesizer.SynthesizeTextToStreamAsync(text);
            }

            return (stream);
        }

        private static string ReplaceUpperCaseWords(this string text, string language)
        {
            switch (language)
            {//OR GO TO THE EMERGENCY ROOM IMMEDIATELY
                case "en-US":
                    new Dictionary<string, string>
                    {
                        ["PLEASE"] = "Please",
                        ["CONSULT"] = "consult",
                        ["YOUR"] = "your",
                        ["DOCTOR"] = "doctor",
                        ["NOT"] = "not",
                        ["OR"] = "or",
                        ["GO"] = "go",
                        ["TO"] = "to",
                        ["THE"] = "the",
                        ["EMERGENCY"] = "emergency",
                        ["ROOM"] = "room",
                        ["IMMEDIATELY"] = "immediately",
                        ["CANCER"] = "cancer"
                    }.ToList().ForEach(kvp => text = text.Replace(kvp.Key, kvp.Value));
                    return text;
                default:
                    return text;
            }
        }

        public static VoiceInformation GetVoice(string id)
            => SpeechSynthesizer.AllVoices.SingleOrDefault(v => v.Id == id);

        public static async Task Speak(this MediaElement media, string text, VoiceInformation voiceInformation)
        {
            //In some cases DoRecognition ends prematurely e.g. when
            //the user allows access to the microphone but there is no
            //microphone available so if the media is still playing just return.
            if (media.CurrentState == MediaElementState.Playing)
                return;

            SpeechSynthesisStream synthesisStream = await SynthesizeTextToSpeechAsync(text, voiceInformation);
            if (synthesisStream == null)
                return;

            try
            {
                TaskCompletionSource<bool> taskCompleted = new TaskCompletionSource<bool>();

                void endOfPlayHandler(object s, RoutedEventArgs e)
                {
                    synthesisStream.Dispose();
                    taskCompleted.SetResult(true);
                }

                media.MediaEnded += endOfPlayHandler;

                media.AutoPlay = true;
                media.SetSource(synthesisStream, synthesisStream.ContentType);
                //media.Play();

                await taskCompleted.Task;
                media.MediaEnded -= endOfPlayHandler;
            }
            catch (System.Exception)
            {
            }
        }

        public static void StopMedia(this MediaElement media)
        {
            if (media.CurrentState == MediaElementState.Playing)
                media.Stop();

            media.Source = null;
        }

        public static HashSet<string> SupportedLanguages = new HashSet<string> { "en-US" };
        public static IList<VoiceModel> SupportedVoices
        {
            get
            {
                return SpeechSynthesizer.AllVoices
                     .Where(vi => SupportedLanguages.Contains(vi.Language))
                     .Select(v => new VoiceModel(v))
                     .OrderBy(v => v.Language)
                     .ThenBy(v => v.DisplayName)
                     .ToList();
            }
        }

        internal static async System.Threading.Tasks.Task ShowDialog(string message)
        {
            var messageDialog = new Windows.UI.Popups.MessageDialog(message);

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            //messageDialog.Commands.Add(new Windows.UI.Popups.UICommand(
            //    "Try again",
            //    new Windows.UI.Popups.UICommandInvokedHandler(CommandInvokedHandler)));
            messageDialog.Commands.Add(new Windows.UI.Popups.UICommand(
                "Close",
                new Windows.UI.Popups.UICommandInvokedHandler(CommandInvokedHandler)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();
        }

        private static void CommandInvokedHandler(Windows.UI.Popups.IUICommand command)
        {
            //throw new NotImplementedException();
        }
    }
}
