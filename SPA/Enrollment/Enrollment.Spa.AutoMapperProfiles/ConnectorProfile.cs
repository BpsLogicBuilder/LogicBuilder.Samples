﻿using AutoMapper;
using Enrollment.Forms.Parameters;
using Enrollment.Forms.View;
using LogicBuilder.Forms.Parameters;

namespace Enrollment.Spa.AutoMapperProfiles
{
    public class ConnectorProfile : Profile
    {
        public ConnectorProfile()
        {
            CreateMap<CommandButtonView, ConnectorParameters>()
                .ConstructUsing((src, context) => new ConnectorParameters
                {
                    ConnectorData = context.Mapper.Map<CommandButtonParameters>(src)
                })
                .ForMember(dest => dest.ConnectorData, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.ButtonIcon, opts => opts.MapFrom(src => ((CommandButtonParameters)src.ConnectorData).ButtonIcon))
                .ForMember(dest => dest.Cancel, opts => opts.MapFrom(src => ((CommandButtonParameters)src.ConnectorData).Cancel))
                .ForMember(dest => dest.ClassString, opts => opts.MapFrom(src => ((CommandButtonParameters)src.ConnectorData).ClassString))
                .ForMember(dest => dest.GridCommandButton, opts => opts.MapFrom(src => ((CommandButtonParameters)src.ConnectorData).GridCommandButton))
                .ForMember(dest => dest.GridId, opts => opts.MapFrom(src => ((CommandButtonParameters)src.ConnectorData).GridId));
            CreateMap<CommandButtonParameters, CommandButtonView>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ShortString, opt => opt.Ignore())
                .ForMember(dest => dest.LongString, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
