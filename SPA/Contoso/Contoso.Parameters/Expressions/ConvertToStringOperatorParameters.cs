namespace Contoso.Parameters.Expressions
{
    public class ConvertToStringOperatorParameters : IExpressionParameter
    {
		public ConvertToStringOperatorParameters()
		{
		}

		public ConvertToStringOperatorParameters(IExpressionParameter sourceOperand)
		{
			SourceOperand = sourceOperand;
		}

		public IExpressionParameter SourceOperand { get; set; }
    }
}