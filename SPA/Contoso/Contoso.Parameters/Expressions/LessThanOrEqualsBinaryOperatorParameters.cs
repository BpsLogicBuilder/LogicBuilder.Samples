namespace Contoso.Parameters.Expressions
{
    public class LessThanOrEqualsBinaryOperatorParameters : BinaryOperatorParameters
    {
		public LessThanOrEqualsBinaryOperatorParameters()
		{
		}

		public LessThanOrEqualsBinaryOperatorParameters(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}