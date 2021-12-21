namespace Contoso.Parameters.Expressions
{
    public class TrimOperatorParameters : IExpressionParameter
    {
		public TrimOperatorParameters()
		{
		}

		public TrimOperatorParameters(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}