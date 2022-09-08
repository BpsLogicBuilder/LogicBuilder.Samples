﻿using Contoso.Forms.Configuration;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using System;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Views
{
    public class ReadOnlyMultiSelectPageCS : ContentPage
    {
        public ReadOnlyMultiSelectPageCS(IReadOnly multiSelectReadOnly)
        {
            this.multiSelectReadOnly = multiSelectReadOnly;
            this.multiSelectTemplateDescriptor = (MultiSelectTemplateDescriptor)
            (
                (
                    this.multiSelectReadOnly.GetType()
                    .GetProperty(nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.MultiSelectTemplate)) ?? throw new ArgumentException($"{nameof(multiSelectReadOnly)}: {{154F3929-11BC-44D2-BD40-2DCE48B91B00}}")
                ) 
                .GetValue(this.multiSelectReadOnly) ?? throw new ArgumentException($"{nameof(multiSelectReadOnly)}: {{65BCA524-1AD6-4FF9-8056-3FC5C8C5126C}}")
            );

            Content = new AbsoluteLayout
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Children =
                {
                    new ContentView
                    {
                        Content = new Grid
                        {
                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.MultiSelectPopupViewStyle),
                            RowDefinitions =
                            {
                                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                            },
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
                                }
                                .SetGridRow(0),
                                new CollectionView
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource("MultiSelectPopupCollectionViewStyle"),
                                    ItemTemplate = EditFormViewHelpers.GetMultiSelectItemTemplateSelector(this.multiSelectTemplateDescriptor)
                                }
                                .AddBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.Items)))
                                .AddBinding(SelectableItemsView.SelectedItemsProperty, new Binding(nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.SelectedItems), BindingMode.OneWay))
                                .SetGridRow(1),
                                new BoxView { Style = LayoutHelpers.GetStaticStyleResource("PopupFooterSeparatorStyle") }
                                .SetGridRow(2),
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
                                        .AddBinding(Button.CommandProperty, new Binding(nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.CancelCommand)))
                                        .SetGridColumn(2)
                                    }
                                }
                                .SetGridRow(3)
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
            this.BindingContext = this.multiSelectReadOnly;
        }

        private IReadOnly multiSelectReadOnly;
        private MultiSelectTemplateDescriptor multiSelectTemplateDescriptor;
    }
}