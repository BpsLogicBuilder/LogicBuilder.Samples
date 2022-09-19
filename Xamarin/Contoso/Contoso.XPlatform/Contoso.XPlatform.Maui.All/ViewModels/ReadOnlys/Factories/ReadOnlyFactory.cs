using Contoso.Forms.Configuration.DataForm;
using System;

namespace Contoso.XPlatform.ViewModels.ReadOnlys.Factories
{
    internal class ReadOnlyFactory : IReadOnlyFactory
    {
        private readonly Func<Type, string, IChildFormGroupSettings, IReadOnly> getFormReadOnly;

        public ReadOnlyFactory(
            Func<Type, string, IChildFormGroupSettings, IReadOnly> getFormReadOnly)
        {
            this.getFormReadOnly = getFormReadOnly;
        }

        public IReadOnly CreateFormReadOnlyObject(Type fieldType, string name, IChildFormGroupSettings setting) 
            => getFormReadOnly
            (
                fieldType,
                name,
                setting
            );
    }
}
