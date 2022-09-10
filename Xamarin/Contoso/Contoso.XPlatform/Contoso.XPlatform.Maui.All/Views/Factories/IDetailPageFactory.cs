using Contoso.XPlatform.Flow.Settings.Screen;
using Microsoft.Maui.Controls;

namespace Contoso.XPlatform.Views.Factories
{
    public interface IDetailPageFactory
    {
        Page CreatePage(ScreenSettingsBase screenSettings);
    }
}
