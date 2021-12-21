namespace Contoso.Parameters.Expressions
{
    public class ToListOperatorParameters : IExpressionParameter
    {
		public ToListOperatorParameters()
		{
		}

		public ToListOperatorParameters(IExpressionParameter sourceOperand)
		{
			SourceOperand = sourceOperand;
		}

		public IExpressionParameter SourceOperand { get; set; }
    }
}