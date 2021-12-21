namespace Contoso.Common.Configuration.ExpressionDescriptors
{
    public class TakeOperatorDescriptor : OperatorDescriptorBase
    {
		public OperatorDescriptorBase SourceOperand { get; set; }
		public int Count { get; set; }
    }
}