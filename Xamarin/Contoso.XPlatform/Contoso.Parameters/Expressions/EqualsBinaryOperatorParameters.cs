namespace Contoso.Parameters.Expressions
{
    public class EqualsBinaryOperatorParameters : BinaryOperatorParameters
    {
		public EqualsBinaryOperatorParameters()
		{
		}

		public EqualsBinaryOperatorParameters(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}