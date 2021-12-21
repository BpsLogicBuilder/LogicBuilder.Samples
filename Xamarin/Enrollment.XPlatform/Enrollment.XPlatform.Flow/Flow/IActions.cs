using Enrollment.Forms.Parameters.Navigation;
using LogicBuilder.Attributes;

namespace Enrollment.XPlatform.Flow
{
    public interface IActions
    {
        [AlsoKnownAs("SetupNavigationMenu")]
        void UpdateNavigationBar(NavigationBarParameters navBar);
    }
}
