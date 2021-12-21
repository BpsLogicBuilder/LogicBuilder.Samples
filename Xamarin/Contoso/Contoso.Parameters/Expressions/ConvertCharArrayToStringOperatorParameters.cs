namespace Contoso.Parameters.Expressions
{
    public class ConvertCharArrayToStringOperatorParameters : IExpressionParameter
    {
		public ConvertCharArrayToStringOperatorParameters()
		{
		}

		public ConvertCharArrayToStringOperatorParameters(IExpressionParameter sourceOperand)
		{
			SourceOperand = sourceOperand;
		}

		public IExpressionParameter SourceOperand { get; set; }
    }
}