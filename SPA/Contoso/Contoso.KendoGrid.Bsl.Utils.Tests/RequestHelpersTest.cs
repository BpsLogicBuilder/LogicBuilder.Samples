using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Contoso.AutoMapperProfiles;
using Contoso.BSL.AutoMapperProfiles;
using Contoso.Common.Configuration.ExpansionDescriptors;
using Contoso.Contexts;
using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using Contoso.KendoGrid.Bsl.Business.Requests;
using Contoso.Repositories;
using Contoso.Stores;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Contoso.KendoGrid.Bsl.Utils.Tests
{
    public class RequestHelpersTest
    {
        static RequestHelpersTest()
        {
            InitializeMapperConfiguration();
        }

        public RequestHelpersTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public async void Get_students_ungrouped_with_aggregates()
        {
            KendoGridDataRequest request = new()
            {
                Options = new KendoGridDataSourceRequestOptions
                {
                    Aggregate = "lastName-count~enrollmentDate-min",
                    Filter = null,
                    Group = null,
                    Page = 1,
                    Sort = "enrollmentDate-asc",
                    PageSize = 5
                },
                ModelType = typeof(StudentModel).FullName,
                DataType = typeof(Student).FullName,
            };

            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            DataSourceResult result = await request.GetData(repository, mapper);

            Assert.Equal(11, result.Total);
            Assert.Equal(5, ((IEnumerable<StudentModel>)result.Data).Count());
            Assert.Equal("Nino", ((IEnumerable<StudentModel>)result.Data).First().FirstName);
            Assert.Equal(2, result.AggregateResults.Count());
            Assert.Equal("Count", result.AggregateResults.First().AggregateMethodName);
            Assert.Equal(11, (int)result.AggregateResults.First().Value);
        }

        [Fact]
        public async void Get_students_grouped_with_aggregates()
        {
            KendoGridDataRequest request = new()
            {
                Options = new KendoGridDataSourceRequestOptions
                {
                    Aggregate = "lastName-count~enrollmentDate-min",
                    Filter = null,
                    Group = "enrollmentDate-asc",
                    Page = 1,
                    Sort = null,
                    PageSize = 5
                },
                ModelType = typeof(StudentModel).FullName,
                DataType = typeof(Student).FullName,
            };

            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            DataSourceResult result = await request.GetData(repository, mapper);

            Assert.Equal(11, result.Total);
            Assert.Equal(3, ((IEnumerable<AggregateFunctionsGroup>)result.Data).Count());
            Assert.Equal(2, result.AggregateResults.Count());
            Assert.Equal("Count", result.AggregateResults.First().AggregateMethodName);
            Assert.Equal(11, (int)result.AggregateResults.First().Value);
        }

        [Fact]
        public async void Get_departments_ungrouped_with_aggregates_and_includes()
        {
            KendoGridDataRequest request = new()
            {
                Options = new KendoGridDataSourceRequestOptions
                {
                    Aggregate = "administratorName-min~name-count~budget-sum~budget-min~startDate-min",
                    Filter = null,
                    Group = null,
                    Page = 1,
                    Sort = null,
                    PageSize = 5
                },
                ModelType = typeof(DepartmentModel).FullName,
                DataType = typeof(Department).FullName,
            };

            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            DataSourceResult result = await request.GetData(repository, mapper);

            Assert.Equal(4, result.Total);
            Assert.Equal(4, ((IEnumerable<DepartmentModel>)result.Data).Count());
            Assert.Equal(5, result.AggregateResults.Count());
            Assert.Equal("Kim Abercrombie", ((IEnumerable<DepartmentModel>)result.Data).First().AdministratorName);
            Assert.Equal("Min", result.AggregateResults.First().AggregateMethodName);
            Assert.Equal("Candace Kapoor", (string)result.AggregateResults.First().Value);
        }

        [Fact]
        public async void Get_departments_grouped_with_aggregates()
        {
            KendoGridDataRequest request = new()
            {
                Options = new KendoGridDataSourceRequestOptions
                {
                    Aggregate = "administratorName-min~name-count~budget-sum~budget-min~startDate-min",
                    Filter = null,
                    Group = "budget-asc",
                    Page = 1,
                    Sort = null,
                    PageSize = 5
                },
                ModelType = typeof(DepartmentModel).FullName,
                DataType = typeof(Department).FullName,
            };

            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            DataSourceResult result = await request.GetData(repository, mapper);

            Assert.Equal(4, result.Total);
            Assert.Equal(2, ((IEnumerable<AggregateFunctionsGroup>)result.Data).Count());
            Assert.Equal(5, result.AggregateResults.Count());
            Assert.Equal("Min", result.AggregateResults.First().AggregateMethodName);
            Assert.Equal("Candace Kapoor", (string)result.AggregateResults.First().Value);
        }

        [Fact]
        public async void Get_instructors_ungrouped_with_aggregates()
        {
            KendoGridDataRequest request = new()
            {
                Options = new KendoGridDataSourceRequestOptions
                {
                    Aggregate = "lastName-count~hireDate-min",
                    Filter = null,
                    Group = null,
                    Page = 1,
                    Sort = null,
                    PageSize = 5
                },
                ModelType = typeof(InstructorModel).FullName,
                DataType = typeof(Instructor).FullName,
            };

            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            DataSourceResult result = await request.GetData(repository, mapper);

            Assert.Equal(5, result.Total);
            Assert.Equal(5, ((IEnumerable<InstructorModel>)result.Data).Count());
            Assert.Equal(2, result.AggregateResults.Count());
            Assert.Equal("Roger Zheng", ((IEnumerable<InstructorModel>)result.Data).First().FullName);
        }

        [Fact]
        public async void Get_instructors_grouped_with_aggregates_and_expansions()
        {
            KendoGridDataRequest request = new()
            {
                Options = new KendoGridDataSourceRequestOptions
                {
                    Aggregate = "lastName-count~hireDate-min",
                    Filter = null,
                    Group = "hireDate-asc",
                    Page = 1,
                    Sort = null,
                    PageSize = 5
                },
                ModelType = typeof(InstructorModel).FullName,
                DataType = typeof(Instructor).FullName,
                SelectExpandDefinition = new SelectExpandDefinitionDescriptor
                {
                    ExpandedItems =
                    [
                        new SelectExpandItemDescriptor
                        {
                            MemberName = "courses"
                        },
                        new SelectExpandItemDescriptor
                        {
                            MemberName = "officeAssignment"
                        }
                    ]
                }
            };

            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            DataSourceResult result = await request.GetData(repository, mapper);

            Assert.Equal(5, result.Total);
            Assert.Equal(5, ((IEnumerable<AggregateFunctionsGroup>)result.Data).Count());
            Assert.NotEmpty(((IEnumerable<AggregateFunctionsGroup>)result.Data).First().Items.Cast<InstructorModel>().First().Courses);
            Assert.Equal(2, result.AggregateResults.Count());
            Assert.Equal("Count", result.AggregateResults.First().AggregateMethodName);
            Assert.Equal(5, (int)result.AggregateResults.First().Value);
        }

        [Fact]
        public async void Get_instructors_grouped_with_aggregates_without_expansions()
        {
            KendoGridDataRequest request = new()
            {
                Options = new KendoGridDataSourceRequestOptions
                {
                    Aggregate = "lastName-count~hireDate-min",
                    Filter = null,
                    Group = "hireDate-asc",
                    Page = 1,
                    Sort = null,
                    PageSize = 5
                },
                ModelType = typeof(InstructorModel).FullName,
                DataType = typeof(Instructor).FullName
            };

            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            DataSourceResult result = await request.GetData(repository, mapper);

            Assert.Equal(5, result.Total);
            Assert.Equal(5, ((IEnumerable<AggregateFunctionsGroup>)result.Data).Count());
            Assert.Null(((IEnumerable<AggregateFunctionsGroup>)result.Data).First().Items.Cast<InstructorModel>().First().Courses);
            Assert.Null(((IEnumerable<AggregateFunctionsGroup>)result.Data).First().Items.Cast<InstructorModel>().First().OfficeAssignment);
            Assert.Equal(2, result.AggregateResults.Count());
            Assert.Equal("Count", result.AggregateResults.First().AggregateMethodName);
            Assert.Equal(5, (int)result.AggregateResults.First().Value);
        }

        #region Helpers
        static MapperConfiguration MapperConfiguration;

        [MemberNotNull(nameof(MapperConfiguration))]
        private static void InitializeMapperConfiguration()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();

                cfg.AddProfile<DescriptorToOperatorMappingProfile>();
                cfg.AddProfile<SchoolProfile>();
                cfg.AddProfile<ExpansionDescriptorToOperatorMappingProfile>();
            });
        }

        [MemberNotNull(nameof(serviceProvider))]
        private void Initialize()
        {
            MapperConfiguration.AssertConfigurationIsValid();
            serviceProvider = new ServiceCollection()
                .AddDbContext<SchoolContext>
                (
                    options => options.UseSqlServer
                    (
                        @"Server=(localdb)\mssqllocaldb;Database=RequestHelpersTest;ConnectRetryCount=0"
                    ),
                    ServiceLifetime.Transient
                )
                .AddTransient<ISchoolStore, SchoolStore>()
                .AddTransient<ISchoolRepository, SchoolRepository>()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    MapperConfiguration
                )
                .AddTransient<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .BuildServiceProvider();

            SchoolContext context = serviceProvider.GetRequiredService<SchoolContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Seed_Database(serviceProvider.GetRequiredService<ISchoolRepository>()).Wait();
        }
        #endregion Helpers

        #region Seed DB
        private static async Task Seed_Database(ISchoolRepository repository)
        {
            if ((await repository.CountAsync<StudentModel, Student>()) > 0)
                return;//database has been seeded

            InstructorModel[] instructors =
            [
                new InstructorModel { FirstName = "Roger",   LastName = "Zheng", HireDate = DateTime.Parse("2004-02-12"), EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new InstructorModel { FirstName = "Kim", LastName = "Abercrombie", HireDate = DateTime.Parse("1995-03-11"), EntityState = LogicBuilder.Domain.EntityStateType.Added},
                new InstructorModel { FirstName = "Fadi", LastName = "Fakhouri", HireDate = DateTime.Parse("2002-07-06"), OfficeAssignment = new OfficeAssignmentModel { Location = "Smith 17" }, EntityState = LogicBuilder.Domain.EntityStateType.Added},
                new InstructorModel { FirstName = "Roger", LastName = "Harui", HireDate = DateTime.Parse("1998-07-01"), OfficeAssignment = new OfficeAssignmentModel { Location = "Gowan 27" }, EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new InstructorModel { FirstName = "Candace", LastName = "Kapoor", HireDate = DateTime.Parse("2001-01-15"), OfficeAssignment = new OfficeAssignmentModel { Location = "Thompson 304" }, EntityState = LogicBuilder.Domain.EntityStateType.Added }
            ];
            await repository.SaveGraphsAsync<InstructorModel, Instructor>(instructors);

            DepartmentModel[] departments =
            [
                new DepartmentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    Name = "English",     Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID = instructors.Single(i => i.FirstName == "Kim" && i.LastName == "Abercrombie").ID,
                    Courses =  new HashSet<CourseModel>
                    {
                        new() {CourseID = 2021, Title = "Composition",    Credits = 3},
                        new() {CourseID = 2042, Title = "Literature",     Credits = 4}
                    }
                },
                new DepartmentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    Name = "Mathematics",
                    Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID = instructors.Single(i => i.FirstName == "Fadi" && i.LastName == "Fakhouri").ID,
                    Courses =  new HashSet<CourseModel>
                    {
                        new() {CourseID = 1045, Title = "Calculus",       Credits = 4},
                        new() {CourseID = 3141, Title = "Trigonometry",   Credits = 4}
                    }
                },
                new DepartmentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    Name = "Engineering", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID = instructors.Single(i => i.FirstName == "Roger" && i.LastName == "Harui").ID,
                    Courses =  new HashSet<CourseModel>
                    {
                        new() {CourseID = 1050, Title = "Chemistry",      Credits = 3}
                    }
                },
                new DepartmentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    Name = "Economics",
                    Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID = instructors.Single(i => i.FirstName == "Candace" && i.LastName == "Kapoor").ID,
                    Courses =  new HashSet<CourseModel>
                    {
                        new() {CourseID = 4022, Title = "Microeconomics", Credits = 3},
                        new() {CourseID = 4041, Title = "Macroeconomics", Credits = 3 }
                    }
                }
            ];
            await repository.SaveGraphsAsync<DepartmentModel, Department>(departments);

            IEnumerable<CourseModel> courses = departments.SelectMany(d => d.Courses);
            CourseAssignmentModel[] courseInstructors =
            [
                new CourseAssignmentModel {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Kapoor").ID
                    },
                new CourseAssignmentModel {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Harui").ID
                    },
                new CourseAssignmentModel {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Zheng").ID
                    },
                new CourseAssignmentModel {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Zheng").ID
                    },
                new CourseAssignmentModel {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Fakhouri").ID
                    },
                new CourseAssignmentModel {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Harui").ID
                    },
                new CourseAssignmentModel {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID
                    },
                new CourseAssignmentModel {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Literature" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID
                    },
            ];
            await repository.SaveGraphsAsync<CourseAssignmentModel, CourseAssignment>(courseInstructors);

            StudentModel[] students =
            [
                new StudentModel
                {
                    EntityState =  LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Carson",   LastName = "Alexander",
                    EnrollmentDate = DateTime.Parse("2010-09-01"),
                    Enrollments = new HashSet<EnrollmentModel>
                    {
                        new() {
                            CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                            Grade = Contoso.Domain.Entities.Grade.A
                        },
                        new() {
                            CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                            Grade = Contoso.Domain.Entities.Grade.C
                        },
                        new() {
                            CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                            Grade = Contoso.Domain.Entities.Grade.B
                        }
                    }
                },
                new StudentModel
                {
                    EntityState =  LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Meredith", LastName = "Alonso",
                    EnrollmentDate = DateTime.Parse("2012-09-01"),
                    Enrollments = new HashSet<EnrollmentModel>
                    {
                        new() {
                            CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                            Grade = Contoso.Domain.Entities.Grade.B
                        },
                        new() {
                            CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                            Grade = Contoso.Domain.Entities.Grade.B
                        },
                        new() {
                            CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                            Grade = Contoso.Domain.Entities.Grade.B
                        }
                    }
                },
                new StudentModel
                {
                    EntityState =  LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Arturo",   LastName = "Anand",
                    EnrollmentDate = DateTime.Parse("2013-09-01"),
                    Enrollments = new HashSet<EnrollmentModel>
                    {
                        new() {
                            CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID
                        },
                        new() {
                            CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
                            Grade = Contoso.Domain.Entities.Grade.B
                        },
                    }
                },
                new StudentModel
                {
                    EntityState =  LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Gytis",    LastName = "Barzdukas",
                    EnrollmentDate = DateTime.Parse("2012-09-01"),
                    Enrollments = new HashSet<EnrollmentModel>
                    {
                        new() {
                            CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                            Grade = Contoso.Domain.Entities.Grade.B
                        }
                    }
                },
                new StudentModel
                {
                    EntityState =  LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Yan",      LastName = "Li",
                    EnrollmentDate = DateTime.Parse("2012-09-01"),
                    Enrollments = new HashSet<EnrollmentModel>
                    {
                        new() {
                            CourseID = courses.Single(c => c.Title == "Composition").CourseID,
                            Grade = Contoso.Domain.Entities.Grade.B
                        }
                    }
                },
                new StudentModel
                {
                    EntityState =  LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Peggy",    LastName = "Justice",
                    EnrollmentDate = DateTime.Parse("2011-09-01"),
                    Enrollments = new HashSet<EnrollmentModel>
                    {
                        new() {
                            CourseID = courses.Single(c => c.Title == "Literature").CourseID,
                            Grade = Contoso.Domain.Entities.Grade.B
                        }
                    }
                },
                new StudentModel
                {
                    EntityState =  LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Laura",    LastName = "Norman",
                    EnrollmentDate = DateTime.Parse("2013-09-01")
                },
                new StudentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Nino",     LastName = "Olivetto",
                    EnrollmentDate = DateTime.Parse("2005-09-01")
                },
                new StudentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Tom",
                    LastName = "Spratt",
                    EnrollmentDate = DateTime.Parse("2010-09-01"),
                    Enrollments = new HashSet<EnrollmentModel>
                    {
                        new() {
                            CourseID = 1045,
                            Grade = Contoso.Domain.Entities.Grade.B
                        }
                    }
                },
                new StudentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Billie",
                    LastName = "Spratt",
                    EnrollmentDate = DateTime.Parse("2010-09-01"),
                    Enrollments = new HashSet<EnrollmentModel>
                    {
                        new() {
                            CourseID = 1050,
                            Grade = Contoso.Domain.Entities.Grade.B
                        }
                    }
                },
                new StudentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Jackson",
                    LastName = "Spratt",
                    EnrollmentDate = DateTime.Parse("2017-09-01"),
                    Enrollments = new HashSet<EnrollmentModel>
                    {
                        new() {
                            CourseID = 2021,
                            Grade = Contoso.Domain.Entities.Grade.B
                        }
                    }
                }
            ];

            await repository.SaveGraphsAsync<StudentModel, Student>(students);

            LookUpsModel[] lookups =
            [
                new  LookUpsModel { ListName = "Grades", Text="A", Value="A", EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new  LookUpsModel { ListName = "Grades", Text="B", Value="B", EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new  LookUpsModel { ListName = "Grades", Text="C", Value="C", EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new  LookUpsModel { ListName = "Grades", Text="D", Value="D", EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new  LookUpsModel { ListName = "Grades", Text="E", Value="E", EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new  LookUpsModel { ListName = "Grades", Text="F", Value="F", EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new  LookUpsModel { ListName = "Credits", Text="One", NumericValue=1, EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new  LookUpsModel { ListName = "Credits", Text="Two", NumericValue=2, EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new  LookUpsModel { ListName = "Credits", Text="Three", NumericValue=3, EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new  LookUpsModel { ListName = "Credits", Text="Four", NumericValue=4, EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new  LookUpsModel { ListName = "Credits", Text="Five", NumericValue=5, EntityState = LogicBuilder.Domain.EntityStateType.Added }
            ];
            await repository.SaveGraphsAsync<LookUpsModel, LookUps>(lookups);
        }
        #endregion Seed DB
    }
}
