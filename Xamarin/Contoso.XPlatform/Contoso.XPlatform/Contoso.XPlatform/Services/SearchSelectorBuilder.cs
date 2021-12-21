using Contoso.Common.Configuration.ExpansionDescriptors;
using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Forms.Configuration.SearchForm;
using Contoso.XPlatform.Utils;
using System;

namespace Contoso.XPlatform.Services
{
    public class SearchSelectorBuilder : ISearchSelectorBuilder
    {
        public FilterLambdaOperatorDescriptor CreateFilter(SearchFilterGroupDescriptor descriptor, Type modelType, string searchText) 
            => CreateSearchFilterHelper.CreateFilter(descriptor, modelType, searchText);

        public SelectorLambdaOperatorDescriptor CreatePagingSelector(SortCollectionDescriptor sortDescriptor, Type modelType, SearchFilterGroupDescriptor filterGroupDescriptor, string searchText)
            => CreatePagingSelectorHelper.CreatePagingSelector(sortDescriptor, modelType, filterGroupDescriptor, searchText);
    }
}
