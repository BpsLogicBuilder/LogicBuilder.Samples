using Contoso.Common.Configuration.ExpansionDescriptors;
using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.Bindings;
using Contoso.Forms.Configuration.DataForm;
using Contoso.Forms.Configuration.ListForm;
using Contoso.Forms.Configuration.Navigation;
using Contoso.Forms.Configuration.SearchForm;
using Contoso.Forms.Configuration.TextForm;
using Contoso.Forms.Configuration.Validation;
using Contoso.XPlatform.Flow.Cache;
using Contoso.XPlatform.Flow.Settings;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.XPlatform
{
    internal static class Descriptors
    {
        internal static DataFormSettingsDescriptor InstructorForm = new DataFormSettingsDescriptor
        {
            Title = "Instructor",
            FormType = FormType.Update,
            RequestDetails = new FormRequestDetailsDescriptor
            {
                GetUrl = "/Instructor/GetSingle",
                ModelType = typeof(InstructorModel).AssemblyQualifiedName,
                DataType = typeof(Instructor).AssemblyQualifiedName,
                Filter = new FilterLambdaOperatorDescriptor
                {
                    FilterBody = new EqualsBinaryOperatorDescriptor
                    {
                        Left = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "f" },
                            MemberFullName = "ID"
                        },
                        Right = new ConstantOperatorDescriptor { Type = typeof(int).FullName, ConstantValue = 3 }
                    },
                    SourceElementType = typeof(InstructorModel).AssemblyQualifiedName,
                    ParameterName = "f"
                },
                SelectExpandDefinition = new Common.Configuration.ExpansionDescriptors.SelectExpandDefinitionDescriptor
                {
                    ExpandedItems = new List<Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor>
                    {
                        new Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor
                        {
                            MemberName = "Courses"
                        },
                        new Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor
                        {
                            MemberName = "OfficeAssignment"
                        }
                    }
                }
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
                            SourceElementType = typeof(CourseModel).GetIQueryableTypeString(),
                            ParameterName = "$it",
                            BodyType = typeof(CourseAssignmentModel).GetIEnumerableTypeString()
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(CourseModel).AssemblyQualifiedName,
                            DataType = typeof(Course).AssemblyQualifiedName,
                            ModelReturnType = typeof(CourseAssignmentModel).GetIEnumerableTypeString(),
                            DataReturnType = typeof(CourseAssignment).GetIEnumerableTypeString()
                        }
                    }
                }
            },
            ModelType = "Contoso.Domain.Entities.InstructorModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
        };

        internal static DataFormSettingsDescriptor InstructorFormWithPopupOfficeAssignment = new DataFormSettingsDescriptor
        {
            Title = "Instructor",
            FormType = FormType.Update,
            RequestDetails = new FormRequestDetailsDescriptor
            {
                GetUrl = "/Instructor/GetSingle",
                ModelType = typeof(InstructorModel).AssemblyQualifiedName,
                DataType = typeof(Instructor).AssemblyQualifiedName,
                Filter = new FilterLambdaOperatorDescriptor
                {
                    FilterBody = new EqualsBinaryOperatorDescriptor
                    {
                        Left = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "f" },
                            MemberFullName = "ID"
                        },
                        Right = new ConstantOperatorDescriptor { Type = typeof(int).FullName, ConstantValue = 3 }
                    },
                    SourceElementType = typeof(InstructorModel).AssemblyQualifiedName,
                    ParameterName = "f"
                },
                SelectExpandDefinition = new Common.Configuration.ExpansionDescriptors.SelectExpandDefinitionDescriptor
                {
                    ExpandedItems = new List<Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor>
                    {
                        new Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor
                        {
                            MemberName = "Courses"
                        },
                        new Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor
                        {
                            MemberName = "OfficeAssignment"
                        }
                    }
                }
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
                    InvalidFormControlText = "(Invalid Form)",
                    ValidFormControlText = "(Form)",
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
                    Title = "Office Assignment"
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
                            SourceElementType = typeof(CourseModel).GetIQueryableTypeString(),
                            ParameterName = "$it",
                            BodyType = typeof(CourseAssignmentModel).GetIEnumerableTypeString()
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(CourseModel).AssemblyQualifiedName,
                            DataType = typeof(Course).AssemblyQualifiedName,
                            ModelReturnType = typeof(CourseAssignmentModel).GetIEnumerableTypeString(),
                            DataReturnType = typeof(CourseAssignment).GetIEnumerableTypeString()
                        }
                    }
                }
            },
            ModelType = typeof(InstructorModel).AssemblyQualifiedName,
        };

        internal static DataFormSettingsDescriptor DepartmentForm = new DataFormSettingsDescriptor
        {
            Title = "Department",
            FormType = FormType.Update,
            RequestDetails = new FormRequestDetailsDescriptor
            {
                GetUrl = "/Department/GetSingle",
                ModelType = typeof(DepartmentModel).AssemblyQualifiedName,
                DataType = typeof(Department).AssemblyQualifiedName,
                Filter = new FilterLambdaOperatorDescriptor
                {
                    FilterBody = new EqualsBinaryOperatorDescriptor
                    {
                        Left = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "f" },
                            MemberFullName = "DepartmentID"
                        },
                        Right = new ConstantOperatorDescriptor { Type = typeof(int).FullName, ConstantValue = 1 }
                    },
                    SourceElementType = typeof(DepartmentModel).AssemblyQualifiedName,
                    ParameterName = "f"
                }
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
                            SourceElementType = typeof(InstructorModel).GetIQueryableTypeString(),
                            ParameterName = "$it",
                            BodyType = typeof(InstructorModel).GetIEnumerableTypeString()
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(InstructorModel).AssemblyQualifiedName,
                            DataType = typeof(Instructor).AssemblyQualifiedName,
                            ModelReturnType = typeof(InstructorModel).GetIEnumerableTypeString(),
                            DataReturnType = typeof(Instructor).GetIEnumerableTypeString()
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
                }
            },
            ModelType = typeof(DepartmentModel).AssemblyQualifiedName
        };

        internal static DataFormSettingsDescriptor DepartmentFormWithCourses = new DataFormSettingsDescriptor
        {
            Title = "Department",
            FormType = FormType.Update,
            RequestDetails = new FormRequestDetailsDescriptor
            {
                GetUrl = "/Department/GetSingle",
                ModelType = typeof(DepartmentModel).AssemblyQualifiedName,
                DataType = typeof(Department).AssemblyQualifiedName,
                Filter = new FilterLambdaOperatorDescriptor
                {
                    FilterBody = new EqualsBinaryOperatorDescriptor
                    {
                        Left = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "f" },
                            MemberFullName = "DepartmentID"
                        },
                        Right = new ConstantOperatorDescriptor { Type = typeof(int).FullName, ConstantValue = 2 }
                    },
                    SourceElementType = typeof(DepartmentModel).AssemblyQualifiedName,
                    ParameterName = "f"
                },
                SelectExpandDefinition = new Common.Configuration.ExpansionDescriptors.SelectExpandDefinitionDescriptor
                {
                    ExpandedItems = new List<Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor>
                    {
                        new Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor
                        {
                            MemberName = "Courses"
                        }
                    }
                }
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
                            SourceElementType = typeof(InstructorModel).GetIQueryableTypeString(),
                            ParameterName = "$it",
                            BodyType = typeof(InstructorModel).GetIEnumerableTypeString()
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(InstructorModel).AssemblyQualifiedName,
                            DataType = typeof(Instructor).AssemblyQualifiedName,
                            ModelReturnType = typeof(InstructorModel).GetIEnumerableTypeString(),
                            DataReturnType = typeof(Instructor).GetIEnumerableTypeString()
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
                    Title = "Courses",
                    Placeholder = "(Courses)",
                    FormGroupTemplate = new FormGroupTemplateDescriptor
                    {
                        TemplateName = "FormGroupArrayTemplate"
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
                    FormsCollectionDisplayTemplate = new FormsCollectionDisplayTemplateDescriptor
                    {
                        TemplateName = "TextDetailTemplate",
                        Bindings = new List<ItemBindingDescriptor>
                        {
                            new TextItemBindingDescriptor
                            {
                                Name = "Header",
                                Property = "CourseID",
                                StringFormat = "ID {0}",
                                TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                            },
                            new TextItemBindingDescriptor
                            {
                                Name = "Text",
                                Property = "Title",
                                StringFormat = "{0}",
                                TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                            },
                            new TextItemBindingDescriptor
                            {
                                Name = "Detail",
                                Property = "Credits",
                                StringFormat = "Credits: {0}",
                                TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                            }
                        }.ToDictionary(cvib => cvib.Name)
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
                                    SourceElementType = typeof(LookUpsModel).GetIQueryableTypeString(),
                                    ParameterName = "$it",
                                    BodyType = typeof(LookUpsModel).GetIEnumerableTypeString()
                                },
                                RequestDetails = new RequestDetailsDescriptor
                                {
                                    DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                                    ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                                    DataType = typeof(LookUps).AssemblyQualifiedName,
                                    ModelReturnType = typeof(LookUpsModel).GetIEnumerableTypeString(),
                                    DataReturnType = typeof(LookUps).GetIEnumerableTypeString()
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
                                    SourceElementType = typeof(DepartmentModel).GetIQueryableTypeString(),
                                    ParameterName = "$it",
                                    BodyType = typeof(DepartmentModel).GetIEnumerableTypeString()
                                },
                                RequestDetails = new RequestDetailsDescriptor
                                {
                                    DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                                    ModelType = typeof(DepartmentModel).AssemblyQualifiedName,
                                    DataType = typeof(Department).AssemblyQualifiedName,
                                    ModelReturnType = typeof(DepartmentModel).GetIEnumerableTypeString(),
                                    DataReturnType = typeof(Department).GetIEnumerableTypeString()
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
                    ModelType = "Contoso.Domain.Entities.CourseModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    KeyFields = new List<string> { "CourseID" }
                }
            },
            ModelType = typeof(DepartmentModel).AssemblyQualifiedName
        };

        internal static DataFormSettingsDescriptor CourseForm = new DataFormSettingsDescriptor
        {
            Title = "Course",
            FormType = FormType.Update,
            RequestDetails = new FormRequestDetailsDescriptor
            {
                GetUrl = "/Course/GetSingle",
                ModelType = typeof(CourseModel).AssemblyQualifiedName,
                DataType = typeof(Course).AssemblyQualifiedName,
                Filter = new FilterLambdaOperatorDescriptor
                {
                    FilterBody = new EqualsBinaryOperatorDescriptor
                    {
                        Left = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "f" },
                            MemberFullName = "CourseID"
                        },
                        Right = new ConstantOperatorDescriptor { Type = typeof(int).FullName, ConstantValue = 1050 }
                    },
                    SourceElementType = typeof(CourseModel).AssemblyQualifiedName,
                    ParameterName = "f"
                }
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
                            SourceElementType = typeof(LookUpsModel).GetIQueryableTypeString(),
                            ParameterName = "$it",
                            BodyType = typeof(LookUpsModel).GetIEnumerableTypeString()
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                            DataType = typeof(LookUps).AssemblyQualifiedName,
                            ModelReturnType = typeof(LookUpsModel).GetIEnumerableTypeString(),
                            DataReturnType = typeof(LookUps).GetIEnumerableTypeString()
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
                            SourceElementType = typeof(DepartmentModel).GetIQueryableTypeString(),
                            ParameterName = "$it",
                            BodyType = typeof(DepartmentModel).GetIEnumerableTypeString()
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(DepartmentModel).AssemblyQualifiedName,
                            DataType = typeof(Department).AssemblyQualifiedName,
                            ModelReturnType = typeof(DepartmentModel).GetIEnumerableTypeString(),
                            DataReturnType = typeof(Department).GetIEnumerableTypeString()
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
            ModelType = typeof(CourseModel).AssemblyQualifiedName
        };

        internal static DataFormSettingsDescriptor StudentForm = new DataFormSettingsDescriptor
        {
            Title = "Student",
            FormType = FormType.Update,
            RequestDetails = new FormRequestDetailsDescriptor
            {
                GetUrl = "/Student/GetSingle",
                ModelType = typeof(StudentModel).AssemblyQualifiedName,
                DataType = typeof(Student).AssemblyQualifiedName,
                Filter = new FilterLambdaOperatorDescriptor
                {
                    FilterBody = new EqualsBinaryOperatorDescriptor
                    {
                        Left = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "f" },
                            MemberFullName = "ID"
                        },
                        Right = new ConstantOperatorDescriptor { Type = typeof(int).FullName, ConstantValue = 1}
                    },
                    SourceElementType = typeof(StudentModel).AssemblyQualifiedName,
                    ParameterName = "f"
                }
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
            ModelType = typeof(StudentModel).AssemblyQualifiedName
        };

        internal static SearchFormSettingsDescriptor StudentSearchForm = new SearchFormSettingsDescriptor
        {
            Title = "Student",
            ModelType = typeof(StudentModel).AssemblyQualifiedName,
            LoadingIndicatorText = "Loading ...",
            FilterPlaceholder = "Filter",
            ItemTemplateName = "TextDetailTemplate",
            Bindings = new List<ItemBindingDescriptor>
            {
                new TextItemBindingDescriptor
                {
                    Name = "Header",
                    Property = "ID",
                    StringFormat = "ID {0}",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                },
                new TextItemBindingDescriptor
                {
                    Name = "Text",
                    Property = "FullName",
                    StringFormat = "{0}",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                },
                new TextItemBindingDescriptor
                {
                    Name = "Detail",
                    Property = "EnrollmentDate",
                    StringFormat = "Enrollment Date: {0:MM/dd/yyyy}",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                }
            }.ToDictionary(cvib => cvib.Name),
            SortCollection = new SortCollectionDescriptor
            {
                SortDescriptions = new List<SortDescriptionDescriptor>
                {
                    new SortDescriptionDescriptor
                    {
                        PropertyName = "EnrollmentDate",
                        SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending
                    }
                },
                Take = 2
            },
            SearchFilterGroup = new SearchFilterGroupDescriptor
            {
                Filters = new List<SearchFilterDescriptorBase>
                {
                    new SearchFilterDescriptor { Field = "EnrollmentDateString" },
                    new SearchFilterGroupDescriptor
                    {
                        Filters = new List<SearchFilterDescriptorBase>
                        {
                            new SearchFilterDescriptor { Field = "FirstName" },
                            new SearchFilterDescriptor { Field = "LastName" }
                        }
                    }
                }
            },
            RequestDetails = new RequestDetailsDescriptor
            {
                DataSourceUrl = "api/List/GetList",
                ModelType = typeof(StudentModel).AssemblyQualifiedName,
                DataType = typeof(Student).AssemblyQualifiedName,
                ModelReturnType = typeof(StudentModel).GetIQueryableTypeString(),
                DataReturnType = typeof(Student).GetIQueryableTypeString()
            }
        };

        internal static ListFormSettingsDescriptor AboutListForm = new ListFormSettingsDescriptor
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
                    StringFormat = "Enrollment Date: {0:MM/dd/yyyy}",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                },
                new TextItemBindingDescriptor
                {
                    Name = "Detail",
                    Property = "NumericValue",
                    StringFormat = "Count: {0:f0}",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                }
            }.ToDictionary(cvib => cvib.Name),
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
                                ParameterName = "$it"
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
                                    SourceOperand = new AsEnumerableOperatorDescriptor()
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
                SourceElementType = typeof(StudentModel).GetIQueryableTypeString(),
                ParameterName = "$it",
                BodyType = typeof(LookUpsModel).GetIQueryableTypeString()
            },
            RequestDetails = new RequestDetailsDescriptor
            {
                DataSourceUrl = "api/List/GetList",
                ModelType = typeof(StudentModel).AssemblyQualifiedName,
                DataType = typeof(Student).AssemblyQualifiedName,
                ModelReturnType = typeof(LookUpsModel).GetIQueryableTypeString(),
                DataReturnType = typeof(LookUps).GetIQueryableTypeString()
            }
        };

        internal static TextFormSettingsDescriptor HomeTextForm = new TextFormSettingsDescriptor
        {
            Title = "Home",
            TextGroups = new List<TextGroupDescriptor>
            {
                new TextGroupDescriptor
                {
                    Title = "Welcome to Contoso University",
                    Labels = new List<LabelItemDescriptorBase>
                    {
                        new LabelItemDescriptor
                        {
                            Text = "Contoso is a sample application generated from a workflow.  The user interface and LINQ queries are dynamically generated from workflow data."
                        }
                    }
                },
                new TextGroupDescriptor
                {
                    Title = "Logic Builder",
                    Labels = new List<LabelItemDescriptorBase>
                    {
                        new FormattedLabelItemDescriptor
                        {
                            Items = new List<SpanItemDescriptorBase>
                            {
                                new SpanItemDescriptor { Text = "The 32-bit Logic Builder can be dowloaded from: " },
                                new HyperLinkSpanItemDescriptor
                                {
                                    Text = "here",
                                    Url = "https://www.microsoft.com/en-us/p/bps-logic-builder-32-bit-visio/9ngkp83g750j"
                                },
                                new SpanItemDescriptor { Text = ". and the 64-bit Logic Builder can be dowloaded from: " },
                                new HyperLinkSpanItemDescriptor
                                {
                                    Text = "here",
                                    Url = "https://www.microsoft.com/en-us/p/bps-logic-builder-64-bit-visio/9pbq81mnwhlx"
                                },
                                new SpanItemDescriptor { Text = ".  Select 32-bit or 64-bit depending on your installed version of Visio." }
                            }
                        }
                    }
                },new TextGroupDescriptor
                {
                    Title = "Source Code",
                    Labels = new List<LabelItemDescriptorBase>
                    {
                        new FormattedLabelItemDescriptor
                        {
                            Items = new List<SpanItemDescriptorBase>
                            {
                                new SpanItemDescriptor { Text = "Please find the " },
                                new HyperLinkSpanItemDescriptor
                                {
                                    Text = "completed Visual Studio Solution",
                                    Url = "https://github.com/BlaiseD/Contoso.XPlatform"
                                },
                                new SpanItemDescriptor { Text = " on GitHub." }
                            }
                        }
                    }
                }
            }
        };

        internal static IList<CommandButtonDescriptor> ButtonDescriptors = new List<CommandButtonDescriptor>
        {
            new CommandButtonDescriptor { Id = 1, LongString = "Save", ShortString = "S", Command = "SubmitCommand", ButtonIcon = "Save" },
            new CommandButtonDescriptor { Id = 2, LongString = "Home", ShortString = "H", Command = "NavigateCommand", ButtonIcon = "Home" }
        };

        internal static IList<CommandButtonDescriptor> ListFormButtonDescriptors = new List<CommandButtonDescriptor>
        {
        };

        internal static IList<CommandButtonDescriptor> TextFormButtonDescriptors = new List<CommandButtonDescriptor>
        {
            new CommandButtonDescriptor { Id = 2, LongString = "Home", ShortString = "H", Command = "NavigateCommand", ButtonIcon = "Home" }
        };

        internal static IList<CommandButtonDescriptor> SearchFormButtonDescriptors = new List<CommandButtonDescriptor>
        {
            new CommandButtonDescriptor { Id = 1, LongString = "Add", ShortString = "A", Command = "AddCommand", ButtonIcon = "Plus" },
            new CommandButtonDescriptor { Id = 2, LongString = "Edit", ShortString = "E", Command = "EditCommand", ButtonIcon = "Edit" },
            new CommandButtonDescriptor { Id = 3, LongString = "Detail", ShortString = "D0", Command = "DetailCommand", ButtonIcon = "Info" },
            new CommandButtonDescriptor { Id = 4, LongString = "Delete", ShortString = "D1", Command = "DeleteCommand", ButtonIcon = "TrashAlt" }
        };

        internal static ScreenSettingsBase GetScreenSettings(string moduleName)
        {
            if (moduleName == "about")
                return new ScreenSettings<ListFormSettingsDescriptor>(AboutListForm, ListFormButtonDescriptors, ViewType.ListPage);
            else if (moduleName == "home")
                return new ScreenSettings<TextFormSettingsDescriptor>(HomeTextForm, TextFormButtonDescriptors, ViewType.TextPage);
            else if (moduleName == "students")
                return new ScreenSettings<DataFormSettingsDescriptor>(StudentForm, ButtonDescriptors, ViewType.EditForm);
                //return new ScreenSettings<SearchFormSettingsDescriptor>(StudentSearchForm, SearchFormButtonDescriptors, ViewType.SearchPage);
            else if (moduleName == "courses")
                return new ScreenSettings<DataFormSettingsDescriptor>(CourseForm, ButtonDescriptors, ViewType.EditForm);
            else if (moduleName == "departments")
                //return new ScreenSettings<EditFormSettingsDescriptor>(DepartmentForm, ButtonDescriptors, ViewType.EditForm);
                return new ScreenSettings<DataFormSettingsDescriptor>(DepartmentFormWithCourses, ButtonDescriptors, ViewType.EditForm);
            else if (moduleName == "instructors")
                //return new ScreenSettings<EditFormSettingsDescriptor>(InstructorForm, ButtonDescriptors, ViewType.EditForm);
                return new ScreenSettings<DataFormSettingsDescriptor>(InstructorFormWithPopupOfficeAssignment, ButtonDescriptors, ViewType.EditForm);
            //else if (moduleName == "studentslist")
                //return new ScreenSettings<SearchFormSettingsDescriptor>(StudentSearchForm, SearchFormButtonDescriptors, ViewType.SearchPage);
            else
            {
                DisplayInvalidPageMessage(moduleName);
                return null;
            }
        }

        async static void DisplayInvalidPageMessage(string page) =>
                await App.Current.MainPage.DisplayAlert("Nav Bar", $"No {page} page.", "Ok");


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
                }//,
                //new NavigationMenuItemDescriptor
                //{
                //    InitialModule = "studentslist",
                //    Icon = "Users",
                //    Text = "studentslist"
                //}
            }
        };

        internal static FlowSettings GetFlowSettings<T>(string currentModule)
        {
            return new FlowSettings(new FlowState(), new FlowDataCache() { NavigationBar = GetNavigationBar(currentModule)}, GetScreenSettings(currentModule));
        }
    }

    
}
