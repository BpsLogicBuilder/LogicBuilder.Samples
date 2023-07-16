namespace Contoso.Parameters.Expressions
{
    public class ConvertToNumericDateOperatorParameters : IExpressionParameter
    {
		public ConvertToNumericDateOperatorParameters()
		{
		}

		public ConvertToNumericDateOperatorParameters(IExpressionParameter sourceOperand)
		{
			SourceOperand = sourceOperand;
		}

		public IExpressionParameter SourceOperand { get; set; }
    }
}