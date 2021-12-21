using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Contoso.AutoMapperProfiles;
using Contoso.BSL.AutoMapperProfiles;
using Contoso.Common.Utils;
using Contoso.Contexts;
using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using Contoso.Parameters.Expansions;
using Contoso.Parameters.Expressions;
using Contoso.Repositories;
using Contoso.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Contoso.Bsl.Flow.Integration.Tests
{
    public class PersistenceOperationsTest
    {
        public PersistenceOperationsTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private static readonly string parameterName = "$it";
        #endregion Fields

        #region Tests
        [Fact]
        public void Add_A_Single_Entity()
        {
            //arrange
            var bodyParameter = new EqualsBinaryOperatorParameters
            (
                new MemberSelectorOperatorParameters("FullName", new ParameterOperatorParameters(parameterName)),
                new ConstantOperatorParameters("Roger Milla")
            );

            //act
            DoTest<StudentModel, Student>
            (
                bodyParameter,
                null,
                new SelectExpandDefinitionParameters
                {
                    ExpandedItems = new List<SelectExpandItemParameters>
                    {
                        new SelectExpandItemParameters { MemberName = "Enrollments" }
                    }
                },
                parameterName,
                (StudentModel studentModel, ISchoolRepository repository) =>
                {
                    PersistenceOperations<StudentModel, Student>.Save
                    (
                        repository, 
                        new StudentModel
                        {
                            EntityState = LogicBuilder.Domain.EntityStateType.Added,
                            EnrollmentDate = new DateTime(2021, 2, 8),
                            Enrollments = new List<EnrollmentModel>
                            {
                                new EnrollmentModel { CourseID = 1050, Grade = Domain.Entities.Grade.A },
                                new EnrollmentModel { CourseID = 4022, Grade = Domain.Entities.Grade.A },
                                new EnrollmentModel { CourseID = 4041, Grade = Domain.Entities.Grade.A }
                            },
                            FirstName = "Roger",
                            LastName = "Milla"
                        }
                    );
                },
                returnValue =>
                {
                    Assert.Equal(new DateTime(2021, 2, 8), returnValue.EnrollmentDate);
                    Assert.Empty(returnValue.Enrollments);
                },
                "$it => ($it.FullName == \"Roger Milla\")"
            );
        }

        [Fact]
        public void Add_An_Object_Graph()
        {
            //arrange
            var bodyParameter = new EqualsBinaryOperatorParameters
            (
                new MemberSelectorOperatorParameters("FullName", new ParameterOperatorParameters(parameterName)),
                new ConstantOperatorParameters("Roger Milla")
            );

            //act
            DoTest<StudentModel, Student>
            (
                bodyParameter,
                null,
                new SelectExpandDefinitionParameters
                {
                    ExpandedItems = new List<SelectExpandItemParameters>
                    {
                        new SelectExpandItemParameters { MemberName = "Enrollments" }
                    }
                },
                parameterName,
                (StudentModel studentModel, ISchoolRepository repository) =>
                {
                    PersistenceOperations<StudentModel, Student>.SaveGraph
                    (
                        repository,
                        new StudentModel
                        {
                            EntityState = LogicBuilder.Domain.EntityStateType.Added,
                            EnrollmentDate = new DateTime(2021, 2, 8),
                            Enrollments = new List<EnrollmentModel>
                            {
                                new EnrollmentModel { CourseID = 1050, Grade = Domain.Entities.Grade.A },
                                new EnrollmentModel { CourseID = 4022, Grade = Domain.Entities.Grade.A },
                                new EnrollmentModel { CourseID = 4041, Grade = Domain.Entities.Grade.A }
                            },
                            FirstName = "Roger",
                            LastName = "Milla"
                        }
                    );
                },
                returnValue =>
                {
                    Assert.Equal(new DateTime(2021, 2, 8), returnValue.EnrollmentDate);
                    Assert.Equal(3, returnValue.Enrollments.Count());
                },
                "$it => ($it.FullName == \"Roger Milla\")"
            );
        }

        [Fact]
        public void Delete_A_Single_Entity_Using_Save()
        {
            //arrange
            var bodyParameter = new EqualsBinaryOperatorParameters
            (
                new MemberSelectorOperatorParameters("FullName", new ParameterOperatorParameters(parameterName)),
                new ConstantOperatorParameters("Carson Alexander")
            );

            //act
            DoTest<StudentModel, Student>
            (
                bodyParameter,
                null,
                null,
                parameterName,
                (StudentModel studentModel, ISchoolRepository repository) =>
                {
                    studentModel.EntityState = LogicBuilder.Domain.EntityStateType.Deleted;
                    PersistenceOperations<StudentModel, Student>.Save(repository, studentModel);
                },
                returnValue =>
                {
                    Assert.Null(returnValue);
                },
                "$it => ($it.FullName == \"Carson Alexander\")"
            );
        }

        [Fact]
        public void Delete_A_Single_Entity_Using_Delete()
        {
            //arrange
            var bodyParameter = new EqualsBinaryOperatorParameters
            (
                new MemberSelectorOperatorParameters("FullName", new ParameterOperatorParameters(parameterName)),
                new ConstantOperatorParameters("Carson Alexander")
            );

            //act
            DoTest<StudentModel, Student>
            (
                bodyParameter,
                null,
                null,
                parameterName,
                (StudentModel studentModel, ISchoolRepository repository) =>
                {
                    IExpressionParameter expressionParameter = GetFilterParameter<StudentModel>(bodyParameter, parameterName);
                    Expression<Func<StudentModel, bool>> expression = ProjectionOperations<StudentModel, Student>.GetFilter
                    (
                        serviceProvider.GetRequiredService<IMapper>().MapToOperator(expressionParameter)
                    );

                    PersistenceOperations<StudentModel, Student>.Delete(repository, expression);
                },
                returnValue =>
                {
                    Assert.Null(returnValue);
                },
                "$it => ($it.FullName == \"Carson Alexander\")"
            );
        }

        [Fact]
        public void Update_A_Single_Entity()
        {
            //arrange
            var bodyParameter = new EqualsBinaryOperatorParameters
            (
                new MemberSelectorOperatorParameters("FullName", new ParameterOperatorParameters(parameterName)),
                new ConstantOperatorParameters("Carson Alexander")
            );

            //act
            DoTest<StudentModel, Student>
            (
                bodyParameter,
                null,
                null,
                parameterName,
                (StudentModel studentModel, ISchoolRepository repository) => 
                {
                    studentModel.EnrollmentDate = new DateTime(2021, 2, 8);
                    studentModel.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
                    PersistenceOperations<StudentModel, Student>.Save(repository, studentModel);
                },
                returnValue =>
                {
                    Assert.Equal(new DateTime(2021, 2, 8), returnValue.EnrollmentDate);
                },
                "$it => ($it.FullName == \"Carson Alexander\")"
            );
        }

        [Fact]
        public void Add_An_Entry_To_A_Child_Collection()
        {
            //arrange
            var bodyParameter = new EqualsBinaryOperatorParameters
            (
                new MemberSelectorOperatorParameters("FullName", new ParameterOperatorParameters(parameterName)),
                new ConstantOperatorParameters("Carson Alexander")
            );

            //act
            DoTest<StudentModel, Student>
            (
                bodyParameter,
                null,
                new SelectExpandDefinitionParameters
                {
                    ExpandedItems = new List<SelectExpandItemParameters>
                    {
                        new SelectExpandItemParameters { MemberName = "Enrollments" }
                    }
                },
                parameterName,
                (StudentModel studentModel, ISchoolRepository repository) =>
                {
                    studentModel.EnrollmentDate = new DateTime(2021, 2, 8);
                    studentModel.Enrollments.Add
                    (
                        new EnrollmentModel
                        {
                            CourseID = 3141,
                            Grade = Domain.Entities.Grade.B,
                            EntityState = LogicBuilder.Domain.EntityStateType.Added
                        }
                    );
                    
                    studentModel.EntityState = LogicBuilder.Domain.EntityStateType.Modified;

                    PersistenceOperations<StudentModel, Student>.SaveGraph(repository, studentModel);
                },
                returnValue =>
                {
                    Assert.Equal(new DateTime(2021, 2, 8), returnValue.EnrollmentDate);
                    Assert.Equal(Domain.Entities.Grade.B, returnValue.Enrollments.Single(e => e.CourseID == 3141).Grade);
                    Assert.Equal(4, returnValue.Enrollments.Count());
                },
                "$it => ($it.FullName == \"Carson Alexander\")"
            );
        }

        [Fact]
        public void Update_An_Entry_In_A_Child_Collection()
        {
            //arrange
            var bodyParameter = new EqualsBinaryOperatorParameters
            (
                new MemberSelectorOperatorParameters("FullName", new ParameterOperatorParameters(parameterName)),
                new ConstantOperatorParameters("Carson Alexander")
            );

            //act
            DoTest<StudentModel, Student>
            (
                bodyParameter,
                null,
                new SelectExpandDefinitionParameters
                {
                    ExpandedItems = new List<SelectExpandItemParameters>
                    {
                        new SelectExpandItemParameters { MemberName = "Enrollments" }
                    }
                },
                parameterName,
                (StudentModel studentModel, ISchoolRepository repository) =>
                {
                    studentModel.EnrollmentDate = new DateTime(2021, 2, 8);
                    var enrollment = studentModel.Enrollments.Single(e => e.CourseID == 1050);
                    enrollment.Grade = Domain.Entities.Grade.B;

                    studentModel.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
                    enrollment.EntityState = LogicBuilder.Domain.EntityStateType.Modified;

                    PersistenceOperations<StudentModel, Student>.SaveGraph(repository, studentModel);
                },
                returnValue =>
                {
                    Assert.Equal(new DateTime(2021, 2, 8), returnValue.EnrollmentDate);
                    Assert.Equal(Domain.Entities.Grade.B, returnValue.Enrollments.Single(e => e.CourseID == 1050).Grade);
                    Assert.Equal(3, returnValue.Enrollments.Count());
                },
                "$it => ($it.FullName == \"Carson Alexander\")"
            );
        }

        [Fact]
        public void Delete_An_Entry_From_A_Child_Collection()
        {
            //arrange
            var bodyParameter = new EqualsBinaryOperatorParameters
            (
                new MemberSelectorOperatorParameters("FullName", new ParameterOperatorParameters(parameterName)),
                new ConstantOperatorParameters("Carson Alexander")
            );

            //act
            DoTest<StudentModel, Student>
            (
                bodyParameter,
                null,
                new SelectExpandDefinitionParameters
                {
                    ExpandedItems = new List<SelectExpandItemParameters>
                    {
                        new SelectExpandItemParameters { MemberName = "Enrollments" }
                    }
                },
                parameterName,
                (StudentModel studentModel, ISchoolRepository repository) =>
                {
                    studentModel.EnrollmentDate = new DateTime(2021, 2, 8);
                    var enrollment = studentModel.Enrollments.Single(e => e.CourseID == 1050);
                    enrollment.Grade = Domain.Entities.Grade.B;

                    studentModel.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
                    enrollment.EntityState = LogicBuilder.Domain.EntityStateType.Deleted;

                    PersistenceOperations<StudentModel, Student>.SaveGraph(repository, studentModel);
                },
                returnValue =>
                {
                    Assert.Equal(new DateTime(2021, 2, 8), returnValue.EnrollmentDate);
                    Assert.Null(returnValue.Enrollments.SingleOrDefault(e => e.CourseID == 1050));
                    Assert.Equal(2, returnValue.Enrollments.Count());
                },
                "$it => ($it.FullName == \"Carson Alexander\")"
            );
        }
        #endregion Tests

        #region Helpers
        private IExpressionParameter GetFilterParameter<T>(IExpressionParameter selectorBody, string parameterName = "$it")
            => new FilterLambdaOperatorParameters
            (
                selectorBody,
                typeof(T),
                parameterName
            );

        void DoTest<TModel, TData>(IExpressionParameter bodyParameter,
            IExpressionParameter queryFunc,
            SelectExpandDefinitionParameters expansion,
            string parameterName, 
            Action<TModel, ISchoolRepository> update, 
            Action<TModel> assert, 
            string expectedExpressionString) where TModel : LogicBuilder.Domain.BaseModel where TData : LogicBuilder.Data.BaseData
        {
            //arrange
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();
            IExpressionParameter expressionParameter = GetFilterParameter<TModel>(bodyParameter, parameterName);

            TestExpressionString();
            TestReturnValue();

            void TestReturnValue()
            {
                //act
                TModel returnValue = ProjectionOperations<TModel, TData>.Get
                (
                    repository,
                    mapper,
                    expressionParameter,
                    queryFunc,
                    expansion
                );

                update(returnValue, repository);

                returnValue = ProjectionOperations<TModel, TData>.Get
                (
                    repository,
                    mapper,
                    expressionParameter,
                    queryFunc,
                    expansion
                );

                //assert
                assert(returnValue);
            }

            void TestExpressionString()
            {
                //act
                Expression<Func<TModel, bool>> expression = ProjectionOperations<TModel, TData>.GetFilter
                (
                    mapper.MapToOperator(expressionParameter)
                );

                //assert
                if (!string.IsNullOrEmpty(expectedExpressionString))
                {
                    AssertFilterStringIsCorrect(expression, expectedExpressionString);
                }
            }
        }

        private void AssertFilterStringIsCorrect(Expression expression, string expected)
        {
            AssertStringIsCorrect(ExpressionStringBuilder.ToString(expression));

            void AssertStringIsCorrect(string resultExpression)
                => Assert.True
                (
                    expected == resultExpression,
                    $"Expected expression '{expected}' but the deserializer produced '{resultExpression}'"
                );
        }

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
                        @"Server=(localdb)\mssqllocaldb;Database=SchoolContext4;ConnectRetryCount=0"
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

            DatabaseSeeder.Seed_Database(serviceProvider.GetRequiredService<ISchoolRepository>()).Wait();
        }
        #endregion Helpers
    }
}
