namespace Contoso.Parameters.Expressions
{
    public class AverageOperatorParameters : SelectorMethodOperatorParametersBase
    {
		public AverageOperatorParameters()
		{
		}

		public AverageOperatorParameters(IExpressionParameter sourceOperand, IExpressionParameter selectorBody = null, string selectorParameterName = null) : base(sourceOperand, selectorBody, selectorParameterName)
		{
		}
    }
}