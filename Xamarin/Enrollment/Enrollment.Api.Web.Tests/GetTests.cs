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
        private const string BASE_URL = "http://localhost:62419/";
        #endregion Fields

        #region Helpers
        private void Initialize()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHttpClient();
            serviceProvider = services.BuildServiceProvider();

            this.clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        }

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

        private EqualsBinaryOperatorDescriptor GetResidencyByIdFilterBodyFromObjectConstant(ResidencyModel residency)
            => new EqualsBinaryOperatorDescriptor
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
                            ExpandedItems = new List<Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor>
                            {
                                new Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor
                                {
                                    MemberName = "StatesLivedIn"
                                }
                            }
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
                            ExpandedItems = new List<Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor>
                            {
                                new Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor
                                {
                                    MemberName = "StatesLivedIn"
                                }
                            }
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
