using Contoso.XPlatform.Flow;
using Contoso.XPlatform.Maui.Tests.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Contoso.XPlatform.Maui.Tests
{
    public class FlowManagerTest
    {
        public FlowManagerTest()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void GetServiceScopeFactory()
        {
            IServiceScopeFactory serviceScopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
            Assert.NotNull(serviceScopeFactory);
        }

        [Fact]
        public void DataFromTheSameScopeMustMatch()
        {
            IFlowManager? flowManager2 = null;
            IServiceScopeFactory serviceScopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
            using (IServiceScope scope = serviceScopeFactory.CreateScope())
            {
                IFlowManager flowManager1 = scope.ServiceProvider.GetRequiredService<IFlowManager>();
                flowManager2 = scope.ServiceProvider.GetRequiredService<IFlowManager>();

                flowManager1.FlowDataCache.Items["A"] = new List<string> { "First", "Second" };
            }

            Assert.Equal("Second", ((List<string>)flowManager2.FlowDataCache.Items["A"])[1]);
        }

        [Fact]
        public void DataFromDifferentScopesCanBeDifferent()
        {
            IFlowManager? flowManager2 = null;
            IServiceScopeFactory serviceScopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
            using (IServiceScope scope = serviceScopeFactory.CreateScope())
            {
                IFlowManager flowManager1 = scope.ServiceProvider.GetRequiredService<IFlowManager>();
                flowManager1.FlowDataCache.Items["A"] = new List<string> { "First", "Second" };
            }

            using (IServiceScope scope = serviceScopeFactory.CreateScope())
            {
                flowManager2 = scope.ServiceProvider.GetRequiredService<IFlowManager>();
            }

            Assert.Empty(flowManager2.FlowDataCache.Items);
        }

        [Fact]
        public void CreatingScopedServicesWithoutCreatingAScopeDefaultsToSingleTon()
        {
            IFlowManager flowManager1 = serviceProvider.GetRequiredService<IFlowManager>();
            IFlowManager flowManager2 = serviceProvider.GetRequiredService<IFlowManager>();

            flowManager1.FlowDataCache.Items["A"] = new List<string> { "First", "Second" };

            Assert.NotEmpty(flowManager2.FlowDataCache.Items);
        }
    }
}
