using Contoso.Bsl.Business.Responses;
using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using Contoso.Web.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Xunit;

namespace Contoso.Bsl.Web.Tests
{
    public class GetTests
    {
        public GetTests()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private IHttpClientFactory clientFactory;
        private const string BASE_URL = "http://localhost:7878/";
        #endregion Fields

        #region Helpers
        private void Initialize()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHttpClient();
            serviceProvider = services.BuildServiceProvider();

            this.clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        }

        private OrderByOperatorDescriptor GetCoursesBodyForCourseModelType()
            => new OrderByOperatorDescriptor
            {
                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "q" },
                SelectorBody = new MemberSelectorOperatorDescriptor
                {
                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                    MemberFullName = "Title"
                },
                SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                SelectorParameterName = "d"
            };

        private SelectOperatorDescriptor GetDepartmentsBodyForDepartmentModelType()
            => new SelectOperatorDescriptor
            {
                SourceOperand = new OrderByOperatorDescriptor
                {
                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "q" },
                    SelectorBody = new MemberSelectorOperatorDescriptor
                    {
                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                        MemberFullName = "Name"
                    },
                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                    SelectorParameterName = "d"
                },
                SelectorBody = new MemberInitOperatorDescriptor
                {
                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                    {
                        ["DepartmentID"] = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                            MemberFullName = "DepartmentID"
                        },
                        ["Name"] = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                            MemberFullName = "Name"
                        }
                    },
                    NewType = typeof(DepartmentModel).AssemblyQualifiedName
                },
                SelectorParameterName = "d"
            };

        private SelectOperatorDescriptor GetBodyForLookupsModel()
            => new SelectOperatorDescriptor
            {
                SourceOperand = new OrderByOperatorDescriptor
                {
                    SourceOperand = new WhereOperatorDescriptor
                    {
                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "q" },
                        FilterBody = new EqualsBinaryOperatorDescriptor
                        {
                            Left = new MemberSelectorOperatorDescriptor
                            {
                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                MemberFullName = "ListName"
                            },
                            Right = new ConstantOperatorDescriptor
                            {
                                ConstantValue = "Credits",
                                Type = typeof(string).AssemblyQualifiedName
                            }
                        },
                        FilterParameterName = "l"
                    },
                    SelectorBody = new MemberSelectorOperatorDescriptor
                    {
                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                        MemberFullName = "NumericValue"
                    },
                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                    SelectorParameterName = "l"
                },
                SelectorBody = new MemberInitOperatorDescriptor
                {
                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                    {
                        ["NumericValue"] = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                            MemberFullName = "NumericValue"
                        },
                        ["Text"] = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                            MemberFullName = "Text"
                        }
                    },
                    NewType = typeof(LookUpsModel).AssemblyQualifiedName
                },
                SelectorParameterName = "l"
            };

        private SelectOperatorDescriptor GetAboutBody_StudentEnrollmentCountByEnrollmentDate()
            => new SelectOperatorDescriptor
            {
                SourceOperand = new OrderByOperatorDescriptor
                {
                    SourceOperand = new GroupByOperatorDescriptor
                    {
                        SourceOperand = new ParameterOperatorDescriptor
                        {
                            ParameterName = "q"
                        },
                        SelectorBody = new MemberSelectorOperatorDescriptor
                        {
                            MemberFullName = "EnrollmentDate",
                            SourceOperand = new ParameterOperatorDescriptor
                            {
                                ParameterName = "item"
                            }
                        },
                        SelectorParameterName = "item"
                    },
                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                    SelectorBody = new MemberSelectorOperatorDescriptor
                    {
                        MemberFullName = "Key",
                        SourceOperand = new ParameterOperatorDescriptor
                        {
                            ParameterName = "group"
                        }
                    },
                    SelectorParameterName = "group"
                },
                SelectorBody = new MemberInitOperatorDescriptor
                {
                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                    {
                        ["DateTimeValue"] = new MemberSelectorOperatorDescriptor
                        {
                            MemberFullName = "Key",
                            SourceOperand = new ParameterOperatorDescriptor
                            {
                                ParameterName = "sel"
                            }
                        },
                        ["NumericValue"] = new ConvertOperatorDescriptor
                        {
                            SourceOperand = new CountOperatorDescriptor
                            {
                                SourceOperand = new AsQueryableOperatorDescriptor()
                                {
                                    SourceOperand = new ParameterOperatorDescriptor
                                    {
                                        ParameterName = "sel"
                                    }
                                }
                            },
                            Type = typeof(double?).FullName
                        }
                    },
                    NewType = typeof(LookUpsModel).AssemblyQualifiedName
                },
                SelectorParameterName = "sel"
            };

        private SelectOperatorDescriptor GetBodyForAdministratorLookup()
            => new SelectOperatorDescriptor
            {
                SourceOperand = new OrderByOperatorDescriptor
                {
                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "$it" },
                    SelectorBody = new MemberSelectorOperatorDescriptor
                    {
                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                        MemberFullName = "FullName"
                    },
                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                    SelectorParameterName = "d"
                },
                SelectorBody = new MemberInitOperatorDescriptor
                {
                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                    {
                        ["ID"] = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                            MemberFullName = "ID"
                        },
                        ["FirstName"] = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                            MemberFullName = "FirstName"
                        },
                        ["LastName"] = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                            MemberFullName = "LastName"
                        },
                        ["FullName"] = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                            MemberFullName = "FullName"
                        }
                    },
                    NewType = typeof(InstructorModel).AssemblyQualifiedName
                },
                SelectorParameterName = "s"
            };

        private EqualsBinaryOperatorDescriptor GetDepartmentByIdFilterBody(int id)
            => new EqualsBinaryOperatorDescriptor
            {
                Left = new MemberSelectorOperatorDescriptor
                {
                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "q" },
                    MemberFullName = "DepartmentID"
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
        #endregion Helpers

        #region Tests
        [Fact]
        public async void GetDropDownListRequest_AdministratorLookup()
        {
            //arrange
            var selectorLambdaOperatorDescriptor = GetExpressionDescriptor<IQueryable<InstructorModel>, IQueryable<InstructorModel>>
            (
                GetBodyForAdministratorLookup(),
                "$it"
            );

            var result = await this.clientFactory.PostAsync<GetListResponse>
            (
                "api/List/GetList",
                JsonSerializer.Serialize
                (
                    new Business.Requests.GetTypedListRequest
                    {
                        Selector = selectorLambdaOperatorDescriptor,
                        ModelType = typeof(InstructorModel).AssemblyQualifiedName,
                        DataType = typeof(Instructor).AssemblyQualifiedName,
                        ModelReturnType = typeof(IQueryable<InstructorModel>).AssemblyQualifiedName,
                        DataReturnType = typeof(IQueryable<Instructor>).AssemblyQualifiedName
                    }
                ),
                BASE_URL
            );

            Assert.True(result.List.Any());
        }

        [Fact]
        public async void GetDropDownListRequest_As_LookUpsModel()
        {
            //arrange
            var selectorLambdaOperatorDescriptor = GetExpressionDescriptor<IQueryable<LookUpsModel>, IEnumerable<LookUpsModel>>
            (
                GetBodyForLookupsModel(),
                "q"
            );

            var result = await this.clientFactory.PostAsync<GetListResponse>
            (
                "api/List/GetList",
                JsonSerializer.Serialize
                (
                    new Business.Requests.GetTypedListRequest
                    {
                        Selector = selectorLambdaOperatorDescriptor,
                        ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                        DataType = typeof(LookUps).AssemblyQualifiedName,
                        ModelReturnType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName,
                        DataReturnType = typeof(IEnumerable<LookUps>).AssemblyQualifiedName
                    }
                ),
                BASE_URL
            );

            Assert.True(result.List.Any());
        }

        [Fact]
        public async void GetDropDownListRequest_As_LookUpsModel_Using_Object_ReturnType()
        {
            //arrange
            var selectorLambdaOperatorDescriptor = GetExpressionDescriptor<IQueryable<LookUpsModel>, IEnumerable<LookUpsModel>>
            (
                GetBodyForLookupsModel(),
                "q"
            );

            var result = await this.clientFactory.PostAsync<GetListResponse>
            (
                "api/List/GetList",
                JsonSerializer.Serialize
                (
                    new Business.Requests.GetTypedListRequest
                    {
                        Selector = selectorLambdaOperatorDescriptor,
                        ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                        DataType = typeof(LookUps).AssemblyQualifiedName,
                        ModelReturnType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName,
                        DataReturnType = typeof(IEnumerable<LookUps>).AssemblyQualifiedName
                    }
                ),
                BASE_URL
            );

            Assert.True(result.List.Any());
        }

        [Fact]
        public async void GetDropDownListRequest_As_DepartmentModel_Using_Object_ReturnType()
        {
            //arrange
            var selectorLambdaOperatorDescriptor = GetExpressionDescriptor<IQueryable<DepartmentModel>, IEnumerable<DepartmentModel>>
            (
                GetDepartmentsBodyForDepartmentModelType(),
                "q"
            );

            var result = await this.clientFactory.PostAsync<GetListResponse>
            (
                "api/List/GetList",
                JsonSerializer.Serialize
                (
                    new Business.Requests.GetTypedListRequest
                    {
                        Selector = selectorLambdaOperatorDescriptor,
                        ModelType = typeof(DepartmentModel).AssemblyQualifiedName,
                        DataType = typeof(Department).AssemblyQualifiedName,
                        ModelReturnType = typeof(IEnumerable<DepartmentModel>).AssemblyQualifiedName,
                        DataReturnType = typeof(IEnumerable<Department>).AssemblyQualifiedName
                    }
                ),
                BASE_URL
            );

            Assert.True(result.List.Any());
        }

        [Fact]
        public async void GetListRequest_As_CourseModel()
        {
            //arrange
            var selectorLambdaOperatorDescriptor = GetExpressionDescriptor<IQueryable<CourseModel>, IEnumerable<CourseModel>>
            (
                GetCoursesBodyForCourseModelType(),
                "q"
            );

            var result = await this.clientFactory.PostAsync<GetListResponse>
            (
                "api/List/GetList",
                JsonSerializer.Serialize
                (
                    new Business.Requests.GetTypedListRequest
                    {
                        Selector = selectorLambdaOperatorDescriptor,
                        ModelType = typeof(CourseModel).AssemblyQualifiedName,
                        DataType = typeof(Course).AssemblyQualifiedName,
                        ModelReturnType = typeof(IEnumerable<CourseModel>).AssemblyQualifiedName,
                        DataReturnType = typeof(IEnumerable<Course>).AssemblyQualifiedName
                    }
                ),
                BASE_URL
            );

            Assert.True(result.List.Any());
        }

        [Fact]
        public async void GetEntityRequest_As_DeopartmentModel()
        {
            var result = await this.clientFactory.PostAsync<GetEntityResponse>
            (
                "api/Entity/GetEntity",
                JsonSerializer.Serialize
                (
                    new Business.Requests.GetEntityRequest
                    {
                        Filter = GetFilterExpressionDescriptor<DepartmentModel>
                        (
                            GetDepartmentByIdFilterBody(2),
                            "q"
                        ),
                        SelectExpandDefinition = new Common.Configuration.ExpansionDescriptors.SelectExpandDefinitionDescriptor
                        {
                            ExpandedItems = new List<Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor>
                            {
                                new Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor
                                {
                                    MemberName = "Courses"
                                }
                            }
                        },
                        ModelType = typeof(DepartmentModel).AssemblyQualifiedName,
                        DataType = typeof(Department).AssemblyQualifiedName
                    }
                ),
                BASE_URL
            );

            Assert.NotNull(result);
            Assert.NotNull(result.Entity);
        }

        [Fact]
        public async void GetAboutListRequest_StudentEnrollmentCountByEnrollmentDate_As_LookUpsModel()
        {
            //arrange
            var selectorLambdaOperatorDescriptor = GetExpressionDescriptor<IQueryable<StudentModel>, IEnumerable<LookUpsModel>>
            (
                GetAboutBody_StudentEnrollmentCountByEnrollmentDate(),
                "q"
            );

            var result = await this.clientFactory.PostAsync<GetListResponse>
            (
                "api/List/GetList",
                JsonSerializer.Serialize
                (
                    new Business.Requests.GetTypedListRequest
                    {
                        Selector = selectorLambdaOperatorDescriptor,
                        ModelType = typeof(StudentModel).AssemblyQualifiedName,
                        DataType = typeof(Student).AssemblyQualifiedName,
                        ModelReturnType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName,
                        DataReturnType = typeof(IEnumerable<LookUps>).AssemblyQualifiedName
                    }
                ),
                BASE_URL
            );

            Assert.True(result.List.Any());
        }
        #endregion Tests
    }
}
