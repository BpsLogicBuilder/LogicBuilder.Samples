using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.Navigation
{
    public class NavigationBarParameters
    {
        public NavigationBarParameters
        (
            [Comments("Brand text for the navigation bar.")]
            string brandText = "Contoso",

            [Comments("Current module indicator used to determine which menu item gets set to active.")]
            string currentModule = "initial",

            [Comments("List of menu items")]
            List<NavigationMenuItemParameters> MenuItems = null
        )
        {
            this.BrandText = brandText;
            this.CurrentModule = currentModule;
            this.MenuItems = MenuItems ?? new List<NavigationMenuItemParameters>();
        }

        public NavigationBarParameters()
        {
        }

        public string BrandText { get; set; }
        public string CurrentModule { get; set; }
        public List<NavigationMenuItemParameters> MenuItems { get; set; }
    }
}
