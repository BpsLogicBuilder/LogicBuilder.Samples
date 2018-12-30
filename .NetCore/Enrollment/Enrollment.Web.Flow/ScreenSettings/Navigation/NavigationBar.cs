using Enrollment.Web.Flow.Cache;
using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Web.Flow.ScreenSettings.Navigation
{
    public class NavigationBar
    {
        public NavigationBar
        (
            [Comments("Brand text for the navigation bar.")]
            string brandText = "Contoso",

            [Comments("Current module indicator used to determine which menu item gets set to active.")]
            int currentModule = TargetModules.Admin,

            [Comments("True if the grid is sortable otherwise false")]
            List<NavigationMenuItem> MenuItems = null
        )
        {
            this.BrandText = brandText;
            this.CurrentModule = currentModule;
            this.MenuItems = MenuItems ?? new List<NavigationMenuItem>();
        }

        public NavigationBar()
        {
        }

        public string BrandText { get; set; }
        public int CurrentModule { get; set; }
        public List<NavigationMenuItem> MenuItems { get; set; }
    }
}
