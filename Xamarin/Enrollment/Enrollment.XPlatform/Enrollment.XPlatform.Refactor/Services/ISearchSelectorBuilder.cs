using Enrollment.Common.Configuration.ExpansionDescriptors;
using Enrollment.Common.Configuration.ExpressionDescriptors;
using Enrollment.Forms.Configuration.SearchForm;
using System;

namespace Enrollment.XPlatform.Services
{
    public interface ISearchSelectorBuilder
    {
        FilterLambdaOperatorDescriptor CreateFilter(SearchFilterGroupDescriptor descriptor, Type modelType, string searchText);
        SelectorLambdaOperatorDescriptor CreatePagingSelector(SortCollectionDescriptor sortDescriptor, Type modelType, SearchFilterGroupDescriptor filterGroupDescriptor, string? searchText);
    }
}
