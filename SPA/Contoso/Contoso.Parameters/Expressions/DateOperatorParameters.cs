namespace Contoso.Parameters.Expressions
{
    public class DateOperatorParameters : IExpressionParameter
    {
		public DateOperatorParameters()
		{
		}

		public DateOperatorParameters(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}