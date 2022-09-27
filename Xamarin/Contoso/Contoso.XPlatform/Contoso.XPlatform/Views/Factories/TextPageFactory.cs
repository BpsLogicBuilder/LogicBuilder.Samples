using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.ViewModels.TextPage;
using System;
using Xamarin.Forms;

namespace Contoso.XPlatform.Views.Factories
{
    public class TextPageFactory : ITextPageFactory
    {
        private readonly Func<TextPageViewModel, TextPageViewCS> _getPage;
        private readonly Func<ScreenSettingsBase, TextPageViewModel> _getViewModel;

        public TextPageFactory(
            Func<TextPageViewModel, TextPageViewCS> getPage,
            Func<ScreenSettingsBase, TextPageViewModel> getViewModel)
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
