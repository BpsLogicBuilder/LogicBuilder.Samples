namespace Contoso.Parameters.Expressions
{
    public class GroupByOperatorParameters : SelectorMethodOperatorParametersBase
    {
		public GroupByOperatorParameters()
		{
		}

		public GroupByOperatorParameters(IExpressionParameter sourceOperand, IExpressionParameter selectorBody, string selectorParameterName) : base(sourceOperand, selectorBody, selectorParameterName)
		{
		}
    }
}