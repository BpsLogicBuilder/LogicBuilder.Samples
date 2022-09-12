using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.ViewModels.ListPage;
using Microsoft.Maui.Controls;
using System;

namespace Contoso.XPlatform.Views.Factories
{
    public class ListPageFactory : IListPageFactory
    {
        private readonly Func<ListPageViewModelBase, ListPageViewCS> _getPage;
        private readonly Func<ScreenSettingsBase, ListPageViewModelBase> _getViewModel;

        public ListPageFactory(
            Func<ListPageViewModelBase, ListPageViewCS> getPage,
            Func<ScreenSettingsBase, ListPageViewModelBase> getViewModel)
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
