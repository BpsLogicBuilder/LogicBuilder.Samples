using AutoMapper;
using Contoso.Forms.Configuration.Bindings;
using Contoso.Forms.Configuration.DataForm;
using Contoso.Forms.Configuration.Validation;
using Contoso.XPlatform.ViewModels;
using System;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public interface IContextProvider
    {
        IConditionalValidationConditionsBuilder ConditionalValidationConditionsBuilder { get; }
        IHideIfConditionalDirectiveBuilder HideIfConditionalDirectiveBuilder { get; }
        IClearIfConditionalDirectiveBuilder ClearIfConditionalDirectiveBuilder { get; }
        IReloadIfConditionalDirectiveBuilder ReloadIfConditionalDirectiveBuilder { get; }
        IEntityStateUpdater EntityStateUpdater { get; }
        IEntityUpdater EntityUpdater { get; }
        Func<Type, List<ItemBindingDescriptor>, ICollectionCellItemsBuilder> GetCollectionCellItemsBuilder { get; }
        Func<Type, List<FormItemSettingsDescriptor>, IFormGroupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>>, EditFormLayout?, string?, IFieldsCollectionBuilder> GetFieldsCollectionBuilder { get; }
        IGetItemFilterBuilder GetItemFilterBuilder { get; }
        Func<Type, List<FormItemSettingsDescriptor>, IFormGroupBoxSettings, DetailFormLayout?, string?, IReadOnlyFieldsCollectionBuilder> GetReadOnlyFieldsCollectionBuilder { get; }
        Func<Type, List<FormItemSettingsDescriptor>, IFormGroupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>>, EditFormLayout?, string?, IUpdateOnlyFieldsCollectionBuilder> GetUpdateOnlyFieldsCollectionBuilder { get; }
        IHttpService HttpService { get; }
        IMapper Mapper { get; }
        ISearchSelectorBuilder SearchSelectorBuilder { get; }
        IPropertiesUpdater PropertiesUpdater { get; }
        IReadOnlyPropertiesUpdater ReadOnlyPropertiesUpdater { get; }
        IReadOnlyCollectionCellPropertiesUpdater ReadOnlyCollectionCellPropertiesUpdater { get; }
        UiNotificationService UiNotificationService { get; }
    }
}
