using Enrollment.Forms.Configuration;
using Enrollment.XPlatform.Constants;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Enrollment.XPlatform.Views
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
                        Content = new StackLayout
                        {
                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.MultiSelectPopupViewStyle),
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
                                },
                                new Grid
                                {
                                    Children =
                                    {
                                        new CollectionView
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.MultiSelectPopupCollectionViewStyle),
                                            ItemTemplate = EditFormViewHelpers.GetMultiSelectItemTemplateSelector(this.multiSelectTemplateDescriptor)
                                        }
                                        .AddBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.Items)))
                                        .AddBinding(SelectableItemsView.SelectedItemsProperty, new Binding(nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.SelectedItems))),
                                        new BoxView()
                                    }
                                },
                                new BoxView { Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupFooterSeparatorStyle) },
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
                            }
                        }
                    }
                }
            };

            this.BackgroundColor = Color.Transparent;
            Visual = VisualMarker.Material;
            this.BindingContext = this.multiSelectReadOnly;
        }

        private readonly IReadOnly multiSelectReadOnly;
        private readonly MultiSelectTemplateDescriptor multiSelectTemplateDescriptor;
    }
}