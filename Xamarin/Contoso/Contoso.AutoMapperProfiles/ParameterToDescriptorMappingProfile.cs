using AutoMapper;
using Contoso.Parameters.Expressions;
using Contoso.Common.Configuration.ExpressionDescriptors;
using System;

namespace Contoso.AutoMapperProfiles
{
    public class ParameterToDescriptorMappingProfile : Profile
    {
        public ParameterToDescriptorMappingProfile()
        {
			CreateMap<AddBinaryOperatorParameters, AddBinaryOperatorDescriptor>();
			CreateMap<AllOperatorParameters, AllOperatorDescriptor>();
			CreateMap<AndBinaryOperatorParameters, AndBinaryOperatorDescriptor>();
			CreateMap<AnyOperatorParameters, AnyOperatorDescriptor>();
			CreateMap<AsEnumerableOperatorParameters, AsEnumerableOperatorDescriptor>();
			CreateMap<AsQueryableOperatorParameters, AsQueryableOperatorDescriptor>();
			CreateMap<AverageOperatorParameters, AverageOperatorDescriptor>();
			CreateMap<BinaryOperatorParameters, BinaryOperatorDescriptor>();
			CreateMap<CastOperatorParameters, CastOperatorDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<CeilingOperatorParameters, CeilingOperatorDescriptor>();
			CreateMap<CollectionCastOperatorParameters, CollectionCastOperatorDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<CollectionConstantOperatorParameters, CollectionConstantOperatorDescriptor>()
				.ForMember(dest => dest.ElementType, opts => opts.MapFrom(x => x.ElementType.AssemblyQualifiedName));
			CreateMap<ConcatOperatorParameters, ConcatOperatorDescriptor>();
			CreateMap<ConstantOperatorParameters, ConstantOperatorDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<ContainsOperatorParameters, ContainsOperatorDescriptor>();
			CreateMap<ConvertCharArrayToStringOperatorParameters, ConvertCharArrayToStringOperatorDescriptor>();
			CreateMap<ConvertOperatorParameters, ConvertOperatorDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<ConvertToEnumOperatorParameters, ConvertToEnumOperatorDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<ConvertToNullableUnderlyingValueOperatorParameters, ConvertToNullableUnderlyingValueOperatorDescriptor>();
			CreateMap<ConvertToNumericDateOperatorParameters, ConvertToNumericDateOperatorDescriptor>();
			CreateMap<ConvertToNumericTimeOperatorParameters, ConvertToNumericTimeOperatorDescriptor>();
			CreateMap<ConvertToStringOperatorParameters, ConvertToStringOperatorDescriptor>();
			CreateMap<CountOperatorParameters, CountOperatorDescriptor>();
			CreateMap<CustomMethodOperatorParameters, CustomMethodOperatorDescriptor>();
			CreateMap<DateOperatorParameters, DateOperatorDescriptor>();
			CreateMap<DayOperatorParameters, DayOperatorDescriptor>();
			CreateMap<DistinctOperatorParameters, DistinctOperatorDescriptor>();
			CreateMap<DivideBinaryOperatorParameters, DivideBinaryOperatorDescriptor>();
			CreateMap<EndsWithOperatorParameters, EndsWithOperatorDescriptor>();
			CreateMap<EqualsBinaryOperatorParameters, EqualsBinaryOperatorDescriptor>();
			CreateMap<FilterLambdaOperatorParameters, FilterLambdaOperatorDescriptor>()
				.ForMember(dest => dest.SourceElementType, opts => opts.MapFrom(x => x.SourceElementType.AssemblyQualifiedName));
			CreateMap<FirstOperatorParameters, FirstOperatorDescriptor>();
			CreateMap<FirstOrDefaultOperatorParameters, FirstOrDefaultOperatorDescriptor>();
			CreateMap<FloorOperatorParameters, FloorOperatorDescriptor>();
			CreateMap<FractionalSecondsOperatorParameters, FractionalSecondsOperatorDescriptor>();
			CreateMap<GreaterThanBinaryOperatorParameters, GreaterThanBinaryOperatorDescriptor>();
			CreateMap<GreaterThanOrEqualsBinaryOperatorParameters, GreaterThanOrEqualsBinaryOperatorDescriptor>();
			CreateMap<GroupByOperatorParameters, GroupByOperatorDescriptor>();
			CreateMap<HasOperatorParameters, HasOperatorDescriptor>();
			CreateMap<HourOperatorParameters, HourOperatorDescriptor>();
			CreateMap<IEnumerableSelectorLambdaOperatorParameters, IEnumerableSelectorLambdaOperatorDescriptor>()
				.ForMember(dest => dest.SourceElementType, opts => opts.MapFrom(x => x.SourceElementType.AssemblyQualifiedName));
			CreateMap<IndexOfOperatorParameters, IndexOfOperatorDescriptor>();
			CreateMap<InOperatorParameters, InOperatorDescriptor>();
			CreateMap<IsOfOperatorParameters, IsOfOperatorDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<LastOperatorParameters, LastOperatorDescriptor>();
			CreateMap<LastOrDefaultOperatorParameters, LastOrDefaultOperatorDescriptor>();
			CreateMap<LengthOperatorParameters, LengthOperatorDescriptor>();
			CreateMap<LessThanBinaryOperatorParameters, LessThanBinaryOperatorDescriptor>();
			CreateMap<LessThanOrEqualsBinaryOperatorParameters, LessThanOrEqualsBinaryOperatorDescriptor>();
			CreateMap<MaxDateTimeOperatorParameters, MaxDateTimeOperatorDescriptor>();
			CreateMap<MaxOperatorParameters, MaxOperatorDescriptor>();
			CreateMap<MemberInitOperatorParameters, MemberInitOperatorDescriptor>()
				.ForMember(dest => dest.NewType, opts => opts.MapFrom(x => x.NewType.AssemblyQualifiedName));
			CreateMap<MemberSelectorOperatorParameters, MemberSelectorOperatorDescriptor>();
			CreateMap<MinDateTimeOperatorParameters, MinDateTimeOperatorDescriptor>();
			CreateMap<MinOperatorParameters, MinOperatorDescriptor>();
			CreateMap<MinuteOperatorParameters, MinuteOperatorDescriptor>();
			CreateMap<ModuloBinaryOperatorParameters, ModuloBinaryOperatorDescriptor>();
			CreateMap<MonthOperatorParameters, MonthOperatorDescriptor>();
			CreateMap<MultiplyBinaryOperatorParameters, MultiplyBinaryOperatorDescriptor>();
			CreateMap<NegateOperatorParameters, NegateOperatorDescriptor>();
			CreateMap<NotEqualsBinaryOperatorParameters, NotEqualsBinaryOperatorDescriptor>();
			CreateMap<NotOperatorParameters, NotOperatorDescriptor>();
			CreateMap<NowDateTimeOperatorParameters, NowDateTimeOperatorDescriptor>();
			CreateMap<OrBinaryOperatorParameters, OrBinaryOperatorDescriptor>();
			CreateMap<OrderByOperatorParameters, OrderByOperatorDescriptor>();
			CreateMap<ParameterOperatorParameters, ParameterOperatorDescriptor>();
			CreateMap<RoundOperatorParameters, RoundOperatorDescriptor>();
			CreateMap<SecondOperatorParameters, SecondOperatorDescriptor>();
			CreateMap<SelectManyOperatorParameters, SelectManyOperatorDescriptor>();
			CreateMap<SelectOperatorParameters, SelectOperatorDescriptor>();
			CreateMap<SelectorLambdaOperatorParameters, SelectorLambdaOperatorDescriptor>()
				.ForMember(dest => dest.SourceElementType, opts => opts.MapFrom(x => x.SourceElementType.AssemblyQualifiedName))
				.ForMember(dest => dest.BodyType, opts => opts.MapFrom(x => x.BodyType.AssemblyQualifiedName));
			CreateMap<SingleOperatorParameters, SingleOperatorDescriptor>();
			CreateMap<SingleOrDefaultOperatorParameters, SingleOrDefaultOperatorDescriptor>();
			CreateMap<SkipOperatorParameters, SkipOperatorDescriptor>();
			CreateMap<StartsWithOperatorParameters, StartsWithOperatorDescriptor>();
			CreateMap<SubstringOperatorParameters, SubstringOperatorDescriptor>();
			CreateMap<SubtractBinaryOperatorParameters, SubtractBinaryOperatorDescriptor>();
			CreateMap<SumOperatorParameters, SumOperatorDescriptor>();
			CreateMap<TakeOperatorParameters, TakeOperatorDescriptor>();
			CreateMap<ThenByOperatorParameters, ThenByOperatorDescriptor>();
			CreateMap<TimeOperatorParameters, TimeOperatorDescriptor>();
			CreateMap<ToListOperatorParameters, ToListOperatorDescriptor>();
			CreateMap<ToLowerOperatorParameters, ToLowerOperatorDescriptor>();
			CreateMap<TotalOffsetMinutesOperatorParameters, TotalOffsetMinutesOperatorDescriptor>();
			CreateMap<TotalSecondsOperatorParameters, TotalSecondsOperatorDescriptor>();
			CreateMap<ToUpperOperatorParameters, ToUpperOperatorDescriptor>();
			CreateMap<TrimOperatorParameters, TrimOperatorDescriptor>();
			CreateMap<WhereOperatorParameters, WhereOperatorDescriptor>();
			CreateMap<YearOperatorParameters, YearOperatorDescriptor>();

            CreateMap<IExpressionParameter, OperatorDescriptorBase>()
				.Include<AddBinaryOperatorParameters, AddBinaryOperatorDescriptor>()
				.Include<AllOperatorParameters, AllOperatorDescriptor>()
				.Include<AndBinaryOperatorParameters, AndBinaryOperatorDescriptor>()
				.Include<AnyOperatorParameters, AnyOperatorDescriptor>()
				.Include<AsEnumerableOperatorParameters, AsEnumerableOperatorDescriptor>()
				.Include<AsQueryableOperatorParameters, AsQueryableOperatorDescriptor>()
				.Include<AverageOperatorParameters, AverageOperatorDescriptor>()
				.Include<BinaryOperatorParameters, BinaryOperatorDescriptor>()
				.Include<CastOperatorParameters, CastOperatorDescriptor>()
				.Include<CeilingOperatorParameters, CeilingOperatorDescriptor>()
				.Include<CollectionCastOperatorParameters, CollectionCastOperatorDescriptor>()
				.Include<CollectionConstantOperatorParameters, CollectionConstantOperatorDescriptor>()
				.Include<ConcatOperatorParameters, ConcatOperatorDescriptor>()
				.Include<ConstantOperatorParameters, ConstantOperatorDescriptor>()
				.Include<ContainsOperatorParameters, ContainsOperatorDescriptor>()
				.Include<ConvertCharArrayToStringOperatorParameters, ConvertCharArrayToStringOperatorDescriptor>()
				.Include<ConvertOperatorParameters, ConvertOperatorDescriptor>()
				.Include<ConvertToEnumOperatorParameters, ConvertToEnumOperatorDescriptor>()
				.Include<ConvertToNullableUnderlyingValueOperatorParameters, ConvertToNullableUnderlyingValueOperatorDescriptor>()
				.Include<ConvertToNumericDateOperatorParameters, ConvertToNumericDateOperatorDescriptor>()
				.Include<ConvertToNumericTimeOperatorParameters, ConvertToNumericTimeOperatorDescriptor>()
				.Include<ConvertToStringOperatorParameters, ConvertToStringOperatorDescriptor>()
				.Include<CountOperatorParameters, CountOperatorDescriptor>()
				.Include<CustomMethodOperatorParameters, CustomMethodOperatorDescriptor>()
				.Include<DateOperatorParameters, DateOperatorDescriptor>()
				.Include<DayOperatorParameters, DayOperatorDescriptor>()
				.Include<DistinctOperatorParameters, DistinctOperatorDescriptor>()
				.Include<DivideBinaryOperatorParameters, DivideBinaryOperatorDescriptor>()
				.Include<EndsWithOperatorParameters, EndsWithOperatorDescriptor>()
				.Include<EqualsBinaryOperatorParameters, EqualsBinaryOperatorDescriptor>()
				.Include<FilterLambdaOperatorParameters, FilterLambdaOperatorDescriptor>()
				.Include<FirstOperatorParameters, FirstOperatorDescriptor>()
				.Include<FirstOrDefaultOperatorParameters, FirstOrDefaultOperatorDescriptor>()
				.Include<FloorOperatorParameters, FloorOperatorDescriptor>()
				.Include<FractionalSecondsOperatorParameters, FractionalSecondsOperatorDescriptor>()
				.Include<GreaterThanBinaryOperatorParameters, GreaterThanBinaryOperatorDescriptor>()
				.Include<GreaterThanOrEqualsBinaryOperatorParameters, GreaterThanOrEqualsBinaryOperatorDescriptor>()
				.Include<GroupByOperatorParameters, GroupByOperatorDescriptor>()
				.Include<HasOperatorParameters, HasOperatorDescriptor>()
				.Include<HourOperatorParameters, HourOperatorDescriptor>()
				.Include<IEnumerableSelectorLambdaOperatorParameters, IEnumerableSelectorLambdaOperatorDescriptor>()
				.Include<IndexOfOperatorParameters, IndexOfOperatorDescriptor>()
				.Include<InOperatorParameters, InOperatorDescriptor>()
				.Include<IsOfOperatorParameters, IsOfOperatorDescriptor>()
				.Include<LastOperatorParameters, LastOperatorDescriptor>()
				.Include<LastOrDefaultOperatorParameters, LastOrDefaultOperatorDescriptor>()
				.Include<LengthOperatorParameters, LengthOperatorDescriptor>()
				.Include<LessThanBinaryOperatorParameters, LessThanBinaryOperatorDescriptor>()
				.Include<LessThanOrEqualsBinaryOperatorParameters, LessThanOrEqualsBinaryOperatorDescriptor>()
				.Include<MaxDateTimeOperatorParameters, MaxDateTimeOperatorDescriptor>()
				.Include<MaxOperatorParameters, MaxOperatorDescriptor>()
				.Include<MemberInitOperatorParameters, MemberInitOperatorDescriptor>()
				.Include<MemberSelectorOperatorParameters, MemberSelectorOperatorDescriptor>()
				.Include<MinDateTimeOperatorParameters, MinDateTimeOperatorDescriptor>()
				.Include<MinOperatorParameters, MinOperatorDescriptor>()
				.Include<MinuteOperatorParameters, MinuteOperatorDescriptor>()
				.Include<ModuloBinaryOperatorParameters, ModuloBinaryOperatorDescriptor>()
				.Include<MonthOperatorParameters, MonthOperatorDescriptor>()
				.Include<MultiplyBinaryOperatorParameters, MultiplyBinaryOperatorDescriptor>()
				.Include<NegateOperatorParameters, NegateOperatorDescriptor>()
				.Include<NotEqualsBinaryOperatorParameters, NotEqualsBinaryOperatorDescriptor>()
				.Include<NotOperatorParameters, NotOperatorDescriptor>()
				.Include<NowDateTimeOperatorParameters, NowDateTimeOperatorDescriptor>()
				.Include<OrBinaryOperatorParameters, OrBinaryOperatorDescriptor>()
				.Include<OrderByOperatorParameters, OrderByOperatorDescriptor>()
				.Include<ParameterOperatorParameters, ParameterOperatorDescriptor>()
				.Include<RoundOperatorParameters, RoundOperatorDescriptor>()
				.Include<SecondOperatorParameters, SecondOperatorDescriptor>()
				.Include<SelectManyOperatorParameters, SelectManyOperatorDescriptor>()
				.Include<SelectOperatorParameters, SelectOperatorDescriptor>()
				.Include<SelectorLambdaOperatorParameters, SelectorLambdaOperatorDescriptor>()
				.Include<SingleOperatorParameters, SingleOperatorDescriptor>()
				.Include<SingleOrDefaultOperatorParameters, SingleOrDefaultOperatorDescriptor>()
				.Include<SkipOperatorParameters, SkipOperatorDescriptor>()
				.Include<StartsWithOperatorParameters, StartsWithOperatorDescriptor>()
				.Include<SubstringOperatorParameters, SubstringOperatorDescriptor>()
				.Include<SubtractBinaryOperatorParameters, SubtractBinaryOperatorDescriptor>()
				.Include<SumOperatorParameters, SumOperatorDescriptor>()
				.Include<TakeOperatorParameters, TakeOperatorDescriptor>()
				.Include<ThenByOperatorParameters, ThenByOperatorDescriptor>()
				.Include<TimeOperatorParameters, TimeOperatorDescriptor>()
				.Include<ToListOperatorParameters, ToListOperatorDescriptor>()
				.Include<ToLowerOperatorParameters, ToLowerOperatorDescriptor>()
				.Include<TotalOffsetMinutesOperatorParameters, TotalOffsetMinutesOperatorDescriptor>()
				.Include<TotalSecondsOperatorParameters, TotalSecondsOperatorDescriptor>()
				.Include<ToUpperOperatorParameters, ToUpperOperatorDescriptor>()
				.Include<TrimOperatorParameters, TrimOperatorDescriptor>()
				.Include<WhereOperatorParameters, WhereOperatorDescriptor>()
				.Include<YearOperatorParameters, YearOperatorDescriptor>();
        }
    }
}