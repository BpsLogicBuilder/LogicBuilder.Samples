using Enrollment.Forms.Configuration.Bindings;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using System;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Services
{
    public interface IReadOnlyCollectionCellPropertiesUpdater
    {
        void UpdateProperties(IEnumerable<IReadOnly> properties, Type modelType, object entity, List<ItemBindingDescriptor> itemBindings);
    }
}
