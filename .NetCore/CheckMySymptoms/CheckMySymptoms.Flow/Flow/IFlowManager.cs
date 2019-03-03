using AutoMapper;
using CheckMySymptoms.Flow.Cache;
using CheckMySymptoms.Flow.Requests;
using CheckMySymptoms.Flow.ScreenSettings;
using CheckMySymptoms.Flow.ScreenSettings.Views;
using LogicBuilder.Forms.Parameters;
using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMySymptoms.Flow
{
    public interface IFlowManager
    {
        FlowSettings Start(string module, FlowState flowState);
        FlowSettings Next(RequestBase response);
        FlowSettings Previous(RequestBase request);
        FlowSettings ToPreviousStep(RequestBase request, int step);
        void FlowComplete();
        void Terminate();
        void Wait();

        void DisplayInputQuestions(InputFormParameters form, ICollection<ConnectorParameters> connectors = null);

        Dictionary<int, object> InputQuestionsAnswers { get; }
        Variables Variables { get; }
        Progress Progress { get; }
        ICustomActions CustomActions { get; }
        ICustomDialogs CustomDialogs { get; }
        void SetCurrentBusinessBackupData();

        FlowDataCache FlowDataCache { get; set; }
        DirectorBase Director { get; }
        IFlowActivity FlowActivity { get; }
        IMapper Mapper { get; }
    }
}
