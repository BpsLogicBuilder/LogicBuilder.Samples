using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.ViewModels;
using Microsoft.Maui.Controls;
using System;

namespace Contoso.XPlatform.Views.Factories
{
    public class EditFormFactory : IEditFormFactory
    {
        private readonly Func<EditFormViewModel, EditFormViewCS> _getPage;
        private readonly Func<ScreenSettingsBase, EditFormViewModel> _getViewModel;

        public EditFormFactory(
            Func<EditFormViewModel, EditFormViewCS> getPage,
            Func<ScreenSettingsBase, EditFormViewModel> getViewModel)
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