using Enrollment.XPlatform.Flow.Settings.Screen;
using Microsoft.Maui.Controls;

namespace Enrollment.XPlatform.Views.Factories
{
    public interface IFlyoutDetailPageFactory
    {
        Page CreatePage(ScreenSettingsBase screenSettings);
    }
}
