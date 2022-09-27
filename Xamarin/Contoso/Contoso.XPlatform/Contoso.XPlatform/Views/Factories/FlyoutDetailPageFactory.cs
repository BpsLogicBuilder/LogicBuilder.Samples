using Contoso.XPlatform.Flow.Settings.Screen;
using System;
using Xamarin.Forms;

namespace Contoso.XPlatform.Views.Factories
{
    public class FlyoutDetailPageFactory : IFlyoutDetailPageFactory
    {
        private readonly IDetailFormFactory _detailFormFactory;
        private readonly IEditFormFactory _editFormFactory;
        private readonly IListPageFactory _listPageFactory;
        private readonly ISearchPageFactory _searchPageFactory;
        private readonly ITextPageFactory _textPageFactory;

        public FlyoutDetailPageFactory(
            IDetailFormFactory detailFormFactory,
            IEditFormFactory editFormFactory,
            IListPageFactory listPageFactory,
            ISearchPageFactory searchPageFactory,
            ITextPageFactory textPageFactory)
        {
            _detailFormFactory = detailFormFactory;
            _editFormFactory = editFormFactory;
            _listPageFactory = listPageFactory;
            _searchPageFactory = searchPageFactory;
            _textPageFactory = textPageFactory;
        }

        public Page CreatePage(ScreenSettingsBase screenSettings)
        {
            if (!Enum.IsDefined(typeof(ViewType), screenSettings.ViewType))
                throw new ArgumentException($"{nameof(screenSettings.ViewType)}: {{CD36EEFB-E368-4864-8445-42479915D72D}}");

            return screenSettings.ViewType switch
            {
                ViewType.DetailForm => _detailFormFactory.CreatePage(screenSettings),
                ViewType.EditForm => _editFormFactory.CreatePage(screenSettings),
                ViewType.ListPage => _listPageFactory.CreatePage(screenSettings),
                ViewType.SearchPage => _searchPageFactory.CreatePage(screenSettings),
                ViewType.TextPage => _textPageFactory.CreatePage(screenSettings),
                _ => throw new ArgumentException($"{nameof(screenSettings.ViewType)}: {{AD813669-B274-438F-85A3-7FC04A734C4A}}"),
            };

            /*
            * Is this or the above better?
            * 
            string viewName = Enum.GetName(typeof(ViewType), screenSettings.ViewType)!;
            IFlyoutDetailPageFactory factory = (IFlyoutDetailPageFactory)App.ServiceProvider.GetRequiredService
            (
                typeof(IFlyoutDetailPageFactory).Assembly.GetType
                (
                    $"Contoso.XPlatform.Views.Factories.I{viewName}Factory"
                ) ?? throw new ArgumentException($"{nameof(screenSettings.ViewType)}: {{55B26AE5-8ECF-4802-8958-CD2E24B5EB22}}")
            );
            return factory.CreatePage(screenSettings);*/
        }
    }
}
