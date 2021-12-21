using AutoMapper;
using Contoso.Forms.Configuration.Navigation;
using Contoso.Forms.Parameters.Navigation;
using Contoso.XPlatform.Flow.Cache;

namespace Contoso.XPlatform.Flow
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
