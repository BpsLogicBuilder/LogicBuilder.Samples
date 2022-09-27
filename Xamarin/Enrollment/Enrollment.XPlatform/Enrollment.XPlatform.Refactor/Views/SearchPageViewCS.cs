using Enrollment.XPlatform.Behaviours;
using Enrollment.XPlatform.Constants;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels;
using Enrollment.XPlatform.ViewModels.SearchPage;
using Xamarin.Forms;

namespace Enrollment.XPlatform.Views
{
    public class SearchPageViewCS : ContentPage
    {
        public SearchPageViewCS(SearchPageViewModelBase searchPageViewModel)
        {
            this.searchPageListViewModel = searchPageViewModel;
            /*MemberNotNull unvailable in 2.1*/
            transitionGrid = null!;
            page = null!;
            /*MemberNotNull unvailable in 2.1*/
            AddContent();
            Visual = VisualMarker.Material;
            BindingContext = this.searchPageListViewModel;
        }

        public SearchPageViewModelBase searchPageListViewModel { get; set; }
        private Grid transitionGrid;
        private StackLayout page;

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (transitionGrid.IsVisible)
                await page.EntranceTransition(transitionGrid, 150);
        }

        //[MemberNotNull(nameof(transitionGrid), nameof(page))]
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
                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.SearchPageViewLayoutStyle),
                            Children =
                            {
                                new Label
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.HeaderStyle)
                                }
                                .AddBinding(Label.TextProperty, new Binding(nameof(SearchPageViewModelBase.Title))),
                                new SearchBar
                                {
                                    Behaviors =
                                    {
                                        new EventToCommandBehavior
                                        {
                                            EventName = nameof(SearchBar.TextChanged),
                                        }
                                        .AddBinding(EventToCommandBehavior.CommandProperty, new Binding(nameof(SearchPageViewModel<Domain.EntityModelBase>.TextChangedCommand)))
                                    }
                                }
                                .AddBinding(SearchBar.TextProperty, new Binding(nameof(SearchPageViewModel<Domain.EntityModelBase>.SearchText)))
                                .AddBinding(SearchBar.PlaceholderProperty, new Binding(nameof(SearchPageViewModelBase.FilterPlaceholder))),
                                new RefreshView
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.SearchFormRefreshViewStyle),
                                    Content = new CollectionView
                                    {
                                        Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.SearchFormCollectionViewStyle),
                                        ItemTemplate = LayoutHelpers.GetCollectionViewItemTemplate
                                        (
                                            this.searchPageListViewModel.FormSettings.ItemTemplateName,
                                            this.searchPageListViewModel.FormSettings.Bindings
                                        )
                                    }
                                    .AddBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(SearchPageViewModel<Domain.EntityModelBase>.Items)))
                                    .AddBinding(SelectableItemsView.SelectionChangedCommandProperty, new Binding(nameof(SearchPageViewModel<Domain.EntityModelBase>.SelectionChangedCommand)))
                                    .AddBinding(SelectableItemsView.SelectedItemProperty, new Binding(nameof(SearchPageViewModel<Domain.EntityModelBase>.SelectedItem)))
                                }
                                .AddBinding(RefreshView.IsRefreshingProperty, new Binding(nameof(SearchPageViewModel<Domain.EntityModelBase>.IsRefreshing)))
                                .AddBinding(RefreshView.CommandProperty, new Binding(nameof(SearchPageViewModel<Domain.EntityModelBase>.RefreshCommand)))
                            }
                        }
                    ),
                    (
                        transitionGrid = new Grid
                        {
                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.TransitionGridStyle)
                        }
                    )
                }
            };
        }
    }
}