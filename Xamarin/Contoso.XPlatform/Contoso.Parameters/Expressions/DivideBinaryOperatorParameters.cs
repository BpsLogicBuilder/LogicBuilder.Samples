namespace Contoso.Parameters.Expressions
{
    public class DivideBinaryOperatorParameters : BinaryOperatorParameters
    {
		public DivideBinaryOperatorParameters()
		{
		}

		public DivideBinaryOperatorParameters(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}