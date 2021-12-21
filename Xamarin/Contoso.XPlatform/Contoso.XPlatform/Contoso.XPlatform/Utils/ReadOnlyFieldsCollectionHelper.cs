using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using LogicBuilder.Expressions.Utils;
using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Contoso.XPlatform.Utils
{
    internal class ReadOnlyFieldsCollectionHelper
    {
        private List<FormItemSettingsDescriptor> fieldSettings;
        private IFormGroupBoxSettings groupBoxSettings;
        private DetailFormLayout formLayout;
        private readonly IContextProvider contextProvider;
        private readonly string parentName;
        private readonly Type modelType;

        public ReadOnlyFieldsCollectionHelper(List<FormItemSettingsDescriptor> fieldSettings,
            IFormGroupBoxSettings groupBoxSettings,
            IContextProvider contextProvider,
            Type modelType,
            DetailFormLayout formLayout = null,
            string parentName = null)
        {
            this.fieldSettings = fieldSettings;
            this.groupBoxSettings = groupBoxSettings;
            this.contextProvider = contextProvider;
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

            new ReadOnlyFieldsCollectionHelper
            (
                setting.FieldSettings,
                setting,
                this.contextProvider,
                this.modelType,
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
            => new ReadOnlyFieldsCollectionHelper
            (
                setting.FieldSettings,
                this.groupBoxSettings,
                this.contextProvider,
                this.modelType,
                this.formLayout,
                GetFieldName(setting.Field)
            ).CreateFields();

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
            => (IReadOnly)Activator.CreateInstance
            (
                typeof(FormReadOnlyObject<>).MakeGenericType(Type.GetType(setting.ModelType)),
                GetFieldName(setting.Field),
                setting,
                this.contextProvider
            );

        private IReadOnly CreateHiddenReadOnlyObject(FormControlSettingsDescriptor setting)
            => (IReadOnly)Activator.CreateInstance
            (
                typeof(HiddenReadOnlyObject<>).MakeGenericType(Type.GetType(setting.Type)),
                GetFieldName(setting.Field),
                setting.TextTemplate.TemplateName,
                this.contextProvider
            );

        private IReadOnly CreateCheckboxReadOnlyObject(FormControlSettingsDescriptor setting)
            => (IReadOnly)Activator.CreateInstance
            (
                typeof(CheckboxReadOnlyObject),
                GetFieldName(setting.Field),
                setting.TextTemplate.TemplateName,
                setting.Title,
                this.contextProvider
            );

        private IReadOnly CreateSwitchReadOnlyObject(FormControlSettingsDescriptor setting)
            => (IReadOnly)Activator.CreateInstance
            (
                typeof(SwitchReadOnlyObject),
                GetFieldName(setting.Field),
                setting.TextTemplate.TemplateName,
                setting.Title,
                this.contextProvider
            );

        private IReadOnly CreateTextFieldReadOnlyObject(FormControlSettingsDescriptor setting)
            => (IReadOnly)Activator.CreateInstance
            (
                typeof(TextFieldReadOnlyObject<>).MakeGenericType(Type.GetType(setting.Type)),
                GetFieldName(setting.Field),
                setting.TextTemplate.TemplateName,
                setting.Title,
                setting.StringFormat,
                this.contextProvider
            );

        private IReadOnly CreatePickerReadOnlyObject(FormControlSettingsDescriptor setting)
            => (IReadOnly)Activator.CreateInstance
            (
                typeof(PickerReadOnlyObject<>).MakeGenericType(Type.GetType(setting.Type)),
                GetFieldName(setting.Field),
                setting.Title,
                setting.StringFormat,
                setting.DropDownTemplate,
                this.contextProvider
            );

        private IReadOnly CreateMultiSelectReadOnlyObject(MultiSelectFormControlSettingsDescriptor setting)
        {
            return GetValidatable(Type.GetType(setting.MultiSelectTemplate.ModelType));
            IReadOnly GetValidatable(Type elementType)
                => (IReadOnly)Activator.CreateInstance
                (
                    typeof(MultiSelectReadOnlyObject<,>).MakeGenericType
                    (
                        typeof(ObservableCollection<>).MakeGenericType(elementType),
                        elementType
                    ),
                    GetFieldName(setting.Field),
                    setting.KeyFields,
                    setting.Title,
                    setting.StringFormat,
                    setting.MultiSelectTemplate,
                    this.contextProvider
                );
        }

        private IReadOnly CreateFormArrayReadOnlyObject(FormGroupArraySettingsDescriptor setting)
        {
            return GetValidatable(Type.GetType(setting.ModelType));
            IReadOnly GetValidatable(Type elementType)
                => (IReadOnly)Activator.CreateInstance
                (
                    typeof(FormArrayReadOnlyObject<,>).MakeGenericType
                    (
                        typeof(ObservableCollection<>).MakeGenericType(elementType),
                        elementType
                    ),
                    GetFieldName(setting.Field),
                    setting,
                    this.contextProvider
                );
        }

        private void ValidateSettingType(string fullPropertyName, string settingFieldType)
        {
            if (!GetModelFieldType(fullPropertyName).AssignableFrom(Type.GetType(settingFieldType)))
                throw new ArgumentException($"{nameof(settingFieldType)}: 049B3B17-154F-4A06-B6B3-863F85FDBB50");

            Type GetModelFieldType(string fullPropertyName)
                => this.modelType.GetMemberInfoFromFullName(fullPropertyName).GetMemberType();
        }
    }
}
