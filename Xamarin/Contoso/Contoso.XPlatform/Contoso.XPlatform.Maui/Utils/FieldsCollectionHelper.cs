using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.DataForm;
using Contoso.Forms.Configuration.Validation;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.Validators.Rules;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.Validatables;
using LogicBuilder.Expressions.Utils;
using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Contoso.XPlatform.Utils
{
    internal class FieldsCollectionHelper
    {
        private List<FormItemSettingsDescriptor> fieldSettings;
        protected IFormGroupBoxSettings groupBoxSettings;
        protected Dictionary<string, List<ValidationRuleDescriptor>> validationMessages { get; }
        protected EditFormLayout formLayout;
        private readonly UiNotificationService uiNotificationService;
        protected readonly IContextProvider contextProvider;
        protected readonly string? parentName;
        protected readonly Type modelType;

        public FieldsCollectionHelper(List<FormItemSettingsDescriptor> fieldSettings,
            IFormGroupBoxSettings groupBoxSettings,
            Dictionary<string, List<ValidationRuleDescriptor>> validationMessages,
            IContextProvider contextProvider,
            Type modelType,
            EditFormLayout? formLayout = null,
            string? parentName = null)
        {
            this.fieldSettings = fieldSettings;
            this.groupBoxSettings = groupBoxSettings;
            this.validationMessages = validationMessages;
            this.contextProvider = contextProvider;
            this.modelType = modelType;
            this.uiNotificationService = contextProvider.UiNotificationService;

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

            new FieldsCollectionHelper
            (
                setting.FieldSettings,
                setting,
                this.validationMessages,
                this.contextProvider,
                this.modelType,
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
            => new FieldsCollectionHelper
            (
                setting.FieldSettings,
                this.groupBoxSettings,
                setting.ValidationMessages,
                this.contextProvider,
                this.modelType,
                this.formLayout,
                GetFieldName(setting.Field)
            ).CreateFields();

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
        {
            return (IValidatable)(
                Activator.CreateInstance
                (
                    typeof(FormValidatableObject<>).MakeGenericType(Type.GetType(setting.ModelType) ?? throw new ArgumentException($"{setting.ModelType}: {{7E79347E-F11F-48B2-8365-49E98A1061EA}}")),
                    GetFieldName(setting.Field),
                    setting,
                    new IValidationRule[] { },
                    this.contextProvider
                ) ?? throw new ArgumentException($"{setting.ModelType}: {{C2F9E53E-563B-4CEE-B070-74CB2714A5D4}}")
            );
        }

        private IValidatable CreateHiddenValidatableObject(FormControlSettingsDescriptor setting, string templateName)
            => ValidatableObjectFactory.GetValidatable
            (
                Activator.CreateInstance
                (
                    typeof(HiddenValidatableObject<>).MakeGenericType(Type.GetType(setting.Type) ?? throw new ArgumentException($"{setting.Type}: {{F69C7063-5F25-408F-9914-08E29245D98C}}")),
                    GetFieldName(setting.Field),
                    templateName,
                    GetValidationRules(setting),
                    this.uiNotificationService
                ) ?? throw new ArgumentException($"{setting.Type}: {{C2F2C3BB-723A-4C94-8941-BBD50F06036B}}"),
                setting
            );

        private IValidatable CreateCheckboxValidatableObject(FormControlSettingsDescriptor setting, string templateName, string title)
            => ValidatableObjectFactory.GetValidatable
            (
                Activator.CreateInstance
                (
                    typeof(CheckboxValidatableObject),
                    GetFieldName(setting.Field),
                    templateName,
                    title,
                    GetValidationRules(setting),
                    this.uiNotificationService
                ) ?? throw new ArgumentException($"{typeof(CheckboxValidatableObject)}: {{CFA66AF1-88E6-465E-A79F-2AA38451C321}}"),
                setting
            );

        private IValidatable CreateSwitchValidatableObject(FormControlSettingsDescriptor setting, string templateName, string title)
            => ValidatableObjectFactory.GetValidatable
            (
                Activator.CreateInstance
                (
                    typeof(SwitchValidatableObject),
                    GetFieldName(setting.Field),
                    templateName,
                    title,
                    GetValidationRules(setting),
                    this.uiNotificationService
                ) ?? throw new ArgumentException($"{typeof(SwitchValidatableObject)}: {{B728AF40-B321-4ABF-B4EB-1E887866EE51}}"),
                setting
            );

        private IValidatable CreateEntryValidatableObject(FormControlSettingsDescriptor setting, string templateName, string placeholder, string stringFormat)
            => ValidatableObjectFactory.GetValidatable
            (
                Activator.CreateInstance
                (
                    typeof(EntryValidatableObject<>).MakeGenericType(Type.GetType(setting.Type) ?? throw new ArgumentException($"{setting.Type}: {{0CF6F381-C03C-46BE-B3A2-815E8C34B96A}}")),
                    GetFieldName(setting.Field),
                    templateName,
                    placeholder,
                    stringFormat,
                    GetValidationRules(setting),
                    this.uiNotificationService
                ) ?? throw new ArgumentException($"{setting.Type}: {{B4018648-8BB6-4D8F-8493-884FF2066AF6}}"),
                setting
            );

        private IValidatable CreateLabelValidatableObject(FormControlSettingsDescriptor setting, string templateName, string title, string placeholder, string stringFormat)
            => ValidatableObjectFactory.GetValidatable
            (
                Activator.CreateInstance
                (
                    typeof(LabelValidatableObject<>).MakeGenericType(Type.GetType(setting.Type) ?? throw new ArgumentException($"{setting.Type}: {{B0097578-8375-4D4F-B933-DB5708BF1A23}}")),
                    GetFieldName(setting.Field),
                    templateName,
                    title,
                    placeholder,
                    stringFormat,
                    GetValidationRules(setting),
                    this.uiNotificationService
                ) ?? throw new ArgumentException($"{setting.Type}: {{32DDD6F8-BA5E-4BDE-A257-7CEBD9ADA715}}"),
                setting
            );

        private IValidatable CreateDatePickerValidatableObject(FormControlSettingsDescriptor setting, string templateName)
            => ValidatableObjectFactory.GetValidatable
            (
                Activator.CreateInstance
                (
                    typeof(DatePickerValidatableObject<>).MakeGenericType(Type.GetType(setting.Type) ?? throw new ArgumentException($"{setting.Type}: {{117F5104-3D4F-4E23-84A9-6DD759446523}}")),
                    GetFieldName(setting.Field),
                    templateName,
                    GetValidationRules(setting),
                    this.uiNotificationService
                ) ?? throw new ArgumentException($"{setting.Type}: {{8827079A-EB58-42A4-9937-7A183C45063A}}"),
                setting
            );

        private IValidatable CreatePickerValidatableObject(FormControlSettingsDescriptor setting, DropDownTemplateDescriptor dropDownTemplate)
            => ValidatableObjectFactory.GetValidatable
            (
                Activator.CreateInstance
                (
                    typeof(PickerValidatableObject<>).MakeGenericType(Type.GetType(setting.Type) ?? throw new ArgumentException($"{setting.Type}: {{1CEE7C08-7FDB-45E6-A13C-9C27E2100EE2}}")),
                    GetFieldName(setting.Field),
                    ValidatableObjectFactory.GetValue(setting),
                    dropDownTemplate,
                    GetValidationRules(setting),
                    this.contextProvider
                ) ?? throw new ArgumentException($"{setting.Type}: {{F261FAD0-8BBB-4F8C-A507-E782F7385011}}"),
                setting
            );

        private IValidatable CreateMultiSelectValidatableObject(MultiSelectFormControlSettingsDescriptor setting)
        {
            return GetValidatable(Type.GetType(setting.MultiSelectTemplate.ModelType) ?? throw new ArgumentException($"{setting.MultiSelectTemplate.ModelType}: {{28BD997E-223F-4671-A195-290E246E371A}}"));
            IValidatable GetValidatable(Type elementType)
                => ValidatableObjectFactory.GetValidatable
                (
                    Activator.CreateInstance
                    (
                        typeof(MultiSelectValidatableObject<,>).MakeGenericType
                        (
                            typeof(ObservableCollection<>).MakeGenericType(elementType),
                            elementType
                        ),
                        GetFieldName(setting.Field),
                        setting,
                        GetValidationRules(setting),
                        this.contextProvider
                    ) ?? throw new ArgumentException($"{setting.MultiSelectTemplate.ModelType}: {{E7665215-519B-434B-8CCD-A2C2D781918C}}"),
                    setting
                );
        }

        private IValidatable CreateFormArrayValidatableObject(FormGroupArraySettingsDescriptor setting)
        {
            return GetValidatable(Type.GetType(setting.ModelType) ?? throw new ArgumentException($"{setting.ModelType}: {{694A445E-E460-45BE-8E6F-C0EE71B1E3CE}}"));
            IValidatable GetValidatable(Type elementType)
                => (IValidatable)(
                    Activator.CreateInstance
                    (
                        typeof(FormArrayValidatableObject<,>).MakeGenericType
                        (
                            typeof(ObservableCollection<>).MakeGenericType(elementType),
                            elementType
                        ),
                        GetFieldName(setting.Field),
                        setting,
                        new IValidationRule[] { },
                        this.contextProvider
                    ) ?? throw new ArgumentException($"{setting.ModelType}: {{45FCB8EF-F39A-4DF9-AA00-FBC6534F9740}}")
                );
        }

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
                this.validationMessages, 
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
