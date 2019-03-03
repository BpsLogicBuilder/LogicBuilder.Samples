using Windows.Media.SpeechSynthesis;

namespace CheckMySymptoms.ViewModels
{
    public class VoiceModel
    {
        public VoiceModel(VoiceInformation vi)
        {
            DisplayName = vi.DisplayName;
            Id = vi.Id;
            Language = vi.Language;
        }
        public VoiceModel()
        {

        }
        public string DisplayName { get; }
        public string Id { get; }
        public string Language { get; }
    }
}
