using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Forms.Configuration.SearchForm;
using System;
using System.Linq;

namespace Contoso.XPlatform.Utils
{
    internal static class CreateSearchFilterHelper
    {
        private static readonly string parameterName = "f";
        public static FilterLambdaOperatorDescriptor CreateFilter(SearchFilterGroupDescriptor descriptor, Type modelType, string searchText) 
            => new FilterLambdaOperatorDescriptor
            {
                FilterBody = CreateFilterGroupBody(descriptor, searchText),
                ParameterName = parameterName,
                SourceElementType = modelType.AssemblyQualifiedName
            };

        public static OperatorDescriptorBase CreateFilterGroupBody(SearchFilterGroupDescriptor descriptor, string searchText)
        {
            if (descriptor?.Filters?.Any() != true)
                throw new ArgumentException($"{nameof(descriptor.Filters)}: A0AA4935-06AB-47AA-BDD1-B3FCBBF2EFC0");

            if (descriptor.Filters.Count > 2)
                throw new ArgumentException($"{nameof(descriptor.Filters)}: FA8286BD-AA2D-4AB8-9FFD-CC171C98607C");

            return descriptor.Filters.Count == 2
                ? new OrBinaryOperatorDescriptor
                {
                    Left = CreateBody(descriptor.Filters.First()),
                    Right = CreateBody(descriptor.Filters.Last())
                }
                : CreateBody(descriptor.Filters.First());

            OperatorDescriptorBase CreateBody(SearchFilterDescriptorBase filterDescriptorBase) 
                => filterDescriptorBase is SearchFilterDescriptor searchFilterDescriptor
                    ? CreateFilterBody(searchFilterDescriptor, searchText)
                    : CreateFilterGroupBody((SearchFilterGroupDescriptor)filterDescriptorBase, searchText);
        }

        private static OperatorDescriptorBase CreateFilterBody(SearchFilterDescriptor descriptor, string searchText) 
            => new ContainsOperatorDescriptor
            {
                Left = new MemberSelectorOperatorDescriptor
                {
                    SourceOperand = new ParameterOperatorDescriptor
                    {
                        ParameterName = parameterName
                    },
                    MemberFullName = descriptor.Field
                },
                Right = new ConstantOperatorDescriptor
                {
                    ConstantValue = searchText,
                    Type = typeof(string).FullName
                }
            };
    }
}
