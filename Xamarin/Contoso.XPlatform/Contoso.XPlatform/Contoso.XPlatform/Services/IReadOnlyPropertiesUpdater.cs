using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public interface IReadOnlyPropertiesUpdater
    {
        void UpdateProperties(IEnumerable<IReadOnly> properties, object entity, List<FormItemSettingsDescriptor> fieldSettings, string parentField = null);
    }
}
