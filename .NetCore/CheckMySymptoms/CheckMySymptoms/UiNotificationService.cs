using CheckMySymptoms.Flow;
using CheckMySymptoms.Flow.Cache;
using CheckMySymptoms.Flow.Requests;
using CheckMySymptoms.Flow.ScreenSettings;
using CheckMySymptoms.Flow.ScreenSettings.Views;
using CheckMySymptoms.Forms.View;
using CheckMySymptoms.Forms.View.Common;
using CheckMySymptoms.Forms.View.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace CheckMySymptoms
{
    public class UiNotificationService
    {
        public UiNotificationService(IFlowManager flowManager, IFlowStateRepository flowStateRepository, IPatientDataRepository patientDataRepository)
        {
            this.FlowManager = flowManager;
            this.flowStateRepository = flowStateRepository;
            this.patientDataRepository = patientDataRepository;
        }

        #region Fields
        private readonly IFlowStateRepository flowStateRepository;
        private readonly IPatientDataRepository patientDataRepository;
        #endregion Fields

        #region Properties
        public IFlowManager FlowManager { get; }
        public Subject<FlowSettings> FlowSettings { get; set; } = new Subject<FlowSettings>();
        public Subject<bool> ExitCalledFromMain { get; set; } = new Subject<bool>();
        public Subject<bool> AdFreeChanged { get; set; } = new Subject<bool>();
        public ScreenSettingsBase ScreenSettings { get; set; }
        public FlowDataCache FlowDataCache { get; set; }
        public ViewType ViewType { get; set; }
        public string SelectedVoice { get; set; }
        public bool IsVoiceOn { get; set; }
        public bool HasAdFree { get; set; }
        #endregion Properties

        #region Methods
        public T GetView<T>() where T : ViewBase
        {
            return ((ScreenSettings<T>)ScreenSettings).Settings;
        }

        public RequestBase CreateRequestForBackup()
        {
            switch (ScreenSettings.ViewType)
            {
                case ViewType.Select:
                    return new SelectRequest
                    {
                        MessageTemplateView = GetView<MessageTemplateView>(),
                        ViewType = ScreenSettings.ViewType
                    };
                case ViewType.InputForm:
                    return new InputFormRequest
                    {
                        Form = GetView<InputFormView>(),
                        ViewType = ScreenSettings.ViewType
                    };
                case ViewType.FlowComplete:
                    return new FlowCompleteRequest
                    {
                        Form = GetView<FlowCompleteView>(),
                        ViewType = ScreenSettings.ViewType
                    };
                default:
                    throw new ArgumentException("{0459D005-6183-43FF-AD24-C96025EB4953}");
            }
        }

        public bool CanGoBack => this.FlowDataCache.DialogList.Count > 1;

        public async Task GoBack()
        {
            if (this.FlowDataCache?.DialogList?.Count > 1)
                await this.Previous(CreateRequestForBackup());
        }

        public async Task Start()
        {
            FlowState flowState = Task.Run(async () => await flowStateRepository.GetFlowState()).Result;
            this.FlowSettings.OnNext(UpdateFlowSettings(await Task.Run(() => this.FlowManager.Start("initial", flowState)), NavigationType.Start));
        }

        public async Task Next(RequestBase request)
        {
            FlowSettings flowSettings = await Task.Run(() => this.FlowManager.Next(request));
            this.FlowSettings.OnNext(UpdateFlowSettings(flowSettings, NavigationType.Next));
        }

        public async Task Previous(RequestBase request)
        {
            this.ExitCalledFromMain.OnNext(true);
            this.FlowSettings.OnNext(UpdateFlowSettings(await Task.Run(() => this.FlowManager.Previous(request)), NavigationType.Previous));
        }

        public async Task ToPreviousStep(RequestBase request, int step)
        {
            this.ExitCalledFromMain.OnNext(true);
            this.FlowSettings.OnNext(UpdateFlowSettings(await Task.Run(() => this.FlowManager.ToPreviousStep(request, step)), NavigationType.Previous));
        }

        public void NotifyExitCalled()
        {
            this.ExitCalledFromMain.OnNext(true);
        }

        public void NotifyAdFreeChanged(bool hasAdFree)
        {
            this.AdFreeChanged.OnNext(hasAdFree);
        }

        private FlowSettings UpdateFlowSettings(FlowSettings flowSettings, NavigationType navigationType = NavigationType.Next)
        {
            this.FlowDataCache = flowSettings.FlowDataCache;
            this.ScreenSettings = flowSettings.ScreenSettings;
            flowSettings.NavigationType = navigationType;

            if (flowSettings.ScreenSettings.ViewType != ViewType.Exception)
                Task.Run(async () => await flowStateRepository.SaveFlowState(flowSettings.FlowState));//just fire and forget

            return flowSettings;
        }

        public async Task DeleteFlowState()
        {
            await this.flowStateRepository.DeleteFlowState();
        }

        public async Task DeletePatientData()
        {
            await this.patientDataRepository.DeleteData();
        }
        #endregion Methods

    }
}
