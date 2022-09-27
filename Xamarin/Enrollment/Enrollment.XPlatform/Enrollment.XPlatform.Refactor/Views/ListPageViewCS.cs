using Enrollment.XPlatform.Constants;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels;
using Enrollment.XPlatform.ViewModels.ListPage;

using Xamarin.Forms;

namespace Enrollment.XPlatform.Views
{
    public class ListPageViewCS : ContentPage
    {
        public ListPageViewCS(ListPageViewModelBase listPageViewModel)
        {
            this.listPageCollectionViewModel = listPageViewModel;
            /*MemberNotNull unvailable in 2.1*/
            transitionGrid = null!;
            page = null!;
            /*MemberNotNull unvailable in 2.1*/
            AddContent();
            Visual = VisualMarker.Material;
            BindingContext = this.listPageCollectionViewModel;
        }

        public ListPageViewModelBase listPageCollectionViewModel { get; set; }
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
            LayoutHelpers.AddToolBarItems(this.ToolbarItems, this.listPageCollectionViewModel.Buttons);
            Title = this.listPageCollectionViewModel.FormSettings.Title;

            Content = new Grid
            {
                Children =
                {
                    (
                        page = new StackLayout
                        {
                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.ListPageViewLayoutStyle),
                            Children =
                            {
                                new Label
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.HeaderStyle)
                                }
                                .AddBinding(Label.TextProperty, new Binding(nameof(ListPageViewModelBase.Title))),
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