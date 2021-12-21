namespace Contoso.Parameters.Expressions
{
    public class OrBinaryOperatorParameters : BinaryOperatorParameters
    {
		public OrBinaryOperatorParameters()
		{
		}

		public OrBinaryOperatorParameters(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}