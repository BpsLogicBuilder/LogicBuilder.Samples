using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.ViewModels.Factories
{
    internal class DirectiveManagersFactory : IDirectiveManagersFactory
    {
        private readonly Func<Type, ObservableCollection<IValidatable>, IFormGroupSettings, IDirectiveManagers> _getDirectiveManagers;
        private readonly Func<Type, ObservableCollection<IReadOnly>, IFormGroupSettings, IReadOnlyDirectiveManagers> _getReadOnlyDirectiveManagers;

        public DirectiveManagersFactory(
            Func<Type, ObservableCollection<IValidatable>, IFormGroupSettings, IDirectiveManagers> getDirectiveManagers,
            Func<Type, ObservableCollection<IReadOnly>, IFormGroupSettings, IReadOnlyDirectiveManagers> getReadOnlyDirectiveManagers)
        {
            _getDirectiveManagers = getDirectiveManagers;
            _getReadOnlyDirectiveManagers = getReadOnlyDirectiveManagers;
        }

        public IDirectiveManagers GetDirectiveManagers(Type modelType, ObservableCollection<IValidatable> properties, IFormGroupSettings formSettings) 
            => _getDirectiveManagers
            (
                modelType,
                properties,
                formSettings
            );

        public IReadOnlyDirectiveManagers GetReadOnlyDirectiveManagers(Type modelType, ObservableCollection<IReadOnly> properties, IFormGroupSettings formSettings)
            => _getReadOnlyDirectiveManagers
            (
                modelType,
                properties,
                formSettings
            );
    }
}
