namespace Contoso.Parameters.Expressions
{
    public class WhereOperatorParameters : FilterMethodOperatorParametersBase
    {
		public WhereOperatorParameters()
		{
		}

		public WhereOperatorParameters(IExpressionParameter sourceOperand, IExpressionParameter filterBody, string filterParameterName) : base(sourceOperand, filterBody, filterParameterName)
		{
		}
    }
}