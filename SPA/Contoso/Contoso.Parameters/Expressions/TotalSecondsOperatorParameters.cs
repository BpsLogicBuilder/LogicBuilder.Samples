namespace Contoso.Parameters.Expressions
{
    public class TotalSecondsOperatorParameters : IExpressionParameter
    {
		public TotalSecondsOperatorParameters()
		{
		}

		public TotalSecondsOperatorParameters(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}