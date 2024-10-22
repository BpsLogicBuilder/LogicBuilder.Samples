﻿using Enrollment.Forms.Configuration.Bindings;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels;
using Enrollment.XPlatform.ViewModels.Factories;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Enrollment.XPlatform.Services
{
    internal class CollectionCellManager : ICollectionCellManager
    {
        private readonly ICollectionBuilderFactory collectionBuilderFactory;
        private readonly IReadOnlyCollectionCellPropertiesUpdater readOnlyCollectionCellPropertiesUpdater;

        public CollectionCellManager(
            ICollectionBuilderFactory collectionBuilderFactory,
            IReadOnlyCollectionCellPropertiesUpdater readOnlyCollectionCellPropertiesUpdater)
        {
            this.collectionBuilderFactory = collectionBuilderFactory;
            this.readOnlyCollectionCellPropertiesUpdater = readOnlyCollectionCellPropertiesUpdater;
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

            this.readOnlyCollectionCellPropertiesUpdater.UpdateProperties
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
