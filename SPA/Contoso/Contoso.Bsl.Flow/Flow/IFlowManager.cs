using AutoMapper;
using Contoso.Bsl.Flow.Cache;
using Contoso.Repositories;
using LogicBuilder.RulesDirector;

namespace Contoso.Bsl.Flow
{
    public interface IFlowManager
    {
        ICustomActions CustomActions { get; }
        DirectorBase Director { get; }
        IFlowActivity FlowActivity { get; }
        FlowDataCache FlowDataCache { get; }
        IMapper Mapper { get; }
        Progress Progress { get; }
        ISchoolRepository SchoolRepository { get; }

        void Start(string module);
        void SetCurrentBusinessBackupData();
        void FlowComplete();
        void Terminate();
    }
}