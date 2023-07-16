using System;
using System.Collections.Generic;

namespace Contoso.Spa.Flow.Responses.TransientFlows
{
    public abstract class BaseFlowResponse
    {
        public bool Success { get; set; }
        public ICollection<string> ErrorMessages { get; set; } = new List<string>();
        public string TypeFullName => GetType().AssemblyQualifiedName ?? throw new ArgumentException($"{nameof(TypeFullName)}: {{0F17E304-6517-4AC7-886A-20E483BCC6C2}}");
    }
}
