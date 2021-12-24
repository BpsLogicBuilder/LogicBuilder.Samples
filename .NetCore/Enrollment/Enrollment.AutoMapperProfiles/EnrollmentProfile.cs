using AutoMapper;
using Enrollment.Data.Automatic;
using Enrollment.Data.Entities;
using Enrollment.Data.Rules;
using Enrollment.Domain.Entities;

namespace Enrollment.AutoMapperProfiles
{
    public class EnrollmentProfile : Profile
    {
        public EnrollmentProfile()
        {
            CreateMap<Personal, PersonalModel>()
                .ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<PersonalModel, Personal>()
                .ForMember(dest => dest.User, opts => opts.Ignore());

            CreateMap<Academic, AcademicModel>()
                .ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<AcademicModel, Academic>()
                .ForMember(dest => dest.User, opts => opts.Ignore());

            CreateMap<Admissions, AdmissionsModel>()
                .ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<AdmissionsModel, Admissions>()
                .ForMember(dest => dest.User, opts => opts.Ignore());

            CreateMap<Certification, CertificationModel>()
                .ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<CertificationModel, Certification>()
                .ForMember(dest => dest.User, opts => opts.Ignore());

            CreateMap<ContactInfo, ContactInfoModel>()
                .ForMember(dest => dest.ConfirmSocialSecurityNumber, opts => opts.MapFrom(src => src.SocialSecurityNumber))
                .ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<ContactInfoModel, ContactInfo>()
                .ForMember(dest => dest.User, opts => opts.Ignore());

            CreateMap<Institution, InstitutionModel>().ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<InstitutionModel, Institution>()
                .ForMember(dest => dest.Academic, opts => opts.Ignore());

            CreateMap<MoreInfo, MoreInfoModel>().ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<MoreInfoModel, MoreInfo>()
                .ForMember(dest => dest.User, opts => opts.Ignore());

            CreateMap<Residency, ResidencyModel>().ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<ResidencyModel, Residency>()
                .ForMember(dest => dest.User, opts => opts.Ignore());

            CreateMap<StateLivedIn, StateLivedInModel>().ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<StateLivedInModel, StateLivedIn>()
                .ForMember(dest => dest.Residency, opts => opts.Ignore());

            CreateMap<User, UserModel>().ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<UserModel, User>();

            CreateMap<LookUps, LookUpsModel>().ForAllMembers(o => o.ExplicitExpansion());
            CreateMap<LookUpsModel, LookUps>();

            CreateMap<RulesModule, RulesModuleModel>()
                .ForMember(dest => dest.NamePlusApplication, opts => opts.MapFrom(x => x.Name + "|" + x.Application))
                .ReverseMap();
            CreateMap<VariableMetaData, VariableMetaDataModel>().ReverseMap();
        }
    }
}
