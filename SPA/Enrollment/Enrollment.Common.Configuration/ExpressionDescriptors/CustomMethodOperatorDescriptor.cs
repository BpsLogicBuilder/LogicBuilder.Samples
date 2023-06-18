using System.Reflection;

namespace Enrollment.Common.Configuration.ExpressionDescriptors
{
    public class CustomMethodOperatorDescriptor : OperatorDescriptorBase
    {
        public MethodInfo MethodInfo { get; set; }
        public OperatorDescriptorBase[] Args { get; set; }
    }
}