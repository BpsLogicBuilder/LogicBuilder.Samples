using System.Collections.Generic;

namespace Contoso.Spa.Flow.ScreenSettings.Navigation
{
    public class NavigationMenuItem(int targetModule, string initialModule = "initial", string Text = "menuText", List<NavigationMenuItem> SubItems = null!)
    {
        public int TargetModule { get; set; } = targetModule;
        public string InitialModule { get; set; } = initialModule;
        public string Text { get; set; } = Text;
        public bool Active { get; set; }
        public List<NavigationMenuItem> SubItems { get; set; } = SubItems ?? [];
    }
}
