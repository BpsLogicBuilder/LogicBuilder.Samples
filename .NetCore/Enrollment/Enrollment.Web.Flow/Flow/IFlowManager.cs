using AutoMapper;
using Enrollment.Repositories;
using Enrollment.Web.Flow.Cache;
using Enrollment.Web.Flow.Requests;
using Enrollment.Web.Flow.ScreenSettings;
using LogicBuilder.RulesDirector;

namespace Enrollment.Web.Flow
{
    public interface IFlowManager
    {
        FlowSettings Start(string module, int stage);
        FlowSettings Next(RequestBase response);
        FlowSettings NavStart(NavBarRequest navBarRequest);
        void FlowComplete();
        void Terminate();

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
