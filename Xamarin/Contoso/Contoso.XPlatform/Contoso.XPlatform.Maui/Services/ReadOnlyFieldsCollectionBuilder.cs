using AutoMapper;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Directives.Factories;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.Factories;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using Contoso.XPlatform.ViewModels.ReadOnlys.Factories;
using LogicBuilder.Expressions.Utils;
using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Contoso.XPlatform.Services
{
    public class ReadOnlyFieldsCollectionBuilder : IReadOnlyFieldsCollectionBuilder
    {
        private readonly ICollectionBuilderFactory collectionBuilderFactory;
        private readonly IReadOnlyFactory readOnlyFactory;
        private readonly List<FormItemSettingsDescriptor> fieldSettings;
        private readonly IFormGroupBoxSettings groupBoxSettings;
        private readonly DetailFormLayout formLayout;
        private readonly string? parentName;
        private readonly Type modelType;

        public ReadOnlyFieldsCollectionBuilder(
            ICollectionBuilderFactory collectionBuilderFactory,
            IReadOnlyFactory readOnlyFactory,
            List<FormItemSettingsDescriptor> fieldSettings,
            IFormGroupBoxSettings groupBoxSettings,
            Type modelType,
            DetailFormLayout? formLayout = null,
            string? parentName = null)
        {
            this.fieldSettings = fieldSettings;
            this.groupBoxSettings = groupBoxSettings;
            this.collectionBuilderFactory = collectionBuilderFactory;
            this.readOnlyFactory = readOnlyFactory;
            this.modelType = modelType;

            if (formLayout == null)
            {
                this.formLayout = new DetailFormLayout();
                if (this.fieldSettings.ShouldCreateDefaultControlGroupBox())
                    this.formLayout.AddControlGroupBox(this.groupBoxSettings);
            }
            else
            {
                this.formLayout = formLayout;
            }

            this.parentName = parentName;
        }

        public DetailFormLayout CreateFields()
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

        private void AddGroupBoxSettings(FormGroupBoxSettingsDescriptor setting)
        {
            this.formLayout.AddControlGroupBox(setting);

            if (setting.FieldSettings.Any(s => s is FormGroupBoxSettingsDescriptor))
                throw new ArgumentException($"{nameof(setting.FieldSettings)}: E5DC0442-F3B9-4C50-B641-304358DF8EBA");

            collectionBuilderFactory.GetReadOnlyFieldsCollectionBuilder
            (
                this.modelType,
                setting.FieldSettings,
                setting,
                this.formLayout,
                this.parentName
            ).CreateFields();
        }

        private void AddMultiSelectControl(MultiSelectFormControlSettingsDescriptor setting)
        {
            ValidateSettingType(GetFieldName(setting.Field), setting.Type);

            if (setting.MultiSelectTemplate.TemplateName == nameof(ReadOnlyControlTemplateSelector.MultiSelectTemplate))
            {
                formLayout.Add(CreateMultiSelectReadOnlyObject(setting), this.groupBoxSettings);
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.DropDownTemplate.TemplateName)}: E63A881F-3B4D-47A1-A13C-835EA8A86C61");
            }
        }

        private void AddFormControl(FormControlSettingsDescriptor setting)
        {
            ValidateSettingType(GetFieldName(setting.Field), setting.Type);

            if (setting.TextTemplate != null)
                AddTextControl(setting);
            else if (setting.DropDownTemplate != null)
                AddDropdownControl(setting);
            else
                throw new ArgumentException($"{nameof(setting)}: 88225DC7-F14A-4CB5-AC97-3FE63BFCC298");
        }

        private void AddTextControl(FormControlSettingsDescriptor setting)
        {
            if (setting.TextTemplate.TemplateName == nameof(ReadOnlyControlTemplateSelector.TextTemplate)
                || setting.TextTemplate.TemplateName == nameof(ReadOnlyControlTemplateSelector.PasswordTemplate)
                || setting.TextTemplate.TemplateName == nameof(ReadOnlyControlTemplateSelector.DateTemplate))
            {
                formLayout.Add(CreateTextFieldReadOnlyObject(setting), this.groupBoxSettings);
            }
            else if (setting.TextTemplate.TemplateName == nameof(ReadOnlyControlTemplateSelector.HiddenTemplate))
            {
                formLayout.Add(CreateHiddenReadOnlyObject(setting), this.groupBoxSettings);
            }
            else if (setting.TextTemplate.TemplateName == nameof(ReadOnlyControlTemplateSelector.CheckboxTemplate))
            {
                formLayout.Add(CreateCheckboxReadOnlyObject(setting), this.groupBoxSettings);
            }
            else if (setting.TextTemplate.TemplateName == nameof(ReadOnlyControlTemplateSelector.SwitchTemplate))
            {
                formLayout.Add(CreateSwitchReadOnlyObject(setting), this.groupBoxSettings);
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.TextTemplate.TemplateName)}: 537C774B-91B8-490D-8F47-38834614C383");
            }
        }

        private void AddDropdownControl(FormControlSettingsDescriptor setting)
        {
            if (setting.DropDownTemplate.TemplateName == nameof(ReadOnlyControlTemplateSelector.PickerTemplate))
            {
                formLayout.Add(CreatePickerReadOnlyObject(setting), this.groupBoxSettings);
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.DropDownTemplate.TemplateName)}: E3D30EE3-4AFF-4706-A1D2-BB5FA852258E");
            }
        }

        private void AddFormGroupSettings(FormGroupSettingsDescriptor setting)
        {
            ValidateSettingType(GetFieldName(setting.Field), setting.ModelType);

            if (setting.FormGroupTemplate == null
                || string.IsNullOrEmpty(setting.FormGroupTemplate.TemplateName))
                throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: 2A49FA6B-F0F0-4AE7-8CA4-A47D39C712C9");

            switch (setting.FormGroupTemplate.TemplateName)
            {
                case FromGroupTemplateNames.InlineFormGroupTemplate:
                    AddFormGroupInline(setting);
                    break;
                case FromGroupTemplateNames.PopupFormGroupTemplate:
                    AddFormGroupPopup(setting);
                    break;
                default:
                    throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: D6A2C8DF-8581-46C3-A4CA-810C9A14E635");
            }
        }

        private void AddFormGroupInline(FormGroupSettingsDescriptor setting)
        {
            collectionBuilderFactory.GetReadOnlyFieldsCollectionBuilder
            (
                this.modelType,
                setting.FieldSettings,
                this.groupBoxSettings,
                this.formLayout,
                GetFieldName(setting.Field)
            ).CreateFields();
        }

        private void AddFormGroupPopup(FormGroupSettingsDescriptor setting)
            => formLayout.Add(CreateFormReadOnlyObject(setting), this.groupBoxSettings);

        private void AddFormGroupArray(FormGroupArraySettingsDescriptor setting)
        {
            ValidateSettingType(GetFieldName(setting.Field), setting.Type);

            if (setting.FormGroupTemplate == null
                || string.IsNullOrEmpty(setting.FormGroupTemplate.TemplateName))
                throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: 0D7B0F95-8230-4ABD-B5B1-479558B4C4F1");

            if (setting.FormGroupTemplate.TemplateName == nameof(QuestionTemplateSelector.FormGroupArrayTemplate))
            {
                formLayout.Add(CreateFormArrayReadOnlyObject(setting), this.groupBoxSettings);
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: 3B2D01FD-4405-484C-A325-E3C9E8E56A3A");
            }
        }

        string GetFieldName(string field)
                => parentName == null ? field : $"{parentName}.{field}";

        private IReadOnly CreateFormReadOnlyObject(FormGroupSettingsDescriptor setting) 
            => this.readOnlyFactory.CreateFormReadOnlyObject
            (
                Type.GetType(setting.ModelType) ?? throw new ArgumentException($"{setting.ModelType}: {{12B7ADCA-C821-48A4-809F-D503C586119B}}"),
                GetFieldName(setting.Field),
                setting
            );

        private IReadOnly CreateHiddenReadOnlyObject(FormControlSettingsDescriptor setting) 
            => readOnlyFactory.CreateHiddenReadOnlyObject
            (
                Type.GetType(setting.Type) ?? throw new ArgumentException($"{nameof(setting.Type)}: {{CC170AD8-8C4C-451B-B113-6F90FF445F0C}}"),
                GetFieldName(setting.Field),
                setting.TextTemplate.TemplateName
            );

        private IReadOnly CreateCheckboxReadOnlyObject(FormControlSettingsDescriptor setting) 
            => readOnlyFactory.CreateCheckboxReadOnlyObject
            (
                GetFieldName(setting.Field),
                setting.TextTemplate.TemplateName,
                setting.Title
            );

        private IReadOnly CreateSwitchReadOnlyObject(FormControlSettingsDescriptor setting) 
            => readOnlyFactory.CreateSwitchReadOnlyObject
            (
                GetFieldName(setting.Field),
                setting.TextTemplate.TemplateName,
                setting.Title
            );

        private IReadOnly CreateTextFieldReadOnlyObject(FormControlSettingsDescriptor setting) 
            => readOnlyFactory.CreateTextFieldReadOnlyObject
            (
                Type.GetType(setting.Type) ?? throw new ArgumentException($"{nameof(setting.Type)}: {{B878CBF2-F63A-43EC-B386-DB999557EAD2}}"),
                GetFieldName(setting.Field),
                setting.TextTemplate.TemplateName,
                setting.Title,
                setting.StringFormat
            );

        private IReadOnly CreatePickerReadOnlyObject(FormControlSettingsDescriptor setting) 
            => readOnlyFactory.CreatePickerReadOnlyObject
            (
                Type.GetType(setting.Type) ?? throw new ArgumentException($"{nameof(setting.Type)}: {{F70FE8A1-B481-4303-BB65-892A4408B113}}"),
                GetFieldName(setting.Field),
                setting.Title,
                setting.StringFormat,
                setting.DropDownTemplate
            );

        private IReadOnly CreateMultiSelectReadOnlyObject(MultiSelectFormControlSettingsDescriptor setting) 
            => readOnlyFactory.CreateMultiSelectReadOnlyObject
            (
                Type.GetType(setting.MultiSelectTemplate.ModelType) ?? throw new ArgumentException($"{nameof(setting.MultiSelectTemplate.ModelType)}: {{BE6E97BF-7843-45D5-BC46-BA5B73804DE5}}"),
                GetFieldName(setting.Field),
                setting.KeyFields,
                setting.Title,
                setting.StringFormat,
                setting.MultiSelectTemplate
            );

        private IReadOnly CreateFormArrayReadOnlyObject(FormGroupArraySettingsDescriptor setting) 
            => readOnlyFactory.CreateFormArrayReadOnlyObject
            (
                Type.GetType(setting.ModelType) ?? throw new ArgumentException($"{nameof(setting.ModelType)}: {{A5D1BE9A-1B9C-4BF3-AA9D-6A49CB0BD2C6}}"),
                GetFieldName(setting.Field),
                setting
            );

        private void ValidateSettingType(string fullPropertyName, string settingFieldType)
        {
            if (!GetModelFieldType(fullPropertyName).AssignableFrom(Type.GetType(settingFieldType)))
                throw new ArgumentException($"{nameof(settingFieldType)}: 049B3B17-154F-4A06-B6B3-863F85FDBB50");

            Type GetModelFieldType(string fullPropertyName)
                => this.modelType.GetMemberInfoFromFullName(fullPropertyName).GetMemberType();
        }
    }
}
