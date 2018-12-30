using AutoMapper;
using Enrollment.Repositories;
using Enrollment.Web.Flow.Cache;
using Enrollment.Web.Flow.Requests;
using Enrollment.Web.Flow.ScreenSettings;
using LogicBuilder.Forms.Parameters;
using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Web.Flow
{
    public interface IFlowManager
    {
        FlowSettings Start(string module, int stage);
        FlowSettings Next(RequestBase response);
        FlowSettings NavStart(NavBarRequest navBarRequest);
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
        IEnrollmentRepository EnrollmentRepository { get; }
        IMapper Mapper { get; }
    }
}
