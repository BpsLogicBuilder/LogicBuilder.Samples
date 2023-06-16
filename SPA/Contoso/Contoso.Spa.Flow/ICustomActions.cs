using Contoso.Spa.Flow.ScreenSettings.Navigation;
using LogicBuilder.Attributes;

namespace Contoso.Spa.Flow
{
    public interface ICustomActions
    {
        [AlsoKnownAs("SetupNavigationMenu")]
        void UpdateNavigationBar(NavigationBar navBar);
    }
}
