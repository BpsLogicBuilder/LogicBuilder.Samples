using Enrollment.Spa.Flow.Cache;
using Enrollment.Spa.Flow.ScreenSettings.Navigation;

namespace Enrollment.Spa.Flow
{
    public class CustomActions : ICustomActions
    {
        private readonly FlowDataCache flowDataCache;

        public CustomActions(FlowDataCache flowDataCache)
        {
            this.flowDataCache = flowDataCache;
        }

        public void UpdateNavigationBar(NavigationBar navBar)
        {
            this.flowDataCache.NavigationBar = navBar;
        }
    }
}
