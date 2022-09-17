using Contoso.Forms.Configuration.Bindings;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.Factories;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.XPlatform.Services
{
    internal class CollectionCellManager : ICollectionCellManager
    {
        private readonly ICollectionBuilderFactory collectionBuilderFactory;
        private readonly IContextProvider contextProvider;

        public CollectionCellManager(ICollectionBuilderFactory collectionBuilderFactory, IContextProvider contextProvider)
        {
            this.collectionBuilderFactory = collectionBuilderFactory;
            this.contextProvider = contextProvider;
        }

        public Dictionary<string, IReadOnly> GetCollectionCellDictionaryItem<TModel>(TModel entity, List<ItemBindingDescriptor> itemBindings)
        {
            ICollection<IReadOnly> properties = collectionBuilderFactory.GetCollectionCellItemsBuilder(typeof(TModel), itemBindings).CreateFields();

            UpdateCollectionCellProperties
            (
                entity,
                properties,
                itemBindings
            );

            return properties.ToDictionary(p => p.Name.ToBindingDictionaryKey());
        }

        public KeyValuePair<Dictionary<string, IReadOnly>, TModel> GetCollectionCellDictionaryModelPair<TModel>(TModel entity, List<ItemBindingDescriptor> itemBindings)
            => new(
                GetCollectionCellDictionaryItem(entity, itemBindings),
                entity
            );

        public void UpdateCollectionCellProperties<TModel>(TModel entity, ICollection<IReadOnly> properties, List<ItemBindingDescriptor> itemBindings)
        {
            if (entity == null)
                throw new ArgumentException($"{nameof(entity)}: {{7088A0A2-CCD8-4D6B-9E04-0E0BC774757D}}");

            Dictionary<string, IReadOnly> propertiesDictionary = properties.ToDictionary(p => p.Name);

            contextProvider.ReadOnlyCollectionCellPropertiesUpdater.UpdateProperties
            (
                properties,
                typeof(TModel),
                entity,
                itemBindings
            );

            itemBindings.ForEach
            (
                binding =>
                {
                    if (binding is DropDownItemBindingDescriptor dropDownItemBinding && dropDownItemBinding.RequiresReload)
                    {
                        if (string.IsNullOrEmpty(dropDownItemBinding.DropDownTemplate.ReloadItemsFlowName))
                            throw new ArgumentException($"{nameof(dropDownItemBinding.DropDownTemplate.ReloadItemsFlowName)}: {{1C9710E4-8D10-4668-BC14-73AF142FF332}}");

                        GetHasItemsSourceReadOnly().Reload(entity, typeof(TModel));
                    }

                    IHasItemsSource GetHasItemsSourceReadOnly()
                        => (IHasItemsSource)propertiesDictionary[binding.Property];
                }
            );
        }
    }
}
