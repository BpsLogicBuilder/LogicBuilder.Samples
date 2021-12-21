using AutoMapper;
using Contoso.XPlatform.Flow;
using Contoso.XPlatform.Flow.Cache;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Tests.Mocks;
using LogicBuilder.RulesDirector;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace Contoso.XPlatform.Tests
{
    public class ScopedFlowManagerServiceTest
    {
        public ScopedFlowManagerServiceTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void DataFromTheSameScopeMustMatch()
        {
            IScopedFlowManagerService scopedFlowManagerService1 = serviceProvider.GetRequiredService<IScopedFlowManagerService>();

            scopedFlowManagerService1.SetFlowDataCacheItem("A", new List<string> { "First", "Second" });
            object aList = scopedFlowManagerService1.GetFlowDataCacheItem("A");

            Assert.Equal("Second", ((List<string>)aList)[1]);
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
                .AddSingleton<UiNotificationService, UiNotificationService>()
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
                .AddTransient<IScopedFlowManagerService, ScopedFlowManagerService>()
                .BuildServiceProvider();
        }
    }
}
