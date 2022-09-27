using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.ViewModels.DetailForm;
using System;
using Xamarin.Forms;

namespace Contoso.XPlatform.Views.Factories
{
    public class DetailFormFactory : IDetailFormFactory
    {
        private readonly Func<DetailFormViewModelBase, DetailFormViewCS> _getPage;
        private readonly Func<ScreenSettingsBase, DetailFormViewModelBase> _getViewModel;

        public DetailFormFactory(
            Func<DetailFormViewModelBase, DetailFormViewCS> getPage,
            Func<ScreenSettingsBase, DetailFormViewModelBase> getViewModel)
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