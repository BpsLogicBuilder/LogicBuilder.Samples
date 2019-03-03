using CheckMySymptoms.Flow.Cache;
using CheckMySymptoms.Flow.ScreenSettings.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMySymptoms.Flow.ScreenSettings
{
    public class FlowSettings
    {
        public FlowSettings(ScreenSettingsBase screenSettings)
        {
            ScreenSettings = screenSettings;
        }

        public FlowSettings(FlowDataCache flowDataCache, ScreenSettingsBase screenSettings)
        {
            FlowDataCache = flowDataCache;
            ScreenSettings = screenSettings;
        }

        public FlowDataCache FlowDataCache { get; set; }
        public ScreenSettingsBase ScreenSettings { get; set; }
        public NavigationType NavigationType { get; set; }
        public FlowState FlowState { get; set; }
    }
}
