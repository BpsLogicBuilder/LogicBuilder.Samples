namespace Contoso.Parameters.Expressions
{
    public class ContainsOperatorParameters : IExpressionParameter
    {
		public ContainsOperatorParameters()
		{
		}

		public ContainsOperatorParameters(IExpressionParameter left, IExpressionParameter right)
		{
			Left = left;
			Right = right;
		}

		public IExpressionParameter Left { get; set; }
		public IExpressionParameter Right { get; set; }
    }
}