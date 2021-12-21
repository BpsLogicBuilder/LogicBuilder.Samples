namespace Contoso.Parameters.Expressions
{
    public class HourOperatorParameters : IExpressionParameter
    {
		public HourOperatorParameters()
		{
		}

		public HourOperatorParameters(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}