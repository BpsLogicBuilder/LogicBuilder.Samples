using Contoso.Common.Configuration.ItemFilter;
using Contoso.Parameters.Expressions;
using Contoso.Utils;
using System;
using System.Linq;

namespace Contoso.XPlatform.Utils
{
    internal static class CreateItemFilterHelper
    {
        private static readonly string parameterName = "f";

        public static FilterLambdaOperatorParameters CreateFilter(ItemFilterGroupDescriptor descriptor, Type modelType, object entity)
            => new FilterLambdaOperatorParameters
            (
                CreateFilterGroupBody(descriptor, entity), 
                modelType, 
                parameterName
            );

        private static IExpressionParameter CreateFilterGroupBody(ItemFilterGroupDescriptor descriptor, object entity)
        {
            if (descriptor?.Filters?.Any() != true)
                throw new ArgumentException($"{nameof(descriptor.Filters)}: 165BB6D3-1D2F-4EEB-B546-102825505ED2");

            if (descriptor.Filters.Count > 2)
                throw new ArgumentException($"{nameof(descriptor.Filters)}: 1EAC3591-1BBE-412A-A915-C779E3463FB7");

            if (descriptor.Filters.Count == 1)
                return CreateBody(descriptor.Filters.First()); ;

            return SetMembers
            (
                GetLogicBinaryOperatorDescriptor(descriptor.Logic)
            );

            IExpressionParameter SetMembers(BinaryOperatorParameters binaryOperator)
            {
                binaryOperator.Left = CreateBody(descriptor.Filters.First());
                binaryOperator.Right = CreateBody(descriptor.Filters.Last());

                return binaryOperator;
            }

            IExpressionParameter CreateBody(ItemFilterDescriptorBase filterDescriptorBase)
            {
                return filterDescriptorBase switch
                {
                    ValueSourceFilterDescriptor valueSourceFilterDescriptor => CreateValueFilterBody(valueSourceFilterDescriptor),
                    MemberSourceFilterDescriptor memberSourceFilterDescriptor => CreateMemberSourceFilterBody(memberSourceFilterDescriptor, entity),
                    ItemFilterGroupDescriptor itemFilterGroupDescriptor => CreateFilterGroupBody(itemFilterGroupDescriptor, entity),
                    _ => throw new ArgumentException($"{nameof(filterDescriptorBase)}: 7EB79C70-91D4-4C9E-96AD-C5ABF1D8603A"),
                };
            }
        }

        private static IExpressionParameter CreateValueFilterBody(ValueSourceFilterDescriptor descriptor)
        {
            return SetMembers
            (
                GetOperatorBinaryOperatorDescriptor(descriptor.Operator)
            );

            BinaryOperatorParameters SetMembers(BinaryOperatorParameters binaryOperator)
            {
                binaryOperator.Left = new MemberSelectorOperatorParameters
                (
                    descriptor.Field,
                    new ParameterOperatorParameters(parameterName)
                );
                binaryOperator.Right = new ConstantOperatorParameters
                (
                    descriptor.Value,
                    Type.GetType(descriptor.Type)
                );

                return binaryOperator;
            }
        }

        private static IExpressionParameter CreateMemberSourceFilterBody(MemberSourceFilterDescriptor descriptor, object entity)
        {
            return SetMembers
            (
                GetOperatorBinaryOperatorDescriptor(descriptor.Operator)
            );

            BinaryOperatorParameters SetMembers(BinaryOperatorParameters binaryOperator)
            {
                binaryOperator.Left = new MemberSelectorOperatorParameters
                (
                    descriptor.Field, 
                    new ParameterOperatorParameters(parameterName)
                );
                binaryOperator.Right = new ConstantOperatorParameters
                (
                    entity.GetPropertyValue(descriptor.MemberSource), 
                    Type.GetType(descriptor.Type)
                );

                return binaryOperator;
            }
        }

        private static BinaryOperatorParameters GetOperatorBinaryOperatorDescriptor(string oper) 
            => oper.ToLowerInvariant() switch
            {
                Operators.eq => new EqualsBinaryOperatorParameters(),
                Operators.neq => new NotEqualsBinaryOperatorParameters(),
                _ => throw new ArgumentException($"{nameof(oper)}: 85953974-7864-4D0A-9B60-1C1D746FD8D1"),
            };

        private static BinaryOperatorParameters GetLogicBinaryOperatorDescriptor(string logic) 
            => logic.ToLowerInvariant() switch
            {
                Logic.and => new AndBinaryOperatorParameters(),
                Logic.or => new OrBinaryOperatorParameters(),
                _ => throw new ArgumentException($"{nameof(logic)}: F937E0EC-734C-406C-90B6-2C6D8DEC4541"),
            };

        private struct Operators
        {
            public const string eq = "eq";
            public const string neq = "neq";
        }

        private struct Logic
        {
            public const string or = "or";
            public const string and = "and";
        }
    }
}
