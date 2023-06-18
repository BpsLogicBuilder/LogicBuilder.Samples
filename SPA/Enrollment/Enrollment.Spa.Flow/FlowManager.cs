using Enrollment.Forms.View;
using Enrollment.Spa.Flow.Cache;
using Enrollment.Spa.Flow.Dialogs;
using Enrollment.Spa.Flow.Requests;
using Enrollment.Spa.Flow.ScreenSettings;
using Enrollment.Spa.Flow.ScreenSettings.Views;
using LogicBuilder.RulesDirector;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;

namespace Enrollment.Spa.Flow
{
    public class FlowManager : IFlowManager
    {
        public FlowManager(ICustomDialogs customDialogs,
            ICustomActions customActions,
            IDirectorFactory directorFactory,
            IFlowActivityFactory flowActivityFactory,
            ILogger<FlowManager> logger,
            FlowDataCache flowDataCache)
        {
            this.CustomDialogs = customDialogs;
            this.CustomActions = customActions;
            this._logger = logger;
            this.Director = directorFactory.Create(this);
            this.FlowActivity = flowActivityFactory.Create(this);
            this.FlowDataCache = flowDataCache;
        }

        private readonly ILogger<FlowManager> _logger;

        public DirectorBase Director { get; }
        public FlowDataCache FlowDataCache { get; set; }
        public Progress Progress { get; } = new Progress();

        public ICustomActions CustomActions { get; }
        public ICustomDialogs CustomDialogs { get; }
        public IFlowActivity FlowActivity { get; }

        private FlowSettings FlowSettings
            => new
            (
                ((Director)this.Director).FlowState,
                FlowDataCache.NavigationBar,
                FlowDataCache.ScreenSettings ?? throw new ArgumentException($"{nameof(FlowDataCache.ScreenSettings)}: {{60B6AFD1-2247-4775-BE99-F3F650A15B0F}}")
            );

        public void FlowComplete()
        {
            _logger.LogWarning(0, "FlowComplete {Progress}", JsonSerializer.Serialize(this.Progress));
            FlowDataCache.ScreenSettings = new ScreenSettings<object>(null!, Array.Empty<CommandButtonView>(), ViewType.FlowComplete);
        }

        public FlowSettings NavStart(NavBarRequest navBarRequest)
        {
            try
            {
                FlowDataCache.RequestedFlowStage = new RequestedFlowStage
                {
                    InitialModule = navBarRequest.InitialModuleName ?? throw new ArgumentException($"{nameof(navBarRequest.InitialModuleName)}: {{91027670-3D9A-444C-A1C9-03B19BC53C19}}"),
                    TargetModule = navBarRequest.TargetModule
                };

                this.Director.StartInitialFlow(FlowDataCache.RequestedFlowStage.InitialModule);

                return this.FlowSettings;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(0, "NavStart {Progress}", JsonSerializer.Serialize(this.Progress));
                _logger.LogError(ex, "Exception: {Message}", ex.Message);
                return this.GetFlowSettings(ex);
            }
        }

        public FlowSettings Next(RequestBase request)
        {
            try
            {
                IDialogHandler handler = BaseDialogHandler.Create(request);

                handler.Complete(this, request);
                this.Director.ExecuteRulesEngine();

                return this.FlowSettings;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(0, "Progress Next {Progress}", JsonSerializer.Serialize(this.Progress));
                _logger.LogError(ex, "Exception: {Message}", ex.Message);
                return this.GetFlowSettings(ex);
            }
        }

        public void SetCurrentBusinessBackupData()
        {
        }

        public FlowSettings Start(string module, int stage)
        {
            try
            {
                FlowDataCache.RequestedFlowStage = new RequestedFlowStage { InitialModule = module, TargetModule = stage };
                this.Director.StartInitialFlow(module);
                return this.FlowSettings;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(0, "Progress Start{Progress}", JsonSerializer.Serialize(this.Progress));
                _logger.LogError(ex, "Exception: {Message}", ex.Message);
                return this.GetFlowSettings(ex);
            }
        }

        public void Terminate()
        {
        }

        private FlowSettings GetFlowSettings(Exception ex)
            => new
            (
                ((Director)this.Director).FlowState,
                FlowDataCache.NavigationBar,
                new ScreenSettings<ExceptionView>
                (
                    new ExceptionView { Message = ex.Message },
                    Array.Empty<CommandButtonView>(),
                    ViewType.Exception
                )
            );
    }
}
