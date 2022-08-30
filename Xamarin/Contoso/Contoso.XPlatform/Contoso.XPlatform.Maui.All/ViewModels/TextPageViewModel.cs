using Contoso.Forms.Configuration.TextForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.ViewModels.TextPage;
using System;

namespace Contoso.XPlatform.ViewModels
{
    public class TextPageViewModel : FlyoutDetailViewModelBase
    {
        public TextPageViewModel(ScreenSettingsBase screenSettings)
        {
            TextPageScreenViewModel = CreateTextPageScreenViewModel((ScreenSettings<TextFormSettingsDescriptor>)screenSettings);
        }

        public TextPageScreenViewModel TextPageScreenViewModel { get; set; }

        private TextPageScreenViewModel CreateTextPageScreenViewModel(ScreenSettings<TextFormSettingsDescriptor> screenSettings) 
            => (TextPageScreenViewModel)(
                Activator.CreateInstance
                (
                    typeof(TextPageScreenViewModel),
                    screenSettings
                ) ?? throw new ArgumentException($"{typeof(TextPageScreenViewModel)}: {{C523F1C5-B187-4685-8B23-3C3C92322004}}")
            );
    }
}
