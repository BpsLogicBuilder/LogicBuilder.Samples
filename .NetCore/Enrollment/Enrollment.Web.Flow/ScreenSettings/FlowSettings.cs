using Enrollment.Web.Flow.ScreenSettings.Navigation;
using Enrollment.Web.Flow.ScreenSettings.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Web.Flow.ScreenSettings
{
    public class FlowSettings
    {
        public FlowSettings(ScreenSettingsBase screenSettings)
        {
            ScreenSettings = screenSettings;
        }

        public FlowSettings(int userId, FlowState flowState, NavigationBar navigationBar, ScreenSettingsBase screenSettings)
        {
            UserId = userId;
            FlowState = flowState;
            NavigationBar = navigationBar;
            ScreenSettings = screenSettings;
        }

        public int UserId { get; set; }
        public FlowState FlowState { get; set; }
        public NavigationBar NavigationBar { get; set; }
        public ScreenSettingsBase ScreenSettings { get; set; }
    }
}
