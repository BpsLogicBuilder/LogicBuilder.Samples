using AutoMapper;
using Contoso.Forms.Configuration;
using Contoso.Forms.Parameters;
using LogicBuilder.Forms.Parameters;

namespace Contoso.XPlatform.AutoMapperProfiles
{
    public class CommandButtonProfile : Profile
    {
        public CommandButtonProfile()
        {
            CreateMap<ConnectorParameters, CommandButtonDescriptor>()
                .ForMember(dest => dest.ButtonIcon, opts => opts.MapFrom(src => ((CommandButtonParameters)src.ConnectorData).ButtonIcon))
                .ForMember(dest => dest.Command, opts => opts.MapFrom(src => ((CommandButtonParameters)src.ConnectorData).Command));
            CreateMap<CommandButtonParameters, CommandButtonDescriptor>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ShortString, opt => opt.Ignore())
                .ForMember(dest => dest.LongString, opt => opt.Ignore());
        }
    }
}
