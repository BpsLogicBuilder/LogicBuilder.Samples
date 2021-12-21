using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public interface IPropertiesUpdater
    {
        void UpdateProperties(IEnumerable<IValidatable> properties, object entity, List<FormItemSettingsDescriptor> FieldSettings, string parentField = null);
    }
}
