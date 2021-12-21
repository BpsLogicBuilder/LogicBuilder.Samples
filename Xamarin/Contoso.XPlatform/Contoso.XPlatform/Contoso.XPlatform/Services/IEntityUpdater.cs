using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Services
{
    public interface IEntityUpdater
    {
        TModel ToModelObject<TModel>(ObservableCollection<IValidatable> modifiedProperties, List<FormItemSettingsDescriptor> fieldSettings, TModel destination = null) where TModel : class;
        object ToModelObject(Type modelType, ObservableCollection<IValidatable> modifiedProperties, List<FormItemSettingsDescriptor> fieldSettings, object destination = null);
    }
}
