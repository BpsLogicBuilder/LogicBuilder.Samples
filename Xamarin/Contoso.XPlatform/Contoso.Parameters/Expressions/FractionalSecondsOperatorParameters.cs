namespace Contoso.Parameters.Expressions
{
    public class FractionalSecondsOperatorParameters : IExpressionParameter
    {
		public FractionalSecondsOperatorParameters()
		{
		}

		public FractionalSecondsOperatorParameters(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}