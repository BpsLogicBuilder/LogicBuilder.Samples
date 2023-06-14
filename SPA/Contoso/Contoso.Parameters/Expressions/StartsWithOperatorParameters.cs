namespace Contoso.Parameters.Expressions
{
    public class StartsWithOperatorParameters : IExpressionParameter
    {
		public StartsWithOperatorParameters()
		{
		}

		public StartsWithOperatorParameters(IExpressionParameter left, IExpressionParameter right)
		{
			Left = left;
			Right = right;
		}

		public IExpressionParameter Left { get; set; }
		public IExpressionParameter Right { get; set; }
    }
}