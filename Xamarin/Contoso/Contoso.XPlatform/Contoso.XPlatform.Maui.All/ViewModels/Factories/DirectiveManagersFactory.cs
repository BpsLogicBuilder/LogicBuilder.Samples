using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.ViewModels.Factories
{
    internal class DirectiveManagersFactory : IDirectiveManagersFactory
    {
        private readonly Func<Type, ObservableCollection<IValidatable>, IFormGroupSettings, IDirectiveManagers> _getDirectiveManagers;

        public DirectiveManagersFactory(Func<Type, ObservableCollection<IValidatable>, IFormGroupSettings, IDirectiveManagers> getDirectiveManagers)
        {
            _getDirectiveManagers = getDirectiveManagers;
        }

        public IDirectiveManagers GetDirectiveManagers(Type modelType, ObservableCollection<IValidatable> properties, DataFormSettingsDescriptor formSettings) 
            => _getDirectiveManagers
            (
                modelType,
                properties,
                formSettings
            );
    }
}
