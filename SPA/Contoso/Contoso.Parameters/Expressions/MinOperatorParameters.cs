namespace Contoso.Parameters.Expressions
{
    public class MinOperatorParameters : SelectorMethodOperatorParametersBase
    {
		public MinOperatorParameters()
		{
		}

		public MinOperatorParameters(IExpressionParameter sourceOperand, IExpressionParameter selectorBody = null, string selectorParameterName = null) : base(sourceOperand, selectorBody, selectorParameterName)
		{
		}
    }
}