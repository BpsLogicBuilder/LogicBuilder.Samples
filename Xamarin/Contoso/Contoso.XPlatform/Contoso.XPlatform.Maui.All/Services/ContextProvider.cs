﻿using AutoMapper;

namespace Contoso.XPlatform.Services
{
    public class ContextProvider : IContextProvider
    {
        public ContextProvider(UiNotificationService uiNotificationService,
            IConditionalValidationConditionsBuilder conditionalValidationConditionsBuilder,
            IEntityStateUpdater entityStateUpdater,
            IEntityUpdater entityUpdater,
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
        }

        public IConditionalValidationConditionsBuilder ConditionalValidationConditionsBuilder { get; }
        public IHideIfConditionalDirectiveBuilder HideIfConditionalDirectiveBuilder { get; }
        public IClearIfConditionalDirectiveBuilder ClearIfConditionalDirectiveBuilder { get; }
        public IReloadIfConditionalDirectiveBuilder ReloadIfConditionalDirectiveBuilder { get; }
        public IEntityStateUpdater EntityStateUpdater { get; }
        public IEntityUpdater EntityUpdater { get; }
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
