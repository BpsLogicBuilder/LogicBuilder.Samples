using AutoMapper;
using Contoso.Data.Entities;
using Contoso.Domain.Entities;

namespace Contoso.BSL.AutoMapperProfiles
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
                .ForMember(dest => dest.CourseNumberAndTitle, opts => opts.MapFrom(x => x.CourseID.ToString() + " " + x.Course.Title))
                .ForMember(dest => dest.Department, opts => opts.MapFrom(x => x.Course.Department.Name))
                .ForAllMembers(o => o.ExplicitExpansion());

            CreateMap<CourseModel, Course>()
                .ForMember(dest => dest.Department, opts => opts.Ignore())
                .ForMember(dest => dest.Enrollments, opts => opts.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.DepartmentName, opts => opts.MapFrom(x => x.Department.Name))
                .ForMember(dest => dest.CourseIDString, opts => opts.MapFrom(x => x.CourseID.ToString()))
                .ForAllMembers(o => o.ExplicitExpansion());

            CreateMap<DepartmentModel, Department>()
                .ForMember(dest => dest.Administrator, opts => opts.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.AdministratorName, opts => opts.MapFrom(x => x.Administrator.FirstName + " " + x.Administrator.LastName))
                .ForMember
                (
                    dest => dest.StartDateString,
                    opts => opts.MapFrom
                    (
                        x => Contexts.BaseDbContextSqlFunctions.FormatDateTime(x.StartDate, "MM/dd/yyyy", "en-US")
                    )
                )
                .ForMember
                (
                    dest => dest.BudgetString,
                    opts => opts.MapFrom
                    (
                        x => Contexts.BaseDbContextSqlFunctions.FormatDecimal(x.Budget, "F2", "en-US")
                    )
                )
                .ForAllMembers(o => o.ExplicitExpansion());

            CreateMap<EnrollmentModel, Enrollment>()
                .ForMember(dest => dest.Student, opts => opts.Ignore())
                .ForMember(dest => dest.Course, opts => opts.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.CourseTitle, opts => opts.MapFrom(x => x.Course.Title))
                .ForMember(dest => dest.StudentName, opts => opts.MapFrom(x => x.Student.FirstName + " " + x.Student.LastName))
                .ForMember(dest => dest.Grade, opts => opts.MapFrom(x => x.Grade.HasValue ? (Contoso.Domain.Entities.Grade?)(int)x.Grade.Value : null))
                .ForMember
                (
                    dest => dest.GradeLetter,
                    opts => opts.MapFrom
                    (
                        x => x.Grade == Data.Entities.Grade.A ? "A"
                            : x.Grade == Data.Entities.Grade.B ? "B"
                            : x.Grade == Data.Entities.Grade.C ? "C"
                            : x.Grade == Data.Entities.Grade.D ? "D"
                            : x.Grade == Data.Entities.Grade.F ? "F" : ""
                    )
                )
                .ForAllMembers(o => o.ExplicitExpansion());

            CreateMap<InstructorModel, Instructor>()
                .ReverseMap()
                .ForMember(dest => dest.FullName, opts => opts.MapFrom(x => x.FirstName + " " + x.LastName))
                .ForMember
                (
                    dest => dest.HireDateString,
                    opts => opts.MapFrom
                    (
                        x => Contexts.BaseDbContextSqlFunctions.FormatDateTime(x.HireDate, "MM/dd/yyyy", "en-US")
                    )
                )
                .ForAllMembers(o => o.ExplicitExpansion());

            CreateMap<OfficeAssignmentModel, OfficeAssignment>()
                .ForMember(dest => dest.Instructor, opts => opts.Ignore())
                .ReverseMap()
                .ForAllMembers(o => o.ExplicitExpansion());

            CreateMap<StudentModel, Student>()
                .ReverseMap()
            .ForMember(dest => dest.FullName, opts => opts.MapFrom(x => x.FirstName + " " + x.LastName))
            .ForMember
            (
                dest => dest.EnrollmentDateString,
                opts => opts.MapFrom
                (
                    x => Contexts.BaseDbContextSqlFunctions.FormatDateTime(x.EnrollmentDate, "MM/dd/yyyy", "en-US")
                )
            )
            .ForAllMembers(o => o.ExplicitExpansion());

            CreateMap<LookUpsModel, LookUps>().ReverseMap();
        }
    }
}
