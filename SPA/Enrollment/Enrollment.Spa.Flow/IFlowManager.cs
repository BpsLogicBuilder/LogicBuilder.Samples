using AutoMapper;
using Enrollment.Spa.Flow.Cache;
using Enrollment.Spa.Flow.Requests;
using Enrollment.Spa.Flow.ScreenSettings;
using LogicBuilder.RulesDirector;

namespace Enrollment.Spa.Flow
{
    public interface IFlowManager
    {
        DirectorBase Director { get; }
        FlowDataCache FlowDataCache { get; set; }
        Progress Progress { get; }
        ICustomActions CustomActions { get; }
        ICustomDialogs CustomDialogs { get; }
        IFlowActivity FlowActivity { get; }
        IMapper Mapper { get; }

        FlowSettings Start(string module, int stage);
        FlowSettings Next(RequestBase request);
        FlowSettings NavStart(NavBarRequest navBarRequest);
        void RunFlow(string flowName);
        void FlowComplete();
        void Terminate();
        void SetCurrentBusinessBackupData();
    }
}
