namespace Contoso.Parameters.Expressions
{
    public class SubtractBinaryOperatorParameters : BinaryOperatorParameters
    {
		public SubtractBinaryOperatorParameters()
		{
		}

		public SubtractBinaryOperatorParameters(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}