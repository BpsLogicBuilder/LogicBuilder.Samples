﻿using Enrollment.Common.Configuration.ExpansionDescriptors;
using Enrollment.Common.Configuration.ItemFilter;
using Enrollment.Data.Entities;
using Enrollment.Domain.Entities;
using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.Bindings;
using Enrollment.Forms.Configuration.SearchForm;
using LogicBuilder.Expressions.Utils.Strutures;
using System.Collections.Generic;
using System.Linq;

namespace Enrollment.XPlatform.Tests
{
    internal static class SearchFormDescriptors
    {
        internal static SearchFormSettingsDescriptor SudentsForm = new()
        {
            Title = "About",
            ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
            LoadingIndicatorText = "Loading ...",
            ItemTemplateName = "TextDetailTemplate",
            Bindings = new List<ItemBindingDescriptor>
            {
                new TextItemBindingDescriptor
                {
                    Name = "Text",
                    Property = "Value",
                    Title = "Value",
                    StringFormat = "City: {0}",
                    TextTemplate = new TextFieldTemplateDescriptor
                    {
                        TemplateName = "TextTemplate"
                    }
                }
            }.ToDictionary(i => i.Name),
            ItemFilterGroup = new ItemFilterGroupDescriptor
            {
                Logic = "and",
                Filters = new List<ItemFilterDescriptorBase>
                {
                    new MemberSourceFilterDescriptor
                    {
                        Field = "UserId",
                        Operator = "eq",
                        MemberSource = "UserId"
                    }
                }
            },
            SearchFilterGroup = new SearchFilterGroupDescriptor
            {
                Filters = new List<SearchFilterDescriptorBase>
                {
                    new SearchFilterDescriptor
                    {
                        Field = "FirstName"
                    }
                }
            },
            SortCollection = new SortCollectionDescriptor
            {
                SortDescriptions = new List<SortDescriptionDescriptor>
                {
                    new SortDescriptionDescriptor { PropertyName = "FullName", SortDirection = ListSortDirection.Descending },
                }
            },
            RequestDetails = new RequestDetailsDescriptor
            {
                DataSourceUrl = "api/List/GetList",
                ModelType = typeof(PersonalModel).AssemblyQualifiedName,
                DataType = typeof(Personal).AssemblyQualifiedName,
                ModelReturnType = typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName,
                DataReturnType = typeof(IQueryable<LookUps>).AssemblyQualifiedName,
            }
        };
    }
}