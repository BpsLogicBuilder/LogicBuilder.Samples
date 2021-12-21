namespace Contoso.Parameters.Expressions
{
    public class DayOperatorParameters : IExpressionParameter
    {
		public DayOperatorParameters()
		{
		}

		public DayOperatorParameters(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}