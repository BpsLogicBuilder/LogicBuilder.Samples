using Contoso.Forms.Configuration.TextForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.ViewModels.TextPage;
using System;

namespace Contoso.XPlatform.ViewModels
{
    public class TextPageViewModel : FlyoutDetailViewModelBase
    {
        private readonly UiNotificationService uiNotificationService;

        public TextPageViewModel(UiNotificationService uiNotificationService, ScreenSettingsBase screenSettings)
        {
            this.uiNotificationService = uiNotificationService;
            TextPageScreenViewModel = CreateTextPageScreenViewModel((ScreenSettings<TextFormSettingsDescriptor>)screenSettings);
        }

        public TextPageScreenViewModel TextPageScreenViewModel { get; set; }

        private TextPageScreenViewModel CreateTextPageScreenViewModel(ScreenSettings<TextFormSettingsDescriptor> screenSettings) 
            => (TextPageScreenViewModel)(
                Activator.CreateInstance
                (
                    typeof(TextPageScreenViewModel),
                    screenSettings,
                    uiNotificationService
                ) ?? throw new ArgumentException($"{typeof(TextPageScreenViewModel)}: {{C523F1C5-B187-4685-8B23-3C3C92322004}}")
            );
    }
}
