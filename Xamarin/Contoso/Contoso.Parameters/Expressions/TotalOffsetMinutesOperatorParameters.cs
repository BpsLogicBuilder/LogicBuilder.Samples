namespace Contoso.Parameters.Expressions
{
    public class TotalOffsetMinutesOperatorParameters : IExpressionParameter
    {
		public TotalOffsetMinutesOperatorParameters()
		{
		}

		public TotalOffsetMinutesOperatorParameters(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}