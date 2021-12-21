using System;

namespace Contoso.Common.Configuration.ExpressionDescriptors
{
    public class CollectionCastOperatorDescriptor : OperatorDescriptorBase
    {
		public OperatorDescriptorBase Operand { get; set; }
		public string Type { get; set; }
    }
}