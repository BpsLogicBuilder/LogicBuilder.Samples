using System;
using System.Collections.Generic;

namespace Enrollment.Spa.Flow.Responses.TransientFlows
{
    public abstract class BaseFlowResponse
    {
        public bool Success { get; set; }
        public ICollection<string> ErrorMessages { get; set; } = new List<string>();
        public string TypeFullName => GetType().AssemblyQualifiedName ?? throw new ArgumentException($"{nameof(TypeFullName)}: {{123120FF-E7A4-4843-8228-9E4D39EAAAF7}}");
    }
}
