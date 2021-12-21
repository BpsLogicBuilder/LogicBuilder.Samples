using Contoso.Forms.Configuration.TextForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.ViewModels.TextPage;
using System;

namespace Contoso.XPlatform.ViewModels
{
    public class TextPageViewModel : FlyoutDetailViewModelBase
    {
        private readonly UiNotificationService uiNotificationService;

        public TextPageViewModel(UiNotificationService uiNotificationService)
        {
            this.uiNotificationService = uiNotificationService;
        }

        public TextPageScreenViewModel TextPageScreenViewModel { get; set; }

        public override void Initialize(ScreenSettingsBase screenSettings)
        {
            TextPageScreenViewModel = CreateTextPageScreenViewModel((ScreenSettings<TextFormSettingsDescriptor>)screenSettings);
        }

        private TextPageScreenViewModel CreateTextPageScreenViewModel(ScreenSettings<TextFormSettingsDescriptor> screenSettings) 
            => (TextPageScreenViewModel)Activator.CreateInstance
            (
                typeof(TextPageScreenViewModel),
                screenSettings,
                uiNotificationService
            );
    }
}
