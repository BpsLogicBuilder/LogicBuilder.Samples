using Enrollment.Common.Configuration.ExpressionDescriptors;
using Enrollment.Data.Entities;
using Enrollment.Domain.Entities;
using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.Bindings;
using Enrollment.Forms.Configuration.ListForm;
using System.Collections.Generic;
using System.Linq;

namespace Enrollment.XPlatform.Tests
{
    internal static class ListFormDescriptors
    {
        internal static ListFormSettingsDescriptor AboutForm = new()
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
            FieldsSelector = new SelectorLambdaOperatorDescriptor
            {
                Selector = new SelectOperatorDescriptor
                {
                    SourceOperand = new OrderByOperatorDescriptor
                    {
                        SourceOperand = new GroupByOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor
                            {
                                ParameterName = "q"
                            },
                            SelectorBody = new MemberSelectorOperatorDescriptor
                            {
                                MemberFullName = "City",
                                SourceOperand = new ParameterOperatorDescriptor
                                {
                                    ParameterName = "item"
                                }
                            },
                            SelectorParameterName = "item"
                        },
                        SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                        SelectorBody = new MemberSelectorOperatorDescriptor
                        {
                            MemberFullName = "Key",
                            SourceOperand = new ParameterOperatorDescriptor
                            {
                                ParameterName = "group"
                            }
                        },
                        SelectorParameterName = "group"
                    },
                    SelectorBody = new MemberInitOperatorDescriptor
                    {
                        MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                        {
                            ["DateTimeValue"] = new MemberSelectorOperatorDescriptor
                            {
                                MemberFullName = "Key",
                                SourceOperand = new ParameterOperatorDescriptor
                                {
                                    ParameterName = "sel"
                                }
                            },
                            ["NumericValue"] = new ConvertOperatorDescriptor
                            {
                                SourceOperand = new CountOperatorDescriptor
                                {
                                    SourceOperand = new AsQueryableOperatorDescriptor()
                                    {
                                        SourceOperand = new ParameterOperatorDescriptor
                                        {
                                            ParameterName = "sel"
                                        }
                                    }
                                },
                                Type = typeof(double?).FullName
                            }
                        },
                        NewType = typeof(LookUpsModel).AssemblyQualifiedName
                    },
                    SelectorParameterName = "sel"
                },
                SourceElementType = typeof(IQueryable<PersonalModel>).AssemblyQualifiedName,
                ParameterName = "$it",
                BodyType = typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName
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
