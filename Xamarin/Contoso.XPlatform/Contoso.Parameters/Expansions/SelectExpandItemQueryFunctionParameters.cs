using Contoso.Parameters.Expressions;

namespace Contoso.Parameters.Expansions
{
    public class SelectExpandItemQueryFunctionParameters
    {
        public SelectExpandItemQueryFunctionParameters()
        {
        }

        public SelectExpandItemQueryFunctionParameters(SortCollectionParameters sortCollection = null)
        {
            SortCollection = sortCollection;
        }

        public SortCollectionParameters SortCollection { get; set; }
    }
}
