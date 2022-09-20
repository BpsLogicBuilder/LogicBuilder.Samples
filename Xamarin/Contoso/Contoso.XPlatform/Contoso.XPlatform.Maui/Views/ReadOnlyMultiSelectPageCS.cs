using Contoso.Forms.Configuration;
using Contoso.XPlatform.Constants;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
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
                                        }.AddBinding(Label.TextProperty, new Binding(nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.Title)))
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
                                            .AddBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.Items)))
                                            /*SelectedItems not being bound on windows https://github.com/dotnet/maui/issues/8435 */
                                            .AddBinding(SelectableItemsView.SelectedItemsProperty, new Binding(nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.SelectedItems), BindingMode.OneWay)),
                                            new BoxView()
                                            {
                                                GestureRecognizers =
                                                {
                                                    new TapGestureRecognizer()
                                                    {
                                                        Command = new Command(() => { })/*This prevents updates to the collection view*/
                                                    }
                                                }
                                            }
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
                                        .AddBinding(Button.CommandProperty, new Binding(nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.CancelCommand)))
                                        .SetGridColumn(2)
                                    }
                                }
                                .SetGridRow(3)
                            }
                        }
                    }
                }
            };

            this.BackgroundColor = Colors.Transparent;
            //Visual = VisualMarker.Default;
            this.BindingContext = this.multiSelectReadOnly;
        }

        private readonly IReadOnly multiSelectReadOnly;
        private readonly MultiSelectTemplateDescriptor multiSelectTemplateDescriptor;
    }
}