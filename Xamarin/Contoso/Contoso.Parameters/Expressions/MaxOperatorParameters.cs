namespace Contoso.Parameters.Expressions
{
    public class MaxOperatorParameters : SelectorMethodOperatorParametersBase
    {
		public MaxOperatorParameters()
		{
		}

		public MaxOperatorParameters(IExpressionParameter sourceOperand, IExpressionParameter selectorBody = null, string selectorParameterName = null) : base(sourceOperand, selectorBody, selectorParameterName)
		{
		}
    }
}