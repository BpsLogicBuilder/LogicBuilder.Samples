using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.ViewModels.DetailForm;
using Microsoft.Maui.Controls;
using System;

namespace Contoso.XPlatform.Views.Factories
{
    public class DetailFormFactory : IDetailFormFactory
    {
        private readonly Func<DetailFormEntityViewModelBase, DetailFormViewCS> _getPage;
        private readonly Func<ScreenSettingsBase, DetailFormEntityViewModelBase> _getViewModel;

        public DetailFormFactory(
            Func<DetailFormEntityViewModelBase, DetailFormViewCS> getPage,
            Func<ScreenSettingsBase, DetailFormEntityViewModelBase> getViewModel)
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