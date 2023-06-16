using Contoso.Spa.Flow.Cache;
using Contoso.Spa.Flow.ScreenSettings.Navigation;

namespace Contoso.Spa.Flow
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
