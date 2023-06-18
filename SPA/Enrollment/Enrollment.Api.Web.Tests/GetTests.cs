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

        private SelectOperatorDescriptor GetAboutBody_PersonCountByDateOfBirth()
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
                            MemberFullName = "DateOfBirth",
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

        private SelectorLambdaOperatorDescriptor GetExpressionDescriptor<T, TResult>(OperatorDescriptorBase selectorBody, string parameterName = "$it")
            => new SelectorLambdaOperatorDescriptor
            {
                Selector = selectorBody,
                SourceElementType = typeof(T).AssemblyQualifiedName,
                ParameterName = parameterName,
                BodyType = typeof(TResult).AssemblyQualifiedName
            };
        #endregion Helpers

        [Fact]
        public async void GetAboutListRequest_PersonCountByDateOfDirth_As_LookUpsModel()
        {
            //arrange
            var selectorLambdaOperatorDescriptor = GetExpressionDescriptor<IQueryable<PersonModel>, IEnumerable<LookUpsModel>>
            (
                GetAboutBody_PersonCountByDateOfBirth(),
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
                        ModelType = typeof(PersonModel).AssemblyQualifiedName,
                        DataType = typeof(Person).AssemblyQualifiedName,
                        ModelReturnType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName,
                        DataReturnType = typeof(IEnumerable<LookUps>).AssemblyQualifiedName
                    }
                ),
                BASE_URL
            );

            Assert.True(result.List.Any());
        }
    }
}
