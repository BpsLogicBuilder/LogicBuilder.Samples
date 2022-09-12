using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.ViewModels.ListPage;
using Microsoft.Maui.Controls;
using System;

namespace Contoso.XPlatform.Views.Factories
{
    public class ListPageFactory : IListPageFactory
    {
        private readonly Func<ListPageCollectionViewModelBase, ListPageViewCS> _getPage;
        private readonly Func<ScreenSettingsBase, ListPageCollectionViewModelBase> _getViewModel;

        public ListPageFactory(
            Func<ListPageCollectionViewModelBase, ListPageViewCS> getPage,
            Func<ScreenSettingsBase, ListPageCollectionViewModelBase> getViewModel)
        {
            _getPage = getPage;
            _getViewModel = getViewModel;
        }

        public Page CreatePage(ScreenSettingsBase screenSettings)
            => _getPage
            (
                _getViewModel(screenSettings)
            );
    }
}
