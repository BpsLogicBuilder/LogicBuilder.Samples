using AutoMapper;
using Enrollment.Forms.Configuration.Navigation;
using Enrollment.XPlatform.ViewModels;

namespace Enrollment.XPlatform.MappingProfiles
{
    public class MenuItemProfile : Profile
    {
        public MenuItemProfile()
        {
            CreateMap<NavigationMenuItemDescriptor, FlyoutMenuItem>();
        }
    }
}
