using Contoso.Forms.Configuration;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using System.Collections.ObjectModel;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using System;

namespace Contoso.XPlatform.Views
{
    public class ReadOnlyChildFormArrayPageCS : ContentPage
    {
        public ReadOnlyChildFormArrayPageCS(IReadOnly formArrayReadOnly)
        {
            this.formArrayReadOnly = formArrayReadOnly;
            this.formsCollectionDisplayTemplateDescriptor = (FormsCollectionDisplayTemplateDescriptor)
            (
                (
                    this.formArrayReadOnly.GetType()
                    .GetProperty(nameof(FormArrayReadOnlyObject<ObservableCollection<string>, string>.FormsCollectionDisplayTemplate))
                         ?? throw new ArgumentException($"{nameof(FormArrayReadOnlyObject<ObservableCollection<string>, string>.FormsCollectionDisplayTemplate)}: {{851412FC-9508-428A-BBF4-01FBC0B8F32D}}")
                )
                .GetValue(this.formArrayReadOnly) ?? throw new ArgumentException($"{nameof(formArrayReadOnly)}: {{0C64977A-3B96-44DC-8BA1-11372793D1BC}}")
            );

            Content = new AbsoluteLayout
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Children =
                {
                    new ContentView
                    {
                        Content = new VerticalStackLayout
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
                                new BoxView 
                                { 
                                    Style = LayoutHelpers.GetStaticStyleResource("PopupFooterSeparatorStyle") 
                                },
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
                                        .SetGridColumn(1),
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource("PopupCancelButtonStyle")
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding(nameof(FormArrayReadOnlyObject<ObservableCollection<string>, string>.CancelCommand)))
                                        .SetGridColumn(2)
                                    }
                                }
                            }
                        }
                    }
                    .AssignDynamicResource(VisualElement.BackgroundColorProperty, "PopupViewBackgroundColor")
                    .SetAbsoluteLayoutBounds(new Rect(0, 0, 1, 1))
                    .SetAbsoluteLayoutFlags(AbsoluteLayoutFlags.All)
                }
            };

            this.BackgroundColor = Colors.Transparent;
            //Visual = VisualMarker.Default;
            this.BindingContext = this.formArrayReadOnly;
        }

        private IReadOnly formArrayReadOnly;
        private FormsCollectionDisplayTemplateDescriptor formsCollectionDisplayTemplateDescriptor;
    }
}