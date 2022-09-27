using Enrollment.XPlatform.Flow.Settings.Screen;
using Xamarin.Forms;

namespace Enrollment.XPlatform.Views.Factories
{
    public interface IFlyoutDetailPageFactory
    {
        Page CreatePage(ScreenSettingsBase screenSettings);
    }
}
