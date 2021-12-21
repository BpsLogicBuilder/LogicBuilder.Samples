using Contoso.Forms.Parameters.Navigation;
using LogicBuilder.Attributes;

namespace Contoso.XPlatform.Flow
{
    public interface IActions
    {
        [AlsoKnownAs("SetupNavigationMenu")]
        void UpdateNavigationBar(NavigationBarParameters navBar);
    }
}
