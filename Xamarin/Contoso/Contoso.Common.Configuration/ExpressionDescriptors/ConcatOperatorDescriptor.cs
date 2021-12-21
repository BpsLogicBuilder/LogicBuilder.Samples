namespace Contoso.Common.Configuration.ExpressionDescriptors
{
    public class ConcatOperatorDescriptor : OperatorDescriptorBase
    {
		public OperatorDescriptorBase Left { get; set; }
		public OperatorDescriptorBase Right { get; set; }
    }
}