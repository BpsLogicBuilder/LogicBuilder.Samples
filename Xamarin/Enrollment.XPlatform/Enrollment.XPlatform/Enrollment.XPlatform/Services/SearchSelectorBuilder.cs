using Enrollment.Common.Configuration.ExpansionDescriptors;
using Enrollment.Common.Configuration.ExpressionDescriptors;
using Enrollment.Forms.Configuration.SearchForm;
using Enrollment.XPlatform.Utils;
using System;

namespace Enrollment.XPlatform.Services
{
    public class SearchSelectorBuilder : ISearchSelectorBuilder
    {
        public FilterLambdaOperatorDescriptor CreateFilter(SearchFilterGroupDescriptor descriptor, Type modelType, string searchText) 
            => CreateSearchFilterHelper.CreateFilter(descriptor, modelType, searchText);

        public SelectorLambdaOperatorDescriptor CreatePagingSelector(SortCollectionDescriptor sortDescriptor, Type modelType, SearchFilterGroupDescriptor filterGroupDescriptor, string searchText)
            => CreatePagingSelectorHelper.CreatePagingSelector(sortDescriptor, modelType, filterGroupDescriptor, searchText);
    }
}
