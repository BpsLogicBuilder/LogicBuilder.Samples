using System.Collections.Generic;

namespace Enrollment.Forms.View.Expansions
{
    public class SortCollectionView
    {
        public ICollection<SortDescriptionView> SortDescriptions { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
