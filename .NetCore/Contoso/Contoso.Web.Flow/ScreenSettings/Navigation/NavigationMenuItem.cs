using Contoso.Web.Flow.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Web.Flow.ScreenSettings.Navigation
{
    public class NavigationMenuItem
    {
        public NavigationMenuItem(int targetModule, string initialModule = "initial", string Text = "menuText", List<NavigationMenuItem> SubItems = null)
        {
            this.TargetModule = targetModule;
            this.InitialModule = initialModule;
            this.Text = Text;
            this.SubItems = SubItems;
        }

        public NavigationMenuItem()
        {
        }

        public int TargetModule { get; set; }
        public string InitialModule { get; set; }
        public string Text { get; set; }
        public bool Active { get; set; }
        public List<NavigationMenuItem> SubItems { get; set; }
    }
}
