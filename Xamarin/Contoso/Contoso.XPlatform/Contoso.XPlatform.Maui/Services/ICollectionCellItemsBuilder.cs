using Contoso.Forms.Configuration.Bindings;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using System;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public interface ICollectionCellItemsBuilder
    {
        ICollection<IReadOnly> CreateCellsCollection(List<ItemBindingDescriptor> itemBindings, Type modelType);
    }
}
