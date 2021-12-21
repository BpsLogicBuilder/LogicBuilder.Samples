using Contoso.Common.Configuration.ExpansionDescriptors;
using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Forms.Configuration.SearchForm;
using System;

namespace Contoso.XPlatform.Services
{
    public interface ISearchSelectorBuilder
    {
        FilterLambdaOperatorDescriptor CreateFilter(SearchFilterGroupDescriptor descriptor, Type modelType, string searchText);
        SelectorLambdaOperatorDescriptor CreatePagingSelector(SortCollectionDescriptor sortDescriptor, Type modelType, SearchFilterGroupDescriptor filterGroupDescriptor, string searchText);
    }
}
