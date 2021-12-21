using Enrollment.Forms.Configuration.TextForm;
using Enrollment.XPlatform.Flow.Settings.Screen;
using Enrollment.XPlatform.ViewModels.TextPage;
using System;

namespace Enrollment.XPlatform.ViewModels
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
