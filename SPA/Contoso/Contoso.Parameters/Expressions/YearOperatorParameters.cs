namespace Contoso.Parameters.Expressions
{
    public class YearOperatorParameters : IExpressionParameter
    {
		public YearOperatorParameters()
		{
		}

		public YearOperatorParameters(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}