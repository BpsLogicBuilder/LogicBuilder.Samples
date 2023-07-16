using AutoMapper;
using Contoso.Common.Configuration.ExpansionDescriptors;
using Contoso.Parameters.Expansions;

namespace Contoso.AutoMapperProfiles
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
