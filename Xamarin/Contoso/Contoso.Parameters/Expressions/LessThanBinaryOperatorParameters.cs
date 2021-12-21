namespace Contoso.Parameters.Expressions
{
    public class LessThanBinaryOperatorParameters : BinaryOperatorParameters
    {
		public LessThanBinaryOperatorParameters()
		{
		}

		public LessThanBinaryOperatorParameters(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}