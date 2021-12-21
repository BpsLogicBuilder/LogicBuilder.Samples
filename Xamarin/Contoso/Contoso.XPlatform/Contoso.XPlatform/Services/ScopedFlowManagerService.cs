using Contoso.Forms.Configuration.Navigation;
using Contoso.XPlatform.Flow;
using Contoso.XPlatform.Flow.Requests;
using Contoso.XPlatform.Flow.Settings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.XPlatform.Services
{
    public class ScopedFlowManagerService : IScopedFlowManagerService
    {
        private readonly IServiceScope scope;

        public ScopedFlowManagerService(IServiceScopeFactory serviceScopeFactory, IAppLogger appLogger, UiNotificationService uiNotificationService)
        {
            this.appLogger = appLogger;
            this.uiNotificationService = uiNotificationService;
            scope = serviceScopeFactory.CreateScope();
            FlowManager = scope.ServiceProvider.GetRequiredService<IFlowManager>();
        }

        private readonly IAppLogger appLogger;
        private readonly UiNotificationService uiNotificationService;

        public IFlowManager FlowManager { get; }

        public async Task Start()
        {
            DateTime dt = DateTime.Now;
            FlowSettings flowSettings = await this.FlowManager.Start("home");
            DateTime dt2 = DateTime.Now;

            appLogger.LogMessage(nameof(ScopedFlowManagerService), $"Start (milliseconds) = {(dt2 - dt).TotalMilliseconds}");

            uiNotificationService.NotifyFlowSettingsChanged(flowSettings);
        }

        public async Task NewFlowStart(NewFlowRequest request)
        {
            DateTime dt = DateTime.Now;
            FlowSettings flowSettings = await this.FlowManager.NewFlowStart(request);
            DateTime dt2 = DateTime.Now;

            appLogger.LogMessage(nameof(ScopedFlowManagerService), $"NavStart (milliseconds) = {(dt2 - dt).TotalMilliseconds}");

            uiNotificationService.NotifyFlowSettingsChanged(flowSettings);
        }

        public async Task RunFlow(NewFlowRequest request)
        {
            DateTime dt = DateTime.Now;
            FlowSettings flowSettings = await this.FlowManager.NewFlowStart(request);
            DateTime dt2 = DateTime.Now;

            appLogger.LogMessage(nameof(ScopedFlowManagerService), $"RunFlow (milliseconds) = {(dt2 - dt).TotalMilliseconds}");
        }

        public async Task Next(CommandButtonRequest request)
        {
            DateTime dt = DateTime.Now;
            FlowSettings flowSettings = await this.FlowManager.Next(request);
            DateTime dt2 = DateTime.Now;

            appLogger.LogMessage(nameof(ScopedFlowManagerService), $"Next (milliseconds) = {(dt2 - dt).TotalMilliseconds}");

            uiNotificationService.NotifyFlowSettingsChanged(flowSettings);
        }

        public void Dispose()
        {
            scope?.Dispose();
        }

        public void SetFlowDataCacheItem(string key, object value)
        {
            if (this.FlowManager.FlowDataCache.Items == null)
                return;

            this.FlowManager.FlowDataCache.Items[key] = value;
        }

        public object GetFlowDataCacheItem(string key)
        {
            if (this.FlowManager.FlowDataCache.Items == null)
                return null;

            return this.FlowManager.FlowDataCache.Items.TryGetValue(key, out object value) ? value : null;
        }

        public void CopyFlowItems()
        {
            this.FlowManager.FlowState = uiNotificationService.FlowSettings.FlowState;
            this.FlowManager.FlowDataCache.PersistentKeys = new List<string>(uiNotificationService.FlowSettings.FlowDataCache.PersistentKeys);
            this.FlowManager.FlowDataCache.Items = new Dictionary<string, object>(uiNotificationService.FlowSettings.FlowDataCache.Items);
            this.FlowManager.FlowDataCache.NavigationBar = new NavigationBarDescriptor
            {
                BrandText = uiNotificationService.FlowSettings.FlowDataCache.NavigationBar.BrandText,
                CurrentModule = uiNotificationService.FlowSettings.FlowDataCache.NavigationBar.CurrentModule,
                MenuItems = new List<NavigationMenuItemDescriptor>(uiNotificationService.FlowSettings.FlowDataCache.NavigationBar.MenuItems)
            };
        }

        public void CopyPersistentFlowItems()
        {
            this.FlowManager.FlowDataCache.PersistentKeys = new List<string>(uiNotificationService.FlowSettings.FlowDataCache.PersistentKeys);
            this.FlowManager.FlowDataCache.Items = new Dictionary<string, object>
            (
                uiNotificationService.FlowSettings.FlowDataCache.Items.Where
                (
                    kvp => uiNotificationService.FlowSettings.FlowDataCache.PersistentKeys.Contains(kvp.Key)
                )
            );
        }
    }
}
