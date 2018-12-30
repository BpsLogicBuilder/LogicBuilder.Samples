using Contoso.Contexts;
using Contoso.Data.Entities;
using Contoso.Stores;
using LogicBuilder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class StoreTests
    {
        public StoreTests()
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
                ).AddTransient<ISchoolStore, SchoolStore>().BuildServiceProvider();

            SchoolContext context = serviceProvider.GetRequiredService<SchoolContext>();
            context.Database.EnsureCreated();

            Task.Run(async () => await Seed_Database(serviceProvider.GetRequiredService<ISchoolStore>())).Wait();
        }
        #endregion Methods

        #region Tests
        [Fact]
        public void Get_students_with_no_includes()
        {
            ISchoolStore store = serviceProvider.GetRequiredService<ISchoolStore>();
            ICollection<Student> list = Task.Run(() => store.GetAsync<Student>()).Result;

            Assert.True(list.Count == 8);
            Assert.Null(list.First().Enrollments);
        }

        [Fact]
        public void Get_students_with_no_includes_inlude_navigation_property()
        {
            ISchoolStore store = serviceProvider.GetRequiredService<ISchoolStore>();
            ICollection<Student> list = Task.Run(() => store.GetAsync<Student>(s => s.Enrollments.Count() > 0, null,
                    new Func<IQueryable<Student>, IIncludableQueryable<Student, object>>[]
                    {
                        a => a.Include(x => x.Enrollments)
                    })).Result;

            Assert.NotNull(list.First().Enrollments);
            Assert.Null(list.First().Enrollments.First().Course);
        }

        [Fact]
        public void Get_students_with_no_includes_inlude_navigation_property_of_navigation_property()
        {
            ISchoolStore store = serviceProvider.GetRequiredService<ISchoolStore>();
            ICollection<Student> list = Task.Run(() => store.GetAsync<Student>(s => s.Enrollments.Count() > 0, null,
                    new Func<IQueryable<Student>, IIncludableQueryable<Student, object>>[]
                    {
                        a => a.Include(x => x.Enrollments).ThenInclude(e => e.Course)
                    })).Result.ToList();

            Assert.NotNull(list.First().Enrollments);
            Assert.NotNull(list.First().Enrollments.First().Course);
        }

        [Fact]
        public void Get_students_filter_by_navigation_property_of_navigation_property()
        {
            ISchoolStore store = serviceProvider.GetRequiredService<ISchoolStore>();
            ICollection<Student> list = Task.Run(() => store.GetAsync<Student>(s => s.Enrollments.Count(e => e.CourseID == 4022) > 0)).Result.ToList();

            Assert.True(list.Count > 0);
        }
        #endregion Tests

        #region Seed DB
        private static async Task Seed_Database(ISchoolStore store)
        {
            if ((await store.CountAsync<Student>()) > 0)
                return;//database has been seeded

            Department[] departments = new Department[]
            {
                new Department
                {
                    EntityState = EntityStateType.Added,
                    Name = "English",     Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    Administrator = new Instructor { FirstName = "Kim", LastName = "Abercrombie", HireDate = DateTime.Parse("1995-03-11")},
                    Courses =  new HashSet<Course>
                    {
                        new Course {CourseID = 2021, Title = "Composition",    Credits = 3},
                        new Course {CourseID = 2042, Title = "Literature",     Credits = 4}
                    }
                },
                new Department
                {
                    EntityState = EntityStateType.Added,
                    Name = "Mathematics",
                    Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    Administrator = new Instructor
                    {
                        FirstName = "Fadi",
                        LastName = "Fakhouri",
                        HireDate = DateTime.Parse("2002-07-06"),
                        OfficeAssignment = new OfficeAssignment { Location = "Smith 17" }
                    },
                    Courses =  new HashSet<Course>
                    {
                        new Course {CourseID = 1045, Title = "Calculus",       Credits = 4},
                        new Course {CourseID = 3141, Title = "Trigonometry",   Credits = 4}
                    }
                },
                new Department
                {
                    EntityState = EntityStateType.Added,
                    Name = "Engineering", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    Administrator = new Instructor
                    {
                        FirstName = "Roger",
                        LastName = "Harui",
                        HireDate = DateTime.Parse("1998-07-01"),
                        OfficeAssignment = new OfficeAssignment { Location = "Gowan 27" }
                    },
                    Courses =  new HashSet<Course>
                    {
                        new Course {CourseID = 1050, Title = "Chemistry",      Credits = 3}
                    }
                },
                new Department
                {
                    EntityState = EntityStateType.Added,
                    Name = "Economics",
                    Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    Administrator = new Instructor
                    {
                        FirstName = "Candace",
                        LastName = "Kapoor",
                        HireDate = DateTime.Parse("2001-01-15"),
                        OfficeAssignment = new OfficeAssignment { Location = "Thompson 304" }
                    },
                    Courses =  new HashSet<Course>
                    {
                        new Course {CourseID = 4022, Title = "Microeconomics", Credits = 3},
                        new Course {CourseID = 4041, Title = "Macroeconomics", Credits = 3 }
                    }
                }
            };
            await store.SaveGraphsAsync<Department>(departments);

            Instructor[] instructors = new Instructor[]
            {
                new Instructor
                {
                    FirstName = "Roger",   LastName = "Zheng",
                    HireDate = DateTime.Parse("2004-02-12"),
                    EntityState = EntityStateType.Added
                }
            };
            await store.SaveGraphsAsync<Instructor>(instructors);

            instructors = (await store.GetAsync<Instructor>()).ToArray();

            IEnumerable<Course> courses = departments.SelectMany(d => d.Courses);

            CourseAssignment[] courseInstructors = new CourseAssignment[]
            {
                new CourseAssignment {
                    EntityState = EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Kapoor").ID
                    },
                new CourseAssignment {
                    EntityState = EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Harui").ID
                    },
                new CourseAssignment {
                    EntityState = EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Zheng").ID
                    },
                new CourseAssignment {
                    EntityState = EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Zheng").ID
                    },
                new CourseAssignment {
                    EntityState = EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Fakhouri").ID
                    },
                new CourseAssignment {
                    EntityState = EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Harui").ID
                    },
                new CourseAssignment {
                    EntityState = EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID
                    },
                new CourseAssignment {
                    EntityState = EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Literature" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID
                    },
            };
            await store.SaveGraphsAsync<CourseAssignment>(courseInstructors);

            Student[] students = new Student[]
            {
                new Student
                {
                    EntityState = EntityStateType.Added,
                    FirstName = "Carson",   LastName = "Alexander",
                    EnrollmentDate = DateTime.Parse("2010-09-01"),
                    Enrollments = new HashSet<Enrollment>
                    {
                        new Enrollment
                        {
                            CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                            Grade = Grade.A
                        },
                        new Enrollment
                        {
                            CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                            Grade = Grade.C
                        },
                        new Enrollment
                        {
                            CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                            Grade = Grade.B
                        }
                    }
                },
                new Student
                {
                    EntityState = EntityStateType.Added,
                    FirstName = "Meredith", LastName = "Alonso",
                    EnrollmentDate = DateTime.Parse("2012-09-01"),
                    Enrollments = new HashSet<Enrollment>
                    {
                        new Enrollment
                        {
                            CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                            Grade = Grade.B
                        },
                        new Enrollment
                        {
                            CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                            Grade = Grade.B
                        },
                        new Enrollment
                        {
                            CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                            Grade = Grade.B
                        }
                    }
                },
                new Student
                {
                    EntityState = EntityStateType.Added,
                    FirstName = "Arturo",   LastName = "Anand",
                    EnrollmentDate = DateTime.Parse("2013-09-01"),
                    Enrollments = new HashSet<Enrollment>
                    {
                        new Enrollment
                        {
                            CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID
                        },
                        new Enrollment
                        {
                            CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
                            Grade = Grade.B
                        },
                    }
                },
                new Student
                {
                    EntityState = EntityStateType.Added,
                    FirstName = "Gytis",    LastName = "Barzdukas",
                    EnrollmentDate = DateTime.Parse("2012-09-01"),
                    Enrollments = new HashSet<Enrollment>
                    {
                        new Enrollment
                        {
                            CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                            Grade = Grade.B
                        }
                    }
                },
                new Student
                {
                    EntityState = EntityStateType.Added,
                    FirstName = "Yan",      LastName = "Li",
                    EnrollmentDate = DateTime.Parse("2012-09-01"),
                    Enrollments = new HashSet<Enrollment>
                    {
                        new Enrollment
                        {
                            CourseID = courses.Single(c => c.Title == "Composition").CourseID,
                            Grade = Grade.B
                        }
                    }
                },
                new Student
                {
                    EntityState = EntityStateType.Added,
                    FirstName = "Peggy",    LastName = "Justice",
                    EnrollmentDate = DateTime.Parse("2011-09-01"),
                    Enrollments = new HashSet<Enrollment>
                    {
                        new Enrollment
                        {
                            CourseID = courses.Single(c => c.Title == "Literature").CourseID,
                            Grade = Grade.B
                        }
                    }
                },
                new Student
                {
                    EntityState = EntityStateType.Added,
                    FirstName = "Laura",    LastName = "Norman",
                    EnrollmentDate = DateTime.Parse("2013-09-01")
                },
                new Student
                {
                    EntityState = EntityStateType.Added,
                    FirstName = "Nino",     LastName = "Olivetto",
                    EnrollmentDate = DateTime.Parse("2005-09-01")
                }
            };
            await store.SaveGraphsAsync<Student>(students);
        }
        #endregion Seed DB
    }
}
