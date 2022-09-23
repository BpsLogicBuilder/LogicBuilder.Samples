using Enrollment.XPlatform.Flow.Settings.Screen;
using Enrollment.XPlatform.ViewModels.SearchPage;
using Microsoft.Maui.Controls;
using System;

namespace Enrollment.XPlatform.Views.Factories
{
    public class SearchPageFactory : ISearchPageFactory
    {
        private readonly Func<SearchPageViewModelBase, SearchPageViewCS> _getPage;
        private readonly Func<ScreenSettingsBase, SearchPageViewModelBase> _getViewModel;

        public SearchPageFactory(
            Func<SearchPageViewModelBase, SearchPageViewCS> getPage,
            Func<ScreenSettingsBase, SearchPageViewModelBase> getViewModel)
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
