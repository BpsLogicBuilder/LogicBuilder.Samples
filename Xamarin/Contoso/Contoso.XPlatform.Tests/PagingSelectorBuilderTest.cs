using AutoMapper;
using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Common.Utils;
using Contoso.Domain.Entities;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Tests.Helpers;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Lambda;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Contoso.XPlatform.Tests
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
            Expression<Func<IQueryable<StudentModel>, IQueryable<StudentModel>>> selector = (Expression<Func<IQueryable<StudentModel>, IQueryable<StudentModel>>>)selectorLambdaOperator.Build();

            //assert
            AssertFilterStringIsCorrect
            (
                selector,
                "$it => $it.OrderBy(o => o.FullName).Skip(10).Take(2)"
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
            Expression<Func<IQueryable<StudentModel>, IQueryable<StudentModel>>> selector = (Expression<Func<IQueryable<StudentModel>, IQueryable<StudentModel>>>)selectorLambdaOperator.Build();

            //assert
            AssertFilterStringIsCorrect
            (
                selector,
                "$it => $it.Where(f => (f.EnrollmentDateString.Contains(\"Joh\") OrElse (f.FirstName.Contains(\"Joh\") OrElse f.LastName.Contains(\"Joh\")))).OrderBy(o => o.FullName).Skip(10).Take(2)"
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
