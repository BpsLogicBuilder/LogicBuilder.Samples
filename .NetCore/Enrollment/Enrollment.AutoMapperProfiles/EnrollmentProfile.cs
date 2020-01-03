using AutoMapper;
using Enrollment.Data.Automatic;
using Enrollment.Data.Entities;
using Enrollment.Data.Rules;
using Enrollment.Domain.Entities;
using System;

namespace Enrollment.AutoMapperProfiles
{
    public class EnrollmentProfile : Profile
    {
        public EnrollmentProfile()
        {
            CreateMap<Personal, PersonalModel>().ReverseMap();
            CreateMap<Academic, AcademicModel>().ReverseMap();
            CreateMap<Admissions, AdmissionsModel>().ReverseMap();
            CreateMap<Certification, CertificationModel>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoModel>()
                .ForMember(dest => dest.ConfirmSocialSecurityNumber, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Institution, InstitutionModel>().ReverseMap();
            CreateMap<MoreInfo, MoreInfoModel>().ReverseMap();
            CreateMap<Residency, ResidencyModel>().ReverseMap();
            CreateMap<StateLivedIn, StateLivedInModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<LookUps, LookUpsModel>().ReverseMap();
            CreateMap<RulesModule, RulesModuleModel>()
                .ForMember(dest => dest.NamePlusApplication, opts => opts.MapFrom(x => x.Name + "|" + x.Application))
                .ReverseMap();
            CreateMap<VariableMetaData, VariableMetaDataModel>().ReverseMap();
        }
    }
}
