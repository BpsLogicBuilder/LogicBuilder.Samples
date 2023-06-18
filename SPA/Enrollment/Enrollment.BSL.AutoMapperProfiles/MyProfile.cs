using AutoMapper;
using Enrollment.Data.Entities;
using Enrollment.Domain.Entities;

namespace Enrollment.BSL.AutoMapperProfiles
{
    public class MyProfile : Profile
    {
        public MyProfile()
        {
            CreateMap<PersonModel, Person>()
                .ReverseMap()
            .ForMember(dest => dest.FullName, opts => opts.MapFrom(x => x.FirstName + " " + x.LastName))
            .ForMember
            (
                dest => dest.DateOfBirthString,
                opts => opts.MapFrom
                (
                    x => Contexts.BaseDbContextSqlFunctions.FormatDateTime(x.DateOfBirth, "MM/dd/yyyy", "en-US")
                )
            )
            .ForAllMembers(o => o.ExplicitExpansion());

            CreateMap<LookUpsModel, LookUps>().ReverseMap();
        }
    }
}