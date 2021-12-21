namespace Contoso.Parameters.Expressions
{
    public class SubstringOperatorParameters : IExpressionParameter
    {
		public SubstringOperatorParameters()
		{
		}

		public SubstringOperatorParameters(IExpressionParameter sourceOperand, params IExpressionParameter[] indexes)
		{
			SourceOperand = sourceOperand;
			Indexes = indexes;
		}

		public IExpressionParameter SourceOperand { get; set; }
		public IExpressionParameter[] Indexes { get; set; }
    }
}