using AutoMapper;
using CheckMySymptoms.Forms.Parameters.Input;
using CheckMySymptoms.Forms.View.Input;
using LogicBuilder.Forms.Parameters;
using System;
using System.Collections.Generic;

namespace CheckMySymptoms.AutoMapperProfiles
{
    public class InputVariablesMappingProfile : Profile
    {
        public InputVariablesMappingProfile()
        {
            CreateMap<InputView<IEnumerable<bool>>, InputQuestionParameters<IEnumerable<bool>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<bool>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<bool>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<bool>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<bool>>, InputQuestionParameters<ICollection<bool>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<bool>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<bool>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<bool>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<bool>>, InputQuestionParameters<IList<bool>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<bool>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<bool>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<bool>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<bool>, InputQuestionParameters<bool>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<bool>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<bool>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<bool>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<DateTime>>, InputQuestionParameters<IEnumerable<DateTime>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<DateTime>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<DateTime>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<DateTime>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<DateTime>>, InputQuestionParameters<ICollection<DateTime>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<DateTime>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<DateTime>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<DateTime>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<DateTime>>, InputQuestionParameters<IList<DateTime>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<DateTime>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<DateTime>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<DateTime>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<DateTime>, InputQuestionParameters<DateTime>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<DateTime>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<DateTime>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<DateTime>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<TimeSpan>>, InputQuestionParameters<IEnumerable<TimeSpan>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<TimeSpan>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<TimeSpan>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<TimeSpan>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<TimeSpan>>, InputQuestionParameters<ICollection<TimeSpan>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<TimeSpan>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<TimeSpan>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<TimeSpan>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<TimeSpan>>, InputQuestionParameters<IList<TimeSpan>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<TimeSpan>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<TimeSpan>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<TimeSpan>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<TimeSpan>, InputQuestionParameters<TimeSpan>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<TimeSpan>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<TimeSpan>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<TimeSpan>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<Guid>>, InputQuestionParameters<IEnumerable<Guid>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<Guid>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<Guid>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<Guid>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<Guid>>, InputQuestionParameters<ICollection<Guid>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<Guid>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<Guid>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<Guid>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<Guid>>, InputQuestionParameters<IList<Guid>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<Guid>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<Guid>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<Guid>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<Guid>, InputQuestionParameters<Guid>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<Guid>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<Guid>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<Guid>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<decimal>>, InputQuestionParameters<IEnumerable<decimal>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<decimal>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<decimal>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<decimal>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<decimal>>, InputQuestionParameters<ICollection<decimal>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<decimal>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<decimal>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<decimal>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<decimal>>, InputQuestionParameters<IList<decimal>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<decimal>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<decimal>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<decimal>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<decimal>, InputQuestionParameters<decimal>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<decimal>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<decimal>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<decimal>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<byte>>, InputQuestionParameters<IEnumerable<byte>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<byte>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<byte>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<byte>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<byte>>, InputQuestionParameters<ICollection<byte>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<byte>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<byte>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<byte>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<byte>>, InputQuestionParameters<IList<byte>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<byte>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<byte>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<byte>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<byte>, InputQuestionParameters<byte>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<byte>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<byte>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<byte>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<short>>, InputQuestionParameters<IEnumerable<short>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<short>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<short>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<short>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<short>>, InputQuestionParameters<ICollection<short>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<short>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<short>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<short>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<short>>, InputQuestionParameters<IList<short>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<short>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<short>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<short>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<short>, InputQuestionParameters<short>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<short>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<short>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<short>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<int>>, InputQuestionParameters<IEnumerable<int>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<int>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<int>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<int>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<int>>, InputQuestionParameters<ICollection<int>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<int>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<int>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<int>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<int>>, InputQuestionParameters<IList<int>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<int>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<int>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<int>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<int>, InputQuestionParameters<int>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<int>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<int>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<int>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<long>>, InputQuestionParameters<IEnumerable<long>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<long>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<long>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<long>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<long>>, InputQuestionParameters<ICollection<long>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<long>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<long>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<long>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<long>>, InputQuestionParameters<IList<long>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<long>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<long>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<long>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<long>, InputQuestionParameters<long>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<long>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<long>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<long>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<float>>, InputQuestionParameters<IEnumerable<float>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<float>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<float>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<float>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<float>>, InputQuestionParameters<ICollection<float>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<float>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<float>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<float>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<float>>, InputQuestionParameters<IList<float>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<float>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<float>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<float>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<float>, InputQuestionParameters<float>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<float>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<float>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<float>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<double>>, InputQuestionParameters<IEnumerable<double>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<double>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<double>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<double>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<double>>, InputQuestionParameters<ICollection<double>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<double>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<double>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<double>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<double>>, InputQuestionParameters<IList<double>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<double>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<double>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<double>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<double>, InputQuestionParameters<double>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<double>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<double>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<double>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<char>>, InputQuestionParameters<IEnumerable<char>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<char>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<char>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<char>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<char>>, InputQuestionParameters<ICollection<char>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<char>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<char>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<char>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<char>>, InputQuestionParameters<IList<char>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<char>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<char>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<char>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<char>, InputQuestionParameters<char>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<char>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<char>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<char>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<sbyte>>, InputQuestionParameters<IEnumerable<sbyte>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<sbyte>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<sbyte>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<sbyte>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<sbyte>>, InputQuestionParameters<ICollection<sbyte>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<sbyte>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<sbyte>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<sbyte>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<sbyte>>, InputQuestionParameters<IList<sbyte>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<sbyte>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<sbyte>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<sbyte>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<sbyte>, InputQuestionParameters<sbyte>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<sbyte>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<sbyte>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<sbyte>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<ushort>>, InputQuestionParameters<IEnumerable<ushort>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<ushort>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<ushort>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<ushort>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<ushort>>, InputQuestionParameters<ICollection<ushort>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<ushort>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<ushort>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<ushort>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<ushort>>, InputQuestionParameters<IList<ushort>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<ushort>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<ushort>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<ushort>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ushort>, InputQuestionParameters<ushort>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ushort>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ushort>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ushort>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<uint>>, InputQuestionParameters<IEnumerable<uint>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<uint>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<uint>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<uint>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<uint>>, InputQuestionParameters<ICollection<uint>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<uint>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<uint>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<uint>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<uint>>, InputQuestionParameters<IList<uint>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<uint>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<uint>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<uint>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<uint>, InputQuestionParameters<uint>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<uint>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<uint>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<uint>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<ulong>>, InputQuestionParameters<IEnumerable<ulong>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<ulong>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<ulong>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<ulong>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<ulong>>, InputQuestionParameters<ICollection<ulong>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<ulong>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<ulong>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<ulong>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<ulong>>, InputQuestionParameters<IList<ulong>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<ulong>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<ulong>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<ulong>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ulong>, InputQuestionParameters<ulong>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ulong>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ulong>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ulong>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<bool?>>, InputQuestionParameters<IEnumerable<bool?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<bool?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<bool?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<bool?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<bool?>>, InputQuestionParameters<ICollection<bool?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<bool?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<bool?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<bool?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<bool?>>, InputQuestionParameters<IList<bool?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<bool?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<bool?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<bool?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<bool?>, InputQuestionParameters<bool?>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<bool?>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<bool?>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<bool?>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<DateTime?>>, InputQuestionParameters<IEnumerable<DateTime?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<DateTime?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<DateTime?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<DateTime?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<DateTime?>>, InputQuestionParameters<ICollection<DateTime?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<DateTime?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<DateTime?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<DateTime?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<DateTime?>>, InputQuestionParameters<IList<DateTime?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<DateTime?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<DateTime?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<DateTime?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<DateTime?>, InputQuestionParameters<DateTime?>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<DateTime?>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<DateTime?>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<DateTime?>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<TimeSpan?>>, InputQuestionParameters<IEnumerable<TimeSpan?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<TimeSpan?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<TimeSpan?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<TimeSpan?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<TimeSpan?>>, InputQuestionParameters<ICollection<TimeSpan?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<TimeSpan?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<TimeSpan?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<TimeSpan?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<TimeSpan?>>, InputQuestionParameters<IList<TimeSpan?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<TimeSpan?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<TimeSpan?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<TimeSpan?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<TimeSpan?>, InputQuestionParameters<TimeSpan?>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<TimeSpan?>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<TimeSpan?>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<TimeSpan?>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<Guid?>>, InputQuestionParameters<IEnumerable<Guid?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<Guid?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<Guid?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<Guid?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<Guid?>>, InputQuestionParameters<ICollection<Guid?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<Guid?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<Guid?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<Guid?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<Guid?>>, InputQuestionParameters<IList<Guid?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<Guid?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<Guid?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<Guid?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<Guid?>, InputQuestionParameters<Guid?>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<Guid?>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<Guid?>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<Guid?>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<decimal?>>, InputQuestionParameters<IEnumerable<decimal?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<decimal?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<decimal?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<decimal?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<decimal?>>, InputQuestionParameters<ICollection<decimal?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<decimal?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<decimal?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<decimal?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<decimal?>>, InputQuestionParameters<IList<decimal?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<decimal?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<decimal?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<decimal?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<decimal?>, InputQuestionParameters<decimal?>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<decimal?>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<decimal?>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<decimal?>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<byte?>>, InputQuestionParameters<IEnumerable<byte?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<byte?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<byte?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<byte?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<byte?>>, InputQuestionParameters<ICollection<byte?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<byte?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<byte?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<byte?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<byte?>>, InputQuestionParameters<IList<byte?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<byte?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<byte?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<byte?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<byte?>, InputQuestionParameters<byte?>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<byte?>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<byte?>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<byte?>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<short?>>, InputQuestionParameters<IEnumerable<short?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<short?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<short?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<short?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<short?>>, InputQuestionParameters<ICollection<short?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<short?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<short?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<short?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<short?>>, InputQuestionParameters<IList<short?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<short?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<short?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<short?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<short?>, InputQuestionParameters<short?>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<short?>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<short?>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<short?>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<int?>>, InputQuestionParameters<IEnumerable<int?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<int?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<int?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<int?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<int?>>, InputQuestionParameters<ICollection<int?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<int?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<int?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<int?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<int?>>, InputQuestionParameters<IList<int?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<int?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<int?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<int?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<int?>, InputQuestionParameters<int?>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<int?>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<int?>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<int?>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<long?>>, InputQuestionParameters<IEnumerable<long?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<long?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<long?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<long?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<long?>>, InputQuestionParameters<ICollection<long?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<long?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<long?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<long?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<long?>>, InputQuestionParameters<IList<long?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<long?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<long?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<long?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<long?>, InputQuestionParameters<long?>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<long?>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<long?>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<long?>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<float?>>, InputQuestionParameters<IEnumerable<float?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<float?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<float?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<float?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<float?>>, InputQuestionParameters<ICollection<float?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<float?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<float?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<float?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<float?>>, InputQuestionParameters<IList<float?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<float?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<float?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<float?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<float?>, InputQuestionParameters<float?>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<float?>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<float?>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<float?>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<double?>>, InputQuestionParameters<IEnumerable<double?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<double?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<double?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<double?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<double?>>, InputQuestionParameters<ICollection<double?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<double?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<double?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<double?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<double?>>, InputQuestionParameters<IList<double?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<double?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<double?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<double?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<double?>, InputQuestionParameters<double?>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<double?>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<double?>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<double?>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<char?>>, InputQuestionParameters<IEnumerable<char?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<char?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<char?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<char?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<char?>>, InputQuestionParameters<ICollection<char?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<char?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<char?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<char?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<char?>>, InputQuestionParameters<IList<char?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<char?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<char?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<char?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<char?>, InputQuestionParameters<char?>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<char?>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<char?>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<char?>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<sbyte?>>, InputQuestionParameters<IEnumerable<sbyte?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<sbyte?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<sbyte?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<sbyte?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<sbyte?>>, InputQuestionParameters<ICollection<sbyte?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<sbyte?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<sbyte?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<sbyte?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<sbyte?>>, InputQuestionParameters<IList<sbyte?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<sbyte?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<sbyte?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<sbyte?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<sbyte?>, InputQuestionParameters<sbyte?>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<sbyte?>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<sbyte?>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<sbyte?>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<ushort?>>, InputQuestionParameters<IEnumerable<ushort?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<ushort?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<ushort?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<ushort?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<ushort?>>, InputQuestionParameters<ICollection<ushort?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<ushort?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<ushort?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<ushort?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<ushort?>>, InputQuestionParameters<IList<ushort?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<ushort?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<ushort?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<ushort?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ushort?>, InputQuestionParameters<ushort?>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ushort?>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ushort?>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ushort?>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<uint?>>, InputQuestionParameters<IEnumerable<uint?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<uint?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<uint?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<uint?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<uint?>>, InputQuestionParameters<ICollection<uint?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<uint?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<uint?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<uint?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<uint?>>, InputQuestionParameters<IList<uint?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<uint?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<uint?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<uint?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<uint?>, InputQuestionParameters<uint?>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<uint?>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<uint?>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<uint?>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<ulong?>>, InputQuestionParameters<IEnumerable<ulong?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<ulong?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<ulong?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<ulong?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<ulong?>>, InputQuestionParameters<ICollection<ulong?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<ulong?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<ulong?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<ulong?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<ulong?>>, InputQuestionParameters<IList<ulong?>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<ulong?>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<ulong?>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<ulong?>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ulong?>, InputQuestionParameters<ulong?>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ulong?>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ulong?>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ulong?>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<IEnumerable<string>>, InputQuestionParameters<IEnumerable<string>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IEnumerable<string>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IEnumerable<string>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IEnumerable<string>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<ICollection<string>>, InputQuestionParameters<ICollection<string>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<ICollection<string>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<ICollection<string>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<ICollection<string>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

			CreateMap<InputView<IList<string>>, InputQuestionParameters<IList<string>>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<IList<string>>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<IList<string>>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<IList<string>>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<InputView<string>, InputQuestionParameters<string>>()
                .ConstructUsing((source, context) => new InputQuestionParameters<string>
                {
                    QuestionData = context.Mapper.Map<InputDataParameters>(source)
                })
                .ReverseMap()
                .ConstructUsing((source, context) =>
                {
                    return context.Mapper.Map<InputView<string>>((InputDataParameters)source.QuestionData);
                });
            CreateMap<InputDataParameters, InputView<string>>()
				.ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.VariableName, opt => opt.Ignore())
				.ForMember(dest => dest.Errors, opt => opt.Ignore())
			.ReverseMap();

            CreateMap<BaseInputView, BaseInputQuestionParameters>()
                .ForMember(dest => dest.QuestionData, opt => opt.Ignore())
				.Include<InputView<bool>, InputQuestionParameters<bool>>()
				.Include<InputView<bool?>, InputQuestionParameters<bool?>>()
				.Include<InputView<IEnumerable<bool>>, InputQuestionParameters<IEnumerable<bool>>>()
				.Include<InputView<IEnumerable<bool?>>, InputQuestionParameters<IEnumerable<bool?>>>()
				.Include<InputView<ICollection<bool>>, InputQuestionParameters<ICollection<bool>>>()
				.Include<InputView<ICollection<bool?>>, InputQuestionParameters<ICollection<bool?>>>()
				.Include<InputView<IList<bool>>, InputQuestionParameters<IList<bool>>>()
				.Include<InputView<IList<bool?>>, InputQuestionParameters<IList<bool?>>>()
				.Include<InputView<DateTime>, InputQuestionParameters<DateTime>>()
				.Include<InputView<DateTime?>, InputQuestionParameters<DateTime?>>()
				.Include<InputView<IEnumerable<DateTime>>, InputQuestionParameters<IEnumerable<DateTime>>>()
				.Include<InputView<IEnumerable<DateTime?>>, InputQuestionParameters<IEnumerable<DateTime?>>>()
				.Include<InputView<ICollection<DateTime>>, InputQuestionParameters<ICollection<DateTime>>>()
				.Include<InputView<ICollection<DateTime?>>, InputQuestionParameters<ICollection<DateTime?>>>()
				.Include<InputView<IList<DateTime>>, InputQuestionParameters<IList<DateTime>>>()
				.Include<InputView<IList<DateTime?>>, InputQuestionParameters<IList<DateTime?>>>()
				.Include<InputView<TimeSpan>, InputQuestionParameters<TimeSpan>>()
				.Include<InputView<TimeSpan?>, InputQuestionParameters<TimeSpan?>>()
				.Include<InputView<IEnumerable<TimeSpan>>, InputQuestionParameters<IEnumerable<TimeSpan>>>()
				.Include<InputView<IEnumerable<TimeSpan?>>, InputQuestionParameters<IEnumerable<TimeSpan?>>>()
				.Include<InputView<ICollection<TimeSpan>>, InputQuestionParameters<ICollection<TimeSpan>>>()
				.Include<InputView<ICollection<TimeSpan?>>, InputQuestionParameters<ICollection<TimeSpan?>>>()
				.Include<InputView<IList<TimeSpan>>, InputQuestionParameters<IList<TimeSpan>>>()
				.Include<InputView<IList<TimeSpan?>>, InputQuestionParameters<IList<TimeSpan?>>>()
				.Include<InputView<Guid>, InputQuestionParameters<Guid>>()
				.Include<InputView<Guid?>, InputQuestionParameters<Guid?>>()
				.Include<InputView<IEnumerable<Guid>>, InputQuestionParameters<IEnumerable<Guid>>>()
				.Include<InputView<IEnumerable<Guid?>>, InputQuestionParameters<IEnumerable<Guid?>>>()
				.Include<InputView<ICollection<Guid>>, InputQuestionParameters<ICollection<Guid>>>()
				.Include<InputView<ICollection<Guid?>>, InputQuestionParameters<ICollection<Guid?>>>()
				.Include<InputView<IList<Guid>>, InputQuestionParameters<IList<Guid>>>()
				.Include<InputView<IList<Guid?>>, InputQuestionParameters<IList<Guid?>>>()
				.Include<InputView<decimal>, InputQuestionParameters<decimal>>()
				.Include<InputView<decimal?>, InputQuestionParameters<decimal?>>()
				.Include<InputView<IEnumerable<decimal>>, InputQuestionParameters<IEnumerable<decimal>>>()
				.Include<InputView<IEnumerable<decimal?>>, InputQuestionParameters<IEnumerable<decimal?>>>()
				.Include<InputView<ICollection<decimal>>, InputQuestionParameters<ICollection<decimal>>>()
				.Include<InputView<ICollection<decimal?>>, InputQuestionParameters<ICollection<decimal?>>>()
				.Include<InputView<IList<decimal>>, InputQuestionParameters<IList<decimal>>>()
				.Include<InputView<IList<decimal?>>, InputQuestionParameters<IList<decimal?>>>()
				.Include<InputView<byte>, InputQuestionParameters<byte>>()
				.Include<InputView<byte?>, InputQuestionParameters<byte?>>()
				.Include<InputView<IEnumerable<byte>>, InputQuestionParameters<IEnumerable<byte>>>()
				.Include<InputView<IEnumerable<byte?>>, InputQuestionParameters<IEnumerable<byte?>>>()
				.Include<InputView<ICollection<byte>>, InputQuestionParameters<ICollection<byte>>>()
				.Include<InputView<ICollection<byte?>>, InputQuestionParameters<ICollection<byte?>>>()
				.Include<InputView<IList<byte>>, InputQuestionParameters<IList<byte>>>()
				.Include<InputView<IList<byte?>>, InputQuestionParameters<IList<byte?>>>()
				.Include<InputView<short>, InputQuestionParameters<short>>()
				.Include<InputView<short?>, InputQuestionParameters<short?>>()
				.Include<InputView<IEnumerable<short>>, InputQuestionParameters<IEnumerable<short>>>()
				.Include<InputView<IEnumerable<short?>>, InputQuestionParameters<IEnumerable<short?>>>()
				.Include<InputView<ICollection<short>>, InputQuestionParameters<ICollection<short>>>()
				.Include<InputView<ICollection<short?>>, InputQuestionParameters<ICollection<short?>>>()
				.Include<InputView<IList<short>>, InputQuestionParameters<IList<short>>>()
				.Include<InputView<IList<short?>>, InputQuestionParameters<IList<short?>>>()
				.Include<InputView<int>, InputQuestionParameters<int>>()
				.Include<InputView<int?>, InputQuestionParameters<int?>>()
				.Include<InputView<IEnumerable<int>>, InputQuestionParameters<IEnumerable<int>>>()
				.Include<InputView<IEnumerable<int?>>, InputQuestionParameters<IEnumerable<int?>>>()
				.Include<InputView<ICollection<int>>, InputQuestionParameters<ICollection<int>>>()
				.Include<InputView<ICollection<int?>>, InputQuestionParameters<ICollection<int?>>>()
				.Include<InputView<IList<int>>, InputQuestionParameters<IList<int>>>()
				.Include<InputView<IList<int?>>, InputQuestionParameters<IList<int?>>>()
				.Include<InputView<long>, InputQuestionParameters<long>>()
				.Include<InputView<long?>, InputQuestionParameters<long?>>()
				.Include<InputView<IEnumerable<long>>, InputQuestionParameters<IEnumerable<long>>>()
				.Include<InputView<IEnumerable<long?>>, InputQuestionParameters<IEnumerable<long?>>>()
				.Include<InputView<ICollection<long>>, InputQuestionParameters<ICollection<long>>>()
				.Include<InputView<ICollection<long?>>, InputQuestionParameters<ICollection<long?>>>()
				.Include<InputView<IList<long>>, InputQuestionParameters<IList<long>>>()
				.Include<InputView<IList<long?>>, InputQuestionParameters<IList<long?>>>()
				.Include<InputView<float>, InputQuestionParameters<float>>()
				.Include<InputView<float?>, InputQuestionParameters<float?>>()
				.Include<InputView<IEnumerable<float>>, InputQuestionParameters<IEnumerable<float>>>()
				.Include<InputView<IEnumerable<float?>>, InputQuestionParameters<IEnumerable<float?>>>()
				.Include<InputView<ICollection<float>>, InputQuestionParameters<ICollection<float>>>()
				.Include<InputView<ICollection<float?>>, InputQuestionParameters<ICollection<float?>>>()
				.Include<InputView<IList<float>>, InputQuestionParameters<IList<float>>>()
				.Include<InputView<IList<float?>>, InputQuestionParameters<IList<float?>>>()
				.Include<InputView<double>, InputQuestionParameters<double>>()
				.Include<InputView<double?>, InputQuestionParameters<double?>>()
				.Include<InputView<IEnumerable<double>>, InputQuestionParameters<IEnumerable<double>>>()
				.Include<InputView<IEnumerable<double?>>, InputQuestionParameters<IEnumerable<double?>>>()
				.Include<InputView<ICollection<double>>, InputQuestionParameters<ICollection<double>>>()
				.Include<InputView<ICollection<double?>>, InputQuestionParameters<ICollection<double?>>>()
				.Include<InputView<IList<double>>, InputQuestionParameters<IList<double>>>()
				.Include<InputView<IList<double?>>, InputQuestionParameters<IList<double?>>>()
				.Include<InputView<char>, InputQuestionParameters<char>>()
				.Include<InputView<char?>, InputQuestionParameters<char?>>()
				.Include<InputView<IEnumerable<char>>, InputQuestionParameters<IEnumerable<char>>>()
				.Include<InputView<IEnumerable<char?>>, InputQuestionParameters<IEnumerable<char?>>>()
				.Include<InputView<ICollection<char>>, InputQuestionParameters<ICollection<char>>>()
				.Include<InputView<ICollection<char?>>, InputQuestionParameters<ICollection<char?>>>()
				.Include<InputView<IList<char>>, InputQuestionParameters<IList<char>>>()
				.Include<InputView<IList<char?>>, InputQuestionParameters<IList<char?>>>()
				.Include<InputView<sbyte>, InputQuestionParameters<sbyte>>()
				.Include<InputView<sbyte?>, InputQuestionParameters<sbyte?>>()
				.Include<InputView<IEnumerable<sbyte>>, InputQuestionParameters<IEnumerable<sbyte>>>()
				.Include<InputView<IEnumerable<sbyte?>>, InputQuestionParameters<IEnumerable<sbyte?>>>()
				.Include<InputView<ICollection<sbyte>>, InputQuestionParameters<ICollection<sbyte>>>()
				.Include<InputView<ICollection<sbyte?>>, InputQuestionParameters<ICollection<sbyte?>>>()
				.Include<InputView<IList<sbyte>>, InputQuestionParameters<IList<sbyte>>>()
				.Include<InputView<IList<sbyte?>>, InputQuestionParameters<IList<sbyte?>>>()
				.Include<InputView<ushort>, InputQuestionParameters<ushort>>()
				.Include<InputView<ushort?>, InputQuestionParameters<ushort?>>()
				.Include<InputView<IEnumerable<ushort>>, InputQuestionParameters<IEnumerable<ushort>>>()
				.Include<InputView<IEnumerable<ushort?>>, InputQuestionParameters<IEnumerable<ushort?>>>()
				.Include<InputView<ICollection<ushort>>, InputQuestionParameters<ICollection<ushort>>>()
				.Include<InputView<ICollection<ushort?>>, InputQuestionParameters<ICollection<ushort?>>>()
				.Include<InputView<IList<ushort>>, InputQuestionParameters<IList<ushort>>>()
				.Include<InputView<IList<ushort?>>, InputQuestionParameters<IList<ushort?>>>()
				.Include<InputView<uint>, InputQuestionParameters<uint>>()
				.Include<InputView<uint?>, InputQuestionParameters<uint?>>()
				.Include<InputView<IEnumerable<uint>>, InputQuestionParameters<IEnumerable<uint>>>()
				.Include<InputView<IEnumerable<uint?>>, InputQuestionParameters<IEnumerable<uint?>>>()
				.Include<InputView<ICollection<uint>>, InputQuestionParameters<ICollection<uint>>>()
				.Include<InputView<ICollection<uint?>>, InputQuestionParameters<ICollection<uint?>>>()
				.Include<InputView<IList<uint>>, InputQuestionParameters<IList<uint>>>()
				.Include<InputView<IList<uint?>>, InputQuestionParameters<IList<uint?>>>()
				.Include<InputView<ulong>, InputQuestionParameters<ulong>>()
				.Include<InputView<ulong?>, InputQuestionParameters<ulong?>>()
				.Include<InputView<IEnumerable<ulong>>, InputQuestionParameters<IEnumerable<ulong>>>()
				.Include<InputView<IEnumerable<ulong?>>, InputQuestionParameters<IEnumerable<ulong?>>>()
				.Include<InputView<ICollection<ulong>>, InputQuestionParameters<ICollection<ulong>>>()
				.Include<InputView<ICollection<ulong?>>, InputQuestionParameters<ICollection<ulong?>>>()
				.Include<InputView<IList<ulong>>, InputQuestionParameters<IList<ulong>>>()
				.Include<InputView<IList<ulong?>>, InputQuestionParameters<IList<ulong?>>>()
				.Include<InputView<string>, InputQuestionParameters<string>>()
				.Include<InputView<IEnumerable<string>>, InputQuestionParameters<IEnumerable<string>>>()
				.Include<InputView<ICollection<string>>, InputQuestionParameters<ICollection<string>>>()
				.Include<InputView<IList<string>>, InputQuestionParameters<IList<string>>>()
            .ReverseMap()
				.Include<InputQuestionParameters<bool>, InputView<bool>>()
				.Include<InputQuestionParameters<bool?>, InputView<bool?>>()
				.Include<InputQuestionParameters<IEnumerable<bool>>, InputView<IEnumerable<bool>>>()
				.Include<InputQuestionParameters<IEnumerable<bool?>>, InputView<IEnumerable<bool?>>>()
				.Include<InputQuestionParameters<ICollection<bool>>, InputView<ICollection<bool>>>()
				.Include<InputQuestionParameters<ICollection<bool?>>, InputView<ICollection<bool?>>>()
				.Include<InputQuestionParameters<IList<bool>>, InputView<IList<bool>>>()
				.Include<InputQuestionParameters<IList<bool?>>, InputView<IList<bool?>>>()
				.Include<InputQuestionParameters<DateTime>, InputView<DateTime>>()
				.Include<InputQuestionParameters<DateTime?>, InputView<DateTime?>>()
				.Include<InputQuestionParameters<IEnumerable<DateTime>>, InputView<IEnumerable<DateTime>>>()
				.Include<InputQuestionParameters<IEnumerable<DateTime?>>, InputView<IEnumerable<DateTime?>>>()
				.Include<InputQuestionParameters<ICollection<DateTime>>, InputView<ICollection<DateTime>>>()
				.Include<InputQuestionParameters<ICollection<DateTime?>>, InputView<ICollection<DateTime?>>>()
				.Include<InputQuestionParameters<IList<DateTime>>, InputView<IList<DateTime>>>()
				.Include<InputQuestionParameters<IList<DateTime?>>, InputView<IList<DateTime?>>>()
				.Include<InputQuestionParameters<TimeSpan>, InputView<TimeSpan>>()
				.Include<InputQuestionParameters<TimeSpan?>, InputView<TimeSpan?>>()
				.Include<InputQuestionParameters<IEnumerable<TimeSpan>>, InputView<IEnumerable<TimeSpan>>>()
				.Include<InputQuestionParameters<IEnumerable<TimeSpan?>>, InputView<IEnumerable<TimeSpan?>>>()
				.Include<InputQuestionParameters<ICollection<TimeSpan>>, InputView<ICollection<TimeSpan>>>()
				.Include<InputQuestionParameters<ICollection<TimeSpan?>>, InputView<ICollection<TimeSpan?>>>()
				.Include<InputQuestionParameters<IList<TimeSpan>>, InputView<IList<TimeSpan>>>()
				.Include<InputQuestionParameters<IList<TimeSpan?>>, InputView<IList<TimeSpan?>>>()
				.Include<InputQuestionParameters<Guid>, InputView<Guid>>()
				.Include<InputQuestionParameters<Guid?>, InputView<Guid?>>()
				.Include<InputQuestionParameters<IEnumerable<Guid>>, InputView<IEnumerable<Guid>>>()
				.Include<InputQuestionParameters<IEnumerable<Guid?>>, InputView<IEnumerable<Guid?>>>()
				.Include<InputQuestionParameters<ICollection<Guid>>, InputView<ICollection<Guid>>>()
				.Include<InputQuestionParameters<ICollection<Guid?>>, InputView<ICollection<Guid?>>>()
				.Include<InputQuestionParameters<IList<Guid>>, InputView<IList<Guid>>>()
				.Include<InputQuestionParameters<IList<Guid?>>, InputView<IList<Guid?>>>()
				.Include<InputQuestionParameters<decimal>, InputView<decimal>>()
				.Include<InputQuestionParameters<decimal?>, InputView<decimal?>>()
				.Include<InputQuestionParameters<IEnumerable<decimal>>, InputView<IEnumerable<decimal>>>()
				.Include<InputQuestionParameters<IEnumerable<decimal?>>, InputView<IEnumerable<decimal?>>>()
				.Include<InputQuestionParameters<ICollection<decimal>>, InputView<ICollection<decimal>>>()
				.Include<InputQuestionParameters<ICollection<decimal?>>, InputView<ICollection<decimal?>>>()
				.Include<InputQuestionParameters<IList<decimal>>, InputView<IList<decimal>>>()
				.Include<InputQuestionParameters<IList<decimal?>>, InputView<IList<decimal?>>>()
				.Include<InputQuestionParameters<byte>, InputView<byte>>()
				.Include<InputQuestionParameters<byte?>, InputView<byte?>>()
				.Include<InputQuestionParameters<IEnumerable<byte>>, InputView<IEnumerable<byte>>>()
				.Include<InputQuestionParameters<IEnumerable<byte?>>, InputView<IEnumerable<byte?>>>()
				.Include<InputQuestionParameters<ICollection<byte>>, InputView<ICollection<byte>>>()
				.Include<InputQuestionParameters<ICollection<byte?>>, InputView<ICollection<byte?>>>()
				.Include<InputQuestionParameters<IList<byte>>, InputView<IList<byte>>>()
				.Include<InputQuestionParameters<IList<byte?>>, InputView<IList<byte?>>>()
				.Include<InputQuestionParameters<short>, InputView<short>>()
				.Include<InputQuestionParameters<short?>, InputView<short?>>()
				.Include<InputQuestionParameters<IEnumerable<short>>, InputView<IEnumerable<short>>>()
				.Include<InputQuestionParameters<IEnumerable<short?>>, InputView<IEnumerable<short?>>>()
				.Include<InputQuestionParameters<ICollection<short>>, InputView<ICollection<short>>>()
				.Include<InputQuestionParameters<ICollection<short?>>, InputView<ICollection<short?>>>()
				.Include<InputQuestionParameters<IList<short>>, InputView<IList<short>>>()
				.Include<InputQuestionParameters<IList<short?>>, InputView<IList<short?>>>()
				.Include<InputQuestionParameters<int>, InputView<int>>()
				.Include<InputQuestionParameters<int?>, InputView<int?>>()
				.Include<InputQuestionParameters<IEnumerable<int>>, InputView<IEnumerable<int>>>()
				.Include<InputQuestionParameters<IEnumerable<int?>>, InputView<IEnumerable<int?>>>()
				.Include<InputQuestionParameters<ICollection<int>>, InputView<ICollection<int>>>()
				.Include<InputQuestionParameters<ICollection<int?>>, InputView<ICollection<int?>>>()
				.Include<InputQuestionParameters<IList<int>>, InputView<IList<int>>>()
				.Include<InputQuestionParameters<IList<int?>>, InputView<IList<int?>>>()
				.Include<InputQuestionParameters<long>, InputView<long>>()
				.Include<InputQuestionParameters<long?>, InputView<long?>>()
				.Include<InputQuestionParameters<IEnumerable<long>>, InputView<IEnumerable<long>>>()
				.Include<InputQuestionParameters<IEnumerable<long?>>, InputView<IEnumerable<long?>>>()
				.Include<InputQuestionParameters<ICollection<long>>, InputView<ICollection<long>>>()
				.Include<InputQuestionParameters<ICollection<long?>>, InputView<ICollection<long?>>>()
				.Include<InputQuestionParameters<IList<long>>, InputView<IList<long>>>()
				.Include<InputQuestionParameters<IList<long?>>, InputView<IList<long?>>>()
				.Include<InputQuestionParameters<float>, InputView<float>>()
				.Include<InputQuestionParameters<float?>, InputView<float?>>()
				.Include<InputQuestionParameters<IEnumerable<float>>, InputView<IEnumerable<float>>>()
				.Include<InputQuestionParameters<IEnumerable<float?>>, InputView<IEnumerable<float?>>>()
				.Include<InputQuestionParameters<ICollection<float>>, InputView<ICollection<float>>>()
				.Include<InputQuestionParameters<ICollection<float?>>, InputView<ICollection<float?>>>()
				.Include<InputQuestionParameters<IList<float>>, InputView<IList<float>>>()
				.Include<InputQuestionParameters<IList<float?>>, InputView<IList<float?>>>()
				.Include<InputQuestionParameters<double>, InputView<double>>()
				.Include<InputQuestionParameters<double?>, InputView<double?>>()
				.Include<InputQuestionParameters<IEnumerable<double>>, InputView<IEnumerable<double>>>()
				.Include<InputQuestionParameters<IEnumerable<double?>>, InputView<IEnumerable<double?>>>()
				.Include<InputQuestionParameters<ICollection<double>>, InputView<ICollection<double>>>()
				.Include<InputQuestionParameters<ICollection<double?>>, InputView<ICollection<double?>>>()
				.Include<InputQuestionParameters<IList<double>>, InputView<IList<double>>>()
				.Include<InputQuestionParameters<IList<double?>>, InputView<IList<double?>>>()
				.Include<InputQuestionParameters<char>, InputView<char>>()
				.Include<InputQuestionParameters<char?>, InputView<char?>>()
				.Include<InputQuestionParameters<IEnumerable<char>>, InputView<IEnumerable<char>>>()
				.Include<InputQuestionParameters<IEnumerable<char?>>, InputView<IEnumerable<char?>>>()
				.Include<InputQuestionParameters<ICollection<char>>, InputView<ICollection<char>>>()
				.Include<InputQuestionParameters<ICollection<char?>>, InputView<ICollection<char?>>>()
				.Include<InputQuestionParameters<IList<char>>, InputView<IList<char>>>()
				.Include<InputQuestionParameters<IList<char?>>, InputView<IList<char?>>>()
				.Include<InputQuestionParameters<sbyte>, InputView<sbyte>>()
				.Include<InputQuestionParameters<sbyte?>, InputView<sbyte?>>()
				.Include<InputQuestionParameters<IEnumerable<sbyte>>, InputView<IEnumerable<sbyte>>>()
				.Include<InputQuestionParameters<IEnumerable<sbyte?>>, InputView<IEnumerable<sbyte?>>>()
				.Include<InputQuestionParameters<ICollection<sbyte>>, InputView<ICollection<sbyte>>>()
				.Include<InputQuestionParameters<ICollection<sbyte?>>, InputView<ICollection<sbyte?>>>()
				.Include<InputQuestionParameters<IList<sbyte>>, InputView<IList<sbyte>>>()
				.Include<InputQuestionParameters<IList<sbyte?>>, InputView<IList<sbyte?>>>()
				.Include<InputQuestionParameters<ushort>, InputView<ushort>>()
				.Include<InputQuestionParameters<ushort?>, InputView<ushort?>>()
				.Include<InputQuestionParameters<IEnumerable<ushort>>, InputView<IEnumerable<ushort>>>()
				.Include<InputQuestionParameters<IEnumerable<ushort?>>, InputView<IEnumerable<ushort?>>>()
				.Include<InputQuestionParameters<ICollection<ushort>>, InputView<ICollection<ushort>>>()
				.Include<InputQuestionParameters<ICollection<ushort?>>, InputView<ICollection<ushort?>>>()
				.Include<InputQuestionParameters<IList<ushort>>, InputView<IList<ushort>>>()
				.Include<InputQuestionParameters<IList<ushort?>>, InputView<IList<ushort?>>>()
				.Include<InputQuestionParameters<uint>, InputView<uint>>()
				.Include<InputQuestionParameters<uint?>, InputView<uint?>>()
				.Include<InputQuestionParameters<IEnumerable<uint>>, InputView<IEnumerable<uint>>>()
				.Include<InputQuestionParameters<IEnumerable<uint?>>, InputView<IEnumerable<uint?>>>()
				.Include<InputQuestionParameters<ICollection<uint>>, InputView<ICollection<uint>>>()
				.Include<InputQuestionParameters<ICollection<uint?>>, InputView<ICollection<uint?>>>()
				.Include<InputQuestionParameters<IList<uint>>, InputView<IList<uint>>>()
				.Include<InputQuestionParameters<IList<uint?>>, InputView<IList<uint?>>>()
				.Include<InputQuestionParameters<ulong>, InputView<ulong>>()
				.Include<InputQuestionParameters<ulong?>, InputView<ulong?>>()
				.Include<InputQuestionParameters<IEnumerable<ulong>>, InputView<IEnumerable<ulong>>>()
				.Include<InputQuestionParameters<IEnumerable<ulong?>>, InputView<IEnumerable<ulong?>>>()
				.Include<InputQuestionParameters<ICollection<ulong>>, InputView<ICollection<ulong>>>()
				.Include<InputQuestionParameters<ICollection<ulong?>>, InputView<ICollection<ulong?>>>()
				.Include<InputQuestionParameters<IList<ulong>>, InputView<IList<ulong>>>()
				.Include<InputQuestionParameters<IList<ulong?>>, InputView<IList<ulong?>>>()
				.Include<InputQuestionParameters<string>, InputView<string>>()
				.Include<InputQuestionParameters<IEnumerable<string>>, InputView<IEnumerable<string>>>()
				.Include<InputQuestionParameters<ICollection<string>>, InputView<ICollection<string>>>()
				.Include<InputQuestionParameters<IList<string>>, InputView<IList<string>>>();
        }
    }
}