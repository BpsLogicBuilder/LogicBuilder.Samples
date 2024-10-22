﻿using Enrollment.Forms.Configuration;
using Enrollment.XPlatform.Constants;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.Validatables;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Enrollment.XPlatform.Views
{
    public class MultiSelectPageCS : ContentPage
    {
        public MultiSelectPageCS(IValidatable multiSelectValidatable)
        {
            this.multiSelectValidatable = multiSelectValidatable;
            this.multiSelectTemplateDescriptor = (MultiSelectTemplateDescriptor)GetPropertyValue(nameof(MultiSelectValidatableObject<ObservableCollection<string>, string>.MultiSelectTemplate));
            IEnumerable? items = (IEnumerable)GetPropertyValue(nameof(MultiSelectValidatableObject<ObservableCollection<string>, string>.Items));

            object GetPropertyValue(string propertyName)
            {
                return (
                    this.multiSelectValidatable.GetType()
                    .GetProperty(propertyName)
                        ?? throw new ArgumentException($"{propertyName}: {{12446C1B-6BD3-4C15-A5A8-7E6A3B8CAC91}}")
                )
                .GetValue(this.multiSelectValidatable) ?? throw new ArgumentException($"{propertyName}: {{13B92D66-0FB3-4C12-96B0-464D9A5177CE}}");
            }

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
                                new ScrollView
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.MultiSelectParentStyle),
                                    Content = new CollectionView
                                    {
                                        HeightRequest = GetCollectionViewHeight(items),
                                        Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.MultiSelectPopupCollectionViewStyle),
                                        ItemTemplate = EditFormViewHelpers.GetMultiSelectItemTemplateSelector(this.multiSelectTemplateDescriptor)
                                    }
                                    .AddBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(MultiSelectValidatableObject<ObservableCollection<string>, string>.Items)))
                                    .AddBinding(SelectableItemsView.SelectedItemsProperty, new Binding(nameof(MultiSelectValidatableObject<ObservableCollection<string>, string>.SelectedItems)))
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

        private double GetCollectionViewHeight(IEnumerable? list)
        {
            const double defaultHeight = 340;
            if (list == null)
                return defaultHeight;

            double height = GetItemHeight() * list.Cast<object>().Count();

            return height < defaultHeight ? defaultHeight : height;
        }

        public static double GetItemHeight()
            => Device.RuntimePlatform switch
            {
                Platforms.Android => 40,
                Platforms.iOS => 45,
                _ => throw new ArgumentOutOfRangeException(nameof(Device.RuntimePlatform)),
            };

        private readonly IValidatable multiSelectValidatable;
        private readonly MultiSelectTemplateDescriptor multiSelectTemplateDescriptor;
    }
}