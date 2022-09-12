using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.ViewModels.TextPage;
using Microsoft.Maui.Controls;
using System;

namespace Contoso.XPlatform.Views.Factories
{
    public class TextPageFactory : ITextPageFactory
    {
        private readonly Func<TextPageScreenViewModel, TextPageViewCS> _getPage;
        private readonly Func<ScreenSettingsBase, TextPageScreenViewModel> _getViewModel;

        public TextPageFactory(
            Func<TextPageScreenViewModel, TextPageViewCS> getPage,
            Func<ScreenSettingsBase, TextPageScreenViewModel> getViewModel)
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
