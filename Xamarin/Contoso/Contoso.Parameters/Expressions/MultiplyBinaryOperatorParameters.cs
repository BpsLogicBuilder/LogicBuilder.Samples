namespace Contoso.Parameters.Expressions
{
    public class MultiplyBinaryOperatorParameters : BinaryOperatorParameters
    {
		public MultiplyBinaryOperatorParameters()
		{
		}

		public MultiplyBinaryOperatorParameters(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}