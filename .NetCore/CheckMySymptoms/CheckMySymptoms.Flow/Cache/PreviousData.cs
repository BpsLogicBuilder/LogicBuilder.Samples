﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMySymptoms.Flow.Cache
{
    public class PreviousData
    {
        public PreviousData(InternalFlowState flowState, FlowDataCache flowDataCache)
        {
            FlowState = flowState;
            FlowDataCache = flowDataCache;
        }

        public InternalFlowState FlowState { get; }
        public FlowDataCache FlowDataCache { get;  }
    }
}
