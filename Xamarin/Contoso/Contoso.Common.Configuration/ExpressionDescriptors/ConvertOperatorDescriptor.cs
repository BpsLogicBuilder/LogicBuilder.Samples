using System;

namespace Contoso.Common.Configuration.ExpressionDescriptors
{
    public class ConvertOperatorDescriptor : OperatorDescriptorBase
    {
		public string Type { get; set; }
		public OperatorDescriptorBase SourceOperand { get; set; }
    }
}