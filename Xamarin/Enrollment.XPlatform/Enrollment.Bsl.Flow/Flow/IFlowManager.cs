using AutoMapper;
using Enrollment.Bsl.Flow.Cache;
using Enrollment.Bsl.Flow.Services;
using Enrollment.Repositories;
using LogicBuilder.RulesDirector;

namespace Enrollment.Bsl.Flow
{
    public interface IFlowManager
    {
        ICustomActions CustomActions { get; }
        IGetItemFilterBuilder GetItemFilterBuilder { get; }
        DirectorBase Director { get; }
        IFlowActivity FlowActivity { get; }
        FlowDataCache FlowDataCache { get; }
        IMapper Mapper { get; }
        Progress Progress { get; }
        IEnrollmentRepository EnrollmentRepository { get; }

        void Start(string module);
        void SetCurrentBusinessBackupData();
        void FlowComplete();
        void Terminate();
    }
}