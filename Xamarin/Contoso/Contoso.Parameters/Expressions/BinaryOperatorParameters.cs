namespace Contoso.Parameters.Expressions
{
    abstract public class BinaryOperatorParameters : IExpressionParameter
    {
		public BinaryOperatorParameters()
		{
		}

		public BinaryOperatorParameters(IExpressionParameter left, IExpressionParameter right)
		{
			Left = left;
			Right = right;
		}

		public IExpressionParameter Left { get; set; }
		public IExpressionParameter Right { get; set; }
    }
}