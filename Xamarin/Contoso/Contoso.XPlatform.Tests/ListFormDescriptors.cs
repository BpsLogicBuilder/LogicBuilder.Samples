using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.Bindings;
using Contoso.Forms.Configuration.ListForm;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.XPlatform.Tests
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
                    Property = "DateTimeValue",
                    Title = "DateTimeValue",
                    StringFormat = "Enrollment Date: {0:MM/dd/yyyy}",
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
                                MemberFullName = "EnrollmentDate",
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
                SourceElementType = typeof(IQueryable<StudentModel>).AssemblyQualifiedName,
                ParameterName = "$it",
                BodyType = typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName
            },
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
