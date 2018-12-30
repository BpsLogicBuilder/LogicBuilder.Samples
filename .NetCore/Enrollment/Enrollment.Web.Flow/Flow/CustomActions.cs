using Enrollment.Web.Flow.Cache;
using Enrollment.Web.Flow.Options;
using Enrollment.Web.Flow.ScreenSettings.Navigation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Web.Flow
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
        private readonly ILogger<CustomActions> _logger;
        private readonly ApplicationOptions _applicationOptions;
        private readonly FlowDataCache flowDataCache;
        #endregion Fields

        public void UpdateNavigationBar(NavigationBar navBar)
        {
            this.flowDataCache.NavigationBar = navBar;
        }
    }
}
