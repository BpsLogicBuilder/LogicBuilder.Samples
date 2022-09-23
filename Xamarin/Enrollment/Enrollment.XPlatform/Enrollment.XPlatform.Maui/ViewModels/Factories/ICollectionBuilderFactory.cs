using Enrollment.Forms.Configuration.Bindings;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.Forms.Configuration.Validation;
using Enrollment.XPlatform.Services;
using System;
using System.Collections.Generic;

namespace Enrollment.XPlatform.ViewModels.Factories
{
    public interface ICollectionBuilderFactory
    {
        ICollectionCellItemsBuilder GetCollectionCellItemsBuilder(Type modelType, List<ItemBindingDescriptor> bindingDescriptors);
        IFieldsCollectionBuilder GetFieldsCollectionBuilder(Type modelType, List<FormItemSettingsDescriptor> fieldSettings, IFormGroupBoxSettings groupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>> validationMessages, EditFormLayout? formLayout, string? parentName);
        IReadOnlyFieldsCollectionBuilder GetReadOnlyFieldsCollectionBuilder(Type modelType, List<FormItemSettingsDescriptor> fieldSettings, IFormGroupBoxSettings groupBoxSettings, DetailFormLayout? formLayout, string? parentName);
        IUpdateOnlyFieldsCollectionBuilder GetUpdateOnlyFieldsCollectionBuilder(Type modelType, List<FormItemSettingsDescriptor> fieldSettings, IFormGroupBoxSettings groupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>> validationMessages, EditFormLayout? formLayout, string? parentName);
    }
}
