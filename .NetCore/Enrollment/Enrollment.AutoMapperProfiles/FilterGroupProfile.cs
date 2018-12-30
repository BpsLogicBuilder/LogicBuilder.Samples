using AutoMapper;
using Enrollment.Forms.Parameters.Common;
using LogicBuilder.Expressions.Utils.DataSource;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.AutoMapperProfiles
{
    public class FilterGroupProfile : Profile
    {
        public FilterGroupProfile()
        {
            CreateMap<FilterGroupParameters, FilterGroup>().ReverseMap();
            CreateMap<FilterDefinitionParameters, Filter>()
                    .ForMember(dest => dest.Filters, opt => opt.Ignore())
                    .ForMember(dest => dest.Logic, opt => opt.Ignore())
                    .ForMember(dest => dest.ValueSourceType, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
