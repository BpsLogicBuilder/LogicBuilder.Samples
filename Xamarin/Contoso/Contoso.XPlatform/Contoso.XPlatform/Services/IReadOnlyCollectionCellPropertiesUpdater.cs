using Contoso.Forms.Configuration.Bindings;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using System;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public interface IReadOnlyCollectionCellPropertiesUpdater
    {
        void UpdateProperties(IEnumerable<IReadOnly> properties, Type modelType, object entity, List<ItemBindingDescriptor> itemBindings);
    }
}
