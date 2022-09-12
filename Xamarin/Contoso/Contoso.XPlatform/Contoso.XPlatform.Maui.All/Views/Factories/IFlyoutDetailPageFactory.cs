using Contoso.XPlatform.Flow.Settings.Screen;
using Microsoft.Maui.Controls;

namespace Contoso.XPlatform.Views.Factories
{
    public interface IFlyoutDetailPageFactory
    {
        Page CreatePage(ScreenSettingsBase screenSettings);
    }
}
