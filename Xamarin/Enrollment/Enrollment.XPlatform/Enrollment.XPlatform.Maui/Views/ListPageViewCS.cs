using Enrollment.XPlatform.Constants;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels;
using Enrollment.XPlatform.ViewModels.ListPage;

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.Diagnostics.CodeAnalysis;

namespace Enrollment.XPlatform.Views
{
    public class ListPageViewCS : ContentPage
    {
        public ListPageViewCS(ListPageViewModelBase listPageViewModel)
        {
            this.listPageCollectionViewModel = listPageViewModel;
            AddContent();
            //Visual = VisualMarker.Default;
            BindingContext = this.listPageCollectionViewModel;
        }

        public ListPageViewModelBase listPageCollectionViewModel { get; set; }
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
            LayoutHelpers.AddToolBarItems(this.ToolbarItems, this.listPageCollectionViewModel.Buttons);
            Title = this.listPageCollectionViewModel.FormSettings.Title;

            Content = new Grid
            {
                Children =
                {
                    (
                        page = new Grid
                        {
                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.ListPageViewLayoutStyle),
                            RowDefinitions = 
                            { 
                                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) }, 
                                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) } 
                            },
                            Children =
                            {
                                new Label
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.HeaderStyle)
                                }
                                .AddBinding(Label.TextProperty, new Binding(nameof(ListPageViewModelBase.Title)))
                                .SetGridRow(0),
                                new CollectionView
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.ListFormCollectionViewStyle),
                                    ItemTemplate = LayoutHelpers.GetCollectionViewItemTemplate
                                    (
                                        this.listPageCollectionViewModel.FormSettings.ItemTemplateName,
                                        this.listPageCollectionViewModel.FormSettings.Bindings
                                    )
                                }
                                .AddBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(ListPageViewModel<Domain.EntityModelBase>.Items)))
                                .SetGridRow(1)
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