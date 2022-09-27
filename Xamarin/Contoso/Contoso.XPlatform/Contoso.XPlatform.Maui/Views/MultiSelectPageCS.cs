using Contoso.Forms.Configuration;
using Contoso.XPlatform.Constants;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Validatables;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
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
                Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupDialogAbsoluteLayoutStyle),
                Children =
                {
                    new ContentView
                    {
                        Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupDialogContentViewStyle),
                        Content = new Grid
                        {
                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.MultiSelectPopupViewStyle),
                            RowDefinitions =
                            {
                                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                            },
                            Children =
                            {
                                new Grid
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupHeaderStyle),
                                    Children =
                                    {
                                        new Label
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupHeaderLabelStyle),
                                        }.AddBinding(Label.TextProperty, new Binding(nameof(MultiSelectValidatableObject<ObservableCollection<string>, string>.Title)))
                                    }
                                }
                                .SetGridRow(0),
                                new ScrollView
                                {
                                    Content = new Grid
                                    {
                                        Children =
                                        {
                                            new CollectionView
                                            {
                                                Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.MultiSelectPopupCollectionViewStyle),
                                                ItemTemplate = EditFormViewHelpers.GetMultiSelectItemTemplateSelector(this.multiSelectTemplateDescriptor)
                                            }
                                            .AddBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(MultiSelectValidatableObject<ObservableCollection<string>, string>.Items)))
                                            /*SelectedItems not being bound on windows https://github.com/dotnet/maui/issues/8435 */
                                            .AddBinding(SelectableItemsView.SelectedItemsProperty, new Binding(nameof(MultiSelectValidatableObject<ObservableCollection<string>, string>.SelectedItems)))
                                        }
                                    }
                                }
                                .SetGridRow(1),
                                new BoxView { Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupFooterSeparatorStyle) }
                                .SetGridRow(2),
                                new Grid
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupFooterStyle),
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
                                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupCancelButtonStyle)
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding(nameof(MultiSelectValidatableObject<ObservableCollection<string>, string>.CancelCommand)))
                                        .SetGridColumn(1),
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupAcceptButtonStyle)
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding(nameof(MultiSelectValidatableObject<ObservableCollection<string>, string>.SubmitCommand)))
                                        .SetGridColumn(2)
                                    }
                                }
                                .SetGridRow(3),
                            }
                        }
                    }
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