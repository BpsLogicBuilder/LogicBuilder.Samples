using AutoMapper;
using Enrollment.Common.Configuration.ItemFilter;
using Enrollment.Common.Utils;
using Enrollment.Domain.Entities;
using Enrollment.Parameters.Expressions;
using Enrollment.XPlatform.Maui.Tests.Helpers;
using Enrollment.XPlatform.Services;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Lambda;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace Enrollment.XPlatform.Maui.Tests
{
    public class CreateItemFilterExpressionTests
    {
        public CreateItemFilterExpressionTests()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void CanCreateSerachByIdExpression()
        {
            //arrange
            ItemFilterGroupDescriptor itemFilterGroupDescriptor = new()
            {
                Filters = new List<ItemFilterDescriptorBase>
                {
                    new MemberSourceFilterDescriptor
                    {
                        Field = "UserId",
                        MemberSource = "UserId",
                        Operator = "eq",
                        Type = "System.Int32"
                    }
                }
            };

            //act
            FilterLambdaOperatorParameters filterLambdaOperatorDescriptor = serviceProvider.GetRequiredService<IGetItemFilterBuilder>().CreateFilter
            (
                itemFilterGroupDescriptor,
                typeof(ResidencyModel),
                residency
            );

            FilterLambdaOperator filterLambdaOperator = (FilterLambdaOperator)serviceProvider.GetRequiredService<IMapper>().MapToOperator(filterLambdaOperatorDescriptor);
            Expression<Func<ResidencyModel, bool>> filter = (Expression<Func<ResidencyModel, bool>>)filterLambdaOperator.Build();

            //assert
            AssertFilterStringIsCorrect
            (
                filter,
                "f => (f.UserId == 3)"
            );
        }

        [Fact]
        public void CanCreateSerachByFirstNameAndLastNameExpression()
        {
            //arrange
            ItemFilterGroupDescriptor itemFilterGroupDescriptor = new()
            {
                Filters = new List<ItemFilterDescriptorBase>
                {
                    new ItemFilterGroupDescriptor
                    {
                        Filters = new List<ItemFilterDescriptorBase>
                        {
                            new MemberSourceFilterDescriptor
                            {
                                Field = "CitizenshipStatus",
                                MemberSource = "CitizenshipStatus",
                                Operator = "eq",
                                Type = "System.String"
                            },
                            new MemberSourceFilterDescriptor
                            {
                                Field = "ResidentState",
                                MemberSource = "ResidentState",
                                Operator = "eq",
                                Type = "System.String"
                            }
                        },
                        Logic = "and"
                    }
                }
            };

            //act
            FilterLambdaOperatorParameters filterLambdaOperatorDescriptor = serviceProvider.GetRequiredService<IGetItemFilterBuilder>().CreateFilter
            (
                itemFilterGroupDescriptor,
                typeof(ResidencyModel),
                residency
            );

            FilterLambdaOperator filterLambdaOperator = (FilterLambdaOperator)serviceProvider.GetRequiredService<IMapper>().MapToOperator(filterLambdaOperatorDescriptor);
            Expression<Func<ResidencyModel, bool>> filter = (Expression<Func<ResidencyModel, bool>>)filterLambdaOperator.Build();

            //assert
            AssertFilterStringIsCorrect
            (
                filter,
                "f => ((f.CitizenshipStatus == \"US\") AndAlso (f.ResidentState == \"OH\"))"
            );
        }

        [Fact]
        public void CanCreateSerachByIdAndFirstNameAndLastNameExpression()
        {
            //arrange
            ItemFilterGroupDescriptor itemFilterGroupDescriptor = new()
            {
                Filters = new List<ItemFilterDescriptorBase>
                {
                    new MemberSourceFilterDescriptor
                    {
                        Field = "UserId",
                        MemberSource = "UserId",
                        Operator = "eq",
                        Type = "System.Int32"
                    },
                    new ItemFilterGroupDescriptor
                    {
                        Filters = new List<ItemFilterDescriptorBase>
                        {
                            new MemberSourceFilterDescriptor
                            {
                                Field = "CitizenshipStatus",
                                MemberSource = "CitizenshipStatus",
                                Operator = "eq",
                                Type = "System.String"
                            },
                            new MemberSourceFilterDescriptor
                            {
                                Field = "ResidentState",
                                MemberSource = "ResidentState",
                                Operator = "eq",
                                Type = "System.String"
                            }
                        },
                        Logic = "and"
                    }
                },
                Logic = "and"
            };

            //act
            FilterLambdaOperatorParameters filterLambdaOperatorDescriptor = serviceProvider.GetRequiredService<IGetItemFilterBuilder>().CreateFilter
            (
                itemFilterGroupDescriptor,
                typeof(ResidencyModel),
                residency
            );

            FilterLambdaOperator filterLambdaOperator = (FilterLambdaOperator)serviceProvider.GetRequiredService<IMapper>().MapToOperator(filterLambdaOperatorDescriptor);
            Expression<Func<ResidencyModel, bool>> filter = (Expression<Func<ResidencyModel, bool>>)filterLambdaOperator.Build();

            //assert
            AssertFilterStringIsCorrect
            (
                filter,
                "f => ((f.UserId == 3) AndAlso ((f.CitizenshipStatus == \"US\") AndAlso (f.ResidentState == \"OH\")))"
            );
        }

        readonly ResidencyModel residency = new()
        {
            UserId = 3,
            CitizenshipStatus = "US",
            ResidentState = "OH"
        };

        private static void AssertFilterStringIsCorrect(Expression expression, string expected)
        {
            AssertStringIsCorrect(ExpressionStringBuilder.ToString(expression));

            void AssertStringIsCorrect(string resultExpression)
                => Assert.True
                (
                    expected == resultExpression,
                    $"Expected expression '{expected}' but the deserializer produced '{resultExpression}'"
                );
        }
    }
}
