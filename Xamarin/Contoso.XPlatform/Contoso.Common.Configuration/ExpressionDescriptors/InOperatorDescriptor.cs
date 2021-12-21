namespace Contoso.Common.Configuration.ExpressionDescriptors
{
    public class InOperatorDescriptor : OperatorDescriptorBase
    {
		public OperatorDescriptorBase ItemToFind { get; set; }
		public OperatorDescriptorBase ListToSearch { get; set; }
    }
}