namespace Contoso.Parameters.Expressions
{
    public class NotEqualsBinaryOperatorParameters : BinaryOperatorParameters
    {
		public NotEqualsBinaryOperatorParameters()
		{
		}

		public NotEqualsBinaryOperatorParameters(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}