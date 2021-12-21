using System.Collections.Generic;

namespace Enrollment.Common.Configuration.ExpressionDescriptors
{
    abstract public class FilterMethodOperatorDescriptorBase : OperatorDescriptorBase
    {
		public OperatorDescriptorBase SourceOperand { get; set; }
		public OperatorDescriptorBase FilterBody { get; set; }
		public string FilterParameterName { get; set; }
    }
}