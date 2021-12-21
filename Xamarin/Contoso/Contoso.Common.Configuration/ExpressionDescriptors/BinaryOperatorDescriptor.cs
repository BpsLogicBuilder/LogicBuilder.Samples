namespace Contoso.Common.Configuration.ExpressionDescriptors
{
    abstract public class BinaryOperatorDescriptor : OperatorDescriptorBase
    {
		public OperatorDescriptorBase Left { get; set; }
		public OperatorDescriptorBase Right { get; set; }
    }
}