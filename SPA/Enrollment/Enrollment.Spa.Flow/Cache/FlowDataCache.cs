using Enrollment.Spa.Flow.ScreenSettings.Navigation;
using Enrollment.Spa.Flow.ScreenSettings.Views;
using System.Collections.Generic;

namespace Enrollment.Spa.Flow.Cache
{
    public class FlowDataCache
    {
        public RequestedFlowStage RequestedFlowStage { get; set; } = new RequestedFlowStage();
        public NavigationBar NavigationBar { get; set; } = new NavigationBar();
        public ScreenSettingsBase? ScreenSettings { get; set; }
        public Dictionary<string, object> Items { get; set; } = new Dictionary<string, object>();
    }
}
