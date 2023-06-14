using System.Collections.Generic;

namespace Contoso.Parameters.Expansions
{
    public class SortCollectionParameters
    {
        public SortCollectionParameters()
        {
        }

        public SortCollectionParameters(List<SortDescriptionParameters> sortDescriptions, int? skip = null, int? take = null)
        {
            SortDescriptions = sortDescriptions;
            Skip = skip;
            Take = take;
        }

        public List<SortDescriptionParameters> SortDescriptions { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
