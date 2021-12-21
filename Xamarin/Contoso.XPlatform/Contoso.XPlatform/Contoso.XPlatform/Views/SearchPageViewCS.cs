using Contoso.XPlatform.Behaviours;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.SearchPage;
using Xamarin.Forms;

namespace Contoso.XPlatform.Views
{
    public class SearchPageViewCS : ContentPage
    {
        public SearchPageViewCS(SearchPageViewModel searchPageViewModel)
        {
            this.searchPageListViewModel = searchPageViewModel.SearchPageEntityViewModel;
            AddContent();
            Visual = VisualMarker.Material;
            BindingContext = this.searchPageListViewModel;
        }

        public SearchPageCollectionViewModelBase searchPageListViewModel { get; set; }
        private Grid transitionGrid;
        private StackLayout page;

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (transitionGrid.IsVisible)
                await page.EntranceTransition(transitionGrid, 150);
        }

        private void AddContent()
        {
            LayoutHelpers.AddToolBarItems(this.ToolbarItems, this.searchPageListViewModel.Buttons);
            Title = searchPageListViewModel.FormSettings.Title;

            Content = new Grid
            {
                Children =
                {
                    (
                        page = new StackLayout
                        {
                            Padding = new Thickness(30),
                            Children =
                            {
                                new Label
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource("HeaderStyle")
                                }
                                .AddBinding(Label.TextProperty, new Binding(nameof(SearchPageCollectionViewModelBase.Title))),
                                new SearchBar
                                {
                                    Behaviors =
                                    {
                                        new EventToCommandBehavior
                                        {
                                            EventName = nameof(SearchBar.TextChanged),
                                        }
                                        .AddBinding(EventToCommandBehavior.CommandProperty, new Binding(nameof(SearchPageCollectionViewModel<Domain.EntityModelBase>.TextChangedCommand)))
                                    }
                                }
                                .AddBinding(SearchBar.TextProperty, new Binding(nameof(SearchPageCollectionViewModel<Domain.EntityModelBase>.SearchText)))
                                .AddBinding(SearchBar.PlaceholderProperty, new Binding(nameof(SearchPageCollectionViewModelBase.FilterPlaceholder))),
                                new RefreshView
                                {
                                    Content = new CollectionView
                                    {
                                        Style = LayoutHelpers.GetStaticStyleResource("SearchFormCollectionViewStyle"),
                                        ItemTemplate = LayoutHelpers.GetCollectionViewItemTemplate
                                        (
                                            this.searchPageListViewModel.FormSettings.ItemTemplateName,
                                            this.searchPageListViewModel.FormSettings.Bindings
                                        )
                                    }
                                    .AddBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(SearchPageCollectionViewModel<Domain.EntityModelBase>.Items)))
                                    .AddBinding(SelectableItemsView.SelectionChangedCommandProperty, new Binding(nameof(SearchPageCollectionViewModel<Domain.EntityModelBase>.SelectionChangedCommand)))
                                    .AddBinding(SelectableItemsView.SelectedItemProperty, new Binding(nameof(SearchPageCollectionViewModel<Domain.EntityModelBase>.SelectedItem)))
                                }
                                .AddBinding(RefreshView.IsRefreshingProperty, new Binding(nameof(SearchPageCollectionViewModel<Domain.EntityModelBase>.IsRefreshing)))
                                .AddBinding(RefreshView.CommandProperty, new Binding(nameof(SearchPageCollectionViewModel<Domain.EntityModelBase>.RefreshCommand)))
                            }
                        }
                    ),
                    (
                        transitionGrid = new Grid().AssignDynamicResource
                        (
                            VisualElement.BackgroundColorProperty,
                            "PageBackgroundColor"
                        )
                    )
                }
            };
        }
    }
}