using Contoso.Spa.Flow.Cache;
using Contoso.Spa.Flow.Requests;
using Contoso.Spa.Flow.ScreenSettings;
using LogicBuilder.RulesDirector;

namespace Contoso.Spa.Flow
{
    public interface IFlowManager
    {
        DirectorBase Director { get; }
        FlowDataCache FlowDataCache { get; set; }
        Progress Progress { get; }
        ICustomActions CustomActions { get; }
        ICustomDialogs CustomDialogs { get; }
        IFlowActivity FlowActivity { get; }

        FlowSettings Start(string module, int stage);
        FlowSettings Next(RequestBase request);
        FlowSettings NavStart(NavBarRequest navBarRequest);
        void FlowComplete();
        void Terminate();
        void SetCurrentBusinessBackupData();
    }
}
