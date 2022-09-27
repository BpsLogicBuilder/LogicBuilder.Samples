using AutoMapper;
using Contoso.Common.Configuration.ItemFilter;
using Contoso.Common.Utils;
using Contoso.Domain.Entities;
using Contoso.Parameters.Expressions;
using Contoso.XPlatform.Tests.Helpers;
using Contoso.XPlatform.Services;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Lambda;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace Contoso.XPlatform.Tests
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
                        Field = "ID",
                        MemberSource = "ID",
                        Operator = "eq",
                        Type = "System.Int32"
                    }
                }
            };

            //act
            FilterLambdaOperatorParameters filterLambdaOperatorDescriptor = serviceProvider.GetRequiredService<IGetItemFilterBuilder>().CreateFilter
            (
                itemFilterGroupDescriptor,
                typeof(InstructorModel),
                inststructor
            );

            FilterLambdaOperator filterLambdaOperator = (FilterLambdaOperator)serviceProvider.GetRequiredService<IMapper>().MapToOperator(filterLambdaOperatorDescriptor);
            Expression<Func<InstructorModel, bool>> filter = (Expression<Func<InstructorModel, bool>>)filterLambdaOperator.Build();

            //assert
            AssertFilterStringIsCorrect
            (
                filter,
                "f => (f.ID == 3)"
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
                                Field = "FirstName",
                                MemberSource = "FirstName",
                                Operator = "eq",
                                Type = "System.String"
                            },
                            new MemberSourceFilterDescriptor
                            {
                                Field = "LastName",
                                MemberSource = "LastName",
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
                typeof(InstructorModel),
                inststructor
            );

            FilterLambdaOperator filterLambdaOperator = (FilterLambdaOperator)serviceProvider.GetRequiredService<IMapper>().MapToOperator(filterLambdaOperatorDescriptor);
            Expression<Func<InstructorModel, bool>> filter = (Expression<Func<InstructorModel, bool>>)filterLambdaOperator.Build();

            //assert
            AssertFilterStringIsCorrect
            (
                filter,
                "f => ((f.FirstName == \"John\") AndAlso (f.LastName == \"Smith\"))"
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
                        Field = "ID",
                        MemberSource = "ID",
                        Operator = "eq",
                        Type = "System.Int32"
                    },
                    new ItemFilterGroupDescriptor
                    {
                        Filters = new List<ItemFilterDescriptorBase>
                        {
                            new MemberSourceFilterDescriptor
                            {
                                Field = "FirstName",
                                MemberSource = "FirstName",
                                Operator = "eq",
                                Type = "System.String"
                            },
                            new MemberSourceFilterDescriptor
                            {
                                Field = "LastName",
                                MemberSource = "LastName",
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
                typeof(InstructorModel),
                inststructor
            );

            FilterLambdaOperator filterLambdaOperator = (FilterLambdaOperator)serviceProvider.GetRequiredService<IMapper>().MapToOperator(filterLambdaOperatorDescriptor);
            Expression<Func<InstructorModel, bool>> filter = (Expression<Func<InstructorModel, bool>>)filterLambdaOperator.Build();

            //assert
            AssertFilterStringIsCorrect
            (
                filter,
                "f => ((f.ID == 3) AndAlso ((f.FirstName == \"John\") AndAlso (f.LastName == \"Smith\")))"
            );
        }

        readonly InstructorModel inststructor = new()
        {
            ID = 3,
            FirstName = "John",
            LastName = "Smith"
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
