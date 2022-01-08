using AutoMapper;
using Contoso.Repositories;
using Contoso.Web.Flow.Cache;
using Contoso.Web.Flow.Requests;
using Contoso.Web.Flow.ScreenSettings;
using LogicBuilder.RulesDirector;

namespace Contoso.Web.Flow
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
        ISchoolRepository SchoolRepository { get; }
        IMapper Mapper { get; }
    }
}