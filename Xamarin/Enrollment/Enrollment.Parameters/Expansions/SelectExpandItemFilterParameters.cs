using Enrollment.Parameters.Expressions;

namespace Enrollment.Parameters.Expansions
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
