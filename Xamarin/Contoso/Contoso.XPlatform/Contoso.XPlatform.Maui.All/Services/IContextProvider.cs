using AutoMapper;

namespace Contoso.XPlatform.Services
{
    public interface IContextProvider
    {
        IValidateIfConditionsBuilder ValidateIfConditionsBuilder { get; }
        IHideIfConditionalDirectiveBuilder HideIfConditionalDirectiveBuilder { get; }
        IClearIfConditionalDirectiveBuilder ClearIfConditionalDirectiveBuilder { get; }
        IReloadIfConditionalDirectiveBuilder ReloadIfConditionalDirectiveBuilder { get; }
        IEntityStateUpdater EntityStateUpdater { get; }
        IEntityUpdater EntityUpdater { get; }
        IGetItemFilterBuilder GetItemFilterBuilder { get; }
        IHttpService HttpService { get; }
        ISearchSelectorBuilder SearchSelectorBuilder { get; }
        IPropertiesUpdater PropertiesUpdater { get; }
        IReadOnlyPropertiesUpdater ReadOnlyPropertiesUpdater { get; }
        IReadOnlyCollectionCellPropertiesUpdater ReadOnlyCollectionCellPropertiesUpdater { get; }
        UiNotificationService UiNotificationService { get; }
    }
}
