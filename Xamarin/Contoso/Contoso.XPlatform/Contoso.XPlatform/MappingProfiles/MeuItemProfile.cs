﻿using AutoMapper;
using Contoso.Forms.Configuration.Navigation;
using Contoso.XPlatform.ViewModels;

namespace Contoso.XPlatform.MappingProfiles
{
    public class MeuItemProfile : Profile
    {
        public MeuItemProfile()
        {
            CreateMap<NavigationMenuItemDescriptor, FlyoutMenuItem>();
        }
    }
}
