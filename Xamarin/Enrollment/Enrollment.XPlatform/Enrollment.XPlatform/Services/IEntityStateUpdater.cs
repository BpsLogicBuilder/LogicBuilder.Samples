using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Enrollment.XPlatform.Services
{
    public interface IEntityStateUpdater
    {
        TModel GetUpdatedModel<TModel>(TModel? existingEntity, Dictionary<string, object?> existingAsDictionary, ObservableCollection<IValidatable> modifiedProperties, List<FormItemSettingsDescriptor> fieldSettings);
    }
}
