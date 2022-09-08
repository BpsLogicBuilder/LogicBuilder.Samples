using Contoso.Forms.Configuration;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Validatables;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using System;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Views
{
    public class MultiSelectPageCS : ContentPage
    {
        public MultiSelectPageCS(IValidatable multiSelectValidatable)
        {
            this.multiSelectValidatable = multiSelectValidatable;
            this.multiSelectTemplateDescriptor = (MultiSelectTemplateDescriptor)
            (
                (
                    this.multiSelectValidatable.GetType()
                    .GetProperty(nameof(MultiSelectValidatableObject<ObservableCollection<string>, string>.MultiSelectTemplate)) 
                        ?? throw new ArgumentException($"{nameof(MultiSelectValidatableObject<ObservableCollection<string>, string>.MultiSelectTemplate)}: {{6841CE95-75AE-4F3D-A541-3D807E5039AB}}")
                )
                .GetValue(this.multiSelectValidatable) ?? throw new ArgumentException($"{nameof(multiSelectValidatable)}: {{39C66580-AC69-4030-BCC6-9C81BAC2F1F3}}")
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
                                        }.AddBinding(Label.TextProperty, new Binding(nameof(MultiSelectValidatableObject<ObservableCollection<string>, string>.Title)))
                                    }
                                }
                                .SetGridRow(0),
                                new CollectionView
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource("MultiSelectPopupCollectionViewStyle"),
                                    ItemTemplate = EditFormViewHelpers.GetMultiSelectItemTemplateSelector(this.multiSelectTemplateDescriptor)
                                }
                                .AddBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(MultiSelectValidatableObject<ObservableCollection<string>, string>.Items)))
                                .AddBinding(SelectableItemsView.SelectedItemsProperty, new Binding(nameof(MultiSelectValidatableObject<ObservableCollection<string>, string>.SelectedItems)))
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
                                        .AddBinding(Button.CommandProperty, new Binding(nameof(MultiSelectValidatableObject<ObservableCollection<string>, string>.CancelCommand)))
                                        .SetGridColumn(1),
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource("PopupAcceptButtonStyle")
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding(nameof(MultiSelectValidatableObject<ObservableCollection<string>, string>.SubmitCommand)))
                                        .SetGridColumn(2)
                                    }
                                }
                                .SetGridRow(3),
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
            this.BindingContext = this.multiSelectValidatable;
        }

        private readonly IValidatable multiSelectValidatable;
        private readonly MultiSelectTemplateDescriptor multiSelectTemplateDescriptor;
    }
}