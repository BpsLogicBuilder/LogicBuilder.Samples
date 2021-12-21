using Contoso.Parameters.Expressions;

namespace Contoso.Parameters.Expansions
{
    public class SelectExpandItemFilterParameters
    {
        public SelectExpandItemFilterParameters()
        {
        }

        public SelectExpandItemFilterParameters(FilterLambdaOperatorParameters filterLambdaOperator)
        {
            FilterLambdaOperator = filterLambdaOperator;
        }

        public FilterLambdaOperatorParameters FilterLambdaOperator { get; set; }
    }
}
