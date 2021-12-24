using System.Collections.Generic;

namespace Contoso.Forms.View.Expansions
{
    public class SelectExpandDefinitionView
    {
        public List<string> Selects { get; set; } = new List<string>();
        public List<SelectExpandItemView> ExpandedItems { get; set; } = new List<SelectExpandItemView>();
    }
}
