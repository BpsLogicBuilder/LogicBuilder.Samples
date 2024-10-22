﻿using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.Bindings;
using Contoso.Forms.Configuration.SearchForm;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.XPlatform.Tests
{
    internal static class SearchFormDescriptors
    {
        internal static SearchFormSettingsDescriptor SudentsForm = new()
        {
            Title = "About",
            ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
            LoadingIndicatorText = "Loading ...",
            ItemTemplateName = "TextDetailTemplate",
            CreatePagingSelectorFlowName = "paging_selector_students",
            Bindings = new List<ItemBindingDescriptor>
            {
                new TextItemBindingDescriptor
                {
                    Name = "Text",
                    Property = "DateTimeValue",
                    Title = "DateTimeValue",
                    StringFormat = "Enrollment Date: {0:MM/dd/yyyy}",
                    TextTemplate = new TextFieldTemplateDescriptor
                    {
                        TemplateName = "TextTemplate"
                    }
                }
            }.ToDictionary(i => i.Name),
            RequestDetails = new RequestDetailsDescriptor
            {
                DataSourceUrl = "api/List/GetList",
                ModelType = typeof(StudentModel).AssemblyQualifiedName,
                DataType = typeof(Student).AssemblyQualifiedName,
                ModelReturnType = typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName,
                DataReturnType = typeof(IQueryable<LookUps>).AssemblyQualifiedName,
            }
        };
    }
}
