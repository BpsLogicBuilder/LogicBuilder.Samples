namespace Contoso.Parameters.Expressions
{
    public class SelectManyOperatorParameters : SelectorMethodOperatorParametersBase
    {
		public SelectManyOperatorParameters()
		{
		}

		public SelectManyOperatorParameters(IExpressionParameter sourceOperand, IExpressionParameter selectorBody, string selectorParameterName) : base(sourceOperand, selectorBody, selectorParameterName)
		{
		}
    }
}