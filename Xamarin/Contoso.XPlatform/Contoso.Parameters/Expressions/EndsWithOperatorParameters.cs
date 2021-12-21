namespace Contoso.Parameters.Expressions
{
    public class EndsWithOperatorParameters : IExpressionParameter
    {
		public EndsWithOperatorParameters()
		{
		}

		public EndsWithOperatorParameters(IExpressionParameter left, IExpressionParameter right)
		{
			Left = left;
			Right = right;
		}

		public IExpressionParameter Left { get; set; }
		public IExpressionParameter Right { get; set; }
    }
}