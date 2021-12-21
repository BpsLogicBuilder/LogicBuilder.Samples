using AutoMapper;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.Navigation;
using Contoso.Forms.Configuration.TextForm;
using Contoso.XPlatform.Flow.Cache;
using Contoso.XPlatform.Flow.Requests;
using Contoso.XPlatform.Flow.Settings;
using Contoso.XPlatform.Flow.Settings.Screen;
using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Contoso.XPlatform.Flow
{
    public class FlowManager : IFlowManager
    {
        public FlowManager(ScreenData screenData,
            IAppLogger appLogger,
            DirectorFactory directorFactory,
            FlowDataCache flowDataCache,
            IActions actions,
            IDialogFunctions dialogFunctions,
            FlowActivityFactory flowActivityFactory,
            IMapper mapper)
        {
            this.screenData = screenData;
            this.appLogger = appLogger;
            this.Director = directorFactory.Create(this);
            this.FlowDataCache = flowDataCache;
            this.Actions = actions;
            this.DialogFunctions = dialogFunctions;
            this.FlowActivity = flowActivityFactory.Create(this);
            this.Mapper = mapper;
        }

        #region Fields
        private readonly ScreenData screenData;
        private readonly IAppLogger appLogger;
        #endregion Fields

        #region Properties
        public Progress Progress { get; } = new Progress();
        public DirectorBase Director { get; }
        public FlowDataCache FlowDataCache { get; }
        public IActions Actions { get; }
        public IDialogFunctions DialogFunctions { get; }
        public IFlowActivity FlowActivity { get; }
        public IMapper Mapper { get; }

        private FlowSettings FlowSettings
            => new FlowSettings
            (
                FlowState,
                FlowDataCache,
                screenData.ScreenSettings
            );

        public FlowState FlowState
        {
            get => ((Director)this.Director).FlowState;
            set => ((Director)this.Director).FlowState = value;
        }
        #endregion Properties

        #region Methods
        Task<FlowSettings> IFlowManager.Start(string module) 
            => Task.Run(() => Start(module));

        private FlowSettings Start(string module)
        {
            try
            {
                Reset();
                this.Director.StartInitialFlow(module);
                return this.FlowSettings;
            }
            catch (Exception ex)
            {
                appLogger.LogMessage(nameof(FlowManager), string.Format("Progress Start {0}", JsonSerializer.Serialize(this.Progress)));
                this.appLogger.LogMessage(nameof(FlowManager), ex.ToString());
                return GetFlowSettings(ex);
            }
        }

        Task<FlowSettings> IFlowManager.NewFlowStart(NewFlowRequest request) 
            => Task.Run(() => NewFlowStart(request));

        private FlowSettings NewFlowStart(NewFlowRequest request)
        {
            try
            {
                this.Director.StartInitialFlow(request.InitialModuleName);
                return this.FlowSettings;
            }
            catch (Exception ex)
            {
                appLogger.LogMessage(nameof(FlowManager), string.Format("Progress Start {0}", JsonSerializer.Serialize(this.Progress)));
                this.appLogger.LogMessage(nameof(FlowManager), ex.ToString());
                return GetFlowSettings(ex);
            }
        }

        Task<FlowSettings> IFlowManager.Next(CommandButtonRequest request) 
            => Task.Run(() => Next(request));

        private FlowSettings Next(CommandButtonRequest request)
        {
            try
            {
                this.Director.SetSelection(request.NewSelection);

                DateTime dt = DateTime.Now;
                this.Director.ExecuteRulesEngine();
                DateTime dt2 = DateTime.Now;
                appLogger.LogMessage(nameof(FlowManager), $"FlowManagerNext (milliseconds) = {(dt2 - dt).TotalMilliseconds}");

                return this.FlowSettings;
            }
            catch (Exception ex)
            {
                appLogger.LogMessage(nameof(FlowManager), string.Format("Progress Start {0}", JsonSerializer.Serialize(this.Progress)));
                this.appLogger.LogMessage(nameof(FlowManager), ex.ToString());
                return GetFlowSettings(ex);
            }
        }

        public void FlowComplete()
        {
        }

        public void SetCurrentBusinessBackupData()
        {
        }

        public void Terminate()
        {
        }

        private void Reset()
        {
            Progress.ClearProgressList();
            this.FlowDataCache.Items.Clear();
            this.FlowDataCache.NavigationBar = new NavigationBarDescriptor();
        }

        private FlowSettings GetFlowSettings(Exception ex) 
            => new FlowSettings
            (
                FlowState,
                FlowDataCache,
                new ScreenSettings<TextFormSettingsDescriptor>
                (
                    new TextFormSettingsDescriptor
                    {
                        Title = nameof(Exception),
                        TextGroups = new List<TextGroupDescriptor>
                        {
                            new TextGroupDescriptor
                            {
                                Title = nameof(Exception.Message),
                                Labels = new List<LabelItemDescriptorBase>
                                {
                                    new LabelItemDescriptor { Text = ex.Message }
                                }
                            },
                            new TextGroupDescriptor
                            {
                                Title = nameof(Exception.StackTrace),
                                Labels = new List<LabelItemDescriptorBase>
                                (
                                    ex.StackTrace
                                        .Split
                                        (
                                            new char[] { '\r', '\n' }, 
                                            StringSplitOptions.RemoveEmptyEntries
                                        )
                                        .Select(i => new LabelItemDescriptor{ Text = i })
                                )
                            },
                            new TextGroupDescriptor
                            {
                                Title = nameof(Progress),
                                Labels = new List<LabelItemDescriptorBase>
                                (
                                    Progress
                                        .ProgressItems
                                        .Select(i => new LabelItemDescriptor{ Text = $"{i.Description} {i.DateAndTime.ToString("hh:mm:ss.fff tt")}" })
                                )
                            }
                        }
                    },
                    new List<CommandButtonDescriptor>(),
                    ViewType.TextPage
                )
            );
        #endregion Methods
    }
}
