using LogicBuilder.Expressions.Utils.Strutures;

namespace Enrollment.Parameters.Expressions
{
    public class ThenByOperatorParameters : SelectorMethodOperatorParametersBase
    {
        public ThenByOperatorParameters()
        {
        }

        public ThenByOperatorParameters(IExpressionParameter sourceOperand, IExpressionParameter selectorBody, ListSortDirection sortDirection, string selectorParameterName) : base(sourceOperand, selectorBody, selectorParameterName)
        {
            SortDirection = sortDirection;
        }

        public ListSortDirection SortDirection { get; set; }
    }
}