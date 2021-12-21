using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Enrollment.AutoMapperProfiles;
using Enrollment.Bsl.Utils;
using Enrollment.BSL.AutoMapperProfiles;
using Enrollment.Common.Configuration.ExpressionDescriptors;
using Enrollment.Common.Utils;
using Enrollment.Contexts;
using Enrollment.Data.Entities;
using Enrollment.Domain.Entities;
using Enrollment.Repositories;
using Enrollment.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml;
using Xunit;

namespace Enrollment.Bsl.Flow.Integration.Tests.GetRequests
{
    public class RequestHelpersTest
    {
        public RequestHelpersTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void Select_Residencies_In_Ascending_Order_As_LookUpsModel_Type()
        {
            //arrange
            var selectorLambdaOperatorDescriptor = GetExpressionDescriptor<IQueryable<ResidencyModel>, IEnumerable<LookUpsModel>>
            (
                GetResidenciesBodyForLookupModelType(),
                "q"
            );
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            IEnrollmentRepository repository = serviceProvider.GetRequiredService<IEnrollmentRepository>();

            //act
            var expression = mapper.MapToOperator(selectorLambdaOperatorDescriptor).Build();
            var list = RequestHelpers.GetList<ResidencyModel, Residency, IEnumerable<LookUpsModel>, IEnumerable<LookUps>>
            (
                new Business.Requests.GetTypedListRequest
                {
                    Selector = selectorLambdaOperatorDescriptor
                },
                repository,
                mapper
            ).Result.List;

            //assert
            AssertFilterStringIsCorrect(expression, "q => Convert(q.OrderBy(d => d.DriversLicenseNumber).Select(d => new LookUpsModel() {NumericValue = Convert(d.UserId), Text = d.DriversLicenseNumber}))");
            Assert.Equal(2, list.Count());
        }

        [Fact]
        public void Select_Users_In_Ascending_Order_As_UserModel_Type()
        {
            //arrange
            var selectorLambdaOperatorDescriptor = GetExpressionDescriptor<IQueryable<UserModel>, IEnumerable<UserModel>>
            (
                GetUsersBodyForUserModelType(),
                "q"
            );
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            IEnrollmentRepository repository = serviceProvider.GetRequiredService<IEnrollmentRepository>();

            //act
            var expression = mapper.MapToOperator(selectorLambdaOperatorDescriptor).Build();
            var list = RequestHelpers.GetList<UserModel, User, IEnumerable<UserModel>, IEnumerable<User>>
            (
                new Business.Requests.GetTypedListRequest
                {
                    Selector = selectorLambdaOperatorDescriptor,
                    ModelType = typeof(UserModel).AssemblyQualifiedName,
                    DataType = typeof(User).AssemblyQualifiedName,
                    ModelReturnType = typeof(IEnumerable<UserModel>).AssemblyQualifiedName,
                    DataReturnType = typeof(IEnumerable<User>).AssemblyQualifiedName
                },
                repository,
                mapper
            ).Result.List.ToList();

            //assert
            AssertFilterStringIsCorrect(expression, "q => Convert(q.OrderBy(d => d.UserName))");
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void Get_Residency_ById_And_Courses_WithGenericHelper()
        {
            //arrange
            var filterLambdaOperatorDescriptor = GetFilterExpressionDescriptor<ResidencyModel>
            (
                GetResidencyByIdFilterBody(1),
                "q"
            );

            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            IEnrollmentRepository repository = serviceProvider.GetRequiredService<IEnrollmentRepository>();

            //act
            var expression = mapper.MapToOperator(filterLambdaOperatorDescriptor).Build();
            var selectAndExpand = new Common.Configuration.ExpansionDescriptors.SelectExpandDefinitionDescriptor
            {
                ExpandedItems = new List<Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor>
                {
                    new Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor
                    {
                        MemberName = "StatesLivedIn"
                    }
                }
            };

            var entity = (ResidencyModel)RequestHelpers.GetEntity<ResidencyModel, Residency>
            (
                new Business.Requests.GetEntityRequest
                {
                    Filter = filterLambdaOperatorDescriptor,
                    SelectExpandDefinition = selectAndExpand
                },
                repository,
                mapper
            ).Result.Entity;

            //assert
            AssertFilterStringIsCorrect(expression, "q => (q.UserId == 1)");
            Assert.Equal(2, entity.StatesLivedIn.Count);
        }

        [Fact]
        public void Get_Residency_ById_And_Courses_WithoutGenericHelper()
        {
            //arrange
            var filterLambdaOperatorDescriptor = GetFilterExpressionDescriptor<ResidencyModel>
            (
                GetResidencyByIdFilterBody(2),
                "q"
            );

            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            IEnrollmentRepository repository = serviceProvider.GetRequiredService<IEnrollmentRepository>();

            //act
            var expression = mapper.MapToOperator(filterLambdaOperatorDescriptor).Build();
            var selectAndExpand = new Common.Configuration.ExpansionDescriptors.SelectExpandDefinitionDescriptor
            {
                ExpandedItems = new List<Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor>
                {
                    new Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor
                    {
                        MemberName = "StatesLivedIn"
                    }
                }
            };

            var entity = (ResidencyModel)RequestHelpers.GetEntity
            (
                new Business.Requests.GetEntityRequest
                {
                    Filter = filterLambdaOperatorDescriptor,
                    SelectExpandDefinition = selectAndExpand,
                    ModelType = typeof(ResidencyModel).AssemblyQualifiedName,
                    DataType = typeof(Residency).AssemblyQualifiedName
                },
                repository,
                mapper
            ).Result.Entity;

            //assert
            AssertFilterStringIsCorrect(expression, "q => (q.UserId == 2)");
            Assert.Equal(2, entity.StatesLivedIn.Count);
        }

        [Fact]
        public void Select_Residencies_In_Ascending_Order_As_ResidencyModel_Type()
        {
            //arrange
            var selectorLambdaOperatorDescriptor = GetExpressionDescriptor<IQueryable<ResidencyModel>, IEnumerable<ResidencyModel>>
            (
                GetResidenciesBodyForResidencyModelType(),
                "q"
            );
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            IEnrollmentRepository repository = serviceProvider.GetRequiredService<IEnrollmentRepository>();

            //act
            var expression = mapper.MapToOperator(selectorLambdaOperatorDescriptor).Build();
            var list = RequestHelpers.GetList<ResidencyModel, Residency, IEnumerable<ResidencyModel>, IEnumerable<Residency>>
            (
                new Business.Requests.GetTypedListRequest
                {
                    Selector = selectorLambdaOperatorDescriptor
                },
                repository,
                mapper
            ).Result.List;

            //assert
            AssertFilterStringIsCorrect(expression, "q => Convert(q.OrderBy(d => d.DriversLicenseNumber).Select(d => new ResidencyModel() {UserId = d.UserId, DriversLicenseNumber = d.DriversLicenseNumber}))");
            Assert.Equal(2, list.Count());
        }

        [Fact]
        public void Select_Residencies_In_Ascending_Order_As_ResidencyModel_Type_With_Courses()
        {
            //arrange
            var selectorLambdaOperatorDescriptor = GetExpressionDescriptor<IQueryable<ResidencyModel>, IQueryable<ResidencyModel>>
            (
                GetResidenciesBodyOrderByDriversLicenseNumber(),
                "q"
            );
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            IEnrollmentRepository repository = serviceProvider.GetRequiredService<IEnrollmentRepository>();

            //act
            var expression = mapper.MapToOperator(selectorLambdaOperatorDescriptor).Build();
            var selectAndExpand = new Common.Configuration.ExpansionDescriptors.SelectExpandDefinitionDescriptor
            {
                ExpandedItems = new List<Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor>
                {
                    new Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor
                    {
                        MemberName = "StatesLivedIn"
                    }
                }
            };
            var list = RequestHelpers.GetList<ResidencyModel, Residency, IQueryable<ResidencyModel>, IQueryable<Residency>>
            (
                new Business.Requests.GetTypedListRequest
                {
                    Selector = selectorLambdaOperatorDescriptor,
                    SelectExpandDefinition = selectAndExpand
                },
                repository,
                mapper
            ).Result.List.Cast<ResidencyModel>().ToList();

            //assert
            AssertFilterStringIsCorrect(expression, "q => Convert(q.OrderBy(d => d.DriversLicenseNumber))");
            Assert.Equal(2, list.Count);
            Assert.True(list.All(d => d.StatesLivedIn.Any()));
        }

        #region Helpers
        private SelectOperatorDescriptor GetResidenciesBodyForLookupModelType()
            => new SelectOperatorDescriptor
            {
                SourceOperand = new OrderByOperatorDescriptor
                {
                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "q" },
                    SelectorBody = new MemberSelectorOperatorDescriptor
                    {
                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                        MemberFullName = "DriversLicenseNumber"
                    },
                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                    SelectorParameterName = "d"
                },
                SelectorBody = new MemberInitOperatorDescriptor
                {
                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                    {
                        ["NumericValue"] = new ConvertOperatorDescriptor
                        {
                            SourceOperand = new MemberSelectorOperatorDescriptor
                            {
                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                                MemberFullName = "UserId"
                            },
                            Type = typeof(double?).FullName
                        },
                        ["Text"] = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                            MemberFullName = "DriversLicenseNumber"
                        }
                    },
                    NewType = typeof(LookUpsModel).AssemblyQualifiedName
                },
                SelectorParameterName = "d"
            };

        private SelectOperatorDescriptor GetResidenciesBodyForResidencyModelType()
            => new SelectOperatorDescriptor
            {
                SourceOperand = new OrderByOperatorDescriptor
                {
                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "q" },
                    SelectorBody = new MemberSelectorOperatorDescriptor
                    {
                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                        MemberFullName = "DriversLicenseNumber"
                    },
                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                    SelectorParameterName = "d"
                },
                SelectorBody = new MemberInitOperatorDescriptor
                {
                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                    {
                        ["UserId"] = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                            MemberFullName = "UserId"
                        },
                        ["DriversLicenseNumber"] = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                            MemberFullName = "DriversLicenseNumber"
                        }
                    },
                    NewType = typeof(ResidencyModel).AssemblyQualifiedName
                },
                SelectorParameterName = "d"
            };

        private OrderByOperatorDescriptor GetResidenciesBodyOrderByDriversLicenseNumber()
            => new OrderByOperatorDescriptor
            {
                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "q" },
                SelectorBody = new MemberSelectorOperatorDescriptor
                {
                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                    MemberFullName = "DriversLicenseNumber"
                },
                SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                SelectorParameterName = "d"
            };

        private OrderByOperatorDescriptor GetUsersBodyForUserModelType()
            => new OrderByOperatorDescriptor
            {
                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "q" },
                SelectorBody = new MemberSelectorOperatorDescriptor
                {
                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                    MemberFullName = "UserName"
                },
                SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                SelectorParameterName = "d"
            };

        private EqualsBinaryOperatorDescriptor GetResidencyByIdFilterBody(int id)
            => new EqualsBinaryOperatorDescriptor
            {
                Left = new MemberSelectorOperatorDescriptor
                {
                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "q" },
                    MemberFullName = "UserId"
                },
                Right = new ConstantOperatorDescriptor { Type = typeof(int).FullName, ConstantValue = id }
            };

        private SelectorLambdaOperatorDescriptor GetExpressionDescriptor<T, TResult>(OperatorDescriptorBase selectorBody, string parameterName = "$it")
            => new SelectorLambdaOperatorDescriptor
            {
                Selector = selectorBody,
                SourceElementType = typeof(T).AssemblyQualifiedName,
                ParameterName = parameterName,
                BodyType = typeof(TResult).AssemblyQualifiedName
            };

        private FilterLambdaOperatorDescriptor GetFilterExpressionDescriptor<T>(OperatorDescriptorBase filterBody, string parameterName = "$it")
            => new FilterLambdaOperatorDescriptor
            {
                FilterBody = filterBody,
                SourceElementType = typeof(T).AssemblyQualifiedName,
                ParameterName = parameterName
            };

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

                    cfg.AddProfile<DescriptorToOperatorMappingProfile>();
                    cfg.AddProfile<EnrollmentProfile>();
                    cfg.AddProfile<ExpansionDescriptorToOperatorMappingProfile>();
                });
            }
            MapperConfiguration.AssertConfigurationIsValid();
            serviceProvider = new ServiceCollection()
                .AddDbContext<EnrollmentContext>
                (
                    options => options.UseSqlServer
                    (
                        @"Server=(localdb)\mssqllocaldb;Database=RequestHelpersTest;ConnectRetryCount=0"
                    ),
                    ServiceLifetime.Transient
                )
                .AddTransient<IEnrollmentStore, EnrollmentStore>()
                .AddTransient<IEnrollmentRepository, EnrollmentRepository>()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    MapperConfiguration
                )
                .AddTransient<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .BuildServiceProvider();

            EnrollmentContext context = serviceProvider.GetRequiredService<EnrollmentContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            DatabaseSeeder.Seed_Database(serviceProvider.GetRequiredService<IEnrollmentRepository>()).Wait();
        }
        #endregion Helpers
    }
}
