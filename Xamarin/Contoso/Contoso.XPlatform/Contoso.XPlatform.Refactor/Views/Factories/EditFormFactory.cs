using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.ViewModels.EditForm;
using System;
using Xamarin.Forms;

namespace Contoso.XPlatform.Views.Factories
{
    public class EditFormFactory : IEditFormFactory
    {
        private readonly Func<EditFormViewModelBase, EditFormViewCS> _getPage;
        private readonly Func<ScreenSettingsBase, EditFormViewModelBase> _getViewModel;

        public EditFormFactory(
            Func<EditFormViewModelBase, EditFormViewCS> getPage,
            Func<ScreenSettingsBase, EditFormViewModelBase> getViewModel)
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