using System.Collections.Generic;

namespace Contoso.Forms.View.Expansions
{
    public class SortCollectionView
    {
        public ICollection<SortDescriptionView> SortDescriptions { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
