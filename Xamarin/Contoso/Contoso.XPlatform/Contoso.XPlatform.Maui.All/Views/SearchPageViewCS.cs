using Contoso.XPlatform.Behaviours;
using Contoso.XPlatform.Constants;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.SearchPage;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.Diagnostics.CodeAnalysis;

namespace Contoso.XPlatform.Views
{
    public class SearchPageViewCS : ContentPage
    {
        public SearchPageViewCS(SearchPageViewModel searchPageViewModel)
        {
            this.SearchPageListViewModel = searchPageViewModel.SearchPageEntityViewModel;
            AddContent();
            //Visual = VisualMarker.Default;
            BindingContext = this.SearchPageListViewModel;
        }

        public SearchPageCollectionViewModelBase SearchPageListViewModel { get; set; }
        private Grid transitionGrid;
        private Grid page;

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (transitionGrid.IsVisible)
                await page.EntranceTransition(transitionGrid, 150);
        }

        [MemberNotNull(nameof(transitionGrid), nameof(page))]
        private void AddContent()
        {
            LayoutHelpers.AddToolBarItems(this.ToolbarItems, this.SearchPageListViewModel.Buttons);
            Title = SearchPageListViewModel.FormSettings.Title;

            Content = new Grid
            {
                Children =
                {
                    (
                        page = new Grid
                        {
                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.SearchPageViewLayoutStyle),
                            RowDefinitions =
                            {
                                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                            },
                            Children =
                            {
                                new Label
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.HeaderStyle)
                                }
                                .AddBinding(Label.TextProperty, new Binding(nameof(SearchPageCollectionViewModelBase.Title)))
                                .SetGridRow(0),
                                new Grid
                                {
                                    ColumnDefinitions =
                                    {
                                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) }
                                    },
                                    Children =
                                    {
                                        new SearchBar
                                        {
                                            HorizontalOptions = LayoutOptions.Fill,
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
#if WINDOWS
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PullButtonStyle),
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding(nameof(SearchPageCollectionViewModel<Domain.EntityModelBase>.ButtonRefreshCommand)))
                                        .SetGridColumn(1)
#endif
                                    }
                                }
                                .SetGridRow(1),
                                new RefreshView
                                {/* RefreshView pulls to the right on iOS https://github.com/dotnet/maui/issues/7315 */
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.SearchFormRefreshViewStyle),
                                    VerticalOptions = LayoutOptions.Start,
                                    Content = new CollectionView
                                    {
                                        ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical),
                                        Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.SearchFormCollectionViewStyle),
                                        ItemTemplate = LayoutHelpers.GetCollectionViewItemTemplate
                                        (
                                            this.SearchPageListViewModel.FormSettings.ItemTemplateName,
                                            this.SearchPageListViewModel.FormSettings.Bindings
                                        )
                                    }
                                    .AddBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(SearchPageCollectionViewModel<Domain.EntityModelBase>.Items)))
                                    .AddBinding(SelectableItemsView.SelectionChangedCommandProperty, new Binding(nameof(SearchPageCollectionViewModel<Domain.EntityModelBase>.SelectionChangedCommand)))
                                    .AddBinding(SelectableItemsView.SelectedItemProperty, new Binding(nameof(SearchPageCollectionViewModel<Domain.EntityModelBase>.SelectedItem)))
                                }
                                .AddBinding(RefreshView.IsRefreshingProperty, new Binding(nameof(SearchPageCollectionViewModel<Domain.EntityModelBase>.IsRefreshing)))
                                .AddBinding(RefreshView.CommandProperty, new Binding(nameof(SearchPageCollectionViewModel<Domain.EntityModelBase>.RefreshCommand)))
                                .SetGridRow(2)
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