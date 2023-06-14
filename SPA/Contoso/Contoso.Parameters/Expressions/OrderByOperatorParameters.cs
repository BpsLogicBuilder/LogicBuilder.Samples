using LogicBuilder.Expressions.Utils.Strutures;

namespace Contoso.Parameters.Expressions
{
    public class OrderByOperatorParameters : SelectorMethodOperatorParametersBase
    {
		public OrderByOperatorParameters()
		{
		}

		public OrderByOperatorParameters(IExpressionParameter sourceOperand, IExpressionParameter selectorBody, ListSortDirection sortDirection, string selectorParameterName) : base(sourceOperand, selectorBody, selectorParameterName)
		{
			SortDirection = sortDirection;
		}

		public ListSortDirection SortDirection { get; set; }
    }
}