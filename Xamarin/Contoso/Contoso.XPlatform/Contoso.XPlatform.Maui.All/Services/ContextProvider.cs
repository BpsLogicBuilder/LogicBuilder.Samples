using AutoMapper;
using Contoso.Forms.Configuration.DataForm;
using Contoso.Forms.Configuration.Validation;
using Contoso.XPlatform.ViewModels;
using System.Collections.Generic;
using System;

namespace Contoso.XPlatform.Services
{
    public class ContextProvider : IContextProvider
    {
        public ContextProvider(UiNotificationService uiNotificationService,
            IConditionalValidationConditionsBuilder conditionalValidationConditionsBuilder,
            IEntityStateUpdater entityStateUpdater,
            IEntityUpdater entityUpdater,
            Func<Type, List<FormItemSettingsDescriptor>, IFormGroupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>>, EditFormLayout?, string?, IFieldsCollectionBuilder> getFieldsCollectionBuilder,
            Func<Type, List<FormItemSettingsDescriptor>, IFormGroupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>>, EditFormLayout?, string?, IUpdateOnlyFieldsCollectionBuilder> getUpdateOnlyFieldsCollectionBuilder,
            IGetItemFilterBuilder getItemFilterBuilder,
            IHttpService httpService,
            IMapper mapper,
            ISearchSelectorBuilder searchSelectorBuilder,
            IPropertiesUpdater propertiesUpdater,
            IReadOnlyPropertiesUpdater readOnlyPropertiesUpdater,
            IReadOnlyCollectionCellPropertiesUpdater readOnlyCollectionCellPropertiesUpdater,
            IHideIfConditionalDirectiveBuilder hideIfConditionalDirectiveBuilder,
            IClearIfConditionalDirectiveBuilder clearIfConditionalDirectiveBuilder,
            IReloadIfConditionalDirectiveBuilder reloadIfConditionalDirectiveBuilder)
        {
            UiNotificationService = uiNotificationService;
            ConditionalValidationConditionsBuilder = conditionalValidationConditionsBuilder;
            EntityStateUpdater = entityStateUpdater;
            EntityUpdater = entityUpdater;
            GetItemFilterBuilder = getItemFilterBuilder;
            HttpService = httpService;
            Mapper = mapper;
            SearchSelectorBuilder = searchSelectorBuilder;
            PropertiesUpdater = propertiesUpdater;
            ReadOnlyPropertiesUpdater = readOnlyPropertiesUpdater;
            ReadOnlyCollectionCellPropertiesUpdater = readOnlyCollectionCellPropertiesUpdater;
            HideIfConditionalDirectiveBuilder = hideIfConditionalDirectiveBuilder;
            ClearIfConditionalDirectiveBuilder = clearIfConditionalDirectiveBuilder;
            ReloadIfConditionalDirectiveBuilder = reloadIfConditionalDirectiveBuilder;
            GetFieldsCollectionBuilder = getFieldsCollectionBuilder;
            GetUpdateOnlyFieldsCollectionBuilder = getUpdateOnlyFieldsCollectionBuilder;

            //passing IContextProvider to FieldsCollectionBuilder will create a circular dependency
            //so creating the instance here instead of using DI.
            ReadOnlyFieldsCollectionBuilder = new ReadOnlyFieldsCollectionBuilder(this);
            CollectionCellItemsBuilder = new CollectionCellItemsBuilder(this);
        }

        public IConditionalValidationConditionsBuilder ConditionalValidationConditionsBuilder { get; }
        public IHideIfConditionalDirectiveBuilder HideIfConditionalDirectiveBuilder { get; }
        public IClearIfConditionalDirectiveBuilder ClearIfConditionalDirectiveBuilder { get; }
        public IReloadIfConditionalDirectiveBuilder ReloadIfConditionalDirectiveBuilder { get; }
        public IEntityStateUpdater EntityStateUpdater { get; }
        public IEntityUpdater EntityUpdater { get; }
        public Func<Type, List<FormItemSettingsDescriptor>, IFormGroupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>>, EditFormLayout?, string?, IFieldsCollectionBuilder> GetFieldsCollectionBuilder { get; }
        public Func<Type, List<FormItemSettingsDescriptor>, IFormGroupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>>, EditFormLayout?, string?, IUpdateOnlyFieldsCollectionBuilder> GetUpdateOnlyFieldsCollectionBuilder { get; }
        public IReadOnlyFieldsCollectionBuilder ReadOnlyFieldsCollectionBuilder { get; }
        public ICollectionCellItemsBuilder CollectionCellItemsBuilder { get; }
        public IGetItemFilterBuilder GetItemFilterBuilder { get; }
        public IHttpService HttpService { get; }
        public IMapper Mapper { get; }
        public ISearchSelectorBuilder SearchSelectorBuilder { get; }
        public IPropertiesUpdater PropertiesUpdater { get; }
        public IReadOnlyPropertiesUpdater ReadOnlyPropertiesUpdater { get; }
        public IReadOnlyCollectionCellPropertiesUpdater ReadOnlyCollectionCellPropertiesUpdater { get; }
        public UiNotificationService UiNotificationService { get; }
    }
}
