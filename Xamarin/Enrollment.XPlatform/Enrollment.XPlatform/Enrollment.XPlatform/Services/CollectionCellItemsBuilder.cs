using Enrollment.Forms.Configuration.Bindings;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using System;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Services
{
    public class CollectionCellItemsBuilder : ICollectionCellItemsBuilder
    {
        private readonly IContextProvider contextProvider;

        public CollectionCellItemsBuilder(IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public ICollection<IReadOnly> CreateCellsCollection(List<ItemBindingDescriptor> itemBindings, Type modelType)
            => new CollectionCellItemsHelper(itemBindings, this.contextProvider, modelType).CreateFields();
    }
}
