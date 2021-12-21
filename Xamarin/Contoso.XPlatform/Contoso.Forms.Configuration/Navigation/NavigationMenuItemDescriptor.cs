using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Navigation
{
    public class NavigationMenuItemDescriptor
    {
        public string InitialModule { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public bool Active { get; set; }
        public List<NavigationMenuItemDescriptor> SubItems { get; set; }
    }
}
