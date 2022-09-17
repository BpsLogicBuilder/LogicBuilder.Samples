using Contoso.Forms.Configuration.Bindings;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public interface ICollectionCellManager
    {
        /// <summary>
        /// Returns a dictionary of the entity's properties where the key is the property name and the value is an IReadOnly implementation for the property.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="entity"></param>
        /// <param name="itemBindings"></param>
        /// <returns></returns>
        Dictionary<string, IReadOnly> GetCollectionCellDictionaryItem<TModel>(TModel entity, List<ItemBindingDescriptor> itemBindings);

        /// <summary>
        /// Returns a key value pair where the key is a dictionary of the entity's properties and the value is the entity itself.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="entity"></param>
        /// <param name="itemBindings"></param>
        /// <returns></returns>
        KeyValuePair<Dictionary<string, IReadOnly>, TModel> GetCollectionCellDictionaryModelPair<TModel>(TModel entity, List<ItemBindingDescriptor> itemBindings);

        /// <summary>
        /// Updates the IReadOnly objects to reflect the entity.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        /// <param name="itemBindings"></param>
        void UpdateCollectionCellProperties<TModel>(TModel entity, ICollection<IReadOnly> properties, List<ItemBindingDescriptor> itemBindings);
    }
}
