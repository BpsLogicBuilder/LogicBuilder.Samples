using System;

namespace Enrollment.Common.Configuration.ExpressionDescriptors
{
    public class ConstantOperatorDescriptor : OperatorDescriptorBase
    {
		public string Type { get; set; }
		public object ConstantValue { get; set; }
    }
}