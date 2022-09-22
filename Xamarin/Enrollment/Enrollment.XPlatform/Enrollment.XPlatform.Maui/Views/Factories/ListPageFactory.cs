using Enrollment.XPlatform.Flow.Settings.Screen;
using Enrollment.XPlatform.ViewModels.ListPage;
using Microsoft.Maui.Controls;
using System;

namespace Enrollment.XPlatform.Views.Factories
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
