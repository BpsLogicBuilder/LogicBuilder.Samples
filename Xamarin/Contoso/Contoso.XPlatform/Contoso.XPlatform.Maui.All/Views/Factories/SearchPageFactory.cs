using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.ViewModels.SearchPage;
using Microsoft.Maui.Controls;
using System;

namespace Contoso.XPlatform.Views.Factories
{
    public class SearchPageFactory : ISearchPageFactory
    {
        private readonly Func<SearchPageCollectionViewModelBase, SearchPageViewCS> _getPage;
        private readonly Func<ScreenSettingsBase, SearchPageCollectionViewModelBase> _getViewModel;

        public SearchPageFactory(
            Func<SearchPageCollectionViewModelBase, SearchPageViewCS> getPage,
            Func<ScreenSettingsBase, SearchPageCollectionViewModelBase> getViewModel)
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
