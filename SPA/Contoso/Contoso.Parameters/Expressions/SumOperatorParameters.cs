namespace Contoso.Parameters.Expressions
{
    public class SumOperatorParameters : SelectorMethodOperatorParametersBase
    {
		public SumOperatorParameters()
		{
		}

		public SumOperatorParameters(IExpressionParameter sourceOperand, IExpressionParameter selectorBody = null, string selectorParameterName = null) : base(sourceOperand, selectorBody, selectorParameterName)
		{
		}
    }
}