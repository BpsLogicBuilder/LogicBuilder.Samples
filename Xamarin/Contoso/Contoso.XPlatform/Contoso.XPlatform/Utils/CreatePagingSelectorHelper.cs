using Contoso.Common.Configuration.ExpansionDescriptors;
using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Forms.Configuration.SearchForm;
using System;
using System.Linq;

namespace Contoso.XPlatform.Utils
{
    internal static class CreatePagingSelectorHelper
    {
        private static readonly string queryParameterName = "q";
        private static readonly string selectorParameterName = "s";
        private static readonly string filterParameterName = "f";

        public static SelectorLambdaOperatorDescriptor CreatePagingSelector(SortCollectionDescriptor sortDescriptor, Type modelType, SearchFilterGroupDescriptor filterGroupDescriptor, string searchText)
            => string.IsNullOrEmpty(searchText)
                ? CreatePagingSelector(sortDescriptor, modelType)
                : GetSelector
                (
                    modelType.GetIQueryableTypeString(),
                    sortDescriptor,
                    CreateWhereBody(filterGroupDescriptor, searchText)
                );

        private static SelectorLambdaOperatorDescriptor CreatePagingSelector(SortCollectionDescriptor sortDescriptor, Type modelType)
            => GetSelector
            (
                modelType.GetIQueryableTypeString(),
                sortDescriptor,
                new ParameterOperatorDescriptor { ParameterName = queryParameterName }
            );

        private static SelectorLambdaOperatorDescriptor GetSelector(string queryableType, SortCollectionDescriptor sortDescriptor, OperatorDescriptorBase sourceOperand)
                => new SelectorLambdaOperatorDescriptor
                {
                    Selector = CreatePagingDescriptor
                    (
                        sortDescriptor,
                        CreateSortBody
                        (
                            sortDescriptor,
                            sourceOperand
                        )
                    ),
                    ParameterName = queryParameterName,
                    BodyType = queryableType,
                    SourceElementType = queryableType
                };

        private static OperatorDescriptorBase CreateWhereBody(SearchFilterGroupDescriptor filterGroupDescriptor, string searchText) 
            => new WhereOperatorDescriptor
            {
                SourceOperand = new ParameterOperatorDescriptor
                {
                    ParameterName = queryParameterName
                },
                FilterBody = CreateSearchFilterHelper.CreateFilterGroupBody(filterGroupDescriptor, searchText),
                FilterParameterName = filterParameterName
            };

        private static OperatorDescriptorBase CreateSortBody(SortCollectionDescriptor sortCollectionDescriptor, OperatorDescriptorBase sourceOperand)
        {
            if (sortCollectionDescriptor?.SortDescriptions?.Any() != true)
                throw new ArgumentException($"{nameof(sortCollectionDescriptor.SortDescriptions)}: C55F5642-5811-4840-B643-E38BDE7DA16E");

            return sortCollectionDescriptor.SortDescriptions.Aggregate
            (
                sourceOperand, 
                (OperatorDescriptorBase descriptor, SortDescriptionDescriptor next) =>
                {
                    return object.ReferenceEquals(descriptor, sourceOperand)
                        ? new OrderByOperatorDescriptor
                        {
                            SortDirection = next.SortDirection,
                            SourceOperand = descriptor,
                            SelectorBody = new MemberSelectorOperatorDescriptor
                            {
                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = selectorParameterName },
                                MemberFullName = next.PropertyName
                            },
                            SelectorParameterName = selectorParameterName

                        }
                        : new ThenByOperatorDescriptor
                        {
                            SortDirection = next.SortDirection,
                            SourceOperand = descriptor,
                            SelectorBody = new MemberSelectorOperatorDescriptor
                            {
                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = selectorParameterName },
                                MemberFullName = next.PropertyName
                            },
                            SelectorParameterName = selectorParameterName
                        };
                }
            );
        }

        private static OperatorDescriptorBase CreatePagingDescriptor(SortCollectionDescriptor sortCollectionDescriptor, OperatorDescriptorBase sortBody) 
            => sortBody.GetSkipOperator(sortCollectionDescriptor).GetTakeOperator(sortCollectionDescriptor);

        private static OperatorDescriptorBase GetSkipOperator(this OperatorDescriptorBase pagingDescriptor, SortCollectionDescriptor sortCollectionDescriptor) 
            => sortCollectionDescriptor.Skip.HasValue
                ? new SkipOperatorDescriptor
                {
                    SourceOperand = pagingDescriptor,
                    Count = sortCollectionDescriptor.Skip.Value
                }
                : pagingDescriptor;

        private static OperatorDescriptorBase GetTakeOperator(this OperatorDescriptorBase pagingDescriptor, SortCollectionDescriptor sortCollectionDescriptor) 
            => sortCollectionDescriptor.Take.HasValue
                ? new TakeOperatorDescriptor
                {
                    SourceOperand = pagingDescriptor,
                    Count = sortCollectionDescriptor.Take.Value
                }
                : pagingDescriptor;
    }
}
