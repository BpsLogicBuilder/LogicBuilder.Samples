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
            IDictionary<string, object> existingValues = mapper.Map<Dictionary<string, object>>(entity) ?? new Dictionary<string, object>();
            IDictionary<string, IReadOnly> propertiesDictionary = properties.ToDictionary(p => p.Name);

            foreach (var binding in itemBindings)
            {
                Type fieldType = GetModelFieldType(modelType, binding.Property);

                if (binding is MultiSelectItemBindingDescriptor multiSelectItemBindingDescriptor)
                {
                    if (existingValues.TryGetValue(binding.Property, out object @value) && @value != null)
                    {
                        propertiesDictionary[binding.Property].Value = Activator.CreateInstance
                        (
                            typeof(ObservableCollection<>).MakeGenericType
                            (
                                fieldType.GetUnderlyingElementType()
                            ),
                            new object[] { @value }
                        );
                    }
                }
                else if (binding is TextItemBindingDescriptor || binding is DropDownItemBindingDescriptor)
                {
                    if (existingValues.TryGetValue(binding.Property, out object @value) && @value != null)
                        propertiesDictionary[binding.Property].Value = @value;
                }
                else
                {
                    throw new ArgumentException($"{nameof(binding)}: 06686FA3-6D27-44DB-BF74-752F5A60466F");
                }
            }
        }

        Type GetModelFieldType(Type modelType, string fullPropertyName)
                => modelType.GetMemberInfoFromFullName(fullPropertyName).GetMemberType();
    }
}
