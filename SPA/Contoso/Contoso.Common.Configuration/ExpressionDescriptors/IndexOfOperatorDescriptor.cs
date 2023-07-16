namespace Contoso.Common.Configuration.ExpressionDescriptors
{
    public class IndexOfOperatorDescriptor : OperatorDescriptorBase
    {
		public OperatorDescriptorBase SourceOperand { get; set; }
		public OperatorDescriptorBase ItemToFind { get; set; }
    }
}