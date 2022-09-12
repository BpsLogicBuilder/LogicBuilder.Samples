using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.ViewModels.EditForm;
using Microsoft.Maui.Controls;
using System;

namespace Contoso.XPlatform.Views.Factories
{
    public class EditFormFactory : IEditFormFactory
    {
        private readonly Func<EditFormEntityViewModelBase, EditFormViewCS> _getPage;
        private readonly Func<ScreenSettingsBase, EditFormEntityViewModelBase> _getViewModel;

        public EditFormFactory(
            Func<EditFormEntityViewModelBase, EditFormViewCS> getPage,
            Func<ScreenSettingsBase, EditFormEntityViewModelBase> getViewModel)
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