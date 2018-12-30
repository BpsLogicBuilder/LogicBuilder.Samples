using AutoMapper;
using Contoso.Domain.Entities;
using Contoso.Kendo.ViewModels;
using Kendo.Mvc.Infrastructure;
using System;
using System.Collections.Generic;

namespace Contoso.Kemdo.AutoMapperProfiles
{
    public class GroupingProfile : Profile
    {
        public GroupingProfile()
        {
            CreateMap<AggregateFunctionsGroup, AggregateFunctionsGroupModel<StudentModel>>()
                .ConstructUsing(Helpers.Converter<StudentModel>)
                .ForMember(d => d.Items, opt => opt.Ignore());

            CreateMap<AggregateFunctionsGroup, AggregateFunctionsGroupModel<CourseModel>>()
                .ConstructUsing(Helpers.Converter<CourseModel>)
                .ForMember(d => d.Items, opt => opt.Ignore());

            CreateMap<AggregateFunctionsGroup, AggregateFunctionsGroupModel<DepartmentModel>>()
                .ConstructUsing(Helpers.Converter<DepartmentModel>)
                .ForMember(d => d.Items, opt => opt.Ignore());

            CreateMap<AggregateFunctionsGroup, AggregateFunctionsGroupModel<InstructorModel>>()
                .ConstructUsing(Helpers.Converter<InstructorModel>)
                .ForMember(d => d.Items, opt => opt.Ignore());

            CreateMap<AggregateFunctionsGroup, AggregateFunctionsGroupModel<CourseAssignmentModel>>()
                .ConstructUsing(Helpers.Converter<CourseAssignmentModel>)
                .ForMember(d => d.Items, opt => opt.Ignore());

            CreateMap<AggregateFunctionsGroup, AggregateFunctionsGroupModel<EnrollmentModel>>()
                .ConstructUsing(Helpers.Converter<EnrollmentModel>)
                .ForMember(d => d.Items, opt => opt.Ignore());
        }
    }

    internal static class Helpers
    {//AggregateFunctionsGroup.Items are a list of TData items.
        public static AggregateFunctionsGroupModel<T> Converter<T>(AggregateFunctionsGroup src, ResolutionContext context)
        {
            System.Collections.IEnumerable items = null;
            if (src.Items != null && src.Items.GetType().UnderlyingElementTypeIsFunctionsGroup())
                items = context.Mapper.Map<IEnumerable<AggregateFunctionsGroupModel<T>>>(src.Items);
            else
                items = context.Mapper.Map<IEnumerable<T>>(src.Items);

            return new AggregateFunctionsGroupModel<T> { Items = items };
        }

        private static bool UnderlyingElementTypeIsFunctionsGroup(this Type type)
        {
            Type[] genericArguments;
            if (!type.IsGenericType || (genericArguments = type.GetGenericArguments()).Length != 1)
                return false;

            return genericArguments[0] == typeof(AggregateFunctionsGroup);
        }
    }
}
