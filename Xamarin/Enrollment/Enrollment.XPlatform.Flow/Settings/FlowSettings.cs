using Enrollment.Forms.Configuration.Navigation;
using Enrollment.XPlatform.Flow.Cache;
using Enrollment.XPlatform.Flow.Settings.Screen;

namespace Enrollment.XPlatform.Flow.Settings
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
