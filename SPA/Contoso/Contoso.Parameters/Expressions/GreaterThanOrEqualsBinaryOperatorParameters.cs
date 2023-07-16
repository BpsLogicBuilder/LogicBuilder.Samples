namespace Contoso.Parameters.Expressions
{
    public class GreaterThanOrEqualsBinaryOperatorParameters : BinaryOperatorParameters
    {
		public GreaterThanOrEqualsBinaryOperatorParameters()
		{
		}

		public GreaterThanOrEqualsBinaryOperatorParameters(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}