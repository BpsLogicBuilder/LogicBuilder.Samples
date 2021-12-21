namespace Contoso.Parameters.Expressions
{
    public class MonthOperatorParameters : IExpressionParameter
    {
		public MonthOperatorParameters()
		{
		}

		public MonthOperatorParameters(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}