using AutoMapper;
using Contoso.Data.Automatic;
using Contoso.Data.Entities;
using Contoso.Data.Rules;
using Contoso.Domain.Entities;
using System;

namespace Contoso.AutoMapperProfiles
{
    public class SchoolProfile : Profile
    {
        public SchoolProfile()
        {
            CreateMap<CourseAssignmentModel, CourseAssignment>()
                .ForMember(dest => dest.Instructor, opts => opts.Ignore())
                .ForMember(dest => dest.Course, opts => opts.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.CourseTitle, opts => opts.MapFrom(x => x.Course.Title))
                .ForMember(dest => dest.CourseNumberAndTitle, opts => opts.MapFrom(x => x.Course.CourseID.ToString() + " " + x.Course.Title))
                .ForMember(dest => dest.Department, opts => opts.MapFrom(x => x.Course.Department.Name));

            CreateMap<CourseModel, Course>()
                .ForMember(dest => dest.Department, opts => opts.Ignore())
                .ForMember(dest => dest.Enrollments, opts => opts.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.DepartmentName, opts => opts.MapFrom(x => x.Department.Name));

            CreateMap<DepartmentModel, Department>()
                .ForMember(dest => dest.Administrator, opts => opts.Ignore())
                .ReverseMap()
                //.ForMember(dest => dest.StartDate, opts => opts.MapFrom(x => x.StartDate.Date)) This works in .Net Core
                //.ForMember(dest => dest.AdministratorName, opts => opts.MapFrom(x => string.Concat(x.Administrator.FirstMidName, " ", x.Administrator.LastName)));
                //Format functions cause clientside evaluation in EF Core - use + instead.
                .ForMember(dest => dest.AdministratorName, opts => opts.MapFrom(x => x.Administrator.FirstName + " " + x.Administrator.LastName));

            CreateMap<EnrollmentModel, Contoso.Data.Entities.Enrollment>()
                .ForMember(dest => dest.Student, opts => opts.Ignore())
                .ForMember(dest => dest.Course, opts => opts.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.CourseTitle, opts => opts.MapFrom(x => x.Course.Title))
                .ForMember(dest => dest.StudentName, opts => opts.MapFrom(x => x.Student.FirstName + " " + x.Student.LastName))
                .ForMember(dest => dest.GradeLetter, opts => opts.MapFrom(x => x.Grade.HasValue ? x.Grade.Value.ToString() : string.Empty));

            CreateMap<InstructorModel, Instructor>()
                .ReverseMap()
                //.ForMember(dest => dest.FullName, opts => opts.MapFrom(x => string.Concat(x.FirstMidName, " ", x.LastName)));
                //Format functions cause clientside evaluation in EF Core - use + instead.
                .ForMember(dest => dest.FullName, opts => opts.MapFrom(x => x.FirstName + " " + x.LastName));

            CreateMap<OfficeAssignmentModel, OfficeAssignment>()
                .ForMember(dest => dest.Instructor, opts => opts.Ignore())
                .ReverseMap();

            CreateMap<StudentModel, Student>()
                .ReverseMap()
            .ForMember(dest => dest.FullName, opts => opts.MapFrom(x => x.FirstName + " " + x.LastName));

            CreateMap<LookUpsModel, LookUps>().ReverseMap();

            CreateMap<RulesModuleModel, RulesModule>().ReverseMap();

            CreateMap<VariableMetaDataModel, VariableMetaData>().ReverseMap();
        }
    }
}
