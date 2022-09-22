using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.Forms.Configuration.Validation;
using Enrollment.XPlatform.Constants;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.Validators;
using Enrollment.XPlatform.Validators.Rules;
using Enrollment.XPlatform.ViewModels;
using Enrollment.XPlatform.ViewModels.Factories;
using Enrollment.XPlatform.ViewModels.Validatables;
using Enrollment.XPlatform.ViewModels.Validatables.Factories;
using LogicBuilder.Expressions.Utils;
using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Enrollment.XPlatform.Services
{
    public class FieldsCollectionBuilder : IFieldsCollectionBuilder
    {
        protected readonly ICollectionBuilderFactory collectionBuilderFactory;
        private readonly IValidatableFactory validatableFactory;
        private readonly IValidatableValueHelper validatableValueHelper;

        private readonly List<FormItemSettingsDescriptor> fieldSettings;
        protected IFormGroupBoxSettings groupBoxSettings;
        protected EditFormLayout formLayout;
        
        protected readonly string? parentName;
        protected readonly Type modelType;

        public FieldsCollectionBuilder(
            ICollectionBuilderFactory collectionBuilderFactory,
            IValidatableFactory validatableFactory,
            IValidatableValueHelper validatableValueHelper,
            List<FormItemSettingsDescriptor> fieldSettings,
            IFormGroupBoxSettings groupBoxSettings,
            Dictionary<string, List<ValidationRuleDescriptor>> validationMessages,
            Type modelType,
            EditFormLayout? formLayout = null,
            string? parentName = null)
        {
            this.fieldSettings = fieldSettings;
            this.groupBoxSettings = groupBoxSettings;
            this.ValidationMessages = validationMessages;
            this.collectionBuilderFactory = collectionBuilderFactory;
            this.validatableFactory = validatableFactory;
            this.validatableValueHelper = validatableValueHelper;
            this.modelType = modelType;

            if (formLayout == null)
            {
                this.formLayout = new EditFormLayout();
                if (this.fieldSettings.ShouldCreateDefaultControlGroupBox())
                    this.formLayout.AddControlGroupBox(this.groupBoxSettings);
            }
            else
            {
                this.formLayout = formLayout;
            }

            this.parentName = parentName;
        }

        protected Dictionary<string, List<ValidationRuleDescriptor>> ValidationMessages { get; }

        public EditFormLayout CreateFields()
        {
            this.CreateFieldsCollection(this.fieldSettings);
            return this.formLayout;
        }

        private void CreateFieldsCollection(List<FormItemSettingsDescriptor> fieldSettings)
        {
            fieldSettings.ForEach
            (
                setting =>
                {
                    switch (setting)
                    {
                        case MultiSelectFormControlSettingsDescriptor multiSelectFormControlSettings:
                            AddMultiSelectControl(multiSelectFormControlSettings);
                            break;
                        case FormControlSettingsDescriptor formControlSettings:
                            AddFormControl(formControlSettings);
                            break;
                        case FormGroupSettingsDescriptor formGroupSettings:
                            AddFormGroupSettings(formGroupSettings);
                            break;
                        case FormGroupArraySettingsDescriptor formGroupArraySettings:
                            AddFormGroupArray(formGroupArraySettings);
                            break;
                        case FormGroupBoxSettingsDescriptor groupBoxSettingsDescriptor:
                            AddGroupBoxSettings(groupBoxSettingsDescriptor);
                            break;
                        default:
                            throw new ArgumentException($"{nameof(setting)}: B024F65A-50DC-4D45-B8F0-9EC0BE0E2FE2");
                    }
                }
            );
        }

        protected virtual void AddGroupBoxSettings(FormGroupBoxSettingsDescriptor setting)
        {
            this.formLayout.AddControlGroupBox(setting);

            if (setting.FieldSettings.Any(s => s is FormGroupBoxSettingsDescriptor))
                throw new ArgumentException($"{nameof(setting.FieldSettings)}: B11BFDCD-9612-4584-A420-8FE511A8B64A");

            collectionBuilderFactory.GetFieldsCollectionBuilder
            (
                this.modelType,
                setting.FieldSettings,
                setting,
                this.ValidationMessages,
                this.formLayout,
                this.parentName
            ).CreateFields();
        }

        protected string GetFieldName(string field)
                => parentName == null ? field : $"{parentName}.{field}";

        private void AddFormGroupSettings(FormGroupSettingsDescriptor setting)
        {
            ValidateSettingType(GetFieldName(setting.Field), setting.ModelType);

            if (setting.FormGroupTemplate == null
                || string.IsNullOrEmpty(setting.FormGroupTemplate.TemplateName))
                throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: 4817E6BF-0B48-4829-BAB8-7AD17E006EA7");

            switch (setting.FormGroupTemplate.TemplateName)
            {
                case FromGroupTemplateNames.InlineFormGroupTemplate:
                    AddFormGroupInline(setting);
                    break;
                case FromGroupTemplateNames.PopupFormGroupTemplate:
                    AddFormGroupPopup(setting);
                    break;
                default:
                    throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: 6664DF64-DF69-415E-8AD2-2AEFC3FA4261");
            }
        }

        private void AddFormGroupPopup(FormGroupSettingsDescriptor setting)
        {
            formLayout.Add(CreateFormValidatableObject(setting), this.groupBoxSettings);
        }

        protected virtual void AddFormGroupInline(FormGroupSettingsDescriptor setting)
        {
            collectionBuilderFactory.GetFieldsCollectionBuilder
            (
                this.modelType,
                setting.FieldSettings,
                 this.groupBoxSettings,
                setting.ValidationMessages,
                this.formLayout,
                GetFieldName(setting.Field)
            ).CreateFields();
        }

        protected virtual void AddFormControl(FormControlSettingsDescriptor setting)
        {
            ValidateSettingType(GetFieldName(setting.Field), setting.Type);

            if (setting.TextTemplate != null)
                AddTextControl(setting, setting.TextTemplate);
            else if (setting.DropDownTemplate != null)
                AddDropdownControl(setting);
            else
                throw new ArgumentException($"{nameof(setting)}: 0556AEAF-C851-44F1-A2A2-66C8814D0F54");
        }

        protected void AddTextControl(FormControlSettingsDescriptor setting, TextFieldTemplateDescriptor textTemplate)
        {
            if (textTemplate.TemplateName == nameof(QuestionTemplateSelector.TextTemplate)
                || textTemplate.TemplateName == nameof(QuestionTemplateSelector.PasswordTemplate))
            {
                formLayout.Add
                (
                    CreateEntryValidatableObject
                    (
                        setting,
                        textTemplate.TemplateName,
                        setting.Placeholder,
                        setting.StringFormat
                    ),
                    this.groupBoxSettings
                );
            }
            else if (textTemplate.TemplateName == nameof(QuestionTemplateSelector.DateTemplate))
            {
                formLayout.Add(CreateDatePickerValidatableObject(setting, textTemplate.TemplateName), this.groupBoxSettings);
            }
            else if (textTemplate.TemplateName == nameof(QuestionTemplateSelector.HiddenTemplate))
            {
                formLayout.Add(CreateHiddenValidatableObject(setting, textTemplate.TemplateName), this.groupBoxSettings);
            }
            else if (textTemplate.TemplateName == nameof(QuestionTemplateSelector.CheckboxTemplate))
            {
                formLayout.Add(CreateCheckboxValidatableObject(setting, textTemplate.TemplateName, setting.Title), this.groupBoxSettings);
            }
            else if (textTemplate.TemplateName == nameof(QuestionTemplateSelector.SwitchTemplate))
            {
                formLayout.Add(CreateSwitchValidatableObject(setting, textTemplate.TemplateName, setting.Title), this.groupBoxSettings);
            }
            else if (textTemplate.TemplateName == nameof(QuestionTemplateSelector.LabelTemplate))
            {
                formLayout.Add
                (
                    CreateLabelValidatableObject
                    (
                        setting,
                        textTemplate.TemplateName,
                        setting.Title,
                        setting.Placeholder,
                        setting.StringFormat
                    ),
                    this.groupBoxSettings
                );
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.TextTemplate.TemplateName)}: BFCC0C85-244A-4896-BAB2-0D29AD0F86D8");
            }
        }

        protected void AddDropdownControl(FormControlSettingsDescriptor setting)
        {
            if (setting.DropDownTemplate.TemplateName == nameof(QuestionTemplateSelector.PickerTemplate))
            {
                formLayout.Add(CreatePickerValidatableObject(setting, setting.DropDownTemplate), this.groupBoxSettings);
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.DropDownTemplate.TemplateName)}: 8A0325D9-E9B0-487D-B569-7E92CDBD4F30");
            }
        }

        private void AddFormGroupArray(FormGroupArraySettingsDescriptor setting)
        {
            ValidateSettingType(GetFieldName(setting.Field), setting.Type);

            if (setting.FormGroupTemplate == null
                || string.IsNullOrEmpty(setting.FormGroupTemplate.TemplateName))
                throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: 0B1F7121-915F-48B9-96A3-B410A67E6853");

            if (setting.FormGroupTemplate.TemplateName == nameof(QuestionTemplateSelector.FormGroupArrayTemplate))
            {
                formLayout.Add(CreateFormArrayValidatableObject(setting), this.groupBoxSettings);
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: 5E4E494A-E3FE-4016-ABB3-F238DC8E72F9");
            }
        }

        private void AddMultiSelectControl(MultiSelectFormControlSettingsDescriptor setting)
        {
            ValidateSettingType(GetFieldName(setting.Field), setting.Type);

            if (setting.MultiSelectTemplate.TemplateName == nameof(QuestionTemplateSelector.MultiSelectTemplate))
            {
                formLayout.Add(CreateMultiSelectValidatableObject(setting), this.groupBoxSettings);
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.DropDownTemplate.TemplateName)}: 880DF2E6-97E8-49F2-B88C-FE8DB4F01C63");
            }
        }

        private IValidatable CreateFormValidatableObject(FormGroupSettingsDescriptor setting) 
            => this.validatableFactory.CreateFormValidatableObject
            (
                Type.GetType(setting.ModelType) ?? throw new ArgumentException($"{setting.ModelType}: {{7E79347E-F11F-48B2-8365-49E98A1061EA}}"),
                GetFieldName(setting.Field),
                nameof(FormValidatableObject<string>),
                setting,
                Array.Empty<IValidationRule>()
            );

        private IValidatable CreateHiddenValidatableObject(FormControlSettingsDescriptor setting, string templateName)
        {
            Type fieldType = Type.GetType(setting.Type) ?? throw new ArgumentException($"{setting.Type}: {{F69C7063-5F25-408F-9914-08E29245D98C}}");
            IValidatable hiddenValidatable = validatableFactory.CreateHiddenValidatableObject
            (
                fieldType,
                GetFieldName(setting.Field),
                templateName,
                GetValidationRules(setting)
            );

            hiddenValidatable.Value = validatableValueHelper.GetDefaultValue(setting, fieldType);
            return hiddenValidatable;
        }

        private IValidatable CreateCheckboxValidatableObject(FormControlSettingsDescriptor setting, string templateName, string title)
        {
            IValidatable checkboxValidatable = validatableFactory.CreateCheckboxValidatableObject
            (
                GetFieldName(setting.Field),
                templateName,
                title,
                GetValidationRules(setting)
            );

            checkboxValidatable.Value = validatableValueHelper.GetDefaultValue(setting, typeof(bool));
            return checkboxValidatable;
        }

        private IValidatable CreateSwitchValidatableObject(FormControlSettingsDescriptor setting, string templateName, string title)
        {
            IValidatable switchValidatable = validatableFactory.CreateSwitchValidatableObject
            (
                GetFieldName(setting.Field),
                templateName,
                title,
                GetValidationRules(setting)
            );

            switchValidatable.Value = validatableValueHelper.GetDefaultValue(setting, typeof(bool));
            return switchValidatable;
        }

        private IValidatable CreateEntryValidatableObject(FormControlSettingsDescriptor setting, string templateName, string placeholder, string stringFormat)
        {
            Type fieldType = Type.GetType(setting.Type) ?? throw new ArgumentException($"{setting.Type}: {{0CF6F381-C03C-46BE-B3A2-815E8C34B96A}}");
            IValidatable entryValidatable = validatableFactory.CreateEntryValidatableObject
            (
                fieldType,
                GetFieldName(setting.Field),
                templateName,
                placeholder,
                stringFormat,
                GetValidationRules(setting)
            );

            entryValidatable.Value = validatableValueHelper.GetDefaultValue(setting, fieldType);
            return entryValidatable;
        }

        private IValidatable CreateLabelValidatableObject(FormControlSettingsDescriptor setting, string templateName, string title, string placeholder, string stringFormat)
        {
            Type fieldType = Type.GetType(setting.Type) ?? throw new ArgumentException($"{setting.Type}: {{B0097578-8375-4D4F-B933-DB5708BF1A23}}");
            IValidatable labelValidatable = validatableFactory.CreateLabelValidatableObject
            (
                fieldType,
                GetFieldName(setting.Field),
                templateName,
                title,
                placeholder,
                stringFormat,
                GetValidationRules(setting)
            );

            labelValidatable.Value = validatableValueHelper.GetDefaultValue(setting, fieldType);
            return labelValidatable;
        }

        private IValidatable CreateDatePickerValidatableObject(FormControlSettingsDescriptor setting, string templateName)
        {
            Type fieldType = Type.GetType(setting.Type) ?? throw new ArgumentException($"{setting.Type}: {{036F8A43-704B-4187-AABA-142B14734582}}");
            IValidatable datePickerValidatable = validatableFactory.CreateDatePickerValidatableObject
            (
                fieldType,
                GetFieldName(setting.Field),
                templateName,
                GetValidationRules(setting)
            );

            datePickerValidatable.Value = validatableValueHelper.GetDefaultValue(setting, fieldType);
            return datePickerValidatable;
        }

        private IValidatable CreatePickerValidatableObject(FormControlSettingsDescriptor setting, DropDownTemplateDescriptor dropDownTemplate)
        {
            Type fieldType = Type.GetType(setting.Type) ?? throw new ArgumentException($"{setting.Type}: {{8FB0D8B8-5273-48AC-98F4-6CE6FC4C81CC}}");
            object? defaultValue = validatableValueHelper.GetDefaultValue(setting, fieldType);
            IValidatable pickerValidatable = validatableFactory.CreatePickerValidatableObject
            (
                fieldType,
                GetFieldName(setting.Field),
                defaultValue,
                dropDownTemplate,
                GetValidationRules(setting)
            );

            pickerValidatable.Value = defaultValue;
            return pickerValidatable;
        }

        private IValidatable CreateMultiSelectValidatableObject(MultiSelectFormControlSettingsDescriptor setting)
        {
            Type elementType = Type.GetType(setting.MultiSelectTemplate.ModelType) ?? throw new ArgumentException($"{setting.MultiSelectTemplate.ModelType}: {{18D5D086-FC25-4BAF-AE4A-8DB037F7CD12}}");
            Type fieldType = Type.GetType(setting.Type) ?? throw new ArgumentException($"{setting.Type}: {{BEA37953-899A-4569-82DA-CAC9A0F6D6C9}}");

            IValidatable multiSelectValidatable = validatableFactory.CreateMultiSelectValidatableObject
            (
                elementType,
                GetFieldName(setting.Field),
                setting,
                GetValidationRules(setting)
            );

            multiSelectValidatable.Value = validatableValueHelper.GetDefaultValue(setting, fieldType);
            return multiSelectValidatable;
        }

        private IValidatable CreateFormArrayValidatableObject(FormGroupArraySettingsDescriptor setting) 
            => validatableFactory.CreateFormArrayValidatableObject
            (
                Type.GetType(setting.ModelType) ?? throw new ArgumentException($"{setting.ModelType}: {{18D5D086-FC25-4BAF-AE4A-8DB037F7CD12}}"),
                GetFieldName(setting.Field),
                setting,
                Array.Empty<IValidationRule>()
            );

        private IValidationRule[]? GetValidationRules(FormControlSettingsDescriptor setting)
            => setting.ValidationSetting?.Validators?.Select
            (
                validator => GetValidatorRule(validator, setting)
            ).ToArray();

        private IValidationRule GetValidatorRule(ValidatorDefinitionDescriptor validator, FormControlSettingsDescriptor setting)
            => new ValidatorRuleFactory(this.parentName).GetValidatorRule
            (
                validator,
                setting,
                this.ValidationMessages,
                formLayout.Properties
            );

        private void ValidateSettingType(string fullPropertyName, string settingFieldType)
        {
            if (!GetModelFieldType(fullPropertyName).AssignableFrom(Type.GetType(settingFieldType)))
                throw new ArgumentException($"{nameof(settingFieldType)}: {{7E7749CE-96C1-4EE1-8705-75541CD7D2D8}}");

            Type GetModelFieldType(string fullPropertyName)
                => this.modelType.GetMemberInfoFromFullName(fullPropertyName).GetMemberType();
        }
    }
}
