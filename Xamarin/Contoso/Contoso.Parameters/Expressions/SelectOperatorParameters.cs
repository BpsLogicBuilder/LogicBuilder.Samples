namespace Contoso.Parameters.Expressions
{
    public class SelectOperatorParameters : SelectorMethodOperatorParametersBase
    {
		public SelectOperatorParameters()
		{
		}

		public SelectOperatorParameters(IExpressionParameter sourceOperand, IExpressionParameter selectorBody, string selectorParameterName) : base(sourceOperand, selectorBody, selectorParameterName)
		{
		}
    }
}