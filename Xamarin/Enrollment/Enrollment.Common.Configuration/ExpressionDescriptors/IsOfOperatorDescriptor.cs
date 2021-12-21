using System;

namespace Enrollment.Common.Configuration.ExpressionDescriptors
{
    public class IsOfOperatorDescriptor : OperatorDescriptorBase
    {
		public OperatorDescriptorBase Operand { get; set; }
		public string Type { get; set; }
    }
}