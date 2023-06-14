namespace Contoso.Parameters.Expressions
{
    public class AndBinaryOperatorParameters : BinaryOperatorParameters
    {
		public AndBinaryOperatorParameters()
		{
		}

		public AndBinaryOperatorParameters(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}