namespace Contoso.Common.Configuration.ExpressionDescriptors
{
    public class SubstringOperatorDescriptor : OperatorDescriptorBase
    {
		public OperatorDescriptorBase SourceOperand { get; set; }
		public OperatorDescriptorBase[] Indexes { get; set; }
    }
}