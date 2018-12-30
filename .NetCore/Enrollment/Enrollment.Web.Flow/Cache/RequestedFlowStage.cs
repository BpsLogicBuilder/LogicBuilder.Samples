using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Web.Flow.Cache
{
    public class RequestedFlowStage
    {
        public string InitialModule { get; set; }
        public int TargetModule { get; set; }
    }
}
