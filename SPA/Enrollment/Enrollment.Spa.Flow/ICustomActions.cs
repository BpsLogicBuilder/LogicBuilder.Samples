using Enrollment.Spa.Flow.ScreenSettings.Navigation;
using LogicBuilder.Attributes;

namespace Enrollment.Spa.Flow
{
    public interface ICustomActions
    {
        [AlsoKnownAs("SetupNavigationMenu")]
        void UpdateNavigationBar(NavigationBar navBar);
    }
}
