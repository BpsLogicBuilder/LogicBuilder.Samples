using Enrollment.Parameters.Expressions;
using Enrollment.Parameters.ItemFilter;
using Enrollment.Utils;
using System;
using System.Linq;

namespace Enrollment.Bsl.Flow.Services
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
                throw new ArgumentException($"{nameof(itemFilterGroup.Filters)}: 1D51E748-1CFE-4C6D-8827-FB2A12691AA9");

            if (itemFilterGroup.Filters.Count > 2)
                throw new ArgumentException($"{nameof(itemFilterGroup.Filters)}: 5A4E1048-A4A2-4401-8237-24B0AC961643");

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
                    _ => throw new ArgumentException($"{nameof(filterBase)}: F80C8C40-EE3B-4020-BAB0-B580625E48AE"),
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
                _ => throw new ArgumentException($"{nameof(oper)}: B15DD258-7EFB-4E2A-937B-505D86E8C829"),
            };

        private static BinaryOperatorParameters GetLogicBinaryOperatorParameters(string logic)
            => logic.ToLowerInvariant() switch
            {
                Logic.and => new AndBinaryOperatorParameters(),
                Logic.or => new OrBinaryOperatorParameters(),
                _ => throw new ArgumentException($"{nameof(logic)}: B8E5AB1F-AFA3-4747-A909-EF18FD0E9753"),
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
