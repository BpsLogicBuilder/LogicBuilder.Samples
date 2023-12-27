using Enrollment.Bsl.Business.Responses;
using Enrollment.Common.Configuration.ExpressionDescriptors;
using Enrollment.Data.Entities;
using Enrollment.Domain.Entities;
using Enrollment.Web.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Xunit;

namespace Enrollment.Api.Web.Tests
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

        private static SelectOperatorDescriptor GetBodyForLookupsModelAsAnonymousTypes()
            => new()
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
                                ConstantValue = "militaryBranch",
                                Type = typeof(string).AssemblyQualifiedName
                            }
                        },
                        FilterParameterName = "l"
                    },
                    SelectorBody = new MemberSelectorOperatorDescriptor
                    {
                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                        MemberFullName = "Text"
                    },
                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                    SelectorParameterName = "l"
                },
                SelectorBody = new MemberInitOperatorDescriptor
                {
                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                    {
                        ["Value"] = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                            MemberFullName = "Value"
                        },
                        ["Text"] = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                            MemberFullName = "Text"
                        }
                    }
                },
                SelectorParameterName = "l"
            };

        private static SelectOperatorDescriptor GetBodyForLookupsModel()
            => new()
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
                                ConstantValue = "militaryBranch",
                                Type = typeof(string).AssemblyQualifiedName
                            }
                        },
                        FilterParameterName = "l"
                    },
                    SelectorBody = new MemberSelectorOperatorDescriptor
                    {
                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                        MemberFullName = "Text"
                    },
                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                    SelectorParameterName = "l"
                },
                SelectorBody = new MemberInitOperatorDescriptor
                {
                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                    {
                        ["Value"] = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                            MemberFullName = "Value"
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

        private static SelectOperatorDescriptor GetBodyConvertLookupsModelToStatesLivedInModel()
            => new()
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
                                ConstantValue = "states",
                                Type = typeof(string).AssemblyQualifiedName
                            }
                        },
                        FilterParameterName = "l"
                    },
                    SelectorBody = new MemberSelectorOperatorDescriptor
                    {
                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                        MemberFullName = "Text"
                    },
                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                    SelectorParameterName = "l"
                },
                SelectorBody = new MemberInitOperatorDescriptor
                {
                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                    {
                        ["State"] = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                            MemberFullName = "Value"
                        }
                    },
                    NewType = typeof(StateLivedInModel).AssemblyQualifiedName
                },
                SelectorParameterName = "l"
            };

        private static EqualsBinaryOperatorDescriptor GetResidencyByIdFilterBody(int id)
            => new()
            {
                Left = new MemberSelectorOperatorDescriptor
                {
                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "q" },
                    MemberFullName = "UserId"
                },
                Right = new ConstantOperatorDescriptor { Type = typeof(int).FullName, ConstantValue = id }
            };

        private static EqualsBinaryOperatorDescriptor GetResidencyByIdFilterBodyFromObjectConstant(ResidencyModel residency)
            => new()
            {
                Left = new MemberSelectorOperatorDescriptor
                {
                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "q" },
                    MemberFullName = "UserId"
                },
                Right = new MemberSelectorOperatorDescriptor
                {
                    SourceOperand = new ConstantOperatorDescriptor { Type = typeof(ResidencyModel).FullName, ConstantValue = residency },
                    MemberFullName = "UserId"
                }
            };

        private static SelectorLambdaOperatorDescriptor GetExpressionDescriptor<T, TResult>(OperatorDescriptorBase selectorBody, string parameterName = "$it")
            => new()
            {
                Selector = selectorBody,
                SourceElementType = typeof(T).AssemblyQualifiedName,
                ParameterName = parameterName,
                BodyType = typeof(TResult).AssemblyQualifiedName
            };

        private static FilterLambdaOperatorDescriptor GetFilterExpressionDescriptor<T>(OperatorDescriptorBase filterBody, string parameterName = "$it")
            => new()
            {
                FilterBody = filterBody,
                SourceElementType = typeof(T).AssemblyQualifiedName,
                ParameterName = parameterName
            };
        #endregion Helpers

        [Fact]
        public async void GetDropDownListRequest_As_AnonymousTypes()
        {
            //arrange
            var selectorLambdaOperatorDescriptor = GetExpressionDescriptor<IQueryable<LookUpsModel>, IEnumerable<object>>
            (
                GetBodyForLookupsModelAsAnonymousTypes(),
                "q"
            );

            var result = await this.clientFactory.PostAsync<System.Text.Json.Nodes.JsonObject>
            (
                "api/AnonymousTypeList/GetList",
                JsonSerializer.Serialize
                (
                    new Bsl.Business.Requests.GetObjectListRequest
                    {
                        Selector = selectorLambdaOperatorDescriptor,
                        ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                        DataType = typeof(LookUps).AssemblyQualifiedName
                    }
                ),
                BASE_URL
            );

            Assert.NotEmpty(result);
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
                    new Bsl.Business.Requests.GetTypedListRequest
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
        public async void GetDropDownListRequest_As_SatesLiveInModel()
        {
            //arrange
            var selectorLambdaOperatorDescriptor = GetExpressionDescriptor<IQueryable<LookUpsModel>, IEnumerable<StateLivedInModel>>
            (
                GetBodyConvertLookupsModelToStatesLivedInModel(),
                "q"
            );

            var result = await this.clientFactory.PostAsync<GetListResponse>
            (
                "api/List/GetList",
                JsonSerializer.Serialize
                (
                    new Bsl.Business.Requests.GetTypedListRequest
                    {
                        Selector = selectorLambdaOperatorDescriptor,
                        ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                        DataType = typeof(LookUps).AssemblyQualifiedName,
                        ModelReturnType = typeof(IEnumerable<StateLivedInModel>).AssemblyQualifiedName,
                        DataReturnType = typeof(IEnumerable<StateLivedIn>).AssemblyQualifiedName
                    }
                ),
                BASE_URL
            );

            Assert.True(result.List.Any());
        }

        [Fact]
        public async void GetEntityRequest_As_ResidencyModel()
        {
            var result = await this.clientFactory.PostAsync<GetEntityResponse>
            (
                "api/Entity/GetEntity",
                JsonSerializer.Serialize
                (
                    new Bsl.Business.Requests.GetEntityRequest
                    {
                        Filter = GetFilterExpressionDescriptor<ResidencyModel>
                        (
                            GetResidencyByIdFilterBody(1),
                            "q"
                        ),
                        SelectExpandDefinition = new Common.Configuration.ExpansionDescriptors.SelectExpandDefinitionDescriptor
                        {
                            ExpandedItems =
                            [
                                new Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor
                                {
                                    MemberName = "StatesLivedIn"
                                }
                            ]
                        },
                        ModelType = typeof(ResidencyModel).AssemblyQualifiedName,
                        DataType = typeof(Residency).AssemblyQualifiedName
                    }
                ),
                BASE_URL
            );

            Assert.NotNull(result);
            Assert.NotNull(result.Entity);
            Assert.NotEmpty(((ResidencyModel)result.Entity).StatesLivedIn);
        }

        [Fact]
        public async void GetEntityRequest_As_ResidencyModel_FromObjectConstant()
        {
            var result = await this.clientFactory.PostAsync<GetEntityResponse>
            (
                "api/Entity/GetEntity",
                JsonSerializer.Serialize
                (
                    new Bsl.Business.Requests.GetEntityRequest
                    {
                        Filter = GetFilterExpressionDescriptor<ResidencyModel>
                        (
                            GetResidencyByIdFilterBodyFromObjectConstant(new ResidencyModel { UserId = 1 }),
                            "q"
                        ),
                        SelectExpandDefinition = new Common.Configuration.ExpansionDescriptors.SelectExpandDefinitionDescriptor
                        {
                            ExpandedItems =
                            [
                                new Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor
                                {
                                    MemberName = "StatesLivedIn"
                                }
                            ]
                        },
                        ModelType = typeof(ResidencyModel).AssemblyQualifiedName,
                        DataType = typeof(Residency).AssemblyQualifiedName
                    }
                ),
                BASE_URL
            );

            Assert.NotNull(result);
            Assert.NotNull(result.Entity);
            Assert.NotEmpty(((ResidencyModel)result.Entity).StatesLivedIn);
        }
    }
}
