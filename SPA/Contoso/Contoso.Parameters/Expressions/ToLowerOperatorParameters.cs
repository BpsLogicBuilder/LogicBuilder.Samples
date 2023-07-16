namespace Contoso.Parameters.Expressions
{
    public class ToLowerOperatorParameters : IExpressionParameter
    {
		public ToLowerOperatorParameters()
		{
		}

		public ToLowerOperatorParameters(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}