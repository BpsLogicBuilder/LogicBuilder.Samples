using Contoso.Web.Flow.ScreenSettings.Navigation;
using Contoso.Web.Flow.ScreenSettings.Views;

namespace Contoso.Web.Flow.ScreenSettings
{
    public class FlowSettings
    {
        public FlowSettings(ScreenSettingsBase screenSettings)
        {
            ScreenSettings = screenSettings;
        }

        public FlowSettings(FlowState flowState, NavigationBar navigationBar, ScreenSettingsBase screenSettings)
        {
            FlowState = flowState;
            NavigationBar = navigationBar;
            ScreenSettings = screenSettings;
        }

        public FlowState FlowState { get; set; }
        public NavigationBar NavigationBar { get; set; }
        public ScreenSettingsBase ScreenSettings { get; set; }
    }
}
