namespace Contoso.Parameters.Expressions
{
    public class ConvertToNumericTimeOperatorParameters : IExpressionParameter
    {
		public ConvertToNumericTimeOperatorParameters()
		{
		}

		public ConvertToNumericTimeOperatorParameters(IExpressionParameter sourceOperand)
		{
			SourceOperand = sourceOperand;
		}

		public IExpressionParameter SourceOperand { get; set; }
    }
}