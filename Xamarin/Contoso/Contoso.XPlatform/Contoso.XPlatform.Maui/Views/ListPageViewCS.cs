using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.ListPage;

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.Diagnostics.CodeAnalysis;

namespace Contoso.XPlatform.Views
{
    public class ListPageViewCS : ContentPage
    {
        public ListPageViewCS(ListPageViewModel listPageViewModel)
        {
            this.listPageCollectionViewModel = listPageViewModel.ListPageCollectionViewModel;
            AddContent();
            Visual = VisualMarker.Default;
            BindingContext = this.listPageCollectionViewModel;
        }

        public ListPageCollectionViewModelBase listPageCollectionViewModel { get; set; }
        private Grid transitionGrid;
        private StackLayout page;

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (transitionGrid.IsVisible)
                await page.EntranceTransition(transitionGrid, 150);
        }

        [MemberNotNull(nameof(transitionGrid), nameof(page))]
        private void AddContent()
        {
            LayoutHelpers.AddToolBarItems(this.ToolbarItems, this.listPageCollectionViewModel.Buttons);
            Title = this.listPageCollectionViewModel.FormSettings.Title;

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
                                .AddBinding(Label.TextProperty, new Binding(nameof(ListPageCollectionViewModelBase.Title))),
                                new CollectionView
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource("ListFormCollectionViewStyle"),
                                    ItemTemplate = LayoutHelpers.GetCollectionViewItemTemplate
                                    (
                                        this.listPageCollectionViewModel.FormSettings.ItemTemplateName,
                                        this.listPageCollectionViewModel.FormSettings.Bindings
                                    )
                                }
                                .AddBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(ListPageCollectionViewModel<Domain.EntityModelBase>.Items)))
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