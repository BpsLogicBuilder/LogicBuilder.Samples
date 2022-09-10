using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.ViewModels;
using Microsoft.Maui.Controls;
using System;

namespace Contoso.XPlatform.Views.Factories
{
    public class ListPageFactory : IListPageFactory
    {
        private readonly Func<ListPageViewModel, ListPageViewCS> _getPage;
        private readonly Func<ScreenSettingsBase, ListPageViewModel> _getViewModel;

        public ListPageFactory(
            Func<ListPageViewModel, ListPageViewCS> getPage,
            Func<ScreenSettingsBase, ListPageViewModel> getViewModel)
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
