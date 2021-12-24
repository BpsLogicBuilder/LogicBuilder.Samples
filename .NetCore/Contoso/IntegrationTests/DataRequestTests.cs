using AutoMapper;
using Contoso.AutoMapperProfiles;
using Contoso.Contexts;
using Contoso.Data.Entities;
using Contoso.Domain;
using Contoso.Domain.Entities;
using Contoso.Forms.View.Expansions;
using Contoso.Kemdo.AutoMapperProfiles;
using Contoso.Kendo.ViewModels;
using Contoso.Repositories;
using Contoso.Stores;
using Contoso.Web.Utils;
using Kendo.Mvc.UI;
using LogicBuilder.Kendo.ExpressionExtensions.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class DataRequestTests
    {
        public DataRequestTests()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        #region Methods
        private void Initialize()
        {
            serviceProvider = new ServiceCollection()
                .AddDbContext<SchoolContext>
                (
                    options =>
                    {
                        options.UseInMemoryDatabase("ContosoUniVersity");
                        options.UseInternalServiceProvider(new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider());
                    }
                )
                .AddTransient<ISchoolStore, SchoolStore>()
                .AddTransient<ISchoolRepository, SchoolRepository>()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    new MapperConfiguration(cfg =>
                    {
                        cfg.AddMaps(typeof(SchoolProfile).GetTypeInfo().Assembly);
                        cfg.AddMaps(typeof(GroupingProfile).GetTypeInfo().Assembly);
                        cfg.AddProfile<ExpansionParameterToViewMappingProfile>();
                        cfg.AddProfile<ExpansionViewToOperatorMappingProfile>();
                    })
                )
                .AddTransient<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .BuildServiceProvider();

            SchoolContext context = serviceProvider.GetRequiredService<SchoolContext>();
            context.Database.EnsureCreated();

            Task.Run(async () => await Seed_Database(serviceProvider.GetRequiredService<ISchoolRepository>())).Wait();
        }
        #endregion Methods

        #region Tests
        [Fact]
        public void Get_students_ungrouped_with_aggregates()
        {
            DataRequest request = new DataRequest
            {
                Options = new DataSourceRequestOptions
                {
                    Aggregate = "lastName-count~enrollmentDate-min",
                    Filter = null,
                    Group = null,
                    Page = 1,
                    Sort = null,
                    PageSize = 5
                },
                ModelType = "Contoso.Domain.Entities.StudentModel",
                DataType = "Contoso.Data.Entities.Student",
                Includes = null,
                Selects = null,
                Distinct = false
            };

            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();
            DataSourceResult result = Task.Run(() => request.InvokeGenericMethod<DataSourceResult>("GetData", repository, serviceProvider.GetRequiredService<IMapper>())).Result;

            Assert.Equal(11, result.Total);
            Assert.Equal(5, ((IEnumerable<StudentModel>)result.Data).Count());
            Assert.Equal(2, result.AggregateResults.Count());
            Assert.Equal("Count", result.AggregateResults.First().AggregateMethodName);
            Assert.Equal(11, (int)result.AggregateResults.First().Value);
        }

        [Fact]
        public void Get_students_grouped_with_aggregates()
        {
            DataRequest request = new DataRequest
            {
                Options = new DataSourceRequestOptions
                {
                    Aggregate = "lastName-count~enrollmentDate-min",
                    Filter = null,
                    Group = "enrollmentDate-asc",
                    Page = 1,
                    Sort = null,
                    PageSize = 5
                },
                ModelType = "Contoso.Domain.Entities.StudentModel",
                DataType = "Contoso.Data.Entities.Student",
                Includes = null,
                Selects = null,
                Distinct = false
            };

            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();
            DataSourceResult result = Task.Run(() => request.InvokeGenericMethod<DataSourceResult>("GetData", repository, serviceProvider.GetRequiredService<IMapper>())).Result;

            Assert.Equal(11, result.Total);
            Assert.Equal(3, ((IEnumerable<AggregateFunctionsGroupModel<StudentModel>>)result.Data).Count());
            Assert.Equal(2, result.AggregateResults.Count());
            Assert.Equal("Count", result.AggregateResults.First().AggregateMethodName);
            Assert.Equal(11, (int)result.AggregateResults.First().Value);
        }

        [Fact]
        public void Get_departments_ungrouped_with_aggregates_and_includes()
        {
            DataRequest request = new DataRequest
            {
                Options = new DataSourceRequestOptions
                {
                    //Queryable.Min<TSource, string> throws System.ArgumentException against In-Memory DB
                    //Aggregate = "administratorName-min~name-count~budget-sum~budget-min~startDate-min",
                    Aggregate = "administratorName-count~name-count~budget-sum~budget-min~startDate-min",
                    Filter = null,
                    Group = null,
                    Page = 1,
                    Sort = null,
                    PageSize = 5
                },
                ModelType = "Contoso.Domain.Entities.DepartmentModel",
                DataType = "Contoso.Data.Entities.Department",
                Includes = null,
                Selects = null,
                Distinct = false
            };

            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();
            DataSourceResult result = Task.Run(() => request.InvokeGenericMethod<DataSourceResult>("GetData", repository, serviceProvider.GetRequiredService<IMapper>())).Result;

            Assert.Equal(4, result.Total);
            Assert.Equal(4, ((IEnumerable<DepartmentModel>)result.Data).Count());
            Assert.Equal(5, result.AggregateResults.Count());
            Assert.Equal("Kim Abercrombie", ((IEnumerable<DepartmentModel>)result.Data).First().AdministratorName);
            //Queryable.Min<TSource, string> throws System.ArgumentException against In-Memory DB
            //Assert.Equal("Min", result.AggregateResults.First().AggregateMethodName);
            //Assert.Equal("Candace Kapoor", (string)result.AggregateResults.First().Value);
        }

        [Fact]
        public void Get_departments_grouped_with_aggregates()
        {
            DataRequest request = new DataRequest
            {
                Options = new DataSourceRequestOptions
                {
                    //Queryable.Min<TSource, string> throws System.ArgumentException against In-Memory DB
                    //Aggregate = "administratorName-min~name-count~budget-sum~budget-min~startDate-min",
                    Aggregate = "administratorName-count~name-count~budget-sum~budget-min~startDate-min",
                    Filter = null,
                    Group = "budget-asc",
                    Page = 1,
                    Sort = null,
                    PageSize = 5
                },
                ModelType = "Contoso.Domain.Entities.DepartmentModel",
                DataType = "Contoso.Data.Entities.Department",
                Includes = null,
                Selects = null,
                Distinct = false
            };

            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();
            DataSourceResult result = Task.Run(() => request.InvokeGenericMethod<DataSourceResult>("GetData", repository, serviceProvider.GetRequiredService<IMapper>())).Result;

            Assert.Equal(4, result.Total);
            Assert.Equal(2, ((IEnumerable<AggregateFunctionsGroupModel<DepartmentModel>>)result.Data).Count());
            Assert.Equal(5, result.AggregateResults.Count());
            //Queryable.Min<TSource, string> throws System.ArgumentException against In-Memory DB
            //Assert.Equal("Min", result.AggregateResults.First().AggregateMethodName);
            //Assert.Equal("Candace Kapoor", (string)result.AggregateResults.First().Value);
        }

        [Fact]
        public void Get_courses_ungrouped_with_aggregates()
        {
            DataRequest request = new DataRequest
            {
                Options = new DataSourceRequestOptions
                {
                    Aggregate = "credits-sum",
                    Filter = null,
                    Group = null,
                    Page = 1,
                    Sort = null,
                    PageSize = 5
                },
                ModelType = "Contoso.Domain.Entities.CourseModel",
                DataType = "Contoso.Data.Entities.Course",
                //Includes = new string[] { "departmentName" },
                Selects = null,
                Distinct = false
            };

            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();
            DataSourceResult result = Task.Run(() => request.InvokeGenericMethod<DataSourceResult>("GetData", repository, serviceProvider.GetRequiredService<IMapper>())).Result;

            Assert.Equal(7, result.Total);
            Assert.Equal(5, ((IEnumerable<CourseModel>)result.Data).Count());
            Assert.Single(result.AggregateResults);
            Assert.Equal("Calculus", ((IEnumerable<CourseModel>)result.Data).First().Title);
        }

        [Fact]
        public void Get_instructors_ungrouped_with_aggregates()
        {
            DataRequest request = new DataRequest
            {
                Options = new DataSourceRequestOptions
                {
                    Aggregate = "lastName-count~hireDate-min",
                    Filter = null,
                    Group = null,
                    Page = 1,
                    Sort = null,
                    PageSize = 5
                },
                ModelType = "Contoso.Domain.Entities.InstructorModel",
                DataType = "Contoso.Data.Entities.Instructor",
                SelectExpandDefinition = new SelectExpandDefinitionView
                {
                    ExpandedItems = new List<SelectExpandItemView>
                    {
                        new SelectExpandItemView
                        {
                            MemberName = "courses"
                        },
                        new SelectExpandItemView
                        {
                            MemberName = "officeAssignment"
                        }
                    }
                },
                Selects = null,
                Distinct = false
            };

            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();
            DataSourceResult result = Task.Run(() => request.InvokeGenericMethod<DataSourceResult>("GetData", repository, serviceProvider.GetRequiredService<IMapper>())).Result;

            Assert.Equal(5, result.Total);
            Assert.Equal(5, ((IEnumerable<InstructorModel>)result.Data).Count());
            Assert.Equal(2, result.AggregateResults.Count());
            Assert.Equal("Roger Zheng", ((IEnumerable<InstructorModel>)result.Data).First().FullName);
        }

        [Fact]
        public void Get_instructors_grouped_with_aggregates_and_includes()
        {
            DataRequest request = new DataRequest
            {
                Options = new DataSourceRequestOptions
                {
                    Aggregate = "lastName-count~hireDate-min",
                    Filter = null,
                    Group = "hireDate-asc",
                    Page = 1,
                    Sort = null,
                    PageSize = 5
                },
                ModelType = "Contoso.Domain.Entities.InstructorModel",
                DataType = "Contoso.Data.Entities.Instructor",
                Includes = new string[] { "courses.courseTitle", "officeAssignment" },
                Selects = null,
                Distinct = false
            };

            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();
            DataSourceResult result = Task.Run(() => request.InvokeGenericMethod<DataSourceResult>("GetData", repository, serviceProvider.GetRequiredService<IMapper>())).Result;

            Assert.Equal(5, result.Total);
            Assert.Equal(5, ((IEnumerable<AggregateFunctionsGroupModel<InstructorModel>>)result.Data).Count());
            Assert.NotEmpty(((IEnumerable<AggregateFunctionsGroupModel<InstructorModel>>)result.Data).First().Items.Cast<InstructorModel>().First().Courses);
            Assert.Equal(2, result.AggregateResults.Count());
            Assert.Equal("Count", result.AggregateResults.First().AggregateMethodName);
            Assert.Equal(5, (int)result.AggregateResults.First().Value);
        }

        [Fact]
        public void Get_instructors_grouped_with_aggregates_without_includes()
        {
            DataRequest request = new DataRequest
            {
                Options = new DataSourceRequestOptions
                {
                    Aggregate = "lastName-count~hireDate-min",
                    Filter = null,
                    Group = "hireDate-asc",
                    Page = 1,
                    Sort = null,
                    PageSize = 5
                },
                ModelType = "Contoso.Domain.Entities.InstructorModel",
                DataType = "Contoso.Data.Entities.Instructor",
                Selects = null,
                Distinct = false
            };

            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();
            DataSourceResult result = Task.Run(() => request.InvokeGenericMethod<DataSourceResult>("GetData", repository, serviceProvider.GetRequiredService<IMapper>())).Result;

            Assert.Equal(5, result.Total);
            Assert.Equal(5, ((IEnumerable<AggregateFunctionsGroupModel<InstructorModel>>)result.Data).Count());
            Assert.Empty(((IEnumerable<AggregateFunctionsGroupModel<InstructorModel>>)result.Data).First().Items.Cast<InstructorModel>().First().Courses);
            Assert.Null(((IEnumerable<AggregateFunctionsGroupModel<InstructorModel>>)result.Data).First().Items.Cast<InstructorModel>().First().OfficeAssignment);
            Assert.Equal(2, result.AggregateResults.Count());
            Assert.Equal("Count", result.AggregateResults.First().AggregateMethodName);
            Assert.Equal(5, (int)result.AggregateResults.First().Value);
        }

        [Fact]
        public void Get_single_student_with_navigation_property_of_navigation_property()
        {
            DataRequest request = new DataRequest
            {
                Options = new DataSourceRequestOptions
                {
                    Aggregate = null,
                    Filter = "id~eq~3",
                    Group = null,
                    Page = 0,
                    Sort = null,
                    PageSize = 0
                },
                ModelType = "Contoso.Domain.Entities.StudentModel",
                DataType = "Contoso.Data.Entities.Student",
                SelectExpandDefinition = new SelectExpandDefinitionView
                {
                    ExpandedItems = new List<SelectExpandItemView>
                    {
                        new SelectExpandItemView
                        {
                            MemberName = "enrollments"
                        }
                    }
                },
                Selects = null,
                Distinct = false
            };

            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();
            StudentModel result = (StudentModel)Task.Run(() => request.InvokeGenericMethod<BaseModelClass>("GetSingle", repository, serviceProvider.GetRequiredService<IMapper>())).Result;

            Assert.Equal("Chemistry", result.Enrollments.First(e => !e.Grade.HasValue).CourseTitle);
            Assert.Equal("Arturo Anand", result.FullName);
        }

        [Fact]
        public void Get_single_department()
        {
            DataRequest request = new DataRequest
            {
                Options = new DataSourceRequestOptions
                {
                    Aggregate = null,
                    Filter = "departmentID~eq~2",
                    Group = null,
                    Page = 1,
                    Sort = null,
                    PageSize = 5
                },
                ModelType = "Contoso.Domain.Entities.DepartmentModel",
                DataType = "Contoso.Data.Entities.Department",
                Includes = null,
                Selects = null,
                Distinct = false
            };

            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();
            DepartmentModel result = (DepartmentModel)Task.Run(() => request.InvokeGenericMethod<BaseModelClass>("GetSingle", repository, serviceProvider.GetRequiredService<IMapper>())).Result;

            Assert.Equal("Mathematics", result.Name);
        }

        [Fact]
        public void Get_instructor_list_with_select_new()
        {
            DataRequest request = new DataRequest
            {
                Options = new DataSourceRequestOptions
                {
                    Aggregate = null,
                    Filter = null,
                    Group = null,
                    Page = 1,
                    Sort = null,
                    PageSize = 5
                },
                ModelType = "Contoso.Domain.Entities.InstructorModel",
                DataType = "Contoso.Data.Entities.Instructor",
                Includes = null,
                Selects = new Dictionary<string, string> { ["id"] = "id", ["fullName"]= "fullName" },
                Distinct = false
            };

            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();
            IEnumerable<dynamic> result = Task.Run(() => request.InvokeGenericMethod<IEnumerable<dynamic>>("GetDynamicSelect", repository, serviceProvider.GetRequiredService<IMapper>())).Result;

            Assert.Equal("Roger Zheng", result.First().fullName);
        }
        #endregion Tests

        #region Seed DB
        private static async Task Seed_Database(ISchoolRepository repository)
        {
            if ((await repository.CountAsync<StudentModel, Student>()) > 0)
                return;//database has been seeded

            InstructorModel[] instructors = new InstructorModel[]
            {
                new InstructorModel { FirstName = "Roger",   LastName = "Zheng", HireDate = DateTime.Parse("2004-02-12"), EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new InstructorModel { FirstName = "Kim", LastName = "Abercrombie", HireDate = DateTime.Parse("1995-03-11"), EntityState = LogicBuilder.Domain.EntityStateType.Added},
                new InstructorModel { FirstName = "Fadi", LastName = "Fakhouri", HireDate = DateTime.Parse("2002-07-06"), OfficeAssignment = new OfficeAssignmentModel { Location = "Smith 17" }, EntityState = LogicBuilder.Domain.EntityStateType.Added},
                new InstructorModel { FirstName = "Roger", LastName = "Harui", HireDate = DateTime.Parse("1998-07-01"), OfficeAssignment = new OfficeAssignmentModel { Location = "Gowan 27" }, EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new InstructorModel { FirstName = "Candace", LastName = "Kapoor", HireDate = DateTime.Parse("2001-01-15"), OfficeAssignment = new OfficeAssignmentModel { Location = "Thompson 304" }, EntityState = LogicBuilder.Domain.EntityStateType.Added }
            };
            await repository.SaveGraphsAsync<InstructorModel, Instructor>(instructors);

            DepartmentModel[] departments = new DepartmentModel[]
            {
                new DepartmentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    Name = "English",     Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID = instructors.Single(i => i.FirstName == "Kim" && i.LastName == "Abercrombie").ID,
                    Courses =  new HashSet<CourseModel>
                    {
                        new CourseModel {CourseID = 2021, Title = "Composition",    Credits = 3},
                        new CourseModel {CourseID = 2042, Title = "Literature",     Credits = 4}
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
                        new CourseModel {CourseID = 1045, Title = "Calculus",       Credits = 4},
                        new CourseModel {CourseID = 3141, Title = "Trigonometry",   Credits = 4}
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
                        new CourseModel {CourseID = 1050, Title = "Chemistry",      Credits = 3}
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
                        new CourseModel {CourseID = 4022, Title = "Microeconomics", Credits = 3},
                        new CourseModel {CourseID = 4041, Title = "Macroeconomics", Credits = 3 }
                    }
                }
            };
            await repository.SaveGraphsAsync<DepartmentModel, Department>(departments);

            IEnumerable<CourseModel> courses = departments.SelectMany(d => d.Courses);
            CourseAssignmentModel[] courseInstructors = new CourseAssignmentModel[]
            {
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
            };
            await repository.SaveGraphsAsync<CourseAssignmentModel, CourseAssignment>(courseInstructors);

            StudentModel[] students = new StudentModel[]
            {
                new StudentModel
                {
                    EntityState =  LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Carson",   LastName = "Alexander",
                    EnrollmentDate = DateTime.Parse("2010-09-01"),
                    Enrollments = new HashSet<EnrollmentModel>
                    {
                        new EnrollmentModel
                        {
                            CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                            Grade = Contoso.Domain.Entities.Grade.A
                        },
                        new EnrollmentModel
                        {
                            CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                            Grade = Contoso.Domain.Entities.Grade.C
                        },
                        new EnrollmentModel
                        {
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
                        new EnrollmentModel
                        {
                            CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                            Grade = Contoso.Domain.Entities.Grade.B
                        },
                        new EnrollmentModel
                        {
                            CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                            Grade = Contoso.Domain.Entities.Grade.B
                        },
                        new EnrollmentModel
                        {
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
                        new EnrollmentModel
                        {
                            CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID
                        },
                        new EnrollmentModel
                        {
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
                        new EnrollmentModel
                        {
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
                        new EnrollmentModel
                        {
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
                        new EnrollmentModel
                        {
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
                        new EnrollmentModel
                        {
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
                        new EnrollmentModel
                        {
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
                        new EnrollmentModel
                        {
                            CourseID = 2021,
                            Grade = Contoso.Domain.Entities.Grade.B
                        }
                    }
                }
            };

            await repository.SaveGraphsAsync<StudentModel, Student>(students);
        }
        #endregion Seed DB
    }
}
