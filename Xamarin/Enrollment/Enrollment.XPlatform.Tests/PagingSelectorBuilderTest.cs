using AutoMapper;
using Enrollment.Common.Configuration.ExpressionDescriptors;
using Enrollment.Common.Utils;
using Enrollment.Domain.Entities;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.Tests.Helpers;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Lambda;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Enrollment.XPlatform.Tests
{
    public class PagingSelectorBuilderTest
    {
        public PagingSelectorBuilderTest()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public async void CanCreatePagingSelectorDescriptorWithoutFilter()
        {
            //act
            SelectorLambdaOperatorDescriptor selectorLambdaOperatorDescriptor = await serviceProvider.GetRequiredService<IPagingSelectorBuilder>().CreateSelector
            (
                10,
                "",
                SearchFormDescriptors.SudentsForm.CreatePagingSelectorFlowName
            );

            SelectorLambdaOperator selectorLambdaOperator = (SelectorLambdaOperator)serviceProvider.GetRequiredService<IMapper>().MapToOperator(selectorLambdaOperatorDescriptor);
            Expression<Func<IQueryable<UserModel>, IQueryable<UserModel>>> selector = (Expression<Func<IQueryable<UserModel>, IQueryable<UserModel>>>)selectorLambdaOperator.Build();

            //assert
            AssertFilterStringIsCorrect
            (
                selector,
                "$it => $it.OrderBy(o => o.UserName).Skip(10).Take(2)"
            );
        }

        [Fact]
        public async void CanCreatePagingSelectorDescriptorWithFilter()
        {
            //act
            SelectorLambdaOperatorDescriptor selectorLambdaOperatorDescriptor = await serviceProvider.GetRequiredService<IPagingSelectorBuilder>().CreateSelector
            (
                10,
                "Joh",
                SearchFormDescriptors.SudentsForm.CreatePagingSelectorFlowName
            );

            SelectorLambdaOperator selectorLambdaOperator = (SelectorLambdaOperator)serviceProvider.GetRequiredService<IMapper>().MapToOperator(selectorLambdaOperatorDescriptor);
            Expression<Func<IQueryable<UserModel>, IQueryable<UserModel>>> selector = (Expression<Func<IQueryable<UserModel>, IQueryable<UserModel>>>)selectorLambdaOperator.Build();

            //assert
            AssertFilterStringIsCorrect
            (
                selector,
                "$it => $it.Where(f => f.UserName.Contains(\"Joh\")).OrderBy(o => o.UserName).Skip(10).Take(2)"
            );
        }

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
