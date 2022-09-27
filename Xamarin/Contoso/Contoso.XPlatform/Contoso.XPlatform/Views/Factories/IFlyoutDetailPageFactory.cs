using Contoso.XPlatform.Flow.Settings.Screen;
using Xamarin.Forms;

namespace Contoso.XPlatform.Views.Factories
{
    public interface IFlyoutDetailPageFactory
    {
        Page CreatePage(ScreenSettingsBase screenSettings);
    }
}
