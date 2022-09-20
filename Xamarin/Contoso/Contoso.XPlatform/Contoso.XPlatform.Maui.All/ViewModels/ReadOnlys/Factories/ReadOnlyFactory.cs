using Contoso.Forms.Configuration.DataForm;
using System;

namespace Contoso.XPlatform.ViewModels.ReadOnlys.Factories
{
    internal class ReadOnlyFactory : IReadOnlyFactory
    {
        private readonly Func<string, string, string, IReadOnly> getCheckboxReadOnly;
        private readonly Func<Type, string, IChildFormGroupSettings, IReadOnly> getFormReadOnly;
        private readonly Func<Type, string, string, IReadOnly> getHiddenReadOnly;
        private readonly Func<string, string, string, IReadOnly> getSwitchReadOnly;

        public ReadOnlyFactory(
            Func<string, string, string, IReadOnly> getCheckboxReadOnly,
            Func<Type, string, IChildFormGroupSettings, IReadOnly> getFormReadOnly,
            Func<Type, string, string, IReadOnly> getHiddenReadOnly,
            Func<string, string, string, IReadOnly> getSwitchReadOnly)
        {
            this.getCheckboxReadOnly = getCheckboxReadOnly;
            this.getFormReadOnly = getFormReadOnly;
            this.getHiddenReadOnly = getHiddenReadOnly;
            this.getSwitchReadOnly = getSwitchReadOnly;
        }

        public IReadOnly CreateCheckboxReadOnlyObject(string name, string templateName, string checkboxLabel)
            => getCheckboxReadOnly
            (
                name, 
                templateName, 
                checkboxLabel
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

        public IReadOnly CreateSwitchReadOnlyObject(string name, string templateName, string switchLabel)
            => getSwitchReadOnly
            (
                name,
                templateName,
                switchLabel
            );
    }
}
