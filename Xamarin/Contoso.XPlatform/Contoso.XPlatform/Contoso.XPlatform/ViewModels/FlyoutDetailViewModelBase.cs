using Contoso.XPlatform.Flow.Settings.Screen;

namespace Contoso.XPlatform.ViewModels
{
    public abstract class FlyoutDetailViewModelBase : ViewModelBase
    {
        public abstract void Initialize(ScreenSettingsBase screenSettings);
    }
}
