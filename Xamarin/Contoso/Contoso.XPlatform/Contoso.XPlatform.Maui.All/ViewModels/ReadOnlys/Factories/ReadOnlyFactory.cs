using Contoso.Forms.Configuration.DataForm;
using System;

namespace Contoso.XPlatform.ViewModels.ReadOnlys.Factories
{
    internal class ReadOnlyFactory : IReadOnlyFactory
    {
        private readonly Func<Type, string, IChildFormGroupSettings, IReadOnly> getFormReadOnly;
        private readonly Func<Type, string, string, IReadOnly> getHiddenReadOnly;

        public ReadOnlyFactory(
            Func<Type, string, IChildFormGroupSettings, IReadOnly> getFormReadOnly,
            Func<Type, string, string, IReadOnly> getHiddenReadOnly)
        {
            this.getFormReadOnly = getFormReadOnly;
            this.getHiddenReadOnly = getHiddenReadOnly;
        }

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
    }
}
