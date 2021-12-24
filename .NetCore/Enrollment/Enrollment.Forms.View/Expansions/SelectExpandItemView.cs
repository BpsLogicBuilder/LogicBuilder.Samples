using System.Collections.Generic;

namespace Enrollment.Forms.View.Expansions
{
    public class SelectExpandItemView
    {
        public string MemberName { get; set; }
        public SelectExpandItemQueryFunctionView QueryFunction { get; set; }
        public List<string> Selects { get; set; } = new List<string>();
        public List<SelectExpandItemView> ExpandedItems { get; set; } = new List<SelectExpandItemView>();
    }
}
