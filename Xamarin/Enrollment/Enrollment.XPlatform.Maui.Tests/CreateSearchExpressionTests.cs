using AutoMapper;
using Enrollment.Common.Configuration.ExpansionDescriptors;
using Enrollment.Common.Configuration.ExpressionDescriptors;
using Enrollment.Common.Utils;
using Enrollment.Domain.Entities;
using Enrollment.Forms.Configuration.SearchForm;
using Enrollment.XPlatform.Maui.Tests.Helpers;
using Enrollment.XPlatform.Services;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Lambda;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Enrollment.XPlatform.Maui.Tests
{
    public class CreateSearchExpressionTests
    {
        public CreateSearchExpressionTests()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void CanCreateFilterFromSearchFilterGroupDescriptor()
        {
            //act
            FilterLambdaOperatorDescriptor filterLambdaOperatorDescriptor = serviceProvider.GetRequiredService<ISearchSelectorBuilder>().CreateFilter
            (
                searchFilterGroupDescriptor,
                typeof(PersonalModel),
                "xxx"
            );
            FilterLambdaOperator filterLambdaOperator = (FilterLambdaOperator)serviceProvider.GetRequiredService<IMapper>().MapToOperator(filterLambdaOperatorDescriptor);
            Expression<Func<PersonalModel, bool>> filter = (Expression<Func<PersonalModel, bool>>)filterLambdaOperator.Build();

            //assert
            AssertFilterStringIsCorrect
            (
                filter,
                "f => (f.MiddleName.Contains(\"xxx\") OrElse (f.FirstName.Contains(\"xxx\") OrElse f.LastName.Contains(\"xxx\")))"
            );
        }

        [Fact]
        public void CanCreateOrderByDescriptorFromSortCollectionDescriptor()
        {
            //act
            SelectorLambdaOperatorDescriptor selectorLambdaOperatorDescriptor = serviceProvider.GetRequiredService<ISearchSelectorBuilder>().CreatePagingSelector
            (
                sortCollectionDescriptor,
                typeof(PersonalModel),
                searchFilterGroupDescriptor,
                string.Empty
            );

            SelectorLambdaOperator selectorLambdaOperator = (SelectorLambdaOperator)serviceProvider.GetRequiredService<IMapper>().MapToOperator(selectorLambdaOperatorDescriptor);
            Expression<Func<IQueryable<PersonalModel>, IQueryable<PersonalModel>>> selector = (Expression<Func<IQueryable<PersonalModel>, IQueryable<PersonalModel>>>)selectorLambdaOperator.Build();

            //assert
            AssertFilterStringIsCorrect
            (
                selector,
                "q => q.OrderBy(s => s.FirstName).ThenBy(s => s.LastName).Skip(3).Take(2)"
            );
        }

        [Fact]
        public void CanWhereOrderByDescriptorFromSortCollectionDescriptor()
        {
            //act
            SelectorLambdaOperatorDescriptor selectorLambdaOperatorDescriptor = serviceProvider.GetRequiredService<ISearchSelectorBuilder>().CreatePagingSelector
            (
                sortCollectionDescriptor,
                typeof(PersonalModel),
                searchFilterGroupDescriptor,
                "xxx"
            );

            SelectorLambdaOperator selectorLambdaOperator = (SelectorLambdaOperator)serviceProvider.GetRequiredService<IMapper>().MapToOperator(selectorLambdaOperatorDescriptor);
            Expression<Func<IQueryable<PersonalModel>, IQueryable<PersonalModel>>> selector = (Expression<Func<IQueryable<PersonalModel>, IQueryable<PersonalModel>>>)selectorLambdaOperator.Build();

            //assert
            AssertFilterStringIsCorrect
            (
                selector,
                "q => q.Where(f => (f.MiddleName.Contains(\"xxx\") OrElse (f.FirstName.Contains(\"xxx\") OrElse f.LastName.Contains(\"xxx\")))).OrderBy(s => s.FirstName).ThenBy(s => s.LastName).Skip(3).Take(2)"
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

        readonly SearchFilterGroupDescriptor searchFilterGroupDescriptor = new()
        {
            Filters = new List<SearchFilterDescriptorBase>
                {
                    new SearchFilterDescriptor { Field = "MiddleName" },
                    new SearchFilterGroupDescriptor
                    {
                        Filters = new List<SearchFilterDescriptorBase>
                        {
                            new SearchFilterDescriptor { Field = "FirstName" },
                            new SearchFilterDescriptor { Field = "LastName" }
                        }
                    }
                }
        };
        readonly SortCollectionDescriptor sortCollectionDescriptor = new()
        {
            SortDescriptions = new List<SortDescriptionDescriptor>
            {
                new SortDescriptionDescriptor
                {
                    PropertyName = "FirstName",
                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending
                }
                ,new SortDescriptionDescriptor
                {
                    PropertyName = "LastName",
                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending
                }
            },
            Skip = 3,
            Take = 2,
        };
    }
}
