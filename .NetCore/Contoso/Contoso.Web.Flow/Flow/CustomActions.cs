using Contoso.Web.Flow.Cache;
using Contoso.Web.Flow.Options;
using Contoso.Web.Flow.ScreenSettings.Navigation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Web.Flow
{
    public class CustomActions : ICustomActions
    {
        public CustomActions(ILogger<CustomActions> logger, IOptions<ApplicationOptions> optionsAccessor, FlowDataCache flowDataCache)
        {
            this.flowDataCache = flowDataCache;
            _logger = logger;
            _applicationOptions = optionsAccessor.Value;
        }

        #region Fields
        private ILogger<CustomActions> _logger;
        private ApplicationOptions _applicationOptions;
        private readonly FlowDataCache flowDataCache;
        #endregion Fields

        public void UpdateNavigationBar(NavigationBar navBar)
        {
            this.flowDataCache.NavigationBar = navBar;
        }
    }
}
