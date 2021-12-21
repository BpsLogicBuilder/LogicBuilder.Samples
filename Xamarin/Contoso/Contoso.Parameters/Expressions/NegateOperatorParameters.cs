namespace Contoso.Parameters.Expressions
{
    public class NegateOperatorParameters : IExpressionParameter
    {
		public NegateOperatorParameters()
		{
		}

		public NegateOperatorParameters(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}