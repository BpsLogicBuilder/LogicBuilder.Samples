using AutoMapper;
using Contoso.XPlatform.Flow;
using Contoso.XPlatform.Flow.Cache;
using Contoso.XPlatform.Tests.Mocks;
using LogicBuilder.RulesDirector;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace Contoso.XPlatform.Tests
{
    public class FlowManagerTest
    {
        public FlowManagerTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
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
            IFlowManager flowManager2 = null;
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
            IFlowManager flowManager2 = null;
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

        static MapperConfiguration MapperConfiguration;
        private void Initialize()
        {
            if (MapperConfiguration == null)
            {
                MapperConfiguration = new MapperConfiguration(cfg =>
                {
                });
            }
            MapperConfiguration.AssertConfigurationIsValid();
            serviceProvider = new ServiceCollection()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    MapperConfiguration
                )
                .AddTransient<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .AddScoped<IFlowManager, FlowManager>()
                .AddScoped<FlowActivityFactory, FlowActivityFactory>()
                .AddScoped<DirectorFactory, DirectorFactory>()
                .AddScoped<FlowDataCache, FlowDataCache>()
                .AddScoped<ScreenData, ScreenData>()
                .AddScoped<IAppLogger, AppLoggerMock>()
                .AddScoped<IRulesCache, RulesCacheMock>()
                .AddScoped<IDialogFunctions, DialogFunctions>()
                .AddScoped<IActions, Actions>()
                .BuildServiceProvider();
        }
    }
}
