using Contoso.Forms.Configuration.Bindings;
using Contoso.Forms.Configuration.DataForm;
using Contoso.Forms.Configuration.Validation;
using Contoso.XPlatform.Services;
using System;
using System.Collections.Generic;

namespace Contoso.XPlatform.ViewModels.Factories
{
    public interface ICollectionBuilderFactory
    {
        ICollectionCellItemsBuilder GetCollectionCellItemsBuilder(Type modelType, List<ItemBindingDescriptor> bindingDescriptors);
        IFieldsCollectionBuilder GetFieldsCollectionBuilder(Type modelType, List<FormItemSettingsDescriptor> fieldSettings, IFormGroupBoxSettings groupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>> validationMessages, EditFormLayout? formLayout, string? parentName);
        IReadOnlyFieldsCollectionBuilder GetReadOnlyFieldsCollectionBuilder(Type modelType, List<FormItemSettingsDescriptor> fieldSettings, IFormGroupBoxSettings groupBoxSettings, DetailFormLayout? formLayout, string? parentName);
        IUpdateOnlyFieldsCollectionBuilder GetUpdateOnlyFieldsCollectionBuilder(Type modelType, List<FormItemSettingsDescriptor> fieldSettings, IFormGroupBoxSettings groupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>> validationMessages, EditFormLayout? formLayout, string? parentName);
    }
}
