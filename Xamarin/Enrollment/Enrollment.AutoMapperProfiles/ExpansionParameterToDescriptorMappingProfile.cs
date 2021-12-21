using AutoMapper;
using Enrollment.Common.Configuration.ExpansionDescriptors;
using Enrollment.Parameters.Expansions;

namespace Enrollment.AutoMapperProfiles
{
    public class ExpansionParameterToDescriptorMappingProfile : Profile
    {
        public ExpansionParameterToDescriptorMappingProfile()
        {
            CreateMap<SelectExpandDefinitionParameters, SelectExpandDefinitionDescriptor>();
            CreateMap<SelectExpandItemFilterParameters, SelectExpandItemFilterDescriptor>();
            CreateMap<SelectExpandItemParameters, SelectExpandItemDescriptor>();
            CreateMap<SelectExpandItemQueryFunctionParameters, SelectExpandItemQueryFunctionDescriptor>();
            CreateMap<SortCollectionParameters, SortCollectionDescriptor>();
            CreateMap<SortDescriptionParameters, SortDescriptionDescriptor>();
        }
    }
}
