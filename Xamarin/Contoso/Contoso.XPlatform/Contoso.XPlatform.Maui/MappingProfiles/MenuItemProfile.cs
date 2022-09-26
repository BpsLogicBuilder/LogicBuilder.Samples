using AutoMapper;
using Contoso.Forms.Configuration.Navigation;
using Contoso.XPlatform.ViewModels;

namespace Contoso.XPlatform.MappingProfiles
{
    public class MenuItemProfile : Profile
    {
        public MenuItemProfile()
        {
            CreateMap<NavigationMenuItemDescriptor, FlyoutMenuItem>();
        }
    }
}
