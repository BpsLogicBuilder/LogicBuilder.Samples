namespace Contoso.Parameters.Expressions
{
    public class GreaterThanBinaryOperatorParameters : BinaryOperatorParameters
    {
		public GreaterThanBinaryOperatorParameters()
		{
		}

		public GreaterThanBinaryOperatorParameters(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}