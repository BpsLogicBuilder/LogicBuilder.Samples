using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Validators;
using System;
using System.Collections.Generic;

namespace Contoso.XPlatform.ViewModels.Validatables.Factories
{
    public interface IValidatableFactory
    {
        IValidatable CreateCheckboxValidatableObject(string name, string templateName, string checkboxLabel, IEnumerable<IValidationRule>? validations);
        IValidatable CreateDatePickerValidatableObject(Type fieldType, string name, string templateName, IEnumerable<IValidationRule>? validations);
        IValidatable CreateEntryValidatableObject(Type fieldType, string name, string templateName, string placeholder, string stringFormat, IEnumerable<IValidationRule>? validations);
        IValidatable CreateFormValidatableObject(Type fieldType, string name, string validatableObjectName, IChildFormGroupSettings setting, IEnumerable<IValidationRule>? validations);
        IValidatable CreateHiddenValidatableObject(Type fieldType, string name, string templateName, IEnumerable<IValidationRule>? validations);
        IValidatable CreateLabelValidatableObject(Type fieldType, string name, string templateName, string title, string placeholder, string stringFormat, IEnumerable<IValidationRule>? validations);
        IValidatable CreateSwitchValidatableObject(string name, string templateName, string checkboxLabel, IEnumerable<IValidationRule>? validations);
    }
}
