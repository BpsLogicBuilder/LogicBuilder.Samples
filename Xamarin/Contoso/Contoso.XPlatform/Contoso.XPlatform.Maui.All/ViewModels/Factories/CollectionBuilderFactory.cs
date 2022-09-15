using Contoso.Forms.Configuration.Bindings;
using Contoso.Forms.Configuration.DataForm;
using Contoso.Forms.Configuration.Validation;
using Contoso.XPlatform.Services;
using System;
using System.Collections.Generic;

namespace Contoso.XPlatform.ViewModels.Factories
{
    internal class CollectionBuilderFactory : ICollectionBuilderFactory
    {
        public Func<Type, List<ItemBindingDescriptor>, ICollectionCellItemsBuilder> _getCollectionCellItemsBuilder;
        public Func<Type, List<FormItemSettingsDescriptor>, IFormGroupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>>, EditFormLayout?, string?, IFieldsCollectionBuilder> _getFieldsCollectionBuilder;
        public Func<Type, List<FormItemSettingsDescriptor>, IFormGroupBoxSettings, DetailFormLayout?, string?, IReadOnlyFieldsCollectionBuilder> _getReadOnlyFieldsCollectionBuilder;
        public Func<Type, List<FormItemSettingsDescriptor>, IFormGroupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>>, EditFormLayout?, string?, IUpdateOnlyFieldsCollectionBuilder> _getUpdateOnlyFieldsCollectionBuilder;

        public CollectionBuilderFactory(
            Func<Type, List<ItemBindingDescriptor>, ICollectionCellItemsBuilder> getCollectionCellItemsBuilder,
            Func<Type, List<FormItemSettingsDescriptor>, IFormGroupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>>, EditFormLayout?, string?, IFieldsCollectionBuilder> getFieldsCollectionBuilder,
            Func<Type, List<FormItemSettingsDescriptor>, IFormGroupBoxSettings, DetailFormLayout?, string?, IReadOnlyFieldsCollectionBuilder> getReadOnlyFieldsCollectionBuilder,
            Func<Type, List<FormItemSettingsDescriptor>, IFormGroupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>>, EditFormLayout?, string?, IUpdateOnlyFieldsCollectionBuilder> getUpdateOnlyFieldsCollectionBuilder)
        {
            _getCollectionCellItemsBuilder = getCollectionCellItemsBuilder;
            _getFieldsCollectionBuilder = getFieldsCollectionBuilder;
            _getReadOnlyFieldsCollectionBuilder = getReadOnlyFieldsCollectionBuilder;
            _getUpdateOnlyFieldsCollectionBuilder = getUpdateOnlyFieldsCollectionBuilder;
        }

        public ICollectionCellItemsBuilder GetCollectionCellItemsBuilder(Type modelType, List<ItemBindingDescriptor> bindingDescriptors) 
            => _getCollectionCellItemsBuilder
            (
                modelType,
                bindingDescriptors
            );

        public IFieldsCollectionBuilder GetFieldsCollectionBuilder(Type modelType, List<FormItemSettingsDescriptor> fieldSettings, IFormGroupBoxSettings groupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>> validationMessages, EditFormLayout? formLayout, string? parentName) 
            => _getFieldsCollectionBuilder
            (
                modelType,
                fieldSettings,
                groupBoxSettings,
                validationMessages,
                formLayout,
                parentName
            );

        public IReadOnlyFieldsCollectionBuilder GetReadOnlyFieldsCollectionBuilder(Type modelType, List<FormItemSettingsDescriptor> fieldSettings, IFormGroupBoxSettings groupBoxSettings, DetailFormLayout? formLayout, string? parentName) 
            => _getReadOnlyFieldsCollectionBuilder
            (
                modelType,
                fieldSettings,
                groupBoxSettings,
                formLayout,
                parentName
            );

        public IUpdateOnlyFieldsCollectionBuilder GetUpdateOnlyFieldsCollectionBuilder(Type modelType, List<FormItemSettingsDescriptor> fieldSettings, IFormGroupBoxSettings groupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>> validationMessages, EditFormLayout? formLayout, string? parentName)
        {
            return _getUpdateOnlyFieldsCollectionBuilder
            ( 
                modelType,
                fieldSettings,
                groupBoxSettings,
                validationMessages,
                formLayout,
                parentName
            );
        }
    }
}
