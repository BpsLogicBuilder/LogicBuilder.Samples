using Contoso.Parameters.Expressions;
using Contoso.Parameters.ItemFilter;
using Contoso.Utils;
using System;
using System.Linq;

namespace Contoso.Bsl.Flow.Services
{
    public class GetItemFilterBuilder : IGetItemFilterBuilder
    {
        public FilterLambdaOperatorParameters CreateFilter(ItemFilterGroupParameters itemFilterGroup, Type modelType, object entity)
            => new FilterLambdaOperatorParameters
            (
                CreateFilterGroupBody(itemFilterGroup, entity),
                modelType,
                parameterName
            );

        private static readonly string parameterName = "f";

        private static IExpressionParameter CreateFilterGroupBody(ItemFilterGroupParameters itemFilterGroup, object entity)
        {
            if (itemFilterGroup?.Filters?.Any() != true)
                throw new ArgumentException($"{nameof(itemFilterGroup.Filters)}: 937A1CD2-9253-4E5D-81E9-D36DFD036861");

            if (itemFilterGroup.Filters.Count > 2)
                throw new ArgumentException($"{nameof(itemFilterGroup.Filters)}: E1D1CCC2-15CA-472F-BF20-F92CC76B27FD");

            if (itemFilterGroup.Filters.Count == 1)
                return CreateBody(itemFilterGroup.Filters.First()); ;

            return SetMembers
            (
                GetLogicBinaryOperatorParameters(itemFilterGroup.Logic)
            );

            IExpressionParameter SetMembers(BinaryOperatorParameters binaryOperator)
            {
                binaryOperator.Left = CreateBody(itemFilterGroup.Filters.First());
                binaryOperator.Right = CreateBody(itemFilterGroup.Filters.Last());

                return binaryOperator;
            }

            IExpressionParameter CreateBody(ItemFilterParametersBase filterBase) 
                => filterBase switch
                {
                    ValueSourceFilterParameters valueSourceFilter => CreateValueFilterBody(valueSourceFilter),
                    MemberSourceFilterParameters memberSourceFilter => CreateMemberSourceFilterBody(memberSourceFilter, entity),
                    ItemFilterGroupParameters itemFilterGroup => CreateFilterGroupBody(itemFilterGroup, entity),
                    _ => throw new ArgumentException($"{nameof(filterBase)}: AB31632F-4961-435C-8BA2-528CBCBEAE88"),
                };
        }

        private static IExpressionParameter CreateValueFilterBody(ValueSourceFilterParameters valueSourceFilter)
        {
            return SetMembers
            (
                GetOperatorBinaryOperatorParameters(valueSourceFilter.Operator)
            );

            BinaryOperatorParameters SetMembers(BinaryOperatorParameters binaryOperator)
            {
                binaryOperator.Left = new MemberSelectorOperatorParameters
                (
                    valueSourceFilter.Field,
                    new ParameterOperatorParameters(parameterName)
                );
                binaryOperator.Right = new ConstantOperatorParameters
                (
                    valueSourceFilter.Value,
                    valueSourceFilter.Type
                );

                return binaryOperator;
            }
        }

        private static IExpressionParameter CreateMemberSourceFilterBody(MemberSourceFilterParameters memberSourceFilter, object entity)
        {
            return SetMembers
            (
                GetOperatorBinaryOperatorParameters(memberSourceFilter.Operator)
            );

            BinaryOperatorParameters SetMembers(BinaryOperatorParameters binaryOperator)
            {
                binaryOperator.Left = new MemberSelectorOperatorParameters
                (
                    memberSourceFilter.Field,
                    new ParameterOperatorParameters(parameterName)
                );
                binaryOperator.Right = new ConstantOperatorParameters
                (
                    entity.GetPropertyValue(memberSourceFilter.MemberSource),
                    memberSourceFilter.Type
                );

                return binaryOperator;
            }
        }

        private static BinaryOperatorParameters GetOperatorBinaryOperatorParameters(string oper)
            => oper.ToLowerInvariant() switch
            {
                Operators.eq => new EqualsBinaryOperatorParameters(),
                Operators.neq => new NotEqualsBinaryOperatorParameters(),
                _ => throw new ArgumentException($"{nameof(oper)}: 6568E4A0-7DA9-4DFA-844A-F2164D824448"),
            };

        private static BinaryOperatorParameters GetLogicBinaryOperatorParameters(string logic)
            => logic.ToLowerInvariant() switch
            {
                Logic.and => new AndBinaryOperatorParameters(),
                Logic.or => new OrBinaryOperatorParameters(),
                _ => throw new ArgumentException($"{nameof(logic)}: D802B6C2-3000-46D8-A2E3-3AFE88A6ECFD"),
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
