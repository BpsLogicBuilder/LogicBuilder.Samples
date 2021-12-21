using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.Bindings;
using Contoso.Forms.Configuration.DataForm;
using Contoso.Forms.Configuration.Navigation;
using Contoso.Forms.Configuration.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.XPlatform.Tests
{
    internal static class Descriptors
    {
        internal static DataFormSettingsDescriptor InstructorFormWithInlineOfficeAssignment = new DataFormSettingsDescriptor
        {
            Title = "Instructor",
            RequestDetails = new FormRequestDetailsDescriptor
            {
                GetUrl = "/Instructor/GetSingle"
            },
            ValidationMessages = new List<ValidationMessageDescriptor>
            {
                new ValidationMessageDescriptor
                {
                    Field = "FirstName",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "First Name is required." }
                    }
                },
                new ValidationMessageDescriptor
                {
                    Field = "LastName",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Last Name is required." }
                    }
                },
                new ValidationMessageDescriptor
                {
                    Field = "HireDate",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Hire Date is required." }
                    }
                }
            }.ToDictionary(v => v.Field, v => v.Rules),
            FieldSettings = new List<FormItemSettingsDescriptor>
            {
                new FormControlSettingsDescriptor
                {
                    Field = "ID",
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
                    Field = "HireDate",
                    Type = "System.DateTime",
                    Title = "Hire Date",
                    Placeholder = "",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "DateTemplate" },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = new DateTime(1900, 1, 1),
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
                new FormGroupSettingsDescriptor
                {
                    Field = "OfficeAssignment",
                    ModelType = typeof(OfficeAssignmentModel).AssemblyQualifiedName,
                    FieldSettings = new List<FormItemSettingsDescriptor>
                    {
                        new FormControlSettingsDescriptor
                        {
                            Field = "Location",
                            Type = "System.String",
                            Title = "Location",
                            Placeholder = "Location",
                            TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                        }
                    },
                    FormGroupTemplate = new FormGroupTemplateDescriptor
                    {
                        TemplateName = "InlineFormGroupTemplate"
                    },
                    Title = ""
                },
                new MultiSelectFormControlSettingsDescriptor
                {
                    KeyFields = new List<string> { "CourseID" },
                    Field = "Courses",
                    Title ="Courses",
                    Placeholder = "Select Courses ...",
                    Type = typeof(ICollection<CourseAssignmentModel>).AssemblyQualifiedName,
                    MultiSelectTemplate =  new MultiSelectTemplateDescriptor
                    {
                        TemplateName = "MultiSelectTemplate",
                        PlaceholderText = "(Courses)",
                        LoadingIndicatorText = "Loading ...",
                        ModelType = typeof(CourseAssignmentModel).AssemblyQualifiedName,
                        TextField = "CourseTitle",
                        ValueField = "CourseID",
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
                                        MemberFullName = "Title"
                                    },
                                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                    SelectorParameterName = "d"
                                },
                                SelectorBody = new MemberInitOperatorDescriptor
                                {
                                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                    {
                                        ["CourseID"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "CourseID"
                                        },
                                        ["CourseTitle"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "Title"
                                        }
                                    },
                                    NewType = typeof(CourseAssignmentModel).AssemblyQualifiedName
                                },
                                SelectorParameterName = "s"
                            },
                            SourceElementType = typeof(IQueryable<CourseModel>).AssemblyQualifiedName,
                            ParameterName = "$it",
                            BodyType = typeof(IEnumerable<CourseAssignmentModel>).AssemblyQualifiedName
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(CourseModel).AssemblyQualifiedName,
                            DataType = typeof(Course).AssemblyQualifiedName,
                            ModelReturnType = typeof(IEnumerable<CourseAssignmentModel>).AssemblyQualifiedName,
                            DataReturnType = typeof(IEnumerable<CourseAssignment>).AssemblyQualifiedName
                        }
                    }
                }
            },
            ModelType = "Contoso.Domain.Entities.InstructorModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
        };

        internal static DataFormSettingsDescriptor InstructorFormWithPopupOfficeAssignment = new DataFormSettingsDescriptor
        {
            Title = "Instructor",
            RequestDetails = new FormRequestDetailsDescriptor
            {
                GetUrl = "/Instructor/GetSingle"
            },
            ValidationMessages = new List<ValidationMessageDescriptor>
            {
                new ValidationMessageDescriptor
                {
                    Field = "FirstName",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "First Name is required." }
                    }
                },
                new ValidationMessageDescriptor
                {
                    Field = "LastName",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Last Name is required." }
                    }
                },
                new ValidationMessageDescriptor
                {
                    Field = "HireDate",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Hire Date is required." }
                    }
                }
            }.ToDictionary(v => v.Field, v => v.Rules),
            FieldSettings = new List<FormItemSettingsDescriptor>
            {
                new FormControlSettingsDescriptor
                {
                    Field = "ID",
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
                    Field = "HireDate",
                    Type = "System.DateTime",
                    Title = "Hire Date",
                    Placeholder = "",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "DateTemplate" },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = new DateTime(1900, 1, 1),
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
                new FormGroupSettingsDescriptor
                {
                    Field = "OfficeAssignment",
                    ModelType = typeof(OfficeAssignmentModel).AssemblyQualifiedName,
                    FieldSettings = new List<FormItemSettingsDescriptor>
                    {
                        new FormControlSettingsDescriptor
                        {
                            Field = "Location",
                            Type = "System.String",
                            Title = "Location",
                            Placeholder = "Location",
                            TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                        }
                    },
                    FormGroupTemplate = new FormGroupTemplateDescriptor
                    {
                        TemplateName = "PopupFormGroupTemplate"
                    },
                    Title = ""
                },
                new MultiSelectFormControlSettingsDescriptor
                {
                    KeyFields = new List<string> { "CourseID" },
                    Field = "Courses",
                    Title ="Courses",
                    Placeholder = "Select Courses ...",
                    Type = typeof(ICollection<CourseAssignmentModel>).AssemblyQualifiedName,
                    MultiSelectTemplate =  new MultiSelectTemplateDescriptor
                    {
                        TemplateName = "MultiSelectTemplate",
                        PlaceholderText = "(Courses)",
                        LoadingIndicatorText = "Loading ...",
                        ModelType = typeof(CourseAssignmentModel).AssemblyQualifiedName,
                        TextField = "CourseTitle",
                        ValueField = "CourseID",
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
                                        MemberFullName = "Title"
                                    },
                                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                    SelectorParameterName = "d"
                                },
                                SelectorBody = new MemberInitOperatorDescriptor
                                {
                                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                    {
                                        ["CourseID"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "CourseID"
                                        },
                                        ["CourseTitle"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "Title"
                                        }
                                    },
                                    NewType = typeof(CourseAssignmentModel).AssemblyQualifiedName
                                },
                                SelectorParameterName = "s"
                            },
                            SourceElementType = typeof(IQueryable<CourseModel>).AssemblyQualifiedName,
                            ParameterName = "$it",
                            BodyType = typeof(IEnumerable<CourseAssignmentModel>).AssemblyQualifiedName
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(CourseModel).AssemblyQualifiedName,
                            DataType = typeof(Course).AssemblyQualifiedName,
                            ModelReturnType = typeof(IEnumerable<CourseAssignmentModel>).AssemblyQualifiedName,
                            DataReturnType = typeof(IEnumerable<CourseAssignment>).AssemblyQualifiedName
                        }
                    }
                }
            },
            ModelType = "Contoso.Domain.Entities.InstructorModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
        };

        internal static DataFormSettingsDescriptor DepartmentForm = new DataFormSettingsDescriptor
        {
            Title = "Department",
            RequestDetails = new FormRequestDetailsDescriptor
            {
                GetUrl = "/Department/GetSingle"
            },
            ValidationMessages = new List<ValidationMessageDescriptor>
            {
                new ValidationMessageDescriptor
                {
                    Field = "Budget",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Budget is required." },
                        new ValidationRuleDescriptor { ClassName = "MustBePositiveNumberRule", Message = "Budget must be a positive number." }
                    }
                },
                new ValidationMessageDescriptor
                {
                    Field = "InstructorID",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Administrator is required." }
                    }
                },
                new ValidationMessageDescriptor
                {
                    Field = "Name",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Name is required." }
                    }
                },
                new ValidationMessageDescriptor
                {
                    Field = "StartDate",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Start Date is required." }
                    }
                }
            }.ToDictionary(v => v.Field, v => v.Rules),
            FieldSettings = new List<FormItemSettingsDescriptor>
            {
                new FormControlSettingsDescriptor
                {
                    Field = "DepartmentID",
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
                    Field = "Budget",
                    Type = "System.Decimal",
                    Title = "Budget",
                    Placeholder = "Budget (required)",
                    StringFormat = "{0:F2}",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = 0m,
                        Validators = new List<ValidatorDefinitionDescriptor>
                        {
                            new ValidatorDefinitionDescriptor
                            {
                                ClassName = "RequiredRule",
                                FunctionName = "Check"
                            },
                            new ValidatorDefinitionDescriptor
                            {
                                ClassName = "MustBePositiveNumberRule",
                                FunctionName = "Check"
                            }
                        }
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "InstructorID",
                    Type = "System.Int32",
                    Title = "Administrator",
                    Placeholder = "Select Administrator (required)",
                    DropDownTemplate = new DropDownTemplateDescriptor
                    {
                        TemplateName = "PickerTemplate",
                        TitleText = "Select Administrator:",
                        LoadingIndicatorText = "Loading ...",
                        TextField = "FullName",
                        ValueField = "ID",
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
                                        MemberFullName = "FullName"
                                    },
                                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                    SelectorParameterName = "d"
                                },
                                SelectorBody = new MemberInitOperatorDescriptor
                                {
                                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                    {
                                        ["ID"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "ID"
                                        },
                                        ["FirstName"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "FirstName"
                                        },
                                        ["LastName"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "LastName"
                                        },
                                        ["FullName"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "FullName"
                                        }
                                    },
                                    NewType = typeof(InstructorModel).AssemblyQualifiedName
                                },
                                SelectorParameterName = "s"
                            },
                            SourceElementType = typeof(IQueryable<InstructorModel>).AssemblyQualifiedName,
                            ParameterName = "$it",
                            BodyType = typeof(IEnumerable<InstructorModel>).AssemblyQualifiedName
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(InstructorModel).AssemblyQualifiedName,
                            DataType = typeof(Instructor).AssemblyQualifiedName,
                            ModelReturnType = typeof(IEnumerable<InstructorModel>).AssemblyQualifiedName,
                            DataReturnType = typeof(IEnumerable<Instructor>).AssemblyQualifiedName
                        }
                    },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = 0,
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
                    Field = "Name",
                    Type = "System.String",
                    Title = "Name",
                    Placeholder = "Name (required)",
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
                    Field = "StartDate",
                    Type = "System.DateTime",
                    Title = "Start Date",
                    Placeholder = "",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "DateTemplate" },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = new DateTime(1900, 1, 1),
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
                new FormGroupArraySettingsDescriptor
                {
                    Field = "Courses",
                    Placeholder = "(Courses)",
                    FormGroupTemplate = new FormGroupTemplateDescriptor
                    {
                        TemplateName = "FormGroupArrayTemplate"
                    },
                    ValidationMessages= new List<ValidationMessageDescriptor>
                    {
                        new ValidationMessageDescriptor
                        {
                            Field = "CourseID",
                            Rules = new List<ValidationRuleDescriptor>
                            {
                                new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "CourseID is required." },
                                new ValidationRuleDescriptor { ClassName = "MustBeIntegerRule", Message = "CourseID must be an integer." }
                            }
                        },
                        new ValidationMessageDescriptor
                        {
                            Field = "Credits",
                            Rules = new List<ValidationRuleDescriptor>
                            {
                                new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Credits is required." },
                                new ValidationRuleDescriptor { ClassName = "RangeRule", Message = "Credits must be between 0 and 5 inclusive." }
                            }
                        },
                        new ValidationMessageDescriptor
                        {
                            Field = "DepartmentID",
                            Rules = new List<ValidationRuleDescriptor>
                            {
                                new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Department is required.." }
                            }
                        },
                        new ValidationMessageDescriptor
                        {
                            Field = "Title",
                            Rules = new List<ValidationRuleDescriptor>
                            {
                                new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Title is required." }
                            }
                        }
                    }.ToDictionary(v => v.Field, v => v.Rules),
                    FormsCollectionDisplayTemplate = new FormsCollectionDisplayTemplateDescriptor
                    {
                        TemplateName = "TextDetailTemplate",
                        Bindings = new List<ItemBindingDescriptor>
                        {
                            new TextItemBindingDescriptor
                            {
                                Name = "Text",
                                Property = "DepartmentName",
                                StringFormat = "{0}",
                                TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                            },
                            new TextItemBindingDescriptor
                            {
                                Name = "Detail",
                                Property = "Credits",
                                StringFormat = "{0}",
                                TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                            }
                        }.ToDictionary(b => b.Name)
                    },
                    FieldSettings = new List<FormItemSettingsDescriptor>
                    {
                        new FormControlSettingsDescriptor
                        {
                            Field = "CourseID",
                            Type = "System.Int32",
                            Title = "Course",
                            Placeholder = "Course ID (required)",
                            TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                            ValidationSetting = new FieldValidationSettingsDescriptor
                            {
                                DefaultValue = 0,
                                Validators = new List<ValidatorDefinitionDescriptor>
                                {
                                    new ValidatorDefinitionDescriptor
                                    {
                                        ClassName = "RequiredRule",
                                        FunctionName = "Check"
                                    },
                                    new ValidatorDefinitionDescriptor
                                    {
                                        ClassName = "MustBeIntegerRule",
                                        FunctionName = "Check"
                                    }
                                }
                            }
                        },
                        new FormControlSettingsDescriptor
                        {
                            Field = "Credits",
                            Type = "System.Int32",
                            Title = "Credits",
                            Placeholder = "Credits (required)",
                            DropDownTemplate = new DropDownTemplateDescriptor
                            {
                                TemplateName = "PickerTemplate",
                                TitleText = "Select credits:",
                                LoadingIndicatorText = "Loading ...",
                                TextField = "Text",
                                ValueField = "NumericValue",
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
                                                        ConstantValue = "Credits",
                                                        Type = typeof(string).AssemblyQualifiedName
                                                    }
                                                },
                                                FilterParameterName = "l"
                                            },
                                            SelectorBody = new MemberSelectorOperatorDescriptor
                                            {
                                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                MemberFullName = "NumericValue"
                                            },
                                            SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                                            SelectorParameterName = "l"
                                        },
                                        SelectorBody = new MemberInitOperatorDescriptor
                                        {
                                            MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                            {
                                                ["NumericValue"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                    MemberFullName = "NumericValue"
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
                                DefaultValue = 0,
                                Validators = new List<ValidatorDefinitionDescriptor>
                                {
                                    new ValidatorDefinitionDescriptor
                                    {
                                        ClassName = "RequiredRule",
                                        FunctionName = "Check"
                                    },
                                    new ValidatorDefinitionDescriptor
                                    {
                                        ClassName = "RangeRule",
                                        FunctionName = "Check",
                                        Arguments = new List<ValidatorArgumentDescriptor>
                                        {
                                            new ValidatorArgumentDescriptor { Name = "min", Value = 0, Type = "System.Int32" },
                                            new ValidatorArgumentDescriptor { Name = "max", Value = 5, Type = "System.Int32" }
                                        }.ToDictionary(vad => vad.Name)
                                    }
                                }
                            }
                        },
                        new FormControlSettingsDescriptor
                        {
                            Field = "DepartmentID",
                            Type = "System.Int32",
                            Title = "Department",
                            Placeholder = "Department(required)",
                            DropDownTemplate = new DropDownTemplateDescriptor
                            {
                                TemplateName = "PickerTemplate",
                                TitleText = "Select Department:",
                                LoadingIndicatorText = "Loading ...",
                                TextField = "Name",
                                ValueField = "DepartmentID",
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
                                                MemberFullName = "Name"
                                            },
                                            SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                            SelectorParameterName = "d"
                                        },
                                        SelectorBody = new MemberInitOperatorDescriptor
                                        {
                                            MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                            {
                                                ["DepartmentID"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                                                    MemberFullName = "DepartmentID"
                                                },
                                                ["Name"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                                                    MemberFullName = "Name"
                                                }
                                            },
                                            NewType = typeof(DepartmentModel).AssemblyQualifiedName
                                        },
                                        SelectorParameterName = "d"
                                    },
                                    SourceElementType = typeof(IQueryable<DepartmentModel>).AssemblyQualifiedName,
                                    ParameterName = "$it",
                                    BodyType = typeof(IEnumerable<DepartmentModel>).AssemblyQualifiedName
                                },
                                RequestDetails = new RequestDetailsDescriptor
                                {
                                    DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                                    ModelType = typeof(DepartmentModel).AssemblyQualifiedName,
                                    DataType = typeof(Department).AssemblyQualifiedName,
                                    ModelReturnType = typeof(IEnumerable<DepartmentModel>).AssemblyQualifiedName,
                                    DataReturnType = typeof(IEnumerable<Department>).AssemblyQualifiedName
                                }
                            },
                            ValidationSetting = new FieldValidationSettingsDescriptor
                            {
                                DefaultValue = 0,
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
                            Field = "Title",
                            Type = "System.String",
                            Title = "Title",
                            Placeholder = "Title (required)",
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
                        }
                    },
                    Type = typeof(ICollection<CourseModel>).AssemblyQualifiedName,
                    ModelType = "Contoso.Domain.Entities.CourseModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    KeyFields = new List<string> { "CourseID" }
                }
            },
            ModelType = "Contoso.Domain.Entities.DepartmentModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
        };

        internal static DataFormSettingsDescriptor DepartmentFormWithAllItemsGrouped = new DataFormSettingsDescriptor
        {
            Title = "Department",
            RequestDetails = new FormRequestDetailsDescriptor
            {
                GetUrl = "/Department/GetSingle"
            },
            ValidationMessages = new List<ValidationMessageDescriptor>
            {
                new ValidationMessageDescriptor
                {
                    Field = "Budget",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Budget is required." },
                        new ValidationRuleDescriptor { ClassName = "MustBePositiveNumberRule", Message = "Budget must be a positive number." }
                    }
                },
                new ValidationMessageDescriptor
                {
                    Field = "InstructorID",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Administrator is required." }
                    }
                },
                new ValidationMessageDescriptor
                {
                    Field = "Name",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Name is required." }
                    }
                },
                new ValidationMessageDescriptor
                {
                    Field = "StartDate",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Start Date is required." }
                    }
                }
            }.ToDictionary(v => v.Field, v => v.Rules),
            FieldSettings = new List<FormItemSettingsDescriptor>
            {
                new FormGroupBoxSettingsDescriptor
                {
                    GroupHeader = "GroupOne",
                    FieldSettings = new List<FormItemSettingsDescriptor>
                    {
                        new FormControlSettingsDescriptor
                        {
                            Field = "DepartmentID",
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
                            Field = "Budget",
                            Type = "System.Decimal",
                            Title = "Budget",
                            Placeholder = "Budget (required)",
                            StringFormat = "{0:F2}",
                            TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                            ValidationSetting = new FieldValidationSettingsDescriptor
                            {
                                DefaultValue = 0m,
                                Validators = new List<ValidatorDefinitionDescriptor>
                                {
                                    new ValidatorDefinitionDescriptor
                                    {
                                        ClassName = "RequiredRule",
                                        FunctionName = "Check"
                                    },
                                    new ValidatorDefinitionDescriptor
                                    {
                                        ClassName = "MustBePositiveNumberRule",
                                        FunctionName = "Check"
                                    }
                                }
                            }
                        },
                        new FormControlSettingsDescriptor
                        {
                            Field = "InstructorID",
                            Type = "System.Int32",
                            Title = "Administrator",
                            Placeholder = "Select Administrator (required)",
                            DropDownTemplate = new DropDownTemplateDescriptor
                            {
                                TemplateName = "PickerTemplate",
                                TitleText = "Select Administrator:",
                                LoadingIndicatorText = "Loading ...",
                                TextField = "FullName",
                                ValueField = "ID",
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
                                                MemberFullName = "FullName"
                                            },
                                            SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                            SelectorParameterName = "d"
                                        },
                                        SelectorBody = new MemberInitOperatorDescriptor
                                        {
                                            MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                            {
                                                ["ID"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                    MemberFullName = "ID"
                                                },
                                                ["FirstName"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                    MemberFullName = "FirstName"
                                                },
                                                ["LastName"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                    MemberFullName = "LastName"
                                                },
                                                ["FullName"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                    MemberFullName = "FullName"
                                                }
                                            },
                                            NewType = typeof(InstructorModel).AssemblyQualifiedName
                                        },
                                        SelectorParameterName = "s"
                                    },
                                    SourceElementType = typeof(IQueryable<InstructorModel>).AssemblyQualifiedName,
                                    ParameterName = "$it",
                                    BodyType = typeof(IEnumerable<InstructorModel>).AssemblyQualifiedName
                                },
                                RequestDetails = new RequestDetailsDescriptor
                                {
                                    DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                                    ModelType = typeof(InstructorModel).AssemblyQualifiedName,
                                    DataType = typeof(Instructor).AssemblyQualifiedName,
                                    ModelReturnType = typeof(IEnumerable<InstructorModel>).AssemblyQualifiedName,
                                    DataReturnType = typeof(IEnumerable<Instructor>).AssemblyQualifiedName
                                }
                            },
                            ValidationSetting = new FieldValidationSettingsDescriptor
                            {
                                DefaultValue = 0,
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
                    }
                },
                new FormGroupBoxSettingsDescriptor
                {
                    GroupHeader = "GroupTwo",
                    FieldSettings = new List<FormItemSettingsDescriptor>
                    {
                        new FormControlSettingsDescriptor
                        {
                            Field = "Name",
                            Type = "System.String",
                            Title = "Name",
                            Placeholder = "Name (required)",
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
                            Field = "StartDate",
                            Type = "System.DateTime",
                            Title = "Start Date",
                            Placeholder = "",
                            TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "DateTemplate" },
                            ValidationSetting = new FieldValidationSettingsDescriptor
                            {
                                DefaultValue = new DateTime(1900, 1, 1),
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
                        new FormGroupArraySettingsDescriptor
                        {
                            Field = "Courses",
                            Placeholder = "(Courses)",
                            FormGroupTemplate = new FormGroupTemplateDescriptor
                            {
                                TemplateName = "FormGroupArrayTemplate"
                            },
                            ValidationMessages= new List<ValidationMessageDescriptor>
                            {
                                new ValidationMessageDescriptor
                                {
                                    Field = "CourseID",
                                    Rules = new List<ValidationRuleDescriptor>
                                    {
                                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "CourseID is required." },
                                        new ValidationRuleDescriptor { ClassName = "MustBeIntegerRule", Message = "CourseID must be an integer." }
                                    }
                                },
                                new ValidationMessageDescriptor
                                {
                                    Field = "Credits",
                                    Rules = new List<ValidationRuleDescriptor>
                                    {
                                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Credits is required." },
                                        new ValidationRuleDescriptor { ClassName = "RangeRule", Message = "Credits must be between 0 and 5 inclusive." }
                                    }
                                },
                                new ValidationMessageDescriptor
                                {
                                    Field = "DepartmentID",
                                    Rules = new List<ValidationRuleDescriptor>
                                    {
                                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Department is required.." }
                                    }
                                },
                                new ValidationMessageDescriptor
                                {
                                    Field = "Title",
                                    Rules = new List<ValidationRuleDescriptor>
                                    {
                                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Title is required." }
                                    }
                                }
                            }.ToDictionary(v => v.Field, v => v.Rules),
                            FormsCollectionDisplayTemplate = new FormsCollectionDisplayTemplateDescriptor
                            {
                                TemplateName = "TextDetailTemplate",
                                Bindings = new List<ItemBindingDescriptor>
                                {
                                    new TextItemBindingDescriptor
                                    {
                                        Name = "Text",
                                        Property = "DepartmentName",
                                        StringFormat = "{0}",
                                        TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                                    },
                                    new TextItemBindingDescriptor
                                    {
                                        Name = "Detail",
                                        Property = "StartDate",
                                        StringFormat = "{0:MMMM dd, yyyy}",
                                        TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                                    }
                                }.ToDictionary(b => b.Name)
                            },
                            FieldSettings = new List<FormItemSettingsDescriptor>
                            {
                                new FormControlSettingsDescriptor
                                {
                                    Field = "CourseID",
                                    Type = "System.Int32",
                                    Title = "Course",
                                    Placeholder = "Course ID (required)",
                                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                                    ValidationSetting = new FieldValidationSettingsDescriptor
                                    {
                                        DefaultValue = 0,
                                        Validators = new List<ValidatorDefinitionDescriptor>
                                        {
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "RequiredRule",
                                                FunctionName = "Check"
                                            },
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "MustBeIntegerRule",
                                                FunctionName = "Check"
                                            }
                                        }
                                    }
                                },
                                new FormControlSettingsDescriptor
                                {
                                    Field = "Credits",
                                    Type = "System.Int32",
                                    Title = "Credits",
                                    Placeholder = "Credits (required)",
                                    DropDownTemplate = new DropDownTemplateDescriptor
                                    {
                                        TemplateName = "PickerTemplate",
                                        TitleText = "Select credits:",
                                        LoadingIndicatorText = "Loading ...",
                                        TextField = "Text",
                                        ValueField = "NumericValue",
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
                                                                ConstantValue = "Credits",
                                                                Type = typeof(string).AssemblyQualifiedName
                                                            }
                                                        },
                                                        FilterParameterName = "l"
                                                    },
                                                    SelectorBody = new MemberSelectorOperatorDescriptor
                                                    {
                                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                        MemberFullName = "NumericValue"
                                                    },
                                                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                                                    SelectorParameterName = "l"
                                                },
                                                SelectorBody = new MemberInitOperatorDescriptor
                                                {
                                                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                                    {
                                                        ["NumericValue"] = new MemberSelectorOperatorDescriptor
                                                        {
                                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                            MemberFullName = "NumericValue"
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
                                        DefaultValue = 0,
                                        Validators = new List<ValidatorDefinitionDescriptor>
                                        {
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "RequiredRule",
                                                FunctionName = "Check"
                                            },
                                            new ValidatorDefinitionDescriptor
                                            {
                                                ClassName = "RangeRule",
                                                FunctionName = "Check",
                                                Arguments = new List<ValidatorArgumentDescriptor>
                                                {
                                                    new ValidatorArgumentDescriptor { Name = "min", Value = 0, Type = "System.Int32" },
                                                    new ValidatorArgumentDescriptor { Name = "max", Value = 5, Type = "System.Int32" }
                                                }.ToDictionary(vad => vad.Name)
                                            }
                                        }
                                    }
                                },
                                new FormControlSettingsDescriptor
                                {
                                    Field = "DepartmentID",
                                    Type = "System.Int32",
                                    Title = "Department",
                                    Placeholder = "Department(required)",
                                    DropDownTemplate = new DropDownTemplateDescriptor
                                    {
                                        TemplateName = "PickerTemplate",
                                        TitleText = "Select Department:",
                                        LoadingIndicatorText = "Loading ...",
                                        TextField = "Name",
                                        ValueField = "DepartmentID",
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
                                                        MemberFullName = "Name"
                                                    },
                                                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                                    SelectorParameterName = "d"
                                                },
                                                SelectorBody = new MemberInitOperatorDescriptor
                                                {
                                                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                                    {
                                                        ["DepartmentID"] = new MemberSelectorOperatorDescriptor
                                                        {
                                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                                                            MemberFullName = "DepartmentID"
                                                        },
                                                        ["Name"] = new MemberSelectorOperatorDescriptor
                                                        {
                                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                                                            MemberFullName = "Name"
                                                        }
                                                    },
                                                    NewType = typeof(DepartmentModel).AssemblyQualifiedName
                                                },
                                                SelectorParameterName = "d"
                                            },
                                            SourceElementType = typeof(IQueryable<DepartmentModel>).AssemblyQualifiedName,
                                            ParameterName = "$it",
                                            BodyType = typeof(IEnumerable<DepartmentModel>).AssemblyQualifiedName
                                        },
                                        RequestDetails = new RequestDetailsDescriptor
                                        {
                                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                                            ModelType = typeof(DepartmentModel).AssemblyQualifiedName,
                                            DataType = typeof(Department).AssemblyQualifiedName,
                                            ModelReturnType = typeof(IEnumerable<DepartmentModel>).AssemblyQualifiedName,
                                            DataReturnType = typeof(IEnumerable<Department>).AssemblyQualifiedName
                                        }
                                    },
                                    ValidationSetting = new FieldValidationSettingsDescriptor
                                    {
                                        DefaultValue = 0,
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
                                    Field = "Title",
                                    Type = "System.String",
                                    Title = "Title",
                                    Placeholder = "Title (required)",
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
                                }
                            },
                            Type = typeof(ICollection<CourseModel>).AssemblyQualifiedName,
                            ModelType = "Contoso.Domain.Entities.CourseModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                            KeyFields = new List<string> { "CourseID" }
                        }
                    }
                }
            },
            ModelType = "Contoso.Domain.Entities.DepartmentModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
        };

        internal static DataFormSettingsDescriptor DepartmentFormWithSomesItemsGrouped = new DataFormSettingsDescriptor
        {
            Title = "Department",
            RequestDetails = new FormRequestDetailsDescriptor
            {
                GetUrl = "/Department/GetSingle"
            },
            ValidationMessages = new List<ValidationMessageDescriptor>
            {
                new ValidationMessageDescriptor
                {
                    Field = "Budget",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Budget is required." },
                        new ValidationRuleDescriptor { ClassName = "MustBePositiveNumberRule", Message = "Budget must be a positive number." }
                    }
                },
                new ValidationMessageDescriptor
                {
                    Field = "InstructorID",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Administrator is required." }
                    }
                },
                new ValidationMessageDescriptor
                {
                    Field = "Name",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Name is required." }
                    }
                },
                new ValidationMessageDescriptor
                {
                    Field = "StartDate",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Start Date is required." }
                    }
                }
            }.ToDictionary(v => v.Field, v => v.Rules),
            FieldSettings = new List<FormItemSettingsDescriptor>
            {
                new FormGroupBoxSettingsDescriptor
                {
                    GroupHeader = "GroupOne",
                    FieldSettings = new List<FormItemSettingsDescriptor>
                    {
                        new FormControlSettingsDescriptor
                        {
                            Field = "DepartmentID",
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
                            Field = "Budget",
                            Type = "System.Decimal",
                            Title = "Budget",
                            Placeholder = "Budget (required)",
                            StringFormat = "{0:F2}",
                            TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                            ValidationSetting = new FieldValidationSettingsDescriptor
                            {
                                DefaultValue = 0m,
                                Validators = new List<ValidatorDefinitionDescriptor>
                                {
                                    new ValidatorDefinitionDescriptor
                                    {
                                        ClassName = "RequiredRule",
                                        FunctionName = "Check"
                                    },
                                    new ValidatorDefinitionDescriptor
                                    {
                                        ClassName = "MustBePositiveNumberRule",
                                        FunctionName = "Check"
                                    }
                                }
                            }
                        },
                        new FormControlSettingsDescriptor
                        {
                            Field = "InstructorID",
                            Type = "System.Int32",
                            Title = "Administrator",
                            Placeholder = "Select Administrator (required)",
                            DropDownTemplate = new DropDownTemplateDescriptor
                            {
                                TemplateName = "PickerTemplate",
                                TitleText = "Select Administrator:",
                                LoadingIndicatorText = "Loading ...",
                                TextField = "FullName",
                                ValueField = "ID",
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
                                                MemberFullName = "FullName"
                                            },
                                            SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                            SelectorParameterName = "d"
                                        },
                                        SelectorBody = new MemberInitOperatorDescriptor
                                        {
                                            MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                            {
                                                ["ID"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                    MemberFullName = "ID"
                                                },
                                                ["FirstName"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                    MemberFullName = "FirstName"
                                                },
                                                ["LastName"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                    MemberFullName = "LastName"
                                                },
                                                ["FullName"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                    MemberFullName = "FullName"
                                                }
                                            },
                                            NewType = typeof(InstructorModel).AssemblyQualifiedName
                                        },
                                        SelectorParameterName = "s"
                                    },
                                    SourceElementType = typeof(IQueryable<InstructorModel>).AssemblyQualifiedName,
                                    ParameterName = "$it",
                                    BodyType = typeof(IEnumerable<InstructorModel>).AssemblyQualifiedName
                                },
                                RequestDetails = new RequestDetailsDescriptor
                                {
                                    DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                                    ModelType = typeof(InstructorModel).AssemblyQualifiedName,
                                    DataType = typeof(Instructor).AssemblyQualifiedName,
                                    ModelReturnType = typeof(IEnumerable<InstructorModel>).AssemblyQualifiedName,
                                    DataReturnType = typeof(IEnumerable<Instructor>).AssemblyQualifiedName
                                }
                            },
                            ValidationSetting = new FieldValidationSettingsDescriptor
                            {
                                DefaultValue = 0,
                                Validators = new List<ValidatorDefinitionDescriptor>
                                {
                                    new ValidatorDefinitionDescriptor
                                    {
                                        ClassName = "RequiredRule",
                                        FunctionName = "Check"
                                    }
                                }
                            }
                        }
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "Name",
                    Type = "System.String",
                    Title = "Name",
                    Placeholder = "Name (required)",
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
                    Field = "StartDate",
                    Type = "System.DateTime",
                    Title = "Start Date",
                    Placeholder = "",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "DateTemplate" },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = new DateTime(1900, 1, 1),
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
                new FormGroupArraySettingsDescriptor
                {
                    Field = "Courses",
                    Placeholder = "(Courses)",
                    FormGroupTemplate = new FormGroupTemplateDescriptor
                    {
                        TemplateName = "FormGroupArrayTemplate"
                    },
                    ValidationMessages= new List<ValidationMessageDescriptor>
                    {
                        new ValidationMessageDescriptor
                        {
                            Field = "CourseID",
                            Rules = new List<ValidationRuleDescriptor>
                            {
                                new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "CourseID is required." },
                                new ValidationRuleDescriptor { ClassName = "MustBeIntegerRule", Message = "CourseID must be an integer." }
                            }
                        },
                        new ValidationMessageDescriptor
                        {
                            Field = "Credits",
                            Rules = new List<ValidationRuleDescriptor>
                            {
                                new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Credits is required." },
                                new ValidationRuleDescriptor { ClassName = "RangeRule", Message = "Credits must be between 0 and 5 inclusive." }
                            }
                        },
                        new ValidationMessageDescriptor
                        {
                            Field = "DepartmentID",
                            Rules = new List<ValidationRuleDescriptor>
                            {
                                new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Department is required.." }
                            }
                        },
                        new ValidationMessageDescriptor
                        {
                            Field = "Title",
                            Rules = new List<ValidationRuleDescriptor>
                            {
                                new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Title is required." }
                            }
                        }
                    }.ToDictionary(v => v.Field, v => v.Rules),
                    FormsCollectionDisplayTemplate = new FormsCollectionDisplayTemplateDescriptor
                    {
                        TemplateName = "TextDetailTemplate",
                        Bindings = new List<ItemBindingDescriptor>
                        {
                            new TextItemBindingDescriptor
                            {
                                Name = "Text",
                                Property = "DepartmentName",
                                StringFormat = "{0}",
                                TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                            },
                            new TextItemBindingDescriptor
                            {
                                Name = "Detail",
                                Property = "StartDate",
                                StringFormat = "{0:MMMM dd, yyyy}",
                                TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                            }
                        }.ToDictionary(b => b.Name)
                    },
                    FieldSettings = new List<FormItemSettingsDescriptor>
                    {
                        new FormControlSettingsDescriptor
                        {
                            Field = "CourseID",
                            Type = "System.Int32",
                            Title = "Course",
                            Placeholder = "Course ID (required)",
                            TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                            ValidationSetting = new FieldValidationSettingsDescriptor
                            {
                                DefaultValue = 0,
                                Validators = new List<ValidatorDefinitionDescriptor>
                                {
                                    new ValidatorDefinitionDescriptor
                                    {
                                        ClassName = "RequiredRule",
                                        FunctionName = "Check"
                                    },
                                    new ValidatorDefinitionDescriptor
                                    {
                                        ClassName = "MustBeIntegerRule",
                                        FunctionName = "Check"
                                    }
                                }
                            }
                        },
                        new FormControlSettingsDescriptor
                        {
                            Field = "Credits",
                            Type = "System.Int32",
                            Title = "Credits",
                            Placeholder = "Credits (required)",
                            DropDownTemplate = new DropDownTemplateDescriptor
                            {
                                TemplateName = "PickerTemplate",
                                TitleText = "Select credits:",
                                LoadingIndicatorText = "Loading ...",
                                TextField = "Text",
                                ValueField = "NumericValue",
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
                                                        ConstantValue = "Credits",
                                                        Type = typeof(string).AssemblyQualifiedName
                                                    }
                                                },
                                                FilterParameterName = "l"
                                            },
                                            SelectorBody = new MemberSelectorOperatorDescriptor
                                            {
                                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                MemberFullName = "NumericValue"
                                            },
                                            SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                                            SelectorParameterName = "l"
                                        },
                                        SelectorBody = new MemberInitOperatorDescriptor
                                        {
                                            MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                            {
                                                ["NumericValue"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                    MemberFullName = "NumericValue"
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
                                DefaultValue = 0,
                                Validators = new List<ValidatorDefinitionDescriptor>
                                {
                                    new ValidatorDefinitionDescriptor
                                    {
                                        ClassName = "RequiredRule",
                                        FunctionName = "Check"
                                    },
                                    new ValidatorDefinitionDescriptor
                                    {
                                        ClassName = "RangeRule",
                                        FunctionName = "Check",
                                        Arguments = new List<ValidatorArgumentDescriptor>
                                        {
                                            new ValidatorArgumentDescriptor { Name = "min", Value = 0, Type = "System.Int32" },
                                            new ValidatorArgumentDescriptor { Name = "max", Value = 5, Type = "System.Int32" }
                                        }.ToDictionary(vad => vad.Name)
                                    }
                                }
                            }
                        },
                        new FormControlSettingsDescriptor
                        {
                            Field = "DepartmentID",
                            Type = "System.Int32",
                            Title = "Department",
                            Placeholder = "Department(required)",
                            DropDownTemplate = new DropDownTemplateDescriptor
                            {
                                TemplateName = "PickerTemplate",
                                TitleText = "Select Department:",
                                LoadingIndicatorText = "Loading ...",
                                TextField = "Name",
                                ValueField = "DepartmentID",
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
                                                MemberFullName = "Name"
                                            },
                                            SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                            SelectorParameterName = "d"
                                        },
                                        SelectorBody = new MemberInitOperatorDescriptor
                                        {
                                            MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                            {
                                                ["DepartmentID"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                                                    MemberFullName = "DepartmentID"
                                                },
                                                ["Name"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                                                    MemberFullName = "Name"
                                                }
                                            },
                                            NewType = typeof(DepartmentModel).AssemblyQualifiedName
                                        },
                                        SelectorParameterName = "d"
                                    },
                                    SourceElementType = typeof(IQueryable<DepartmentModel>).AssemblyQualifiedName,
                                    ParameterName = "$it",
                                    BodyType = typeof(IEnumerable<DepartmentModel>).AssemblyQualifiedName
                                },
                                RequestDetails = new RequestDetailsDescriptor
                                {
                                    DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                                    ModelType = typeof(DepartmentModel).AssemblyQualifiedName,
                                    DataType = typeof(Department).AssemblyQualifiedName,
                                    ModelReturnType = typeof(IEnumerable<DepartmentModel>).AssemblyQualifiedName,
                                    DataReturnType = typeof(IEnumerable<Department>).AssemblyQualifiedName
                                }
                            },
                            ValidationSetting = new FieldValidationSettingsDescriptor
                            {
                                DefaultValue = 0,
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
                            Field = "Title",
                            Type = "System.String",
                            Title = "Title",
                            Placeholder = "Title (required)",
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
                        }
                    },
                    Type = typeof(ICollection<CourseModel>).AssemblyQualifiedName,
                    ModelType = "Contoso.Domain.Entities.CourseModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    KeyFields = new List<string> { "CourseID" }
                }
            },
            ModelType = "Contoso.Domain.Entities.DepartmentModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
        };

        internal static DataFormSettingsDescriptor CourseForm = new DataFormSettingsDescriptor
        {
            Title = "Course",
            RequestDetails = new FormRequestDetailsDescriptor
            {
                GetUrl = "/Course/GetSingle"
            },
            ValidationMessages = new List<ValidationMessageDescriptor>
            {
                new ValidationMessageDescriptor
                {
                    Field = "CourseID",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "CourseID is required." },
                        new ValidationRuleDescriptor { ClassName = "MustBeIntegerRule", Message = "CourseID must be an integer." }
                    }
                },
                new ValidationMessageDescriptor
                {
                    Field = "Credits",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Credits is required." },
                        new ValidationRuleDescriptor { ClassName = "RangeRule", Message = "Credits must be between 0 and 5 inclusive." }
                    }
                },
                new ValidationMessageDescriptor
                {
                    Field = "DepartmentID",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Department is required.." }
                    }
                },
                new ValidationMessageDescriptor
                {
                    Field = "Title",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Title is required." }
                    }
                }
            }.ToDictionary(v => v.Field, v => v.Rules),
            FieldSettings = new List<FormItemSettingsDescriptor>
            {
                new FormControlSettingsDescriptor
                {
                    Field = "CourseID",
                    Type = "System.Int32",
                    Title = "Course",
                    Placeholder = "Course ID (required)",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                    UpdateOnlyTextTemplate = new TextFieldTemplateDescriptor { TemplateName = "LabelTemplate" },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = 0,
                        Validators = new List<ValidatorDefinitionDescriptor>
                        {
                            new ValidatorDefinitionDescriptor
                            {
                                ClassName = "RequiredRule",
                                FunctionName = "Check"
                            },
                            new ValidatorDefinitionDescriptor
                            {
                                ClassName = "MustBeIntegerRule",
                                FunctionName = "Check"
                            }
                        }
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "Credits",
                    Type = "System.Int32",
                    Title = "Credits",
                    Placeholder = "Credits (required)",
                    DropDownTemplate = new DropDownTemplateDescriptor
                    {
                        TemplateName = "PickerTemplate",
                        TitleText = "Select credits:",
                        LoadingIndicatorText = "Loading ...",
                        TextField = "Text",
                        ValueField = "NumericValue",
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
                                                ConstantValue = "Credits",
                                                Type = typeof(string).AssemblyQualifiedName
                                            }
                                        },
                                        FilterParameterName = "l"
                                    },
                                    SelectorBody = new MemberSelectorOperatorDescriptor
                                    {
                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                        MemberFullName = "NumericValue"
                                    },
                                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                                    SelectorParameterName = "l"
                                },
                                SelectorBody = new MemberInitOperatorDescriptor
                                {
                                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                    {
                                        ["NumericValue"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "NumericValue"
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
                        DefaultValue = 0,
                        Validators = new List<ValidatorDefinitionDescriptor>
                        {
                            new ValidatorDefinitionDescriptor
                            {
                                ClassName = "RequiredRule",
                                FunctionName = "Check"
                            },
                            new ValidatorDefinitionDescriptor
                            {
                                ClassName = "RangeRule",
                                FunctionName = "Check",
                                Arguments = new List<ValidatorArgumentDescriptor>
                                {
                                    new ValidatorArgumentDescriptor { Name = "min", Value = 0, Type = "System.Int32" },
                                    new ValidatorArgumentDescriptor { Name = "max", Value = 5, Type = "System.Int32" }
                                }.ToDictionary(v => v.Name)
                            }
                        }
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "DepartmentID",
                    Type = "System.Int32",
                    Title = "Department",
                    Placeholder = "Department(required)",
                    DropDownTemplate = new DropDownTemplateDescriptor
                    {
                        TemplateName = "PickerTemplate",
                        TitleText = "Select Department:",
                        LoadingIndicatorText = "Loading ...",
                        TextField = "Name",
                        ValueField = "DepartmentID",
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
                                        MemberFullName = "Name"
                                    },
                                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                    SelectorParameterName = "d"
                                },
                                SelectorBody = new MemberInitOperatorDescriptor
                                {
                                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                    {
                                        ["DepartmentID"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                                            MemberFullName = "DepartmentID"
                                        },
                                        ["Name"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                                            MemberFullName = "Name"
                                        }
                                    },
                                    NewType = typeof(DepartmentModel).AssemblyQualifiedName
                                },
                                SelectorParameterName = "d"
                            },
                            SourceElementType = typeof(IQueryable<DepartmentModel>).AssemblyQualifiedName,
                            ParameterName = "$it",
                            BodyType = typeof(IEnumerable<DepartmentModel>).AssemblyQualifiedName
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(DepartmentModel).AssemblyQualifiedName,
                            DataType = typeof(Department).AssemblyQualifiedName,
                            ModelReturnType = typeof(IEnumerable<DepartmentModel>).AssemblyQualifiedName,
                            DataReturnType = typeof(IEnumerable<Department>).AssemblyQualifiedName
                        }
                    },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = 0,
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
                    Field = "Title",
                    Type = "System.String",
                    Title = "Title",
                    Placeholder = "Title (required)",
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
                }
            },
            ModelType = "Contoso.Domain.Entities.CourseModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
        };

        internal static DataFormSettingsDescriptor StudentForm = new DataFormSettingsDescriptor
        {
            Title = "Student",
            RequestDetails = new FormRequestDetailsDescriptor
            {
                GetUrl = "/Student/GetSingle"
            },
            ValidationMessages = new List<ValidationMessageDescriptor>
            {
                new ValidationMessageDescriptor
                {
                    Field = "FirstName",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "First Name is required." }
                    }
                },
                new ValidationMessageDescriptor
                {
                    Field = "LastName",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Last Name is required." }
                    }
                },
                new ValidationMessageDescriptor
                {
                    Field = "EnrollmentDate",
                    Rules = new List<ValidationRuleDescriptor>
                    {
                        new ValidationRuleDescriptor { ClassName = "RequiredRule", Message = "Enrollment Date is required." }
                    }
                }
            }.ToDictionary(v => v.Field, v => v.Rules),
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
                    Field = "EnrollmentDate",
                    Type = "System.DateTime",
                    Title = "Enrollment Date",
                    Placeholder = "",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "DateTemplate" },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = new DateTime(1900, 1, 1),
                        Validators = new List<ValidatorDefinitionDescriptor>
                        {
                            new ValidatorDefinitionDescriptor
                            {
                                ClassName = "RequiredRule",
                                FunctionName = "Check"
                            }
                        }
                    }
                }
            },
            ConditionalDirectives = new List<Forms.Configuration.Directives.VariableDirectivesDescriptor>
            {
                new Forms.Configuration.Directives.VariableDirectivesDescriptor
                {
                    Field = "EnrollmentDate",
                    ConditionalDirectives = new List<Forms.Configuration.Directives.DirectiveDescriptor>
                    {
                        new Forms.Configuration.Directives.DirectiveDescriptor
                        {
                            Definition = new Forms.Configuration.Directives.DirectiveDefinitionDescriptor
                            {
                                ClassName = "ValidateIf",
                                FunctionName = "Check"
                            },
                            Condition = new Common.Configuration.ExpressionDescriptors.FilterLambdaOperatorDescriptor
                            {
                                SourceElementType = typeof(Domain.Entities.StudentModel).AssemblyQualifiedName,
                                ParameterName = "f",
                                FilterBody = new Common.Configuration.ExpressionDescriptors.EqualsBinaryOperatorDescriptor
                                {
                                    Left = new Common.Configuration.ExpressionDescriptors.MemberSelectorOperatorDescriptor
                                    {
                                        MemberFullName = "FirstName",
                                        SourceOperand = new Common.Configuration.ExpressionDescriptors.ParameterOperatorDescriptor{ ParameterName = "f" }
                                    },
                                    Right = new Common.Configuration.ExpressionDescriptors.MemberSelectorOperatorDescriptor
                                    {
                                        MemberFullName = "LastName",
                                        SourceOperand = new Common.Configuration.ExpressionDescriptors.ParameterOperatorDescriptor{ ParameterName = "f" }
                                    }
                                }
                            }
                        }
                    }
                }
            }.ToDictionary(vd => vd.Field, vd => vd.ConditionalDirectives),
            ModelType = "Contoso.Domain.Entities.StudentModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
        };

        internal static IList<CommandButtonDescriptor> ButtonDescriptors = new List<CommandButtonDescriptor>
        {
            new CommandButtonDescriptor { Id = 1, LongString = "Save", ShortString = "S", Command = "SubmitCommand", ButtonIcon = "Save" },
            new CommandButtonDescriptor { Id = 2, LongString = "Home", ShortString = "H", Command = "NavigateCommand", ButtonIcon = "Home" }
        };


        internal static NavigationBarDescriptor GetNavigationBar(string currentModule) => new NavigationBarDescriptor
        {
            BrandText = "Contoso",
            CurrentModule = currentModule,
            MenuItems = new List<NavigationMenuItemDescriptor>
            {
                new NavigationMenuItemDescriptor
                {
                    InitialModule = "home",
                    Icon = "Home",
                    Text = "Home"
                },
                new NavigationMenuItemDescriptor
                {
                    InitialModule = "about",
                    Icon = "University",
                    Text = "About"
                },
                new NavigationMenuItemDescriptor
                {
                    InitialModule = "students",
                    Icon = "Users",
                    Text = "Students"
                },
                new NavigationMenuItemDescriptor
                {
                    InitialModule = "courses",
                    Icon = "BookOpen",
                    Text = "Courses"
                },
                new NavigationMenuItemDescriptor
                {
                    InitialModule = "departments",
                    Icon = "Building",
                    Text = "Departments"
                },
                new NavigationMenuItemDescriptor
                {
                    InitialModule = "instructors",
                    Icon = "ChalkboardTeacher",
                    Text = "Instructors"
                }
            }
        };
    }
}
