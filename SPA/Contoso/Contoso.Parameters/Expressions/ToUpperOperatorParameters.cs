namespace Contoso.Parameters.Expressions
{
    public class ToUpperOperatorParameters : IExpressionParameter
    {
		public ToUpperOperatorParameters()
		{
		}

		public ToUpperOperatorParameters(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}