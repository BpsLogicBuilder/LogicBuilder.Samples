using AutoMapper;
using Contoso.Forms.View.Expansions;
using LogicBuilder.Expressions.Utils.Expansions;
using LogicBuilder.Expressions.Utils.Strutures;

namespace Contoso.AutoMapperProfiles
{
    public class ExpansionViewToOperatorMappingProfile : Profile
    {
        public ExpansionViewToOperatorMappingProfile()
        {
            CreateMap<SelectExpandDefinitionView, SelectExpandDefinition>();
            CreateMap<SelectExpandItemView, SelectExpandItem>();
            CreateMap<SelectExpandItemQueryFunctionView, SelectExpandItemQueryFunction>();
            CreateMap<SortCollectionView, SortCollection>()
                .ForMember(dest => dest.Skip, opts => opts.MapFrom(src => src.Skip.HasValue ? src.Skip.Value : 0))
                .ForMember(dest => dest.Take, opts => opts.MapFrom(src => src.Take.HasValue ? src.Take.Value : int.MaxValue));
            CreateMap<SortDescriptionView, SortDescription>();
        }
    }
}
