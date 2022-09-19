using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Validators;
using System;
using System.Collections.Generic;

namespace Contoso.XPlatform.ViewModels.Validatables.Factories
{
    internal class ValidatableFactory : IValidatableFactory
    {
        private readonly Func<string, string, string, IEnumerable<IValidationRule>?, IValidatable> getCheckboxValidatable;
        private readonly Func<Type, string, string, IEnumerable<IValidationRule>?, IValidatable> getDatePickerValidatable;
        private readonly Func<Type, string, string, string, string, IEnumerable<IValidationRule>?, IValidatable> getEntryValidatable;
        private readonly Func<Type, string, string, IChildFormGroupSettings, IEnumerable<IValidationRule>?, IValidatable> getFormValidatable;
        private readonly Func<Type, string, string, IEnumerable<IValidationRule>?, IValidatable> getHiddenValidatable;
        private readonly Func<Type, string, string, string, string, string, IEnumerable<IValidationRule>?, IValidatable> getLabelValidatable;
        private readonly Func<Type, string, MultiSelectFormControlSettingsDescriptor, IEnumerable<IValidationRule>?, IValidatable> getMultiSelectValidatable;
        private readonly Func<Type, string, object?, DropDownTemplateDescriptor, IEnumerable<IValidationRule>?, IValidatable> getPickerValidatable;
        private readonly Func<string, string, string, IEnumerable<IValidationRule>?, IValidatable> getSwitchValidatable;

        public ValidatableFactory(
            Func<string, string, string, IEnumerable<IValidationRule>?, IValidatable> getCheckboxValidatable,
            Func<Type, string, string, IEnumerable<IValidationRule>?, IValidatable> getDatePickerValidatable,
            Func<Type, string, string, string, string, IEnumerable<IValidationRule>?, IValidatable> getEntryValidatable,
            Func<Type, string, string, IChildFormGroupSettings, IEnumerable<IValidationRule>?, IValidatable> getFormValidatable,
            Func<Type, string, string, IEnumerable<IValidationRule>?, IValidatable> getHiddenValidatable,
            Func<Type, string, string, string, string, string, IEnumerable<IValidationRule>?, IValidatable> getLabelValidatable,
            Func<Type, string, MultiSelectFormControlSettingsDescriptor, IEnumerable<IValidationRule>?, IValidatable> getMultiSelectValidatable,
            Func<Type, string, object?, DropDownTemplateDescriptor, IEnumerable<IValidationRule>?, IValidatable> getPickerValidatable,
            Func<string, string, string, IEnumerable<IValidationRule>?, IValidatable> getSwitchValidatable)
        {
            this.getCheckboxValidatable = getCheckboxValidatable;
            this.getDatePickerValidatable = getDatePickerValidatable;
            this.getEntryValidatable = getEntryValidatable;
            this.getFormValidatable = getFormValidatable;
            this.getHiddenValidatable = getHiddenValidatable;
            this.getLabelValidatable = getLabelValidatable;
            this.getMultiSelectValidatable = getMultiSelectValidatable;
            this.getPickerValidatable = getPickerValidatable;
            this.getSwitchValidatable = getSwitchValidatable;
        }

        public IValidatable CreateCheckboxValidatableObject(string name, string templateName, string checkboxLabel, IEnumerable<IValidationRule>? validations) 
            => getCheckboxValidatable
            (
                name,
                templateName,
                checkboxLabel,
                validations
            );

        public IValidatable CreateDatePickerValidatableObject(Type fieldType, string name, string templateName, IEnumerable<IValidationRule>? validations)
            => getDatePickerValidatable
            (
                fieldType,
                name,
                templateName,
                validations
            );

        public IValidatable CreateEntryValidatableObject(Type fieldType, string name, string templateName, string placeholder, string stringFormat, IEnumerable<IValidationRule>? validations) 
            => getEntryValidatable
            (
                fieldType,
                name,
                templateName,
                placeholder,
                stringFormat,
                validations
            );

        public IValidatable CreateFormValidatableObject(Type fieldType, string name, string validatableObjectName, IChildFormGroupSettings setting, IEnumerable<IValidationRule>? validations)
        {
            return this.getFormValidatable
            (
                fieldType,
                name,
                validatableObjectName,
                setting,
                validations
            );
        }

        public IValidatable CreateHiddenValidatableObject(Type fieldType, string name, string templateName, IEnumerable<IValidationRule>? validations) 
            => getHiddenValidatable
            (
                fieldType,
                name,
                templateName,
                validations
            );

        public IValidatable CreateLabelValidatableObject(Type fieldType, string name, string templateName, string title, string placeholder, string stringFormat, IEnumerable<IValidationRule>? validations) 
            => getLabelValidatable
            (
                fieldType,
                name,
                templateName,
                title,
                placeholder,
                stringFormat,
                validations
            );

        public IValidatable CreateMultiSelectValidatableObject(Type elementType, string name, MultiSelectFormControlSettingsDescriptor setting, IEnumerable<IValidationRule>? validations) 
            => getMultiSelectValidatable
            (
                elementType,
                name,
                setting,
                validations
            );

        public IValidatable CreatePickerValidatableObject(Type fieldType, string name, object? defaultValue, DropDownTemplateDescriptor dropDownTemplate, IEnumerable<IValidationRule>? validations) 
            => getPickerValidatable
            (
                fieldType,
                name,
                defaultValue,
                dropDownTemplate,
                validations
            );

        public IValidatable CreateSwitchValidatableObject(string name, string templateName, string checkboxLabel, IEnumerable<IValidationRule>? validations)
            => getSwitchValidatable
            (
                name,
                templateName,
                checkboxLabel,
                validations
            );
    }
}
