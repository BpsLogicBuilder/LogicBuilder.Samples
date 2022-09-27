using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Services
{
    public interface IPropertiesUpdater
    {
        void UpdateProperties(IEnumerable<IValidatable> properties, object? entity, List<FormItemSettingsDescriptor> FieldSettings, string? parentField = null);
    }
}
