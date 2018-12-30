using AutoMapper;
using Enrollment.Domain.Entities;
using Enrollment.Kendo.ViewModels;
using Kendo.Mvc.Infrastructure;
using System;
using System.Collections.Generic;

namespace Enrollment.Kemdo.AutoMapperProfiles
{
    public class GroupingProfile : Profile
    {
        public GroupingProfile()
        {
            CreateMap<AggregateFunctionsGroup, AggregateFunctionsGroupModel<LookUpsModel>>()
                .ConstructUsing(Helpers.Converter<LookUpsModel>)
                .ForMember(d => d.Items, opt => opt.Ignore());

            CreateMap<AggregateFunctionsGroup, AggregateFunctionsGroupModel<UserModel>>()
                .ConstructUsing(Helpers.Converter<UserModel>)
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
