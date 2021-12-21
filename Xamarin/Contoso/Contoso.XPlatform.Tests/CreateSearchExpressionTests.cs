using AutoMapper;
using Contoso.AutoMapperProfiles;
using Contoso.Common.Configuration.ExpansionDescriptors;
using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Common.Utils;
using Contoso.Domain.Entities;
using Contoso.Forms.Configuration.SearchForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Lambda;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Contoso.XPlatform.Tests
{
    public class CreateSearchExpressionTests
    {
        public CreateSearchExpressionTests()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void CanCreateFilterFromSearchFilterGroupDescriptor()
        {
            //act
            FilterLambdaOperatorDescriptor filterLambdaOperatorDescriptor = serviceProvider.GetRequiredService<ISearchSelectorBuilder>().CreateFilter
            (
                searchFilterGroupDescriptor,
                typeof(StudentModel),
                "xxx"
            );
            FilterLambdaOperator filterLambdaOperator = (FilterLambdaOperator)serviceProvider.GetRequiredService<IMapper>().MapToOperator(filterLambdaOperatorDescriptor);
            Expression<Func<StudentModel, bool>> filter = (Expression<Func<StudentModel, bool>>)filterLambdaOperator.Build();

            //assert
            AssertFilterStringIsCorrect
            (
                filter, 
                "f => (f.EnrollmentDateString.Contains(\"xxx\") OrElse (f.FirstName.Contains(\"xxx\") OrElse f.LastName.Contains(\"xxx\")))"
            );
        }

        [Fact]
        public void CanOrderByDescriptorFromSortCollectionDescriptor()
        {
            //act
            SelectorLambdaOperatorDescriptor selectorLambdaOperatorDescriptor = serviceProvider.GetRequiredService<ISearchSelectorBuilder>().CreatePagingSelector
            (
                sortCollectionDescriptor,
                typeof(StudentModel),
                searchFilterGroupDescriptor,
                string.Empty
            );

            SelectorLambdaOperator selectorLambdaOperator = (SelectorLambdaOperator)serviceProvider.GetRequiredService<IMapper>().MapToOperator(selectorLambdaOperatorDescriptor);
            Expression<Func<IQueryable<StudentModel>, IQueryable<StudentModel>>> selector = (Expression<Func<IQueryable<StudentModel>, IQueryable<StudentModel>>>)selectorLambdaOperator.Build();

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
                typeof(StudentModel),
                searchFilterGroupDescriptor,
                "xxx"
            );

            SelectorLambdaOperator selectorLambdaOperator = (SelectorLambdaOperator)serviceProvider.GetRequiredService<IMapper>().MapToOperator(selectorLambdaOperatorDescriptor);
            Expression<Func<IQueryable<StudentModel>, IQueryable<StudentModel>>> selector = (Expression<Func<IQueryable<StudentModel>, IQueryable<StudentModel>>>)selectorLambdaOperator.Build();

            //assert
            AssertFilterStringIsCorrect
            (
                selector,
                "q => q.Where(f => (f.EnrollmentDateString.Contains(\"xxx\") OrElse (f.FirstName.Contains(\"xxx\") OrElse f.LastName.Contains(\"xxx\")))).OrderBy(s => s.FirstName).ThenBy(s => s.LastName).Skip(3).Take(2)"
            );
        }

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
                    cfg.AddProfile<ParameterToDescriptorMappingProfile>();
                    cfg.AddProfile<DescriptorToOperatorMappingProfile>();
                    cfg.AddProfile<ExpansionParameterToDescriptorMappingProfile>();
                    cfg.AddProfile<ExpansionDescriptorToOperatorMappingProfile>();
                });
            }
            MapperConfiguration.AssertConfigurationIsValid();
            serviceProvider = new ServiceCollection()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    MapperConfiguration
                )
                .AddTransient<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .AddSingleton<UiNotificationService, UiNotificationService>()
                .AddSingleton<IFieldsCollectionBuilder, FieldsCollectionBuilder>()
                .AddSingleton<ICollectionCellItemsBuilder, CollectionCellItemsBuilder>()
                .AddSingleton<IConditionalValidationConditionsBuilder, ConditionalValidationConditionsBuilder>()
                .AddSingleton<IHideIfConditionalDirectiveBuilder, HideIfConditionalDirectiveBuilder>()
                .AddSingleton<IClearIfConditionalDirectiveBuilder, ClearIfConditionalDirectiveBuilder>()
                .AddSingleton<IReloadIfConditionalDirectiveBuilder, ReloadIfConditionalDirectiveBuilder>()
                .AddSingleton<ISearchSelectorBuilder, SearchSelectorBuilder>()
                .AddHttpClient()
                .AddSingleton<IHttpService, HttpServiceMock>()
                .AddTransient<EditFormViewModel, EditFormViewModel>()
                .BuildServiceProvider();
        }

        SearchFilterGroupDescriptor searchFilterGroupDescriptor = new SearchFilterGroupDescriptor
        {
            Filters = new List<SearchFilterDescriptorBase>
                {
                    new SearchFilterDescriptor { Field = "EnrollmentDateString" },
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

        SortCollectionDescriptor sortCollectionDescriptor = new SortCollectionDescriptor
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
