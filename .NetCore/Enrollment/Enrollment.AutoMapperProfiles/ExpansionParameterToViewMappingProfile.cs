using AutoMapper;
using Enrollment.Forms.Parameters.Expansions;
using Enrollment.Forms.View.Expansions;

namespace Enrollment.AutoMapperProfiles
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
