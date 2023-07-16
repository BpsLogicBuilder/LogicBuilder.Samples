using AutoMapper;
using Contoso.Common.Configuration.ExpansionDescriptors;
using LogicBuilder.Expressions.Utils.Expansions;
using LogicBuilder.Expressions.Utils.Strutures;

namespace Contoso.AutoMapperProfiles
{
    public class ExpansionDescriptorToOperatorMappingProfile : Profile
    {
        public ExpansionDescriptorToOperatorMappingProfile()
        {
            CreateMap<SelectExpandDefinitionDescriptor, SelectExpandDefinition>();
            CreateMap<SelectExpandItemFilterDescriptor, SelectExpandItemFilter>();
            CreateMap<SelectExpandItemDescriptor, SelectExpandItem>();
            CreateMap<SelectExpandItemQueryFunctionDescriptor, SelectExpandItemQueryFunction>();
            CreateMap<SortCollectionDescriptor, SortCollection>()
                .ForMember(dest => dest.Skip, opts => opts.MapFrom(src => src.Skip.HasValue ? src.Skip.Value : 0))
                .ForMember(dest => dest.Take, opts => opts.MapFrom(src => src.Take.HasValue ? src.Take.Value : int.MaxValue));
            CreateMap<SortDescriptionDescriptor, SortDescription>();
        }
    }
}
