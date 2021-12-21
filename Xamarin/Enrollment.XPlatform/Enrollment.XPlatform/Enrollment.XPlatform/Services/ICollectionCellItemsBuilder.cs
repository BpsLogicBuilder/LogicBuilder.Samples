using Enrollment.Forms.Configuration.Bindings;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using System;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Services
{
    public interface ICollectionCellItemsBuilder
    {
        ICollection<IReadOnly> CreateCellsCollection(List<ItemBindingDescriptor> itemBindings, Type modelType);
    }
}
