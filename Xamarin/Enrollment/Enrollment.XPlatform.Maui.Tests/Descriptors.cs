using Enrollment.Common.Configuration.ExpressionDescriptors;
using Enrollment.Data.Entities;
using Enrollment.Domain.Entities;
using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.Bindings;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.Forms.Configuration.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Enrollment.XPlatform.Maui.Tests
{
    internal static class Descriptors
    {
        internal static DataFormSettingsDescriptor ResidencyForm = new()
        {
            Title = "Residency",
            RequestDetails = new FormRequestDetailsDescriptor
            {
                GetUrl = "/Residency/GetSingle"
            },
            ValidationMessages = new Dictionary<string, List<ValidationRuleDescriptor>>
            {

            },
            FieldSettings = new List<FormItemSettingsDescriptor>
            {
                new FormControlSettingsDescriptor
                {
                    Field = "UserId",
                    Type = "System.Int32",
                    Title = "User ID",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "HiddenTemplate" },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = 0
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "CitizenshipStatus",
                    Type = "System.String",
                    Title = "Citizenship Status",
                    Placeholder = "Citizenship Status (required)",
                    DropDownTemplate = new DropDownTemplateDescriptor
                    {
                        TemplateName = "PickerTemplate",
                        TitleText = "Select Citizenship Status:",
                        LoadingIndicatorText = "Loading ...",
                        TextField = "Text",
                        ValueField = "Value",
                        TextAndValueSelector = new SelectorLambdaOperatorDescriptor
                        {
                            Selector = new SelectOperatorDescriptor
                            {
                                SourceOperand = new OrderByOperatorDescriptor
                                {
                                    SourceOperand = new WhereOperatorDescriptor
                                    {
                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "$it" },
                                        FilterBody = new EqualsBinaryOperatorDescriptor
                                        {
                                            Left = new MemberSelectorOperatorDescriptor
                                            {
                                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                MemberFullName = "ListName"
                                            },
                                            Right = new ConstantOperatorDescriptor
                                            {
                                                ConstantValue = "citizenshipstatus",
                                                Type = typeof(string).AssemblyQualifiedName
                                            }
                                        },
                                        FilterParameterName = "l"
                                    },
                                    SelectorBody = new MemberSelectorOperatorDescriptor
                                    {
                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                        MemberFullName = "Text"
                                    },
                                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                                    SelectorParameterName = "l"
                                },
                                SelectorBody = new MemberInitOperatorDescriptor
                                {
                                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                    {
                                        ["Value"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "Value"
                                        },
                                        ["Text"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "Text"
                                        }
                                    },
                                    NewType = typeof(LookUpsModel).AssemblyQualifiedName
                                },
                                SelectorParameterName = "s"
                            },
                            SourceElementType = typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName,
                            ParameterName = "$it",
                            BodyType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                            DataType = typeof(LookUps).AssemblyQualifiedName,
                            ModelReturnType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName,
                            DataReturnType = typeof(IEnumerable<LookUps>).AssemblyQualifiedName
                        }
                    },
                    UpdateOnlyTextTemplate = new TextFieldTemplateDescriptor { TemplateName = "LabelTemplate" },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = "",
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "ResidentState",
                    Type = "System.String",
                    Title = "Resident State",
                    Placeholder = "Resident State (required)",
                    DropDownTemplate = new DropDownTemplateDescriptor
                    {
                        TemplateName = "PickerTemplate",
                        TitleText = "Select Citizenship Status:",
                        LoadingIndicatorText = "Loading ...",
                        TextField = "Text",
                        ValueField = "Value",
                        TextAndValueSelector = new SelectorLambdaOperatorDescriptor
                        {
                            Selector = new SelectOperatorDescriptor
                            {
                                SourceOperand = new OrderByOperatorDescriptor
                                {
                                    SourceOperand = new WhereOperatorDescriptor
                                    {
                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "$it" },
                                        FilterBody = new EqualsBinaryOperatorDescriptor
                                        {
                                            Left = new MemberSelectorOperatorDescriptor
                                            {
                                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                MemberFullName = "states"
                                            },
                                            Right = new ConstantOperatorDescriptor
                                            {
                                                ConstantValue = "citizenshipstatus",
                                                Type = typeof(string).AssemblyQualifiedName
                                            }
                                        },
                                        FilterParameterName = "l"
                                    },
                                    SelectorBody = new MemberSelectorOperatorDescriptor
                                    {
                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                        MemberFullName = "Text"
                                    },
                                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                                    SelectorParameterName = "l"
                                },
                                SelectorBody = new MemberInitOperatorDescriptor
                                {
                                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                    {
                                        ["Value"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "Value"
                                        },
                                        ["Text"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "Text"
                                        }
                                    },
                                    NewType = typeof(LookUpsModel).AssemblyQualifiedName
                                },
                                SelectorParameterName = "s"
                            },
                            SourceElementType = typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName,
                            ParameterName = "$it",
                            BodyType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                            DataType = typeof(LookUps).AssemblyQualifiedName,
                            ModelReturnType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName,
                            DataReturnType = typeof(IEnumerable<LookUps>).AssemblyQualifiedName
                        }
                    },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = ""
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "HasValidDriversLicense",
                    Type = "System.Boolean",
                    Title = "Has Valid Drivers License",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "SwitchTemplate" },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = false
                    }
                },
                new MultiSelectFormControlSettingsDescriptor
                {
                    KeyFields = new List<string> { "State" },
                    Field = "StatesLivedIn",
                    Title ="States Lived In",
                    Placeholder = "Select States Lived In ...",
                    Type = typeof(ICollection<StateLivedInModel>).AssemblyQualifiedName,
                    MultiSelectTemplate =  new MultiSelectTemplateDescriptor
                    {
                        TemplateName = "MultiSelectTemplate",
                        PlaceholderText = "(States Lived In)",
                        LoadingIndicatorText = "Loading ...",
                        ModelType = typeof(StateLivedInModel).AssemblyQualifiedName,
                        TextField = "Text",
                        ValueField = "Value",
                        TextAndValueSelector = new SelectorLambdaOperatorDescriptor
                        {
                            Selector = new SelectOperatorDescriptor
                            {
                                SourceOperand = new OrderByOperatorDescriptor
                                {
                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "$it" },
                                    SelectorBody = new MemberSelectorOperatorDescriptor
                                    {
                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                                        MemberFullName = "Text"
                                    },
                                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                    SelectorParameterName = "d"
                                },
                                SelectorBody = new MemberInitOperatorDescriptor
                                {
                                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                    {
                                        ["State"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "Value"
                                        }
                                    },
                                    NewType = typeof(LookUpsModel).AssemblyQualifiedName
                                },
                                SelectorParameterName = "s"
                            },
                            SourceElementType = typeof(IQueryable<LookUps>).AssemblyQualifiedName,
                            ParameterName = "$it",
                            BodyType = typeof(IEnumerable<StateLivedInModel>).AssemblyQualifiedName
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                            DataType = typeof(LookUps).AssemblyQualifiedName,
                            ModelReturnType = typeof(IEnumerable<StateLivedInModel>).AssemblyQualifiedName,
                            DataReturnType = typeof(IEnumerable<StateLivedIn>).AssemblyQualifiedName
                        }
                    }
                }
            },
            ModelType = typeof(ResidencyModel).AssemblyQualifiedName
        };

        internal static DataFormSettingsDescriptor AcademicForm = new()
        {
            Title = "Academic",
            RequestDetails = new FormRequestDetailsDescriptor
            {
                GetUrl = "/Academic/GetSingle"
            },
            ValidationMessages = new Dictionary<string, List<ValidationRuleDescriptor>>
            {

            },
            FieldSettings = new List<FormItemSettingsDescriptor>
            {
                new FormControlSettingsDescriptor
                {
                    Field = "UserId",
                    Type = "System.Int32",
                    Title = "User ID",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "HiddenTemplate" },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = 0
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "LastHighSchoolLocation",
                    Type = "System.String",
                    Title = "Last High School Location",
                    Placeholder = "Last High School Location (required)",
                    DropDownTemplate = new DropDownTemplateDescriptor
                    {
                        TemplateName = "PickerTemplate",
                        TitleText = "Select Last High School Location:",
                        LoadingIndicatorText = "Loading ...",
                        TextField = "Text",
                        ValueField = "Value",
                        TextAndValueSelector = new SelectorLambdaOperatorDescriptor
                        {
                            Selector = new SelectOperatorDescriptor
                            {
                                SourceOperand = new OrderByOperatorDescriptor
                                {
                                    SourceOperand = new WhereOperatorDescriptor
                                    {
                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "$it" },
                                        FilterBody = new EqualsBinaryOperatorDescriptor
                                        {
                                            Left = new MemberSelectorOperatorDescriptor
                                            {
                                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                MemberFullName = "ListName"
                                            },
                                            Right = new ConstantOperatorDescriptor
                                            {
                                                ConstantValue = "highSchoolLocation",
                                                Type = typeof(string).AssemblyQualifiedName
                                            }
                                        },
                                        FilterParameterName = "l"
                                    },
                                    SelectorBody = new MemberSelectorOperatorDescriptor
                                    {
                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                        MemberFullName = "Text"
                                    },
                                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                                    SelectorParameterName = "l"
                                },
                                SelectorBody = new MemberInitOperatorDescriptor
                                {
                                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                    {
                                        ["Value"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "Value"
                                        },
                                        ["Text"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "Text"
                                        }
                                    },
                                    NewType = typeof(LookUpsModel).AssemblyQualifiedName
                                },
                                SelectorParameterName = "s"
                            },
                            SourceElementType = typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName,
                            ParameterName = "$it",
                            BodyType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                            DataType = typeof(LookUps).AssemblyQualifiedName,
                            ModelReturnType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName,
                            DataReturnType = typeof(IEnumerable<LookUps>).AssemblyQualifiedName
                        }
                    },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = ""
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "NcHighSchoolName",
                    Type = "System.String",
                    Title = "NC High School Name",
                    Placeholder = "NC High School Name (required)",
                    DropDownTemplate = new DropDownTemplateDescriptor
                    {
                        TemplateName = "PickerTemplate",
                        TitleText = "Select NC High School Name:",
                        LoadingIndicatorText = "Loading ...",
                        TextField = "Text",
                        ValueField = "Value",
                        TextAndValueSelector = new SelectorLambdaOperatorDescriptor
                        {
                            Selector = new SelectOperatorDescriptor
                            {
                                SourceOperand = new OrderByOperatorDescriptor
                                {
                                    SourceOperand = new WhereOperatorDescriptor
                                    {
                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "$it" },
                                        FilterBody = new EqualsBinaryOperatorDescriptor
                                        {
                                            Left = new MemberSelectorOperatorDescriptor
                                            {
                                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                MemberFullName = "ListName"
                                            },
                                            Right = new ConstantOperatorDescriptor
                                            {
                                                ConstantValue = "ncHighSchools",
                                                Type = typeof(string).AssemblyQualifiedName
                                            }
                                        },
                                        FilterParameterName = "l"
                                    },
                                    SelectorBody = new MemberSelectorOperatorDescriptor
                                    {
                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                        MemberFullName = "Text"
                                    },
                                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                                    SelectorParameterName = "l"
                                },
                                SelectorBody = new MemberInitOperatorDescriptor
                                {
                                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                    {
                                        ["Value"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "Value"
                                        },
                                        ["Text"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "Text"
                                        }
                                    },
                                    NewType = typeof(LookUpsModel).AssemblyQualifiedName
                                },
                                SelectorParameterName = "s"
                            },
                            SourceElementType = typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName,
                            ParameterName = "$it",
                            BodyType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                            DataType = typeof(LookUps).AssemblyQualifiedName,
                            ModelReturnType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName,
                            DataReturnType = typeof(IEnumerable<LookUps>).AssemblyQualifiedName
                        }
                    },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = ""
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "FromDate",
                    Type = "System.DateTime",
                    Title = "From Date",
                    Placeholder = "",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "DateTemplate" },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = new DateTime(1900, 1, 1)
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "ToDate",
                    Type = "System.DateTime",
                    Title = "To Date",
                    Placeholder = "",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "DateTemplate" },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = new DateTime(1900, 1, 1)
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "GraduationStatus",
                    Type = "System.String",
                    Title = "Graduation Status",
                    Placeholder = "Graduation Status(required)",
                    DropDownTemplate = new DropDownTemplateDescriptor
                    {
                        TemplateName = "PickerTemplate",
                        TitleText = "Select Graduation Status:",
                        LoadingIndicatorText = "Loading ...",
                        TextField = "Text",
                        ValueField = "Value",
                        TextAndValueSelector = new SelectorLambdaOperatorDescriptor
                        {
                            Selector = new SelectOperatorDescriptor
                            {
                                SourceOperand = new OrderByOperatorDescriptor
                                {
                                    SourceOperand = new WhereOperatorDescriptor
                                    {
                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "$it" },
                                        FilterBody = new EqualsBinaryOperatorDescriptor
                                        {
                                            Left = new MemberSelectorOperatorDescriptor
                                            {
                                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                MemberFullName = "ListName"
                                            },
                                            Right = new ConstantOperatorDescriptor
                                            {
                                                ConstantValue = "graduationStatus",
                                                Type = typeof(string).AssemblyQualifiedName
                                            }
                                        },
                                        FilterParameterName = "l"
                                    },
                                    SelectorBody = new MemberSelectorOperatorDescriptor
                                    {
                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                        MemberFullName = "Text"
                                    },
                                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                                    SelectorParameterName = "l"
                                },
                                SelectorBody = new MemberInitOperatorDescriptor
                                {
                                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                    {
                                        ["Value"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "Value"
                                        },
                                        ["Text"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "Text"
                                        }
                                    },
                                    NewType = typeof(LookUpsModel).AssemblyQualifiedName
                                },
                                SelectorParameterName = "s"
                            },
                            SourceElementType = typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName,
                            ParameterName = "$it",
                            BodyType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                            DataType = typeof(LookUps).AssemblyQualifiedName,
                            ModelReturnType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName,
                            DataReturnType = typeof(IEnumerable<LookUps>).AssemblyQualifiedName
                        }
                    },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = ""
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "EarnedCreditAtCmc",
                    Type = "System.Boolean",
                    Title = "Earned Credit At CMC",
                    Placeholder = "",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "SwitchTemplate" },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = false
                    }
                },
                new FormGroupArraySettingsDescriptor
                {
                    Field = "Institutions",
                    Placeholder = "(Institutions)",
                    FormGroupTemplate = new FormGroupTemplateDescriptor
                    {
                        TemplateName = "FormGroupArrayTemplate"
                    },
                    ValidationMessages =  new Dictionary<string, List<ValidationRuleDescriptor>>
                    {
                    },
                    FormsCollectionDisplayTemplate = new FormsCollectionDisplayTemplateDescriptor
                    {
                        TemplateName = "TextDetailTemplate",
                        Bindings = new List<ItemBindingDescriptor>
                        {
                            new TextItemBindingDescriptor
                            {
                                Name = "Text",
                                Property = "InstitutionName",
                                StringFormat = "{0}",
                                TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                            },
                            new TextItemBindingDescriptor
                            {
                                Name = "Detail",
                                Property = "InstitutionState",
                                StringFormat = "{0}",
                                TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                            }
                        }.ToDictionary(b => b.Name)
                    },
                    FieldSettings = new List<FormItemSettingsDescriptor>
                    {
                        new FormControlSettingsDescriptor
                        {
                            Field = "InstitutionId",
                            Type = "System.Int32",
                            Title = "ID",
                            TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "HiddenTemplate" },
                            ValidationSetting = new FieldValidationSettingsDescriptor
                            {
                                DefaultValue = 0
                            }
                        },
                        new FormControlSettingsDescriptor
                        {
                            Field = "InstitutionState",
                            Type = "System.String",
                            Title = "Institution State",
                            Placeholder = "Institution State",
                            DropDownTemplate = new DropDownTemplateDescriptor
                            {
                                TemplateName = "PickerTemplate",
                                TitleText = "Select Institution State:",
                                LoadingIndicatorText = "Loading ...",
                                TextField = "Text",
                                ValueField = "Value",
                                TextAndValueSelector = new SelectorLambdaOperatorDescriptor
                                {
                                    Selector = new SelectOperatorDescriptor
                                    {
                                        SourceOperand = new OrderByOperatorDescriptor
                                        {
                                            SourceOperand = new WhereOperatorDescriptor
                                            {
                                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "$it" },
                                                FilterBody = new EqualsBinaryOperatorDescriptor
                                                {
                                                    Left = new MemberSelectorOperatorDescriptor
                                                    {
                                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                        MemberFullName = "ListName"
                                                    },
                                                    Right = new ConstantOperatorDescriptor
                                                    {
                                                        ConstantValue = "states",
                                                        Type = typeof(string).AssemblyQualifiedName
                                                    }
                                                },
                                                FilterParameterName = "l"
                                            },
                                            SelectorBody = new MemberSelectorOperatorDescriptor
                                            {
                                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                MemberFullName = "Text"
                                            },
                                            SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                                            SelectorParameterName = "l"
                                        },
                                        SelectorBody = new MemberInitOperatorDescriptor
                                        {
                                            MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                            {
                                                ["Value"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                    MemberFullName = "Value"
                                                },
                                                ["Text"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                    MemberFullName = "Text"
                                                }
                                            },
                                            NewType = typeof(LookUpsModel).AssemblyQualifiedName
                                        },
                                        SelectorParameterName = "s"
                                    },
                                    SourceElementType = typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName,
                                    ParameterName = "$it",
                                    BodyType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName
                                },
                                RequestDetails = new RequestDetailsDescriptor
                                {
                                    DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                                    ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                                    DataType = typeof(LookUps).AssemblyQualifiedName,
                                    ModelReturnType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName,
                                    DataReturnType = typeof(IEnumerable<LookUps>).AssemblyQualifiedName
                                }
                            },
                            ValidationSetting = new FieldValidationSettingsDescriptor
                            {
                                DefaultValue = ""
                            }
                        },
                        new FormControlSettingsDescriptor
                        {
                            Field = "InstitutionName",
                            Type = "System.String",
                            Title = "Institution Name",
                            Placeholder = "Institution Name",
                            DropDownTemplate = new DropDownTemplateDescriptor
                            {
                                TemplateName = "PickerTemplate",
                                TitleText = "Select Highest Degree Earned:",
                                LoadingIndicatorText = "Loading ...",
                                TextField = "Text",
                                ValueField = "Value",
                                TextAndValueSelector = new SelectorLambdaOperatorDescriptor
                                {
                                    Selector = new SelectOperatorDescriptor
                                    {
                                        SourceOperand = new OrderByOperatorDescriptor
                                        {
                                            SourceOperand = new WhereOperatorDescriptor
                                            {
                                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "$it" },
                                                FilterBody = new EqualsBinaryOperatorDescriptor
                                                {
                                                    Left = new MemberSelectorOperatorDescriptor
                                                    {
                                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                        MemberFullName = "ListName"
                                                    },
                                                    Right = new ConstantOperatorDescriptor
                                                    {
                                                        ConstantValue = "northCarolinaInstitutions",
                                                        Type = typeof(string).AssemblyQualifiedName
                                                    }
                                                },
                                                FilterParameterName = "l"
                                            },
                                            SelectorBody = new MemberSelectorOperatorDescriptor
                                            {
                                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                MemberFullName = "Text"
                                            },
                                            SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                                            SelectorParameterName = "l"
                                        },
                                        SelectorBody = new MemberInitOperatorDescriptor
                                        {
                                            MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                            {
                                                ["Value"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                    MemberFullName = "Value"
                                                },
                                                ["Text"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                    MemberFullName = "Text"
                                                }
                                            },
                                            NewType = typeof(LookUpsModel).AssemblyQualifiedName
                                        },
                                        SelectorParameterName = "s"
                                    },
                                    SourceElementType = typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName,
                                    ParameterName = "$it",
                                    BodyType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName
                                },
                                RequestDetails = new RequestDetailsDescriptor
                                {
                                    DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                                    ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                                    DataType = typeof(LookUps).AssemblyQualifiedName,
                                    ModelReturnType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName,
                                    DataReturnType = typeof(IEnumerable<LookUps>).AssemblyQualifiedName
                                }
                            },
                            ValidationSetting = new FieldValidationSettingsDescriptor
                            {
                                DefaultValue = ""
                            }
                        },
                        new FormControlSettingsDescriptor
                        {
                            Field = "StartYear",
                            Type = "System.String",
                            Title = "StartYear",
                            Placeholder = "StartYear (required)",
                            TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                            ValidationSetting = new FieldValidationSettingsDescriptor
                            {
                                DefaultValue = ""
                            }
                        },
                        new FormControlSettingsDescriptor
                        {
                            Field = "EndYear",
                            Type = "System.String",
                            Title = "EndYear",
                            Placeholder = "EndYear (required)",
                            TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                            ValidationSetting = new FieldValidationSettingsDescriptor
                            {
                                DefaultValue = ""
                            }
                        },
                        new FormControlSettingsDescriptor
                        {
                            Field = "HighestDegreeEarned",
                            Type = "System.String",
                            Title = "Highest Degree Earned",
                            Placeholder = "Highest Degree Earned",
                            DropDownTemplate = new DropDownTemplateDescriptor
                            {
                                TemplateName = "PickerTemplate",
                                TitleText = "Select Highest Degree Earned:",
                                LoadingIndicatorText = "Loading ...",
                                TextField = "Text",
                                ValueField = "Value",
                                TextAndValueSelector = new SelectorLambdaOperatorDescriptor
                                {
                                    Selector = new SelectOperatorDescriptor
                                    {
                                        SourceOperand = new OrderByOperatorDescriptor
                                        {
                                            SourceOperand = new WhereOperatorDescriptor
                                            {
                                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "$it" },
                                                FilterBody = new EqualsBinaryOperatorDescriptor
                                                {
                                                    Left = new MemberSelectorOperatorDescriptor
                                                    {
                                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                        MemberFullName = "ListName"
                                                    },
                                                    Right = new ConstantOperatorDescriptor
                                                    {
                                                        ConstantValue = "degreeEarned",
                                                        Type = typeof(string).AssemblyQualifiedName
                                                    }
                                                },
                                                FilterParameterName = "l"
                                            },
                                            SelectorBody = new MemberSelectorOperatorDescriptor
                                            {
                                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                MemberFullName = "Text"
                                            },
                                            SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                                            SelectorParameterName = "l"
                                        },
                                        SelectorBody = new MemberInitOperatorDescriptor
                                        {
                                            MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                            {
                                                ["Value"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                    MemberFullName = "Value"
                                                },
                                                ["Text"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                    MemberFullName = "Text"
                                                }
                                            },
                                            NewType = typeof(LookUpsModel).AssemblyQualifiedName
                                        },
                                        SelectorParameterName = "s"
                                    },
                                    SourceElementType = typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName,
                                    ParameterName = "$it",
                                    BodyType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName
                                },
                                RequestDetails = new RequestDetailsDescriptor
                                {
                                    DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                                    ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                                    DataType = typeof(LookUps).AssemblyQualifiedName,
                                    ModelReturnType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName,
                                    DataReturnType = typeof(IEnumerable<LookUps>).AssemblyQualifiedName
                                }
                            },
                            ValidationSetting = new FieldValidationSettingsDescriptor
                            {
                                DefaultValue = ""
                            }
                        },
                    },
                    Type = typeof(ICollection<InstitutionModel>).AssemblyQualifiedName,
                    ModelType = typeof(InstitutionModel).AssemblyQualifiedName,
                    KeyFields = new List<string> { "InstitutionId" }
                }
            }
        };

        internal static DataFormSettingsDescriptor PersonalFrom = new()
        {
            Title = "Personal",
            RequestDetails = new FormRequestDetailsDescriptor
            {
                GetUrl = "/Personal/GetSingle"
            },
            FieldSettings = new List<FormItemSettingsDescriptor>
            {
                new FormGroupSettingsDescriptor
                {
                    Field = "Personal",
                    Title = "Personal",
                    ValidFormControlText = "(Personal)",
                    InvalidFormControlText ="(Invalid Personal)",
                    ModelType = typeof(PersonalModel).AssemblyQualifiedName,
                    FormGroupTemplate = new FormGroupTemplateDescriptor
                    {
                        TemplateName = "InlineFormGroupTemplate",
                    },
                    ValidationMessages = new Dictionary<string, List<ValidationRuleDescriptor>>
                    {
                        ["FirstName"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "First Name is required." }
                        },
                        ["MiddleName"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Middle Name is required." }
                        },
                        ["LastName"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Last Name is required." }
                        },
                        ["PrimaryEmail"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Primary Email is required." },
                            new ValidationRuleDescriptor { ClassName = "IsValidEmailRule", Message = "Primary Email must be a valid email address." }
                        },
                        ["Address1"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Address1 is required." }
                        },
                        ["City"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "City is required." }
                        },
                        ["County"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "County is required." }
                        },
                        ["State"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "State is required." }
                        },
                        ["ZipCode"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Zip Code is required." },
                            new ValidationRuleDescriptor { ClassName = "IsPatternMatchRule", Message = "Zip Code must be a valid zip code (12345)" }
                        },
                        ["CellPhone"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Cell Phone is required." },
                            new ValidationRuleDescriptor { ClassName = "IsPatternMatchRule", Message = "Cell Phone must be a valid phone number." }
                        },
                        ["OtherPhone"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Other Phone is required." },
                            new ValidationRuleDescriptor { ClassName = "IsPatternMatchRule", Message = "Other Phone must be a valid phone number." }
                        }
                    },
                    FieldSettings = new List<FormItemSettingsDescriptor>
                    {
                        new FormGroupBoxSettingsDescriptor
                        {
                            GroupHeader = "Name",
                            FieldSettings = new List<FormItemSettingsDescriptor>
                            {
                                new FormControlSettingsDescriptor
                                {
                                    Field = "FirstName",
                                    Type = "System.String",
                                    Title = "First Name",
                                    Placeholder = "First Name (required)",
                                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                                    ValidationSetting = new FieldValidationSettingsDescriptor
                                    {
                                        DefaultValue = "",
                                        Validators = new List<ValidatorDefinitionDescriptor>
                                        {
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "RequiredRule",
                                                FunctionName = "Check"
                                            }
                                        }
                                    }
                                },
                                new FormControlSettingsDescriptor
                                {
                                    Field = "MiddleName",
                                    Type = "System.String",
                                    Title = "Middle Name",
                                    Placeholder = "Middle Name (required)",
                                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                                    ValidationSetting = new FieldValidationSettingsDescriptor
                                    {
                                        DefaultValue = "",
                                        Validators = new List<ValidatorDefinitionDescriptor>
                                        {
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "RequiredRule",
                                                FunctionName = "Check"
                                            }
                                        }
                                    }
                                },
                                new FormControlSettingsDescriptor
                                {
                                    Field = "LastName",
                                    Type = "System.String",
                                    Title = "Last Name",
                                    Placeholder = "Last Name (required)",
                                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                                    ValidationSetting = new FieldValidationSettingsDescriptor
                                    {
                                        DefaultValue = "",
                                        Validators = new List<ValidatorDefinitionDescriptor>
                                        {
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "RequiredRule",
                                                FunctionName = "Check"
                                            }
                                        }
                                    }
                                },
                                new FormControlSettingsDescriptor
                                {
                                    Field = "PrimaryEmail",
                                    Type = "System.String",
                                    Title = "Primary Email",
                                    Placeholder = "Primary Email (required)",
                                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                                    ValidationSetting = new FieldValidationSettingsDescriptor
                                    {
                                        DefaultValue = "",
                                        Validators = new List<ValidatorDefinitionDescriptor>
                                        {
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "RequiredRule",
                                                FunctionName = "Check"
                                            },
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "IsValidEmailRule",
                                                FunctionName = "Check"
                                            }
                                        }
                                    }
                                },
                                new FormControlSettingsDescriptor
                                {
                                    Field = "Suffix",
                                    Type = "System.String",
                                    Title = "Suffix",
                                    Placeholder = "Suffix",
                                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                                    ValidationSetting = new FieldValidationSettingsDescriptor
                                    {
                                        DefaultValue = ""
                                    }
                                },
                            }
                        },
                        new FormGroupBoxSettingsDescriptor
                        {
                            GroupHeader = "Address",
                            FieldSettings = new List<FormItemSettingsDescriptor>
                            {
                                new FormControlSettingsDescriptor
                                {
                                    Field = "Address1",
                                    Type = "System.String",
                                    Title = "Address1",
                                    Placeholder = "Address1 (required)",
                                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                                    ValidationSetting = new FieldValidationSettingsDescriptor
                                    {
                                        DefaultValue = "",
                                        Validators = new List<ValidatorDefinitionDescriptor>
                                        {
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "RequiredRule",
                                                FunctionName = "Check"
                                            }
                                        }
                                    }
                                },
                                new FormControlSettingsDescriptor
                                {
                                    Field = "Address2",
                                    Type = "System.String",
                                    Title = "Address2",
                                    Placeholder = "Address2",
                                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                                    ValidationSetting = new FieldValidationSettingsDescriptor
                                    {
                                        DefaultValue = ""
                                    }
                                },
                                new FormControlSettingsDescriptor
                                {
                                    Field = "City",
                                    Type = "System.String",
                                    Title = "City",
                                    Placeholder = "City (required)",
                                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                                    ValidationSetting = new FieldValidationSettingsDescriptor
                                    {
                                        DefaultValue = "",
                                        Validators = new List<ValidatorDefinitionDescriptor>
                                        {
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "RequiredRule",
                                                FunctionName = "Check"
                                            }
                                        }
                                    }
                                },
                                new FormControlSettingsDescriptor
                                {
                                    Field = "County",
                                    Type = "System.String",
                                    Title = "County",
                                    Placeholder = "County (required)",
                                    DropDownTemplate = new DropDownTemplateDescriptor
                                    {
                                        TemplateName = "PickerTemplate",
                                        TitleText = "Select County:",
                                        LoadingIndicatorText = "Loading ...",
                                        TextField = "Text",
                                        ValueField = "Value",
                                        TextAndValueSelector = new SelectorLambdaOperatorDescriptor
                                        {
                                            Selector = new SelectOperatorDescriptor
                                            {
                                                SourceOperand = new OrderByOperatorDescriptor
                                                {
                                                    SourceOperand = new WhereOperatorDescriptor
                                                    {
                                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "$it" },
                                                        FilterBody = new EqualsBinaryOperatorDescriptor
                                                        {
                                                            Left = new MemberSelectorOperatorDescriptor
                                                            {
                                                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                                MemberFullName = "ListName"
                                                            },
                                                            Right = new ConstantOperatorDescriptor
                                                            {
                                                                ConstantValue = "counties",
                                                                Type = typeof(string).AssemblyQualifiedName
                                                            }
                                                        },
                                                        FilterParameterName = "l"
                                                    },
                                                    SelectorBody = new MemberSelectorOperatorDescriptor
                                                    {
                                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                        MemberFullName = "Text"
                                                    },
                                                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                                                    SelectorParameterName = "l"
                                                },
                                                SelectorBody = new MemberInitOperatorDescriptor
                                                {
                                                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                                    {
                                                        ["Value"] = new MemberSelectorOperatorDescriptor
                                                        {
                                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                            MemberFullName = "Value"
                                                        },
                                                        ["Text"] = new MemberSelectorOperatorDescriptor
                                                        {
                                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                            MemberFullName = "Text"
                                                        }
                                                    },
                                                    NewType = typeof(LookUpsModel).AssemblyQualifiedName
                                                },
                                                SelectorParameterName = "s"
                                            },
                                            SourceElementType = typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName,
                                            ParameterName = "$it",
                                            BodyType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName
                                        },
                                        RequestDetails = new RequestDetailsDescriptor
                                        {
                                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                                            ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                                            DataType = typeof(LookUps).AssemblyQualifiedName,
                                            ModelReturnType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName,
                                            DataReturnType = typeof(IEnumerable<LookUps>).AssemblyQualifiedName
                                        }
                                    },
                                    ValidationSetting = new FieldValidationSettingsDescriptor
                                    {
                                        DefaultValue = "",
                                        Validators = new List<ValidatorDefinitionDescriptor>
                                        {
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "RequiredRule",
                                                FunctionName = "Check"
                                            }
                                        }
                                    }
                                },
                                new FormControlSettingsDescriptor
                                {
                                    Field = "State",
                                    Type = "System.String",
                                    Title = "State",
                                    Placeholder = "State (required)",
                                    DropDownTemplate = new DropDownTemplateDescriptor
                                    {
                                        TemplateName = "PickerTemplate",
                                        TitleText = "Select State:",
                                        LoadingIndicatorText = "Loading ...",
                                        TextField = "Text",
                                        ValueField = "Value",
                                        TextAndValueSelector = new SelectorLambdaOperatorDescriptor
                                        {
                                            Selector = new SelectOperatorDescriptor
                                            {
                                                SourceOperand = new OrderByOperatorDescriptor
                                                {
                                                    SourceOperand = new WhereOperatorDescriptor
                                                    {
                                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "$it" },
                                                        FilterBody = new EqualsBinaryOperatorDescriptor
                                                        {
                                                            Left = new MemberSelectorOperatorDescriptor
                                                            {
                                                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                                MemberFullName = "ListName"
                                                            },
                                                            Right = new ConstantOperatorDescriptor
                                                            {
                                                                ConstantValue = "states",
                                                                Type = typeof(string).AssemblyQualifiedName
                                                            }
                                                        },
                                                        FilterParameterName = "l"
                                                    },
                                                    SelectorBody = new MemberSelectorOperatorDescriptor
                                                    {
                                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                        MemberFullName = "Text"
                                                    },
                                                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                                                    SelectorParameterName = "l"
                                                },
                                                SelectorBody = new MemberInitOperatorDescriptor
                                                {
                                                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                                    {
                                                        ["Value"] = new MemberSelectorOperatorDescriptor
                                                        {
                                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                            MemberFullName = "Value"
                                                        },
                                                        ["Text"] = new MemberSelectorOperatorDescriptor
                                                        {
                                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                            MemberFullName = "Text"
                                                        }
                                                    },
                                                    NewType = typeof(LookUpsModel).AssemblyQualifiedName
                                                },
                                                SelectorParameterName = "s"
                                            },
                                            SourceElementType = typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName,
                                            ParameterName = "$it",
                                            BodyType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName
                                        },
                                        RequestDetails = new RequestDetailsDescriptor
                                        {
                                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                                            ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                                            DataType = typeof(LookUps).AssemblyQualifiedName,
                                            ModelReturnType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName,
                                            DataReturnType = typeof(IEnumerable<LookUps>).AssemblyQualifiedName
                                        }
                                    },
                                    ValidationSetting = new FieldValidationSettingsDescriptor
                                    {
                                        DefaultValue = "",
                                        Validators = new List<ValidatorDefinitionDescriptor>
                                        {
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "RequiredRule",
                                                FunctionName = "Check"
                                            }
                                        }
                                    }
                                },
                                new FormControlSettingsDescriptor
                                {
                                    Field = "ZipCode",
                                    Type = "System.String",
                                    Title = "Zip Code",
                                    Placeholder = "Zip Code (required)",
                                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                                    ValidationSetting = new FieldValidationSettingsDescriptor
                                    {
                                        DefaultValue = "",
                                        Validators = new List<ValidatorDefinitionDescriptor>
                                        {
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "RequiredRule",
                                                FunctionName = "Check"
                                            },
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "IsPatternMatchRule",
                                                FunctionName = "Check",
                                                Arguments = new Dictionary<string, ValidatorArgumentDescriptor>
                                                {
                                                    ["pattern"] = new ValidatorArgumentDescriptor
                                                    {
                                                        Name = "pattern",
                                                        Value  = @"^[\d]{5}$",
                                                        Type = "System.String"
                                                    }
                                                }
                                            }
                                        }
                                    }
                                },
                            }
                        },
                        new FormGroupBoxSettingsDescriptor
                        {
                            GroupHeader = "Phone Numbers",
                            FieldSettings = new List<FormItemSettingsDescriptor>
                            {
                                new FormControlSettingsDescriptor
                                {
                                    Field = "CellPhone",
                                    Type = "System.String",
                                    Title = "Cell Phone",
                                    Placeholder = "Cell Phone (required)",
                                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                                    ValidationSetting = new FieldValidationSettingsDescriptor
                                    {
                                        DefaultValue = "",
                                        Validators = new List<ValidatorDefinitionDescriptor>
                                        {
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "RequiredRule",
                                                FunctionName = "Check"
                                            },
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "IsPatternMatchRule",
                                                FunctionName = "Check",
                                                Arguments = new Dictionary<string, ValidatorArgumentDescriptor>
                                                {
                                                    ["pattern"] = new ValidatorArgumentDescriptor
                                                    {
                                                        Name = "pattern",
                                                        Value  = @"^[\d]{3}-[\d]{3}-[\d]{4}$",
                                                        Type = "System.String"
                                                    }
                                                }
                                            }
                                        }
                                    }
                                },
                                new FormControlSettingsDescriptor
                                {
                                    Field = "OtherPhone",
                                    Type = "System.String",
                                    Title = "Other Phone",
                                    Placeholder = "Other Phone (required)",
                                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                                    ValidationSetting = new FieldValidationSettingsDescriptor
                                    {
                                        DefaultValue = "",
                                        Validators = new List<ValidatorDefinitionDescriptor>
                                        {
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "RequiredRule",
                                                FunctionName = "Check"
                                            },
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "IsPatternMatchRule",
                                                FunctionName = "Check",
                                                Arguments = new Dictionary<string, ValidatorArgumentDescriptor>
                                                {
                                                    ["pattern"] = new ValidatorArgumentDescriptor
                                                    {
                                                        Name = "pattern",
                                                        Value  = @"^[\d]{3}-[\d]{3}-[\d]{4}$",
                                                        Type = "System.String"
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            },
            FormType = FormType.Update,
            ModelType = typeof(UserModel).AssemblyQualifiedName
        };

        internal static DataFormSettingsDescriptor PersonalFromWithDefaultGroupForSomeFields = new()
        {
            Title = "PersonalRoot",
            RequestDetails = new FormRequestDetailsDescriptor
            {
                GetUrl = "/Personal/GetSingle"
            },
            FieldSettings = new List<FormItemSettingsDescriptor>
            {
                new FormGroupSettingsDescriptor
                {
                    Field = "Personal",
                    Title = "Personal",
                    ValidFormControlText = "(Personal)",
                    InvalidFormControlText ="(Invalid Personal)",
                    ModelType = typeof(PersonalModel).AssemblyQualifiedName,
                    FormGroupTemplate = new FormGroupTemplateDescriptor
                    {
                        TemplateName = "InlineFormGroupTemplate",
                    },
                    ValidationMessages = new Dictionary<string, List<ValidationRuleDescriptor>>
                    {
                        ["FirstName"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "First Name is required." }
                        },
                        ["MiddleName"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Middle Name is required." }
                        },
                        ["LastName"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Last Name is required." }
                        },
                        ["PrimaryEmail"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Primary Email is required." },
                            new ValidationRuleDescriptor { ClassName = "IsValidEmailRule", Message = "Primary Email must be a valid email address." }
                        },
                        ["Address1"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Address1 is required." }
                        },
                        ["City"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "City is required." }
                        },
                        ["County"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "County is required." }
                        },
                        ["State"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "State is required." }
                        },
                        ["ZipCode"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Zip Code is required." },
                            new ValidationRuleDescriptor { ClassName = "IsPatternMatchRule", Message = "Zip Code must be a valid zip code (12345)" }
                        },
                        ["CellPhone"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Cell Phone is required." },
                            new ValidationRuleDescriptor { ClassName = "IsPatternMatchRule", Message = "Cell Phone must be a valid phone number." }
                        },
                        ["OtherPhone"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Other Phone is required." },
                            new ValidationRuleDescriptor { ClassName = "IsPatternMatchRule", Message = "Other Phone must be a valid phone number." }
                        }
                    },
                    FieldSettings = new List<FormItemSettingsDescriptor>
                    {
                        new FormGroupBoxSettingsDescriptor
                        {
                            GroupHeader = "Address",
                            FieldSettings = new List<FormItemSettingsDescriptor>
                            {
                                new FormControlSettingsDescriptor
                                {
                                    Field = "Address1",
                                    Type = "System.String",
                                    Title = "Address1",
                                    Placeholder = "Address1 (required)",
                                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                                    ValidationSetting = new FieldValidationSettingsDescriptor
                                    {
                                        DefaultValue = "",
                                        Validators = new List<ValidatorDefinitionDescriptor>
                                        {
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "RequiredRule",
                                                FunctionName = "Check"
                                            }
                                        }
                                    }
                                },
                                new FormControlSettingsDescriptor
                                {
                                    Field = "Address2",
                                    Type = "System.String",
                                    Title = "Address2",
                                    Placeholder = "Address2",
                                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                                    ValidationSetting = new FieldValidationSettingsDescriptor
                                    {
                                        DefaultValue = ""
                                    }
                                },
                                new FormControlSettingsDescriptor
                                {
                                    Field = "City",
                                    Type = "System.String",
                                    Title = "City",
                                    Placeholder = "City (required)",
                                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                                    ValidationSetting = new FieldValidationSettingsDescriptor
                                    {
                                        DefaultValue = "",
                                        Validators = new List<ValidatorDefinitionDescriptor>
                                        {
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "RequiredRule",
                                                FunctionName = "Check"
                                            }
                                        }
                                    }
                                },
                                new FormControlSettingsDescriptor
                                {
                                    Field = "County",
                                    Type = "System.String",
                                    Title = "County",
                                    Placeholder = "County (required)",
                                    DropDownTemplate = new DropDownTemplateDescriptor
                                    {
                                        TemplateName = "PickerTemplate",
                                        TitleText = "Select County:",
                                        LoadingIndicatorText = "Loading ...",
                                        TextField = "Text",
                                        ValueField = "Value",
                                        TextAndValueSelector = new SelectorLambdaOperatorDescriptor
                                        {
                                            Selector = new SelectOperatorDescriptor
                                            {
                                                SourceOperand = new OrderByOperatorDescriptor
                                                {
                                                    SourceOperand = new WhereOperatorDescriptor
                                                    {
                                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "$it" },
                                                        FilterBody = new EqualsBinaryOperatorDescriptor
                                                        {
                                                            Left = new MemberSelectorOperatorDescriptor
                                                            {
                                                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                                MemberFullName = "ListName"
                                                            },
                                                            Right = new ConstantOperatorDescriptor
                                                            {
                                                                ConstantValue = "counties",
                                                                Type = typeof(string).AssemblyQualifiedName
                                                            }
                                                        },
                                                        FilterParameterName = "l"
                                                    },
                                                    SelectorBody = new MemberSelectorOperatorDescriptor
                                                    {
                                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                        MemberFullName = "Text"
                                                    },
                                                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                                                    SelectorParameterName = "l"
                                                },
                                                SelectorBody = new MemberInitOperatorDescriptor
                                                {
                                                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                                    {
                                                        ["Value"] = new MemberSelectorOperatorDescriptor
                                                        {
                                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                            MemberFullName = "Value"
                                                        },
                                                        ["Text"] = new MemberSelectorOperatorDescriptor
                                                        {
                                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                            MemberFullName = "Text"
                                                        }
                                                    },
                                                    NewType = typeof(LookUpsModel).AssemblyQualifiedName
                                                },
                                                SelectorParameterName = "s"
                                            },
                                            SourceElementType = typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName,
                                            ParameterName = "$it",
                                            BodyType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName
                                        },
                                        RequestDetails = new RequestDetailsDescriptor
                                        {
                                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                                            ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                                            DataType = typeof(LookUps).AssemblyQualifiedName,
                                            ModelReturnType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName,
                                            DataReturnType = typeof(IEnumerable<LookUps>).AssemblyQualifiedName
                                        }
                                    },
                                    ValidationSetting = new FieldValidationSettingsDescriptor
                                    {
                                        DefaultValue = "",
                                        Validators = new List<ValidatorDefinitionDescriptor>
                                        {
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "RequiredRule",
                                                FunctionName = "Check"
                                            }
                                        }
                                    }
                                },
                                new FormControlSettingsDescriptor
                                {
                                    Field = "State",
                                    Type = "System.String",
                                    Title = "State",
                                    Placeholder = "State (required)",
                                    DropDownTemplate = new DropDownTemplateDescriptor
                                    {
                                        TemplateName = "PickerTemplate",
                                        TitleText = "Select State:",
                                        LoadingIndicatorText = "Loading ...",
                                        TextField = "Text",
                                        ValueField = "Value",
                                        TextAndValueSelector = new SelectorLambdaOperatorDescriptor
                                        {
                                            Selector = new SelectOperatorDescriptor
                                            {
                                                SourceOperand = new OrderByOperatorDescriptor
                                                {
                                                    SourceOperand = new WhereOperatorDescriptor
                                                    {
                                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "$it" },
                                                        FilterBody = new EqualsBinaryOperatorDescriptor
                                                        {
                                                            Left = new MemberSelectorOperatorDescriptor
                                                            {
                                                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                                MemberFullName = "ListName"
                                                            },
                                                            Right = new ConstantOperatorDescriptor
                                                            {
                                                                ConstantValue = "states",
                                                                Type = typeof(string).AssemblyQualifiedName
                                                            }
                                                        },
                                                        FilterParameterName = "l"
                                                    },
                                                    SelectorBody = new MemberSelectorOperatorDescriptor
                                                    {
                                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                        MemberFullName = "Text"
                                                    },
                                                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                                                    SelectorParameterName = "l"
                                                },
                                                SelectorBody = new MemberInitOperatorDescriptor
                                                {
                                                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                                    {
                                                        ["Value"] = new MemberSelectorOperatorDescriptor
                                                        {
                                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                            MemberFullName = "Value"
                                                        },
                                                        ["Text"] = new MemberSelectorOperatorDescriptor
                                                        {
                                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                            MemberFullName = "Text"
                                                        }
                                                    },
                                                    NewType = typeof(LookUpsModel).AssemblyQualifiedName
                                                },
                                                SelectorParameterName = "s"
                                            },
                                            SourceElementType = typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName,
                                            ParameterName = "$it",
                                            BodyType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName
                                        },
                                        RequestDetails = new RequestDetailsDescriptor
                                        {
                                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                                            ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                                            DataType = typeof(LookUps).AssemblyQualifiedName,
                                            ModelReturnType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName,
                                            DataReturnType = typeof(IEnumerable<LookUps>).AssemblyQualifiedName
                                        }
                                    },
                                    ValidationSetting = new FieldValidationSettingsDescriptor
                                    {
                                        DefaultValue = "",
                                        Validators = new List<ValidatorDefinitionDescriptor>
                                        {
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "RequiredRule",
                                                FunctionName = "Check"
                                            }
                                        }
                                    }
                                },
                                new FormControlSettingsDescriptor
                                {
                                    Field = "ZipCode",
                                    Type = "System.String",
                                    Title = "Zip Code",
                                    Placeholder = "Zip Code (required)",
                                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                                    ValidationSetting = new FieldValidationSettingsDescriptor
                                    {
                                        DefaultValue = "",
                                        Validators = new List<ValidatorDefinitionDescriptor>
                                        {
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "RequiredRule",
                                                FunctionName = "Check"
                                            },
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "IsPatternMatchRule",
                                                FunctionName = "Check",
                                                Arguments = new Dictionary<string, ValidatorArgumentDescriptor>
                                                {
                                                    ["pattern"] = new ValidatorArgumentDescriptor
                                                    {
                                                        Name = "pattern",
                                                        Value  = @"^[\d]{5}$",
                                                        Type = "System.String"
                                                    }
                                                }
                                            }
                                        }
                                    }
                                },
                            }
                        },
                        new FormGroupBoxSettingsDescriptor
                        {
                            GroupHeader = "Phone Numbers",
                            FieldSettings = new List<FormItemSettingsDescriptor>
                            {
                                new FormControlSettingsDescriptor
                                {
                                    Field = "CellPhone",
                                    Type = "System.String",
                                    Title = "Cell Phone",
                                    Placeholder = "Cell Phone (required)",
                                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                                    ValidationSetting = new FieldValidationSettingsDescriptor
                                    {
                                        DefaultValue = "",
                                        Validators = new List<ValidatorDefinitionDescriptor>
                                        {
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "RequiredRule",
                                                FunctionName = "Check"
                                            },
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "IsPatternMatchRule",
                                                FunctionName = "Check",
                                                Arguments = new Dictionary<string, ValidatorArgumentDescriptor>
                                                {
                                                    ["pattern"] = new ValidatorArgumentDescriptor
                                                    {
                                                        Name = "pattern",
                                                        Value  = @"^[\d]{3}-[\d]{3}-[\d]{4}$",
                                                        Type = "System.String"
                                                    }
                                                }
                                            }
                                        }
                                    }
                                },
                                new FormControlSettingsDescriptor
                                {
                                    Field = "OtherPhone",
                                    Type = "System.String",
                                    Title = "Other Phone",
                                    Placeholder = "Other Phone (required)",
                                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                                    ValidationSetting = new FieldValidationSettingsDescriptor
                                    {
                                        DefaultValue = "",
                                        Validators = new List<ValidatorDefinitionDescriptor>
                                        {
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "RequiredRule",
                                                FunctionName = "Check"
                                            },
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "IsPatternMatchRule",
                                                FunctionName = "Check",
                                                Arguments = new Dictionary<string, ValidatorArgumentDescriptor>
                                                {
                                                    ["pattern"] = new ValidatorArgumentDescriptor
                                                    {
                                                        Name = "pattern",
                                                        Value  = @"^[\d]{3}-[\d]{3}-[\d]{4}$",
                                                        Type = "System.String"
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new FormGroupSettingsDescriptor
                {
                    Field = "Personal",
                    Title = "Personal0",
                    ValidFormControlText = "(Personal)",
                    InvalidFormControlText ="(Invalid Personal)",
                    ModelType = typeof(PersonalModel).AssemblyQualifiedName,
                    FormGroupTemplate = new FormGroupTemplateDescriptor
                    {
                        TemplateName = "InlineFormGroupTemplate",
                    },
                    ValidationMessages = new Dictionary<string, List<ValidationRuleDescriptor>>
                    {
                        ["FirstName"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "First Name is required." }
                        },
                        ["MiddleName"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Middle Name is required." }
                        },
                        ["LastName"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Last Name is required." }
                        },
                        ["PrimaryEmail"] = new List<ValidationRuleDescriptor>
                        {
                            new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Primary Email is required." },
                            new ValidationRuleDescriptor { ClassName = "IsValidEmailRule", Message = "Primary Email must be a valid email address." }
                        }
                    },
                    FieldSettings = new List<FormItemSettingsDescriptor>
                    {
                        new FormControlSettingsDescriptor
                        {
                            Field = "FirstName",
                            Type = "System.String",
                            Title = "First Name",
                            Placeholder = "First Name (required)",
                            TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                            ValidationSetting = new FieldValidationSettingsDescriptor
                            {
                                DefaultValue = "",
                                Validators = new List<ValidatorDefinitionDescriptor>
                                {
                                    new ValidatorDefinitionDescriptor
                                    {
                                        ClassName = "RequiredRule",
                                        FunctionName = "Check"
                                    }
                                }
                            }
                        },
                        new FormControlSettingsDescriptor
                        {
                            Field = "MiddleName",
                            Type = "System.String",
                            Title = "Middle Name",
                            Placeholder = "Middle Name (required)",
                            TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                            ValidationSetting = new FieldValidationSettingsDescriptor
                            {
                                DefaultValue = "",
                                Validators = new List<ValidatorDefinitionDescriptor>
                                {
                                    new ValidatorDefinitionDescriptor
                                    {
                                        ClassName = "RequiredRule",
                                        FunctionName = "Check"
                                    }
                                }
                            }
                        },
                        new FormControlSettingsDescriptor
                        {
                            Field = "LastName",
                            Type = "System.String",
                            Title = "Last Name",
                            Placeholder = "Last Name (required)",
                            TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                            ValidationSetting = new FieldValidationSettingsDescriptor
                            {
                                DefaultValue = "",
                                Validators = new List<ValidatorDefinitionDescriptor>
                                {
                                    new ValidatorDefinitionDescriptor
                                    {
                                        ClassName = "RequiredRule",
                                        FunctionName = "Check"
                                    }
                                }
                            }
                        },
                        new FormControlSettingsDescriptor
                        {
                            Field = "PrimaryEmail",
                            Type = "System.String",
                            Title = "Primary Email",
                            Placeholder = "Primary Email (required)",
                            TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                            ValidationSetting = new FieldValidationSettingsDescriptor
                            {
                                DefaultValue = "",
                                Validators = new List<ValidatorDefinitionDescriptor>
                                {
                                    new ValidatorDefinitionDescriptor
                                    {
                                        ClassName = "RequiredRule",
                                        FunctionName = "Check"
                                    },
                                    new ValidatorDefinitionDescriptor
                                    {
                                        ClassName = "IsValidEmailRule",
                                        FunctionName = "Check"
                                    }
                                }
                            }
                        },
                        new FormControlSettingsDescriptor
                        {
                            Field = "Suffix",
                            Type = "System.String",
                            Title = "Suffix",
                            Placeholder = "Suffix",
                            TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                            ValidationSetting = new FieldValidationSettingsDescriptor
                            {
                                DefaultValue = ""
                            }
                        },
                    }
                }
            },
            FormType = FormType.Update,
            ModelType = typeof(UserModel).AssemblyQualifiedName
        };

        internal static IList<CommandButtonDescriptor> ButtonDescriptors = new List<CommandButtonDescriptor>
        {
            new CommandButtonDescriptor { Id = 1, LongString = "Save", ShortString = "S", Command = "SubmitCommand", ButtonIcon = "Save" },
            new CommandButtonDescriptor { Id = 2, LongString = "Home", ShortString = "H", Command = "NavigateCommand", ButtonIcon = "Home" }
        };
    }
}
