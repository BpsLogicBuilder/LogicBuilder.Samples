using AutoMapper;
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
            FlowDataCache flowDataCache,
            IMapper mapper)
        {
            this.CustomDialogs = customDialogs;
            this.CustomActions = customActions;
            this._logger = logger;
            this.Director = directorFactory.Create(this);
            this.FlowActivity = flowActivityFactory.Create(this);
            this.FlowDataCache = flowDataCache;
            this.Mapper = mapper;
        }

        private readonly ILogger<FlowManager> _logger;

        public DirectorBase Director { get; }
        public FlowDataCache FlowDataCache { get; set; }
        public Progress Progress { get; } = new Progress();

        public ICustomActions CustomActions { get; }
        public ICustomDialogs CustomDialogs { get; }
        public IFlowActivity FlowActivity { get; }
        public IMapper Mapper { get; }

        private FlowSettings FlowSettings
            => new
            (
                GetUserId(),
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
                this.FlowDataCache.Items[nameof(Domain.Entities.UserModel.UserId)] = navBarRequest.UserId;
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

        public void RunFlow(string flowName)
        {
            try
            {
                this.Director.StartInitialFlow(flowName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: {Message}", ex.Message);
                throw;
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
        {
            return new
            (
                GetUserId(),
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

        private int GetUserId() 
            => FlowDataCache.Items.TryGetValue(nameof(Domain.Entities.UserModel.UserId), out object? userId)
                ? (int)userId : 0;
    }
}
