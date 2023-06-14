namespace Contoso.Parameters.Expressions
{
    public class NotOperatorParameters : IExpressionParameter
    {
		public NotOperatorParameters()
		{
		}

		public NotOperatorParameters(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}