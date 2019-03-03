using AutoMapper;
using CheckMySymptoms.Flow.Cache;
using CheckMySymptoms.Flow.Dialogs;
using CheckMySymptoms.Flow.Requests;
using CheckMySymptoms.Flow.ScreenSettings;
using CheckMySymptoms.Flow.ScreenSettings.Views;
using CheckMySymptoms.Forms.View;
using CheckMySymptoms.Forms.View.Common;
using CheckMySymptoms.Forms.View.Input;
using LogicBuilder.Forms.Parameters;
using LogicBuilder.RulesDirector;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CheckMySymptoms.Flow
{
    public class FlowManager : IFlowManager
    {
        public FlowManager(IMapper mapper,
            ICustomDialogs customDialogs,
            ICustomActions customActions,
            DirectorFactory directorFactory,
            FlowActivityFactory flowActivityFactory,
            //ILogger<FlowManager> logger,
            FlowDataCache flowDataCache,
            ScreenData screenData)
        {
            this.CustomDialogs = customDialogs;
            this.CustomActions = customActions;
            //this._logger = logger;
            this.Mapper = mapper;
            this.Director = directorFactory.Create(this);
            this.FlowActivity = flowActivityFactory.Create(this);
            this.FlowDataCache = flowDataCache;
            this.screenData = screenData;
            Reset();
        }

        #region Constants
        #endregion Constants

        #region Fields
        //private ILogger<FlowManager> _logger;
        private readonly ScreenData screenData;
        #endregion Fields

        #region Properties
        public Variables Variables => this.FlowDataCache.Variables;
        public Progress Progress { get; } = new Progress();
        public FlowDataCache FlowDataCache { get; set; }

        public ICustomActions CustomActions { get; private set; }

        public ICustomDialogs CustomDialogs { get; private set; }
        public DirectorBase Director { get; }
        public IFlowActivity FlowActivity { get; }

        public IMapper Mapper { get; }

        public Dictionary<int, object> InputQuestionsAnswers { get; } = new Dictionary<int, object>();

        private FlowDataCache BusinessBackupdata { get; set; }
        private Stack<PreviousData> previousDataStack = new Stack<PreviousData>();
        private Stack<ViewFieldsListPair> previousRepopulateStack = new Stack<ViewFieldsListPair>();
        private Stack<ViewFieldsListPair> forwardRepopulateStack = new Stack<ViewFieldsListPair>();

        public ScreenSettingsBase ScreenSettings { get; set; }
        #endregion Properties

        public void DisplayInputQuestions(InputFormParameters form, ICollection<ConnectorParameters> connectors = null)
        {
            InputFormView formView = this.Mapper.Map<InputFormView>(form);
            this.screenData.ScreenSettings = new ScreenSettings<InputFormView>
            (
                formView,
                this.Mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonView>>(connectors),
                ViewType.InputForm,
                new MenuItem { Text = formView.Title, Icon = formView.Icon }
            );
        }

        public void DisplayQuestions(QuestionFormParameters form, ICollection<ConnectorParameters> connectors = null)
        {
            throw new NotImplementedException();
        }

        public void FlowComplete()
        {
            this.screenData.ScreenSettings = new ScreenSettings<ViewBase>(null, (IEnumerable<CommandButtonView>)null, ViewType.FlowComplete, new MenuItem { });
        }

        public void SetCurrentBusinessBackupData()
        {
            BusinessBackupdata = FlowDataCache.Clone();
        }

        public void Terminate()
        {
        }

        public void Wait()
        {
        }

        private FlowSettings GetFlowSettings(Exception ex)
            => new FlowSettings
            (
                new ScreenSettings<ExceptionView>
                (
                    new ExceptionView { Caption = ex.GetType().Name, Message = ex.Message },
                    new CommandButtonView[] { },
                    ViewType.Exception,
                    new MenuItem { }
                )
            );

        public FlowSettings Start(string module, FlowState flowState)
        {
            try
            {
                Reset();
                if (flowState != null)
                {
                    try
                    {
                        Resume(flowState);
                    }
                    catch(Exception ex)
                    {
                        Reset();
                        this.Director.StartInitialFlow(module);
                    }
                }
                else
                    this.Director.StartInitialFlow(module);

                return this.GetFlowSettings();
            }
            catch (Exception ex)
            {
                //_logger.LogWarning(0, string.Format("Progress Start {0}", Newtonsoft.Json.JsonConvert.SerializeObject(this.Progress)));
                //this._logger.LogError(ex, ex.Message);
                //return this.GetFlowSettings(ex);
                Reset();
                this.Director.StartInitialFlow(module);
                return this.GetFlowSettings();
            }

        }

        public FlowSettings Next(RequestBase request)
        {
            try
            {
                IDialogHandler handler = BaseDialogHandler.Create(request);
                IEnumerable<ValidationResult> errors = handler.GetErrors(request);
                if (errors.Count() > 0)
                    return new FlowSettings(handler.GetScreenSettings(request, errors));

                handler.Complete(this);

                previousRepopulateStack.Push(new ViewFieldsListPair { View = request.View, Fields = handler.FieldValues });

                this.Director.ExecuteRulesEngine();

                return this.GetFlowSettings();
            }
            catch (Exception ex)
            {
                //_logger.LogWarning(0, string.Format("Progress Next {0}", Newtonsoft.Json.JsonConvert.SerializeObject(this.Progress)));
                //this._logger.LogError(ex, ex.Message);
                return this.GetFlowSettings(ex);
            }
        }

        private void ResetValuesOnBackup(PreviousData data)
        {
            this.FlowDataCache.Copy(data.FlowDataCache);

            BusinessBackupdata = this.FlowDataCache;
            ((Director)this.FlowActivity.Director).InternalFlowState = data.FlowState;
        }

        public FlowSettings Previous(RequestBase request)
        {
            if (previousDataStack.Count <= 1)
                throw new InvalidOperationException("{81BF4217-F4D2-459E-B842-85E4BC9D59D2}");

            IDialogHandler handler = BaseDialogHandler.Create(request);

            //remove backup data for current form
            previousDataStack.Pop();

            forwardRepopulateStack.Push(new ViewFieldsListPair { View = request.View, Fields = handler.FieldValues });//Get Fields to repopulate current form

            //get backup data for previous form
            ResetValuesOnBackup(previousDataStack.Pop());

            this.Director.ExecuteRulesEngine();

            return this.GetFlowSettingsOnBackup();
        }

        private void UpdateViewOnPrevious(FlowSettings flowSettings)
        {
            ViewFieldsListPair pair = previousRepopulateStack.Pop();
            if (pair == null)
                return;

            DoUpdate(flowSettings.ScreenSettings.View);
            void DoUpdate(ViewBase view)
            {
                if (pair.View.GetType() != view.GetType())
                    return;

                view.UpdateFields(pair.Fields);
            }
        }


        private void UpdateViewOnNext(FlowSettings flowSettings)
        {
            if (forwardRepopulateStack.Count == 0)
                return;

            ViewFieldsListPair pair = forwardRepopulateStack.Pop();
            if (pair == null)
                return;

            DoUpdate(flowSettings.ScreenSettings.View);
            void DoUpdate(ViewBase view)
            {
                if (!pair.View.Equals(view))
                {
                    forwardRepopulateStack.Clear();
                    return;
                }

                view.UpdateFields(pair.Fields);
            }
        }

        public FlowSettings ToPreviousStep(RequestBase request, int step)
        {
            int count = previousDataStack.Count - step;//step is zero based argument of the nav links
            //Number of items in the back up stack is one more than the number of links
            //e.g if when level == 0 and number items in the stack is 4 we must pop the stack 4 times;

            for (int i = 0; i < count - 1; i++)
                previousDataStack.Pop();

            IDialogHandler handler = BaseDialogHandler.Create(request);

            forwardRepopulateStack.Push(new ViewFieldsListPair { View = request.View, Fields = handler.FieldValues });//Get Fields to repopulate current form

            for (int i = 1; i < count - 1; i++)//pop one less item from the repopulate stack.  Repopulate stack gets updated on IDialog complete
            {
                forwardRepopulateStack.Push(previousRepopulateStack.Pop());//get fields for prior forms
            }

            //get backup data for previous form
            ResetValuesOnBackup(previousDataStack.Pop());

            this.Director.ExecuteRulesEngine();

            return this.GetFlowSettingsOnBackup();
        }

        private void Resume(FlowState flowState)
        {
            if (flowState == null)
                throw new ArgumentException("{B01A87E7-337F-491B-8E34-6B1EE352DCC7}");

            this.previousDataStack = flowState.PreviousDataStack;
            this.previousRepopulateStack = flowState.PreviousRepopulateStack;
            this.forwardRepopulateStack = flowState.ForwardRepopulateStack;


            //get backup data for previous visible form

            ResetValuesOnBackup(this.previousDataStack.Pop());

            //set is baching up to true only if SaveState was invoked from the client.  Because the repopulate stack was increased in the SaveState method only only if SaveState was invoked from the client.
            //isNewDialogState is true when the state is saved automatically for each new dialog
            //isBackingUp = !isNewDialogState;
            this.Director.ExecuteRulesEngine();
            //isBackingUp = false;
        }

        private void Reset()
        {
            ((Director)this.Director).InternalFlowState = new InternalFlowState
            {
                Driver = string.Empty,
                Selection = string.Empty,
                CallingModuleDriverStack = new List<string>(),
                CallingModuleStack = new List<string>(),
                ModuleBeginName = string.Empty,
                ModuleEndName = string.Empty,
                DialogClosed = false
            };

            Progress.ClearProgressList();
            previousDataStack.Clear();
            previousRepopulateStack.Clear();
            forwardRepopulateStack.Clear();
            this.FlowDataCache.Reset();
        }

        private FlowSettings GetFlowSettings()
        {
            this.FlowDataCache.DialogList.Add(this.screenData.ScreenSettings.DialogListItem);
            previousDataStack.Push(new PreviousData(((Director)this.FlowActivity.Director).InternalFlowState, BusinessBackupdata.Clone()));

            return DoGet
            (
                new FlowSettings(this.FlowDataCache, this.screenData.ScreenSettings),
                UpdateViewOnNext
            );
        }

        private FlowSettings GetFlowSettingsOnBackup()
        {
            this.FlowDataCache.DialogList.Add(this.screenData.ScreenSettings.DialogListItem);
            previousDataStack.Push(new PreviousData(((Director)this.FlowActivity.Director).InternalFlowState, BusinessBackupdata.Clone()));

            return DoGet
            (
                new FlowSettings(this.FlowDataCache, this.screenData.ScreenSettings),
                UpdateViewOnPrevious
            );
        }

        private FlowSettings DoGet(FlowSettings settings, Action<FlowSettings> updateView)
        {
            updateView(settings);
            settings.FlowState = new FlowState(this.previousDataStack, this.previousRepopulateStack, this.forwardRepopulateStack);
            return settings;
        }
    }
}
