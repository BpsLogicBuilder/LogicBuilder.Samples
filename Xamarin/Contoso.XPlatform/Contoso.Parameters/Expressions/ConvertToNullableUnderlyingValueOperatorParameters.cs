namespace Contoso.Parameters.Expressions
{
    public class ConvertToNullableUnderlyingValueOperatorParameters : IExpressionParameter
    {
		public ConvertToNullableUnderlyingValueOperatorParameters()
		{
		}

		public ConvertToNullableUnderlyingValueOperatorParameters(IExpressionParameter sourceOperand)
		{
			SourceOperand = sourceOperand;
		}

		public IExpressionParameter SourceOperand { get; set; }
    }
}