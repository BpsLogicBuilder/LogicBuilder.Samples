namespace Contoso.Parameters.Expressions
{
    public class AllOperatorParameters : FilterMethodOperatorParametersBase
    {
		public AllOperatorParameters()
		{
		}

		public AllOperatorParameters(IExpressionParameter sourceOperand, IExpressionParameter filterBody = null, string filterParameterName = null) : base(sourceOperand, filterBody, filterParameterName)
		{
		}
    }
}