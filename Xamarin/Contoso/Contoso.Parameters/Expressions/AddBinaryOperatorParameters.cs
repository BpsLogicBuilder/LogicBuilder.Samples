namespace Contoso.Parameters.Expressions
{
    public class AddBinaryOperatorParameters : BinaryOperatorParameters
    {
		public AddBinaryOperatorParameters()
		{
		}

		public AddBinaryOperatorParameters(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}