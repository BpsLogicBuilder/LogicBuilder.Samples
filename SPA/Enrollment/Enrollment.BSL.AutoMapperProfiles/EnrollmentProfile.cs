using AutoMapper;
using Enrollment.Data.Entities;
using Enrollment.Domain.Entities;

namespace Enrollment.BSL.AutoMapperProfiles
{
    public class EnrollmentProfile : Profile
    {
        public EnrollmentProfile()
        {
            CreateMap<Personal, PersonalModel>()
                .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.UserIdString, opts => opts.MapFrom(src => src.UserId.ToString()))
                .ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<PersonalModel, Personal>();

            CreateMap<Academic, AcademicModel>()
                .ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<AcademicModel, Academic>();

            CreateMap<Admissions, AdmissionsModel>()
                .ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<AdmissionsModel, Admissions>();

            CreateMap<Certification, CertificationModel>()
                .ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<CertificationModel, Certification>();

            CreateMap<ContactInfo, ContactInfoModel>()
                .ForMember(dest => dest.ConfirmSocialSecurityNumber, opts => opts.MapFrom(src => src.SocialSecurityNumber))
                .ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<ContactInfoModel, ContactInfo>();

            CreateMap<Institution, InstitutionModel>().ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<InstitutionModel, Institution>();

            CreateMap<MoreInfo, MoreInfoModel>().ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<MoreInfoModel, MoreInfo>();

            CreateMap<Residency, ResidencyModel>().ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<ResidencyModel, Residency>();

            CreateMap<StateLivedIn, StateLivedInModel>().ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<StateLivedInModel, StateLivedIn>();

            CreateMap<User, UserModel>().ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<UserModel, User>();

            CreateMap<LookUps, LookUpsModel>().ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<LookUpsModel, LookUps>();
        }
    }
}