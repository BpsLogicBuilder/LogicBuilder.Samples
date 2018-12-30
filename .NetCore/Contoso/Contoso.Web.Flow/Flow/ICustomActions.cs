using Contoso.Web.Flow.ScreenSettings.Navigation;
using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Web.Flow
{
    public interface ICustomActions
    {
        [AlsoKnownAs("SetupNavigationMenu")]
        void UpdateNavigationBar(NavigationBar navBar);
    }
}
