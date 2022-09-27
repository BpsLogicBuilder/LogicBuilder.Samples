using Contoso.XPlatform.Tests.Helpers;
using Contoso.XPlatform.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Contoso.XPlatform.Tests
{
    public class ScopedFlowManagerServiceTest
    {
        public ScopedFlowManagerServiceTest()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void DataFromTheSameScopeMustMatch()
        {
            IScopedFlowManagerService scopedFlowManagerService1 = serviceProvider.GetRequiredService<IScopedFlowManagerService>();

            scopedFlowManagerService1.SetFlowDataCacheItem("A", new List<string> { "First", "Second" });
            object? aList = scopedFlowManagerService1.GetFlowDataCacheItem("A");

            Assert.Equal("Second", ((List<string>)aList!)[1]);
        }

        [Fact]
        public void DataFromDifferentScopesCanBeDifferent()
        {
            IScopedFlowManagerService scopedFlowManagerService1 = serviceProvider.GetRequiredService<IScopedFlowManagerService>();
            IScopedFlowManagerService scopedFlowManagerService2 = serviceProvider.GetRequiredService<IScopedFlowManagerService>();

            scopedFlowManagerService1.SetFlowDataCacheItem("A", new List<string> { "First", "Second" });

            Assert.Empty(scopedFlowManagerService2.FlowManager.FlowDataCache.Items);
            Assert.Equal("Second", ((List<string>)scopedFlowManagerService1.FlowManager.FlowDataCache.Items["A"])[1]);
        }
    }
}
