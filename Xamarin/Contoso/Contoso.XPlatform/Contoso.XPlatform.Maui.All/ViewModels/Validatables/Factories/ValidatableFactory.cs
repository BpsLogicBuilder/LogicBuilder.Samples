using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Validators;
using System;
using System.Collections.Generic;

namespace Contoso.XPlatform.ViewModels.Validatables.Factories
{
    internal class ValidatableFactory : IValidatableFactory
    {
        private readonly Func<Type, string, string, string, string, IEnumerable<IValidationRule>?, IValidatable> getEntryValidatable;
        private readonly Func<Type, string, string, IChildFormGroupSettings, IEnumerable<IValidationRule>?, IValidatable> getFormValidatable;
        private readonly Func<Type, string, string, IEnumerable<IValidationRule>?, IValidatable> getHiddenValidatable;
        private readonly Func<Type, string, string, string, string, string, IEnumerable<IValidationRule>?, IValidatable> getLabelValidatable;

        public ValidatableFactory(
            Func<Type, string, string, string, string, IEnumerable<IValidationRule>?, IValidatable> getEntryValidatable,
            Func<Type, string, string, IChildFormGroupSettings, IEnumerable<IValidationRule>?, IValidatable> getFormValidatable,
            Func<Type, string, string, IEnumerable<IValidationRule>?, IValidatable> getHiddenValidatable,
            Func<Type, string, string, string, string, string, IEnumerable<IValidationRule>?, IValidatable> getLabelValidatable)
        {
            this.getEntryValidatable = getEntryValidatable;
            this.getFormValidatable = getFormValidatable;
            this.getHiddenValidatable = getHiddenValidatable;
            this.getLabelValidatable = getLabelValidatable;
        }

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
        {
            return this.getHiddenValidatable
            (
                fieldType,
                name,
                templateName,
                validations
            );
        }

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
    }
}
