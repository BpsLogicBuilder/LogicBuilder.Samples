using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.DataForm;
using System;
using System.Collections.Generic;

namespace Enrollment.XPlatform.ViewModels.ReadOnlys.Factories
{
    internal class ReadOnlyFactory : IReadOnlyFactory
    {
        private readonly Func<string, string, string, IReadOnly> getCheckboxReadOnly;
        private readonly Func<Type, string, FormGroupArraySettingsDescriptor, IReadOnly> getFormArrayReadOnly;
        private readonly Func<Type, string, IChildFormGroupSettings, IReadOnly> getFormReadOnly;
        private readonly Func<Type, string, string, IReadOnly> getHiddenReadOnly;
        private readonly Func<Type, string, List<string>, string, string, MultiSelectTemplateDescriptor, IReadOnly> getMultiSelectReadOnly;
        private readonly Func<Type, string, string, string, DropDownTemplateDescriptor, IReadOnly> getPickerReadOnly;
        private readonly Func<string, string, string, IReadOnly> getSwitchReadOnly;
        private readonly Func<Type, string, string, string, string, IReadOnly> getTextFieldReadOnly;

        public ReadOnlyFactory(
            Func<string, string, string, IReadOnly> getCheckboxReadOnly,
            Func<Type, string, FormGroupArraySettingsDescriptor, IReadOnly> getFormArrayReadOnly,
            Func<Type, string, IChildFormGroupSettings, IReadOnly> getFormReadOnly,
            Func<Type, string, string, IReadOnly> getHiddenReadOnly,
            Func<Type, string, List<string>, string, string, MultiSelectTemplateDescriptor, IReadOnly> getMultiSelectReadOnly,
            Func<Type, string, string, string, DropDownTemplateDescriptor, IReadOnly> getPickerReadOnly,
            Func<string, string, string, IReadOnly> getSwitchReadOnly,
            Func<Type, string, string, string, string, IReadOnly> getTextFieldReadOnly)
        {
            this.getCheckboxReadOnly = getCheckboxReadOnly;
            this.getFormArrayReadOnly = getFormArrayReadOnly;
            this.getFormReadOnly = getFormReadOnly;
            this.getHiddenReadOnly = getHiddenReadOnly;
            this.getMultiSelectReadOnly = getMultiSelectReadOnly;
            this.getPickerReadOnly = getPickerReadOnly;
            this.getSwitchReadOnly = getSwitchReadOnly;
            this.getTextFieldReadOnly = getTextFieldReadOnly;
        }

        public IReadOnly CreateCheckboxReadOnlyObject(string name, string templateName, string checkboxLabel)
            => getCheckboxReadOnly
            (
                name, 
                templateName, 
                checkboxLabel
            );

        public IReadOnly CreateFormArrayReadOnlyObject(Type elementType, string name, FormGroupArraySettingsDescriptor setting)
            => this.getFormArrayReadOnly
            (
                elementType,
                name,
                setting
            );

        public IReadOnly CreateFormReadOnlyObject(Type fieldType, string name, IChildFormGroupSettings setting) 
            => getFormReadOnly
            (
                fieldType,
                name,
                setting
            );

        public IReadOnly CreateHiddenReadOnlyObject(Type fieldType, string name, string templateName)
            => getHiddenReadOnly
            (
                fieldType,
                name,
                templateName
            );

        public IReadOnly CreateMultiSelectReadOnlyObject(Type elementType, string name, List<string> keyFields, string title, string stringFormat, MultiSelectTemplateDescriptor multiSelectTemplate)
            => this.getMultiSelectReadOnly
            (
                elementType,
                name, 
                keyFields,
                title, 
                stringFormat, 
                multiSelectTemplate
            );

        public IReadOnly CreatePickerReadOnlyObject(Type fieldType, string name, string title, string stringFormat, DropDownTemplateDescriptor dropDownTemplate)
            => this.getPickerReadOnly
            (
                fieldType, 
                name, 
                title, 
                stringFormat, 
                dropDownTemplate
            );

        public IReadOnly CreateSwitchReadOnlyObject(string name, string templateName, string switchLabel)
            => getSwitchReadOnly
            (
                name,
                templateName,
                switchLabel
            );

        public IReadOnly CreateTextFieldReadOnlyObject(Type fieldType, string name, string templateName, string title, string stringFormat)
            => getTextFieldReadOnly
            (
                fieldType, 
                name, 
                templateName, 
                title, 
                stringFormat
            );
    }
}
