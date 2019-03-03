using CheckMySymptoms.Flow.Cache;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CheckMySymptoms.Flow
{
    public interface IFlowStateRepository
    {
        Task<FlowState> GetFlowState();
        Task SaveFlowState(FlowState flowState);
        Task DeleteFlowState();
    }
}
