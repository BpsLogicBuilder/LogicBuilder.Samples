namespace Contoso.Parameters.Expressions
{
    public class RoundOperatorParameters : IExpressionParameter
    {
		public RoundOperatorParameters()
		{
		}

		public RoundOperatorParameters(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}