using System.Reflection;

namespace Contoso.Common.Configuration.ExpressionDescriptors
{
    public class CustomMethodOperatorDescriptor : OperatorDescriptorBase
    {
		public MethodInfo MethodInfo { get; set; }
		public OperatorDescriptorBase[] Args { get; set; }
    }
}