namespace Contoso.Parameters.Expressions
{
    public class MinuteOperatorParameters : IExpressionParameter
    {
		public MinuteOperatorParameters()
		{
		}

		public MinuteOperatorParameters(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}