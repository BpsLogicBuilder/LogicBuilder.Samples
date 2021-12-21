using Contoso.Forms.Configuration;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.SearchPage;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contoso.XPlatform.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPageView : ContentPage
    {
        public SearchPageCollectionViewModelBase searchPageListViewModel { get; set; }

        public SearchPageView(SearchPageViewModel searchPageViewModel)
        {
            this.searchPageListViewModel = searchPageViewModel.SearchPageEntityViewModel;
            InitializeComponent();
            AddToolBarItems();
            Title = this.searchPageListViewModel.FormSettings.Title;
            this.BindingContext = this.searchPageListViewModel;

            void AddToolBarItems()
            {
                foreach (var button in this.searchPageListViewModel.Buttons)
                    this.ToolbarItems.Add(BuildToolbarItem(button));
            }

            ToolbarItem BuildToolbarItem(CommandButtonDescriptor button)
                => new ToolbarItem
                {
                    AutomationId = button.ShortString,
                    //Text = button.LongString,
                    IconImageSource = new FontImageSource
                    {
                        FontFamily = EditFormViewHelpers.GetFontAwesomeFontFamily(),
                        Glyph = FontAwesomeIcons.Solid[button.ButtonIcon],
                        Size = 20
                    },
                    Order = ToolbarItemOrder.Primary,
                    Priority = 0,
                    CommandParameter = button
                }
                .AddBinding(MenuItem.CommandProperty, new Binding(button.Command))
                .SetAutomationPropertiesName(button.ShortString);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (transitionGrid.IsVisible)
                await page.EntranceTransition(transitionGrid, 150);
        }
    }
}