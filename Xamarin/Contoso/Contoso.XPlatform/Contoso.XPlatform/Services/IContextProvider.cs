using AutoMapper;

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
        IFieldsCollectionBuilder FieldsCollectionBuilder { get; }
        IUpdateOnlyFieldsCollectionBuilder UpdateOnlyFieldsCollectionBuilder { get; }
        IReadOnlyFieldsCollectionBuilder ReadOnlyFieldsCollectionBuilder { get; }
        ICollectionCellItemsBuilder CollectionCellItemsBuilder { get; }
        IGetItemFilterBuilder GetItemFilterBuilder { get; }
        IHttpService HttpService { get; }
        IMapper Mapper { get; }
        ISearchSelectorBuilder SearchSelectorBuilder { get; }
        IPropertiesUpdater PropertiesUpdater { get; }
        IReadOnlyPropertiesUpdater ReadOnlyPropertiesUpdater { get; }
        IReadOnlyCollectionCellPropertiesUpdater ReadOnlyCollectionCellPropertiesUpdater { get; }
        UiNotificationService UiNotificationService { get; }
    }
}
