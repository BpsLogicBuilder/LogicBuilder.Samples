using AutoMapper;
using Contoso.Forms.Configuration.Bindings;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using LogicBuilder.Expressions.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Contoso.XPlatform.Services
{
    public class ReadOnlyCollectionCellPropertiesUpdater : IReadOnlyCollectionCellPropertiesUpdater
    {
        private readonly IMapper mapper;

        public ReadOnlyCollectionCellPropertiesUpdater(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public void UpdateProperties(IEnumerable<IReadOnly> properties, Type modelType, object entity, List<ItemBindingDescriptor> itemBindings)
            => UpdateReadOnlys(properties, modelType, entity, itemBindings);

        private void UpdateReadOnlys(IEnumerable<IReadOnly> properties, Type modelType, object entity, List<ItemBindingDescriptor> itemBindings)
        {
            IDictionary<string, object?> existingValues = GetExistingValues(entity, itemBindings);/*can't use IMapper.Map here because thekey includes multipart names.*/
            IDictionary<string, IReadOnly> propertiesDictionary = properties.ToDictionary(p => p.Name);

            foreach (var binding in itemBindings)
            {
                Type fieldType = ReadOnlyCollectionCellPropertiesUpdater.GetModelFieldType(modelType, binding.Property);

                if (binding is MultiSelectItemBindingDescriptor multiSelectItemBindingDescriptor)
                {
                    if (existingValues.TryGetValue(binding.Property, out object? @value) && @value != null)
                    {
                        propertiesDictionary[binding.Property].Value = Activator.CreateInstance
                        (
                            typeof(ObservableCollection<>).MakeGenericType
                            (
                                fieldType.GetUnderlyingElementType()
                            ),
                            new object[] { @value }
                        ) ?? throw new ArgumentException($"{fieldType}: {{C5A07697-029C-418F-B6B0-D1E96A72D2BC}}");
                    }
                }
                else if (binding is TextItemBindingDescriptor || binding is DropDownItemBindingDescriptor)
                {
                    if (existingValues.TryGetValue(binding.Property, out object? @value) && @value != null)
                        propertiesDictionary[binding.Property].Value = @value;
                }
                else
                {
                    throw new ArgumentException($"{nameof(binding)}: 06686FA3-6D27-44DB-BF74-752F5A60466F");
                }
            }
        }

        private static IDictionary<string, object?> GetExistingValues(object entity, List<ItemBindingDescriptor> itemBindings) 
            => itemBindings.Select
            (
                binding => new KeyValuePair<string, object?>(binding.Property, GetValue(entity, binding.Property))
            )
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        private static Type GetModelFieldType(Type modelType, string fullPropertyName)
                => modelType.GetMemberInfoFromFullName(fullPropertyName).GetMemberType();

        private static object GetValue(object entity, string fullName)
        {
            string[] parts = fullName.Split('.');
            if (parts.Length == 1)
                return Contoso.Utils.TypeHelpers.GetPropertyValue(entity, fullName);
            else
                return GetValue
                (
                    Contoso.Utils.TypeHelpers.GetPropertyValue(entity, parts[0]),
                    string.Join(",", parts.Skip(1))
                );
        }
    }
}
