using Contoso.Forms.Configuration;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Contoso.XPlatform.Views
{
    public class ReadOnlyChildFormArrayPageCS : ContentPage
    {
        public ReadOnlyChildFormArrayPageCS(IReadOnly formArrayReadOnly)
        {
            this.formArrayReadOnly = formArrayReadOnly;
            this.formsCollectionDisplayTemplateDescriptor = (FormsCollectionDisplayTemplateDescriptor)this.formArrayReadOnly.GetType()
                .GetProperty(nameof(FormArrayReadOnlyObject<ObservableCollection<string>, string>.FormsCollectionDisplayTemplate))
                .GetValue(this.formArrayReadOnly);

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
                            Style = LayoutHelpers.GetStaticStyleResource("FormArrayPopupViewStyle"),
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
                                        }.AddBinding(Label.TextProperty, new Binding(nameof(FormArrayReadOnlyObject<ObservableCollection<string>, string>.Title)))
                                    }
                                },
                                new CollectionView
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource("FormArrayPopupCollectionViewStyle"),
                                    ItemTemplate = LayoutHelpers.GetCollectionViewItemTemplate
                                    (
                                        this.formsCollectionDisplayTemplateDescriptor.TemplateName,
                                        this.formsCollectionDisplayTemplateDescriptor.Bindings
                                    )
                                }
                                .AddBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(FormArrayReadOnlyObject<ObservableCollection<string>, string>.Items)))
                                .AddBinding(SelectableItemsView.SelectionChangedCommandProperty, new Binding(nameof(FormArrayReadOnlyObject<ObservableCollection<string>, string>.SelectionChangedCommand)))
                                .AddBinding(SelectableItemsView.SelectedItemProperty, new Binding(nameof(FormArrayReadOnlyObject<ObservableCollection<string>, string>.SelectedItem))),
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
                                            Style = LayoutHelpers.GetStaticStyleResource("PopupDetailButtonStyle")
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding(nameof(FormArrayReadOnlyObject<ObservableCollection<string>, string>.DetailCommand)))
                                        .SetGridColumn(2),
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource("PopupCancelButtonStyle")
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding(nameof(FormArrayReadOnlyObject<ObservableCollection<string>, string>.CancelCommand)))
                                        .SetGridColumn(3)
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
            this.BindingContext = this.formArrayReadOnly;
        }

        private IReadOnly formArrayReadOnly;
        private FormsCollectionDisplayTemplateDescriptor formsCollectionDisplayTemplateDescriptor;
    }
}