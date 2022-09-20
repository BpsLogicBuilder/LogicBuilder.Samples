namespace Contoso.XPlatform.Services
{
    public class ContextProvider : IContextProvider
    {
        public ContextProvider(UiNotificationService uiNotificationService,
            IEntityStateUpdater entityStateUpdater,
            IEntityUpdater entityUpdater,
            IGetItemFilterBuilder getItemFilterBuilder,
            IHttpService httpService,
            ISearchSelectorBuilder searchSelectorBuilder,
            IPropertiesUpdater propertiesUpdater,
            IReadOnlyPropertiesUpdater readOnlyPropertiesUpdater,
            IReadOnlyCollectionCellPropertiesUpdater readOnlyCollectionCellPropertiesUpdater)
        {
            UiNotificationService = uiNotificationService;
            EntityStateUpdater = entityStateUpdater;
            EntityUpdater = entityUpdater;
            GetItemFilterBuilder = getItemFilterBuilder;
            HttpService = httpService;
            SearchSelectorBuilder = searchSelectorBuilder;
            PropertiesUpdater = propertiesUpdater;
            ReadOnlyPropertiesUpdater = readOnlyPropertiesUpdater;
            ReadOnlyCollectionCellPropertiesUpdater = readOnlyCollectionCellPropertiesUpdater;
        }

        public IEntityStateUpdater EntityStateUpdater { get; }
        public IEntityUpdater EntityUpdater { get; }
        public IGetItemFilterBuilder GetItemFilterBuilder { get; }
        public IHttpService HttpService { get; }
        public ISearchSelectorBuilder SearchSelectorBuilder { get; }
        public IPropertiesUpdater PropertiesUpdater { get; }
        public IReadOnlyPropertiesUpdater ReadOnlyPropertiesUpdater { get; }
        public IReadOnlyCollectionCellPropertiesUpdater ReadOnlyCollectionCellPropertiesUpdater { get; }
        public UiNotificationService UiNotificationService { get; }
    }
}
