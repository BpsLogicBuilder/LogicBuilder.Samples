using System.Collections.Generic;

namespace Enrollment.Spa.Flow.ScreenSettings.Navigation
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

        public int TargetModule { get; set; }
        public string InitialModule { get; set; }
        public string Text { get; set; }
        public bool Active { get; set; }
        public List<NavigationMenuItem> SubItems { get; set; }
    }
}
