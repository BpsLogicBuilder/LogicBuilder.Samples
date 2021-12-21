using AutoMapper;
using Enrollment.Common.Configuration.ItemFilter;
using Enrollment.Parameters.ItemFilter;

namespace Enrollment.AutoMapperProfiles
{
    public class ItemFilterParameterToDescriptorMappingProfile : Profile
    {
        public ItemFilterParameterToDescriptorMappingProfile()
        {
            CreateMap<ItemFilterGroupParameters, ItemFilterGroupDescriptor>();

            CreateMap<MemberSourceFilterParameters, MemberSourceFilterDescriptor>()
                .ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));

            CreateMap<ValueSourceFilterParameters, ValueSourceFilterDescriptor>()
                .ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));

            CreateMap<ItemFilterParametersBase, ItemFilterDescriptorBase>()
                .Include<ItemFilterGroupParameters, ItemFilterGroupDescriptor>()
                .Include<MemberSourceFilterParameters, MemberSourceFilterDescriptor>()
                .Include<ValueSourceFilterParameters, ValueSourceFilterDescriptor>();
        }
    }
}
