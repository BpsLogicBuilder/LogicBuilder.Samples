using Enrollment.Forms.Configuration;
using Enrollment.XPlatform.Constants;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace Enrollment.XPlatform.Views
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
                                        }.AddBinding(Label.TextProperty, new Binding(nameof(MultiSelectValidatableObject<ObservableCollection<string>, string>.Title)))
                                    }
                                },
                                new CollectionView
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.MultiSelectPopupCollectionViewStyle),
                                    ItemTemplate = EditFormViewHelpers.GetMultiSelectItemTemplateSelector(this.multiSelectTemplateDescriptor)
                                }
                                .AddBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(MultiSelectValidatableObject<ObservableCollection<string>, string>.Items)))
                                .AddBinding(SelectableItemsView.SelectedItemsProperty, new Binding(nameof(MultiSelectValidatableObject<ObservableCollection<string>, string>.SelectedItems))),
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
                            }
                        }
                    }
                }
            };

            this.BackgroundColor = Color.Transparent;
            Visual = VisualMarker.Material;
            this.BindingContext = this.multiSelectValidatable;
        }

        private readonly IValidatable multiSelectValidatable;
        private readonly MultiSelectTemplateDescriptor multiSelectTemplateDescriptor;
    }
}