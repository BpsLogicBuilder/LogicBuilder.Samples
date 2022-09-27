using Enrollment.XPlatform.Flow.Settings.Screen;
using Enrollment.XPlatform.ViewModels.DetailForm;
using System;
using Xamarin.Forms;

namespace Enrollment.XPlatform.Views.Factories
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