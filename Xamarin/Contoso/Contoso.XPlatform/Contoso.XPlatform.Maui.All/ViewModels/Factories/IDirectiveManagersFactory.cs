using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.ViewModels.Factories
{
    public interface IDirectiveManagersFactory
    {
        IDirectiveManagers GetDirectiveManagers(Type modelType, ObservableCollection<IValidatable> properties, DataFormSettingsDescriptor formSettings);
    }
}
