using AutoMapper;
using Enrollment.Forms.Configuration.Navigation;
using Enrollment.XPlatform.ViewModels;

namespace Enrollment.XPlatform.MappingProfiles
{
    public class MeuItemProfile : Profile
    {
        public MeuItemProfile()
        {
            CreateMap<NavigationMenuItemDescriptor, FlyoutMenuItem>();
        }
    }
}
