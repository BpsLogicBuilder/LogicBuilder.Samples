using AutoMapper;
using Contoso.Common.Configuration.ExpressionDescriptors;
using LogicBuilder.Expressions.Utils.ExpressionBuilder;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Arithmetic;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Cacnonical;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Collection;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Conversions;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.DateTimeOperators;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Lambda;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Logical;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Operand;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.StringOperators;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace Contoso.AutoMapperProfiles
{
    public class DescriptorToOperatorMappingProfile : Profile
    {
        const string PARAMETERS_KEY = "parameters";

        public DescriptorToOperatorMappingProfile()
        {
			CreateMap<AddBinaryOperatorDescriptor, AddBinaryOperator>();
			CreateMap<AllOperatorDescriptor, AllOperator>()
				.ConstructUsing
				(
					(src, context) => new AllOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.FilterBody),
						src.FilterParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<AndBinaryOperatorDescriptor, AndBinaryOperator>();
			CreateMap<AnyOperatorDescriptor, AnyOperator>()
				.ConstructUsing
				(
					(src, context) => new AnyOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.FilterBody),
						src.FilterParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<AsEnumerableOperatorDescriptor, AsEnumerableOperator>();
			CreateMap<AsQueryableOperatorDescriptor, AsQueryableOperator>();
			CreateMap<AverageOperatorDescriptor, AverageOperator>()
				.ConstructUsing
				(
					(src, context) => new AverageOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.SelectorBody),
						src.SelectorParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<BinaryOperatorDescriptor, BinaryOperator>();
			CreateMap<CastOperatorDescriptor, CastOperator>()
				.ForMember(dest => dest.Type, opts => opts.Ignore())
				.ForCtorParam("type", opts => opts.MapFrom(x => Type.GetType(x.Type)));
			CreateMap<CeilingOperatorDescriptor, CeilingOperator>();
			CreateMap<CollectionCastOperatorDescriptor, CollectionCastOperator>()
				.ForMember(dest => dest.Type, opts => opts.Ignore())
				.ForCtorParam("type", opts => opts.MapFrom(x => Type.GetType(x.Type)));
			CreateMap<CollectionConstantOperatorDescriptor, CollectionConstantOperator>()
				.ForMember(dest => dest.ElementType, opts => opts.Ignore())
				.ForCtorParam("elementType", opts => opts.MapFrom(x => Type.GetType(x.ElementType)));
			CreateMap<ConcatOperatorDescriptor, ConcatOperator>();
			CreateMap<ConstantOperatorDescriptor, ConstantOperator>()
				.ForMember(dest => dest.Type, opts => opts.Ignore())
				.ForCtorParam("type", opts => opts.MapFrom(x => Type.GetType(x.Type)));
			CreateMap<ContainsOperatorDescriptor, ContainsOperator>();
			CreateMap<ConvertCharArrayToStringOperatorDescriptor, ConvertCharArrayToStringOperator>();
			CreateMap<ConvertOperatorDescriptor, ConvertOperator>()
				.ForMember(dest => dest.Type, opts => opts.Ignore())
				.ForCtorParam("type", opts => opts.MapFrom(x => Type.GetType(x.Type)));
			CreateMap<ConvertToEnumOperatorDescriptor, ConvertToEnumOperator>()
				.ForMember(dest => dest.Type, opts => opts.Ignore())
				.ForCtorParam("type", opts => opts.MapFrom(x => Type.GetType(x.Type)));
			CreateMap<ConvertToNullableUnderlyingValueOperatorDescriptor, ConvertToNullableUnderlyingValueOperator>();
			CreateMap<ConvertToNumericDateOperatorDescriptor, ConvertToNumericDateOperator>();
			CreateMap<ConvertToNumericTimeOperatorDescriptor, ConvertToNumericTimeOperator>();
			CreateMap<ConvertToStringOperatorDescriptor, ConvertToStringOperator>();
			CreateMap<CountOperatorDescriptor, CountOperator>()
				.ConstructUsing
				(
					(src, context) => new CountOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.FilterBody),
						src.FilterParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<CustomMethodOperatorDescriptor, CustomMethodOperator>();
			CreateMap<DateOperatorDescriptor, DateOperator>();
			CreateMap<DayOperatorDescriptor, DayOperator>();
			CreateMap<DistinctOperatorDescriptor, DistinctOperator>();
			CreateMap<DivideBinaryOperatorDescriptor, DivideBinaryOperator>();
			CreateMap<EndsWithOperatorDescriptor, EndsWithOperator>();
			CreateMap<EqualsBinaryOperatorDescriptor, EqualsBinaryOperator>();
			CreateMap<FilterLambdaOperatorDescriptor, FilterLambdaOperator>()
				.ConstructUsing
				(
					(src, context) => new FilterLambdaOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.FilterBody),
						Type.GetType(src.SourceElementType),
						src.ParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<FirstOperatorDescriptor, FirstOperator>()
				.ConstructUsing
				(
					(src, context) => new FirstOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.FilterBody),
						src.FilterParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<FirstOrDefaultOperatorDescriptor, FirstOrDefaultOperator>()
				.ConstructUsing
				(
					(src, context) => new FirstOrDefaultOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.FilterBody),
						src.FilterParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<FloorOperatorDescriptor, FloorOperator>();
			CreateMap<FractionalSecondsOperatorDescriptor, FractionalSecondsOperator>();
			CreateMap<GreaterThanBinaryOperatorDescriptor, GreaterThanBinaryOperator>();
			CreateMap<GreaterThanOrEqualsBinaryOperatorDescriptor, GreaterThanOrEqualsBinaryOperator>();
			CreateMap<GroupByOperatorDescriptor, GroupByOperator>()
				.ConstructUsing
				(
					(src, context) => new GroupByOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.SelectorBody),
						src.SelectorParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<HasOperatorDescriptor, HasOperator>();
			CreateMap<HourOperatorDescriptor, HourOperator>();
			CreateMap<IEnumerableSelectorLambdaOperatorDescriptor, IEnumerableSelectorLambdaOperator>()
				.ConstructUsing
				(
					(src, context) => new IEnumerableSelectorLambdaOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.Selector),
						Type.GetType(src.SourceElementType),
						src.ParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<IndexOfOperatorDescriptor, IndexOfOperator>();
			CreateMap<InOperatorDescriptor, InOperator>();
			CreateMap<IsOfOperatorDescriptor, IsOfOperator>()
				.ForMember(dest => dest.Type, opts => opts.Ignore())
				.ForCtorParam("type", opts => opts.MapFrom(x => Type.GetType(x.Type)));
			CreateMap<LastOperatorDescriptor, LastOperator>()
				.ConstructUsing
				(
					(src, context) => new LastOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.FilterBody),
						src.FilterParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<LastOrDefaultOperatorDescriptor, LastOrDefaultOperator>()
				.ConstructUsing
				(
					(src, context) => new LastOrDefaultOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.FilterBody),
						src.FilterParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<LengthOperatorDescriptor, LengthOperator>();
			CreateMap<LessThanBinaryOperatorDescriptor, LessThanBinaryOperator>();
			CreateMap<LessThanOrEqualsBinaryOperatorDescriptor, LessThanOrEqualsBinaryOperator>();
			CreateMap<MaxDateTimeOperatorDescriptor, MaxDateTimeOperator>();
			CreateMap<MaxOperatorDescriptor, MaxOperator>()
				.ConstructUsing
				(
					(src, context) => new MaxOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.SelectorBody),
						src.SelectorParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<MemberInitOperatorDescriptor, MemberInitOperator>()
				.ForMember(dest => dest.NewType, opts => opts.Ignore())
				.ForCtorParam("newType", opts => opts.MapFrom(x => Type.GetType(x.NewType)));
			CreateMap<MemberSelectorOperatorDescriptor, MemberSelectorOperator>();
			CreateMap<MinDateTimeOperatorDescriptor, MinDateTimeOperator>();
			CreateMap<MinOperatorDescriptor, MinOperator>()
				.ConstructUsing
				(
					(src, context) => new MinOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.SelectorBody),
						src.SelectorParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<MinuteOperatorDescriptor, MinuteOperator>();
			CreateMap<ModuloBinaryOperatorDescriptor, ModuloBinaryOperator>();
			CreateMap<MonthOperatorDescriptor, MonthOperator>();
			CreateMap<MultiplyBinaryOperatorDescriptor, MultiplyBinaryOperator>();
			CreateMap<NegateOperatorDescriptor, NegateOperator>();
			CreateMap<NotEqualsBinaryOperatorDescriptor, NotEqualsBinaryOperator>();
			CreateMap<NotOperatorDescriptor, NotOperator>();
			CreateMap<NowDateTimeOperatorDescriptor, NowDateTimeOperator>();
			CreateMap<OrBinaryOperatorDescriptor, OrBinaryOperator>();
			CreateMap<OrderByOperatorDescriptor, OrderByOperator>()
				.ConstructUsing
				(
					(src, context) => new OrderByOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.SelectorBody),
						src.SortDirection,
						src.SelectorParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<ParameterOperatorDescriptor, ParameterOperator>()
				.ConstructUsing
				(
					(src, context) => new ParameterOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						src.ParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<RoundOperatorDescriptor, RoundOperator>();
			CreateMap<SecondOperatorDescriptor, SecondOperator>();
			CreateMap<SelectManyOperatorDescriptor, SelectManyOperator>()
				.ConstructUsing
				(
					(src, context) => new SelectManyOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.SelectorBody),
						src.SelectorParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<SelectOperatorDescriptor, SelectOperator>()
				.ConstructUsing
				(
					(src, context) => new SelectOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.SelectorBody),
						src.SelectorParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<SelectorLambdaOperatorDescriptor, SelectorLambdaOperator>()
				.ConstructUsing
				(
					(src, context) => new SelectorLambdaOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.Selector),
						Type.GetType(src.SourceElementType),
						Type.GetType(src.BodyType),
						src.ParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<SingleOperatorDescriptor, SingleOperator>()
				.ConstructUsing
				(
					(src, context) => new SingleOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.FilterBody),
						src.FilterParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<SingleOrDefaultOperatorDescriptor, SingleOrDefaultOperator>()
				.ConstructUsing
				(
					(src, context) => new SingleOrDefaultOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.FilterBody),
						src.FilterParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<SkipOperatorDescriptor, SkipOperator>();
			CreateMap<StartsWithOperatorDescriptor, StartsWithOperator>();
			CreateMap<SubstringOperatorDescriptor, SubstringOperator>();
			CreateMap<SubtractBinaryOperatorDescriptor, SubtractBinaryOperator>();
			CreateMap<SumOperatorDescriptor, SumOperator>()
				.ConstructUsing
				(
					(src, context) => new SumOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.SelectorBody),
						src.SelectorParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<TakeOperatorDescriptor, TakeOperator>();
			CreateMap<ThenByOperatorDescriptor, ThenByOperator>()
				.ConstructUsing
				(
					(src, context) => new ThenByOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.SelectorBody),
						src.SortDirection,
						src.SelectorParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<TimeOperatorDescriptor, TimeOperator>();
			CreateMap<ToListOperatorDescriptor, ToListOperator>();
			CreateMap<ToLowerOperatorDescriptor, ToLowerOperator>();
			CreateMap<TotalOffsetMinutesOperatorDescriptor, TotalOffsetMinutesOperator>();
			CreateMap<TotalSecondsOperatorDescriptor, TotalSecondsOperator>();
			CreateMap<ToUpperOperatorDescriptor, ToUpperOperator>();
			CreateMap<TrimOperatorDescriptor, TrimOperator>();
			CreateMap<WhereOperatorDescriptor, WhereOperator>()
				.ConstructUsing
				(
					(src, context) => new WhereOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.FilterBody),
						src.FilterParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<YearOperatorDescriptor, YearOperator>();

            CreateMap<IExpressionOperatorDescriptor, IExpressionPart>()
				.Include<AddBinaryOperatorDescriptor, AddBinaryOperator>()
				.Include<AllOperatorDescriptor, AllOperator>()
				.Include<AndBinaryOperatorDescriptor, AndBinaryOperator>()
				.Include<AnyOperatorDescriptor, AnyOperator>()
				.Include<AsEnumerableOperatorDescriptor, AsEnumerableOperator>()
				.Include<AsQueryableOperatorDescriptor, AsQueryableOperator>()
				.Include<AverageOperatorDescriptor, AverageOperator>()
				.Include<BinaryOperatorDescriptor, BinaryOperator>()
				.Include<CastOperatorDescriptor, CastOperator>()
				.Include<CeilingOperatorDescriptor, CeilingOperator>()
				.Include<CollectionCastOperatorDescriptor, CollectionCastOperator>()
				.Include<CollectionConstantOperatorDescriptor, CollectionConstantOperator>()
				.Include<ConcatOperatorDescriptor, ConcatOperator>()
				.Include<ConstantOperatorDescriptor, ConstantOperator>()
				.Include<ContainsOperatorDescriptor, ContainsOperator>()
				.Include<ConvertCharArrayToStringOperatorDescriptor, ConvertCharArrayToStringOperator>()
				.Include<ConvertOperatorDescriptor, ConvertOperator>()
				.Include<ConvertToEnumOperatorDescriptor, ConvertToEnumOperator>()
				.Include<ConvertToNullableUnderlyingValueOperatorDescriptor, ConvertToNullableUnderlyingValueOperator>()
				.Include<ConvertToNumericDateOperatorDescriptor, ConvertToNumericDateOperator>()
				.Include<ConvertToNumericTimeOperatorDescriptor, ConvertToNumericTimeOperator>()
				.Include<ConvertToStringOperatorDescriptor, ConvertToStringOperator>()
				.Include<CountOperatorDescriptor, CountOperator>()
				.Include<CustomMethodOperatorDescriptor, CustomMethodOperator>()
				.Include<DateOperatorDescriptor, DateOperator>()
				.Include<DayOperatorDescriptor, DayOperator>()
				.Include<DistinctOperatorDescriptor, DistinctOperator>()
				.Include<DivideBinaryOperatorDescriptor, DivideBinaryOperator>()
				.Include<EndsWithOperatorDescriptor, EndsWithOperator>()
				.Include<EqualsBinaryOperatorDescriptor, EqualsBinaryOperator>()
				.Include<FilterLambdaOperatorDescriptor, FilterLambdaOperator>()
				.Include<FirstOperatorDescriptor, FirstOperator>()
				.Include<FirstOrDefaultOperatorDescriptor, FirstOrDefaultOperator>()
				.Include<FloorOperatorDescriptor, FloorOperator>()
				.Include<FractionalSecondsOperatorDescriptor, FractionalSecondsOperator>()
				.Include<GreaterThanBinaryOperatorDescriptor, GreaterThanBinaryOperator>()
				.Include<GreaterThanOrEqualsBinaryOperatorDescriptor, GreaterThanOrEqualsBinaryOperator>()
				.Include<GroupByOperatorDescriptor, GroupByOperator>()
				.Include<HasOperatorDescriptor, HasOperator>()
				.Include<HourOperatorDescriptor, HourOperator>()
				.Include<IEnumerableSelectorLambdaOperatorDescriptor, IEnumerableSelectorLambdaOperator>()
				.Include<IndexOfOperatorDescriptor, IndexOfOperator>()
				.Include<InOperatorDescriptor, InOperator>()
				.Include<IsOfOperatorDescriptor, IsOfOperator>()
				.Include<LastOperatorDescriptor, LastOperator>()
				.Include<LastOrDefaultOperatorDescriptor, LastOrDefaultOperator>()
				.Include<LengthOperatorDescriptor, LengthOperator>()
				.Include<LessThanBinaryOperatorDescriptor, LessThanBinaryOperator>()
				.Include<LessThanOrEqualsBinaryOperatorDescriptor, LessThanOrEqualsBinaryOperator>()
				.Include<MaxDateTimeOperatorDescriptor, MaxDateTimeOperator>()
				.Include<MaxOperatorDescriptor, MaxOperator>()
				.Include<MemberInitOperatorDescriptor, MemberInitOperator>()
				.Include<MemberSelectorOperatorDescriptor, MemberSelectorOperator>()
				.Include<MinDateTimeOperatorDescriptor, MinDateTimeOperator>()
				.Include<MinOperatorDescriptor, MinOperator>()
				.Include<MinuteOperatorDescriptor, MinuteOperator>()
				.Include<ModuloBinaryOperatorDescriptor, ModuloBinaryOperator>()
				.Include<MonthOperatorDescriptor, MonthOperator>()
				.Include<MultiplyBinaryOperatorDescriptor, MultiplyBinaryOperator>()
				.Include<NegateOperatorDescriptor, NegateOperator>()
				.Include<NotEqualsBinaryOperatorDescriptor, NotEqualsBinaryOperator>()
				.Include<NotOperatorDescriptor, NotOperator>()
				.Include<NowDateTimeOperatorDescriptor, NowDateTimeOperator>()
				.Include<OrBinaryOperatorDescriptor, OrBinaryOperator>()
				.Include<OrderByOperatorDescriptor, OrderByOperator>()
				.Include<ParameterOperatorDescriptor, ParameterOperator>()
				.Include<RoundOperatorDescriptor, RoundOperator>()
				.Include<SecondOperatorDescriptor, SecondOperator>()
				.Include<SelectManyOperatorDescriptor, SelectManyOperator>()
				.Include<SelectOperatorDescriptor, SelectOperator>()
				.Include<SelectorLambdaOperatorDescriptor, SelectorLambdaOperator>()
				.Include<SingleOperatorDescriptor, SingleOperator>()
				.Include<SingleOrDefaultOperatorDescriptor, SingleOrDefaultOperator>()
				.Include<SkipOperatorDescriptor, SkipOperator>()
				.Include<StartsWithOperatorDescriptor, StartsWithOperator>()
				.Include<SubstringOperatorDescriptor, SubstringOperator>()
				.Include<SubtractBinaryOperatorDescriptor, SubtractBinaryOperator>()
				.Include<SumOperatorDescriptor, SumOperator>()
				.Include<TakeOperatorDescriptor, TakeOperator>()
				.Include<ThenByOperatorDescriptor, ThenByOperator>()
				.Include<TimeOperatorDescriptor, TimeOperator>()
				.Include<ToListOperatorDescriptor, ToListOperator>()
				.Include<ToLowerOperatorDescriptor, ToLowerOperator>()
				.Include<TotalOffsetMinutesOperatorDescriptor, TotalOffsetMinutesOperator>()
				.Include<TotalSecondsOperatorDescriptor, TotalSecondsOperator>()
				.Include<ToUpperOperatorDescriptor, ToUpperOperator>()
				.Include<TrimOperatorDescriptor, TrimOperator>()
				.Include<WhereOperatorDescriptor, WhereOperator>()
				.Include<YearOperatorDescriptor, YearOperator>();
        }
    }
}