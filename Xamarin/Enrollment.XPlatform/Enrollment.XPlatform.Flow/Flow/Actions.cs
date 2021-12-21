using AutoMapper;
using Enrollment.Forms.Configuration.Navigation;
using Enrollment.Forms.Parameters.Navigation;
using Enrollment.XPlatform.Flow.Cache;

namespace Enrollment.XPlatform.Flow
{
    public class Actions : IActions
    {
        #region Fields
        private readonly FlowDataCache flowDataCache;
        private readonly IMapper mapper;

        public Actions(FlowDataCache flowDataCache, IMapper mapper)
        {
            this.flowDataCache = flowDataCache;
            this.mapper = mapper;
        }
        #endregion Fields

        public void UpdateNavigationBar(NavigationBarParameters navBar)
        {
            this.flowDataCache.NavigationBar = mapper.Map<NavigationBarDescriptor>(navBar);
        }
    }
}
