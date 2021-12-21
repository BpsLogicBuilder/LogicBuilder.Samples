using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Navigation
{
    public class NavigationBarDescriptor
    {
        public string BrandText { get; set; }
        public string CurrentModule { get; set; }
        public List<NavigationMenuItemDescriptor> MenuItems { get; set; }
    }
}
