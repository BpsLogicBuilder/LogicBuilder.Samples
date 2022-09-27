using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Validators;
using System;
using System.Collections.Generic;

namespace Enrollment.XPlatform.ViewModels.Validatables.Factories
{
    public interface IValidatableFactory
    {
        IValidatable CreateCheckboxValidatableObject(string name, string templateName, string checkboxLabel, IEnumerable<IValidationRule>? validations);
        IValidatable CreateDatePickerValidatableObject(Type fieldType, string name, string templateName, IEnumerable<IValidationRule>? validations);
        IValidatable CreateEntryValidatableObject(Type fieldType, string name, string templateName, string placeholder, string stringFormat, IEnumerable<IValidationRule>? validations);
        IValidatable CreateFormArrayValidatableObject(Type elementType, string name, FormGroupArraySettingsDescriptor setting, IEnumerable<IValidationRule>? validations);
        IValidatable CreateFormValidatableObject(Type fieldType, string name, string validatableObjectName, IChildFormGroupSettings setting, IEnumerable<IValidationRule>? validations);
        IValidatable CreateHiddenValidatableObject(Type fieldType, string name, string templateName, IEnumerable<IValidationRule>? validations);
        IValidatable CreateLabelValidatableObject(Type fieldType, string name, string templateName, string title, string placeholder, string stringFormat, IEnumerable<IValidationRule>? validations);
        IValidatable CreateMultiSelectValidatableObject(Type elementType, string name, MultiSelectFormControlSettingsDescriptor setting, IEnumerable<IValidationRule>? validations);
        IValidatable CreatePickerValidatableObject(Type fieldType, string name, object? defaultValue, DropDownTemplateDescriptor dropDownTemplate, IEnumerable<IValidationRule>? validations);
        IValidatable CreateSwitchValidatableObject(string name, string templateName, string checkboxLabel, IEnumerable<IValidationRule>? validations);
    }
}
