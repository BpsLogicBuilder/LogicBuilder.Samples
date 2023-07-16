using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Contoso.AutoMapperProfiles;
using Contoso.BSL.AutoMapperProfiles;
using Contoso.Contexts;
using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using Contoso.Parameters.Expansions;
using Contoso.Parameters.Expressions;
using Contoso.Repositories;
using Contoso.Stores;
using LogicBuilder.Expressions.Utils.Strutures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Contoso.Bsl.Flow.Integration.Tests
{
    public class ProjectionOperationsTest
    {
        public ProjectionOperationsTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        #region Tests
        [Fact]
        public void Get_students_with_filtered_inlude_no_filter_select_expand_definition()
        {
            ICollection<StudentModel> students = ProjectionOperations<StudentModel, Student>.GetItems
            (
                serviceProvider.GetRequiredService<ISchoolRepository>(),
                serviceProvider.GetRequiredService<IMapper>(),
                new FilterLambdaOperatorParameters
                (
                    new GreaterThanBinaryOperatorParameters
                    (
                        new CountOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("Enrollments", new ParameterOperatorParameters("f"))
                        ),
                        new ConstantOperatorParameters(0)
                    ),
                    typeof(StudentModel),
                    "f"
                ),
                null,
                new SelectExpandDefinitionParameters
                (
                    null,
                    new List<SelectExpandItemParameters>
                    {
                        new SelectExpandItemParameters
                        {
                            MemberName = "Enrollments"
                        }
                    }
                )
            );

            Assert.True(students.First().Enrollments.Count > 0);
        }

        [Fact]
        public void Get_students_no_filtered_inlude_no_filter_select_expand_definition()
        {
            ICollection<StudentModel> students = ProjectionOperations<StudentModel, Student>.GetItems
            (
                serviceProvider.GetRequiredService<ISchoolRepository>(),
                serviceProvider.GetRequiredService<IMapper>(),
                new FilterLambdaOperatorParameters
                (
                    new GreaterThanBinaryOperatorParameters
                    (
                        new CountOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("Enrollments", new ParameterOperatorParameters("f"))
                        ),
                        new ConstantOperatorParameters(0)
                    ),
                    typeof(StudentModel),
                    "f"
                ),
                null,
                null
            );

            Assert.Null(students.First().Enrollments);
        }

        [Fact]
        public void Get_students_with_filtered_inlude_with_filter_select_expand_definition()
        {
            ICollection<StudentModel> students = ProjectionOperations<StudentModel, Student>.GetItems
            (
                serviceProvider.GetRequiredService<ISchoolRepository>(),
                serviceProvider.GetRequiredService<IMapper>(),
                new FilterLambdaOperatorParameters
                (
                    new GreaterThanBinaryOperatorParameters
                    (
                        new CountOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("Enrollments", new ParameterOperatorParameters("f"))
                        ),
                        new ConstantOperatorParameters(0)
                    ),
                    typeof(StudentModel),
                    "f"
                ),
                null,
                new SelectExpandDefinitionParameters
                (
                    null,
                    new List<SelectExpandItemParameters>
                    {
                        new SelectExpandItemParameters
                        (
                            "enrollments",
                            new SelectExpandItemFilterParameters
                            (
                                new FilterLambdaOperatorParameters
                                (
                                    new EqualsBinaryOperatorParameters
                                    (
                                        new MemberSelectorOperatorParameters("enrollmentID", new ParameterOperatorParameters("a")),
                                        new ConstantOperatorParameters(-1)
                                    ),
                                    typeof(EnrollmentModel),
                                    "a"
                                )
                            ),
                            null,
                            null,
                            null
                        )
                    }
                )
            );

            Assert.False(students.First().Enrollments.Any());
        }

        [Fact]
        public void Get_students_with_filtered_inlude_no_filter_sorted_select_expand_definition()
        {
            ICollection<StudentModel> students = ProjectionOperations<StudentModel, Student>.GetItems
            (
                serviceProvider.GetRequiredService<ISchoolRepository>(),
                serviceProvider.GetRequiredService<IMapper>(),
                new FilterLambdaOperatorParameters
                (
                    new GreaterThanBinaryOperatorParameters
                    (
                        new CountOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("Enrollments", new ParameterOperatorParameters("f"))
                        ),
                        new ConstantOperatorParameters(0)
                    ),
                    typeof(StudentModel),
                    "f"
                ),
                null,
                new SelectExpandDefinitionParameters
                (
                    null,
                    new List<SelectExpandItemParameters>
                    {
                        new SelectExpandItemParameters
                        (
                            "enrollments",
                            new SelectExpandItemFilterParameters
                            (
                                new FilterLambdaOperatorParameters
                                (
                                    new GreaterThanBinaryOperatorParameters
                                    (
                                        new MemberSelectorOperatorParameters("enrollmentID", new ParameterOperatorParameters("a")),
                                        new ConstantOperatorParameters(0)
                                    ),
                                    typeof(EnrollmentModel),
                                    "a"
                                )
                            ),
                            new SelectExpandItemQueryFunctionParameters
                            (
                                new SortCollectionParameters
                                (
                                    new List<SortDescriptionParameters>
                                    {
                                        new SortDescriptionParameters("Grade", ListSortDirection.Ascending)
                                    },
                                    null,
                                    null
                                )
                            ),
                            null,
                            null
                        )
                    }
                )
            );

            Assert.True(students.First().Enrollments.Count > 0);
            Assert.True
            (
                string.Compare
                (
                    students.First().Enrollments.First().GradeLetter,
                    students.Skip(1).First().Enrollments.First().GradeLetter
                ) <= 0
            );
        }

        [Fact]
        public void Get_students_with_filtered_inlude_no_filter_sort_skip_and_take_select_expand_definition()
        {
            ICollection<StudentModel> students = ProjectionOperations<StudentModel, Student>.GetItems
            (
                serviceProvider.GetRequiredService<ISchoolRepository>(),
                serviceProvider.GetRequiredService<IMapper>(),
                new FilterLambdaOperatorParameters
                (
                    new AndBinaryOperatorParameters
                    (
                        new EqualsBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("FirstName", new ParameterOperatorParameters("f")),
                            new ConstantOperatorParameters("Carson")
                        ),
                        new EqualsBinaryOperatorParameters
                        (
                            new MemberSelectorOperatorParameters("LastName", new ParameterOperatorParameters("f")),
                            new ConstantOperatorParameters("Alexander")
                        )
                    ),
                    typeof(StudentModel),
                    "f"
                ),
                null,
                new SelectExpandDefinitionParameters
                (
                    null,
                    new List<SelectExpandItemParameters>
                    {
                        new SelectExpandItemParameters
                        (
                            "enrollments",
                            new SelectExpandItemFilterParameters
                            (
                                new FilterLambdaOperatorParameters
                                (
                                    new GreaterThanBinaryOperatorParameters
                                    (
                                        new MemberSelectorOperatorParameters("enrollmentID", new ParameterOperatorParameters("a")),
                                        new ConstantOperatorParameters(0)
                                    ),
                                    typeof(EnrollmentModel),
                                    "a"
                                )
                            ),
                            new SelectExpandItemQueryFunctionParameters
                            (
                                new SortCollectionParameters
                                (
                                    new List<SortDescriptionParameters>
                                    {
                                        new SortDescriptionParameters("Grade", ListSortDirection.Descending)
                                    },
                                    1,
                                    2
                                )
                            ),
                            null,
                            null
                        )
                    }
                )
            );

            Assert.Single(students);
            Assert.Equal(2, students.First().Enrollments.Count);
            Assert.Equal("A", students.First().Enrollments.Last().GradeLetter);
        }

        [Fact]
        public void Get_enrollments_filtered_by_grade_letter()
        {
            ICollection<EnrollmentModel> enrollments = ProjectionOperations<EnrollmentModel, Enrollment>.GetItems
            (
                serviceProvider.GetRequiredService<ISchoolRepository>(),
                serviceProvider.GetRequiredService<IMapper>(),
                new FilterLambdaOperatorParameters
                (
                    new EqualsBinaryOperatorParameters
                    (
                        new MemberSelectorOperatorParameters("GradeLetter", new ParameterOperatorParameters("f")),
                        new ConstantOperatorParameters("A")
                    ),
                    typeof(EnrollmentModel),
                    "f"
                ),
                null,
                null
            );

            Assert.Single(enrollments);
        }
        #endregion Tests

        #region Helpers
        static MapperConfiguration MapperConfiguration;
        private void Initialize()
        {
            if (MapperConfiguration == null)
            {
                MapperConfiguration = new MapperConfiguration(cfg =>
                {
                    cfg.AddExpressionMapping();

                    cfg.AddProfile<ParameterToDescriptorMappingProfile>();
                    cfg.AddProfile<DescriptorToOperatorMappingProfile>();
                    cfg.AddProfile<SchoolProfile>();
                    cfg.AddProfile<ExpansionParameterToDescriptorMappingProfile>();
                    cfg.AddProfile<ExpansionDescriptorToOperatorMappingProfile>();
                });
            }
            MapperConfiguration.AssertConfigurationIsValid();

            serviceProvider = new ServiceCollection()
                .AddDbContext<SchoolContext>
                (
                    options => options.UseSqlServer
                    (
                        @"Server=(localdb)\mssqllocaldb;Database=SchoolContext1;ConnectRetryCount=0"
                    )
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
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            DatabaseSeeder.Seed_Database(serviceProvider.GetRequiredService<ISchoolRepository>()).Wait();
        }
        #endregion Helpers
    }
}
