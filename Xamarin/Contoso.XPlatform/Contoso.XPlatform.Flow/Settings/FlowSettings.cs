using Contoso.Forms.Configuration.Navigation;
using Contoso.XPlatform.Flow.Cache;
using Contoso.XPlatform.Flow.Settings.Screen;

namespace Contoso.XPlatform.Flow.Settings
{
    public class FlowSettings
    {
        public FlowSettings(FlowState flowState, FlowDataCache flowDataCache, ScreenSettingsBase screenSettings)
        {
            FlowState = flowState;
            FlowDataCache = flowDataCache;
            ScreenSettings = screenSettings;
        }

        public FlowState FlowState { get; set; }
        public FlowDataCache FlowDataCache { get; set; }
        public ScreenSettingsBase ScreenSettings { get; set; }
    }
}
