namespace Contoso.Parameters.Expressions
{
    public class TimeOperatorParameters : IExpressionParameter
    {
		public TimeOperatorParameters()
		{
		}

		public TimeOperatorParameters(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}