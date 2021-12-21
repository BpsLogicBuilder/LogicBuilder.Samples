using Contoso.Forms.Configuration;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Contoso.XPlatform.Views
{
    public class ReadOnlyMultiSelectPageCS : ContentPage
    {
        public ReadOnlyMultiSelectPageCS(IReadOnly multiSelectReadOnly)
        {
            this.multiSelectReadOnly = multiSelectReadOnly;
            this.multiSelectTemplateDescriptor = (MultiSelectTemplateDescriptor)this.multiSelectReadOnly.GetType()
                .GetProperty(nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.MultiSelectTemplate))
                .GetValue(this.multiSelectReadOnly);

            Content = new AbsoluteLayout
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Children =
                {
                    new ContentView
                    {
                        Content = new StackLayout
                        {
                            Style = LayoutHelpers.GetStaticStyleResource("MultiSelectPopupViewStyle"),
                            Children =
                            {
                                new Grid
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource("PopupHeaderStyle"),
                                    Children =
                                    {
                                        new Label
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource("PopupHeaderLabelStyle"),
                                        }.AddBinding(Label.TextProperty, new Binding("Title"))
                                    }
                                },
                                new Grid
                                {
                                    Children =
                                    {
                                        new CollectionView
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource("MultiSelectPopupCollectionViewStyle"),
                                            ItemTemplate = EditFormViewHelpers.GetMultiSelectItemTemplateSelector(this.multiSelectTemplateDescriptor)
                                        }
                                        .AddBinding(ItemsView.ItemsSourceProperty, new Binding("Items"))
                                        .AddBinding(SelectableItemsView.SelectedItemsProperty, new Binding("SelectedItems")),
                                        new BoxView()
                                    }
                                },
                                new BoxView { Style = LayoutHelpers.GetStaticStyleResource("PopupFooterSeparatorStyle") },
                                new Grid
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource("PopupFooterStyle"),
                                    ColumnDefinitions =
                                    {
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) },
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) },
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) }
                                    },
                                    Children =
                                    {
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource("PopupCancelButtonStyle")
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding("CancelCommand"))
                                        .SetGridColumn(2)
                                    }
                                }
                            }
                        }
                    }
                    .AssignDynamicResource(VisualElement.BackgroundColorProperty, "PopupViewBackgroundColor")
                    .SetAbsoluteLayoutBounds(new Rectangle(0, 0, 1, 1))
                    .SetAbsoluteLayoutFlags(AbsoluteLayoutFlags.All)
                }
            };

            this.BackgroundColor = Color.Transparent;
            Visual = VisualMarker.Material;
            this.BindingContext = this.multiSelectReadOnly;
        }

        private IReadOnly multiSelectReadOnly;
        private MultiSelectTemplateDescriptor multiSelectTemplateDescriptor;
    }
}