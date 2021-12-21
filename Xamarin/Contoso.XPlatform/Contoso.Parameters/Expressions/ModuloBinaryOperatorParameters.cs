namespace Contoso.Parameters.Expressions
{
    public class ModuloBinaryOperatorParameters : BinaryOperatorParameters
    {
		public ModuloBinaryOperatorParameters()
		{
		}

		public ModuloBinaryOperatorParameters(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}