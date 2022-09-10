using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.ViewModels;
using Microsoft.Maui.Controls;
using System;

namespace Contoso.XPlatform.Views.Factories
{
    public class SearchPageFactory : ISearchPageFactory
    {
        private readonly Func<SearchPageViewModel, SearchPageViewCS> _getPage;
        private readonly Func<ScreenSettingsBase, SearchPageViewModel> _getViewModel;

        public SearchPageFactory(
            Func<SearchPageViewModel, SearchPageViewCS> getPage,
            Func<ScreenSettingsBase, SearchPageViewModel> getViewModel)
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
