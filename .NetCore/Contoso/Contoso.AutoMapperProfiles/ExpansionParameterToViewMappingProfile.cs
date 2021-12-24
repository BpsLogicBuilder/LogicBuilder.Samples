using AutoMapper;
using Contoso.Forms.Parameters.Expansions;
using Contoso.Forms.View.Expansions;

namespace Contoso.AutoMapperProfiles
{
    public class ExpansionParameterToViewMappingProfile : Profile
    {
        public ExpansionParameterToViewMappingProfile()
        {
            CreateMap<SelectExpandDefinitionParameters, SelectExpandDefinitionView>();
            CreateMap<SelectExpandItemParameters, SelectExpandItemView>();
            CreateMap<SelectExpandItemQueryFunctionParameters, SelectExpandItemQueryFunctionView>();
            CreateMap<SortCollectionParameters, SortCollectionView>();
            CreateMap<SortDescriptionParameters, SortDescriptionView>();
        }
    }
}
