using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.ViewModels;
using Microsoft.Maui.Controls;
using System;

namespace Contoso.XPlatform.Views.Factories
{
    public class DetailFormFactory : IDetailFormFactory
    {
        private readonly Func<DetailFormViewModel, DetailFormViewCS> _getPage;
        private readonly Func<ScreenSettingsBase, DetailFormViewModel> _getViewModel;

        public DetailFormFactory(
            Func<DetailFormViewModel, DetailFormViewCS> getPage,
            Func<ScreenSettingsBase, DetailFormViewModel> getViewModel)
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