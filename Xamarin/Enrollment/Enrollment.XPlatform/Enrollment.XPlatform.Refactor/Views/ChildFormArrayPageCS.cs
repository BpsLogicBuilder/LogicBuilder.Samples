using Enrollment.Forms.Configuration;
using Enrollment.XPlatform.Constants;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace Enrollment.XPlatform.Views
{
    public class ChildFormArrayPageCS : ContentPage
    {
        public ChildFormArrayPageCS(IValidatable formArrayValidatable)
        {
            this.formArrayValidatable = formArrayValidatable;
            this.formsCollectionDisplayTemplateDescriptor = (FormsCollectionDisplayTemplateDescriptor)
            (
                (
                    this.formArrayValidatable.GetType()
                    .GetProperty(nameof(FormArrayValidatableObject<ObservableCollection<string>, string>.FormsCollectionDisplayTemplate))
                        ?? throw new ArgumentException($"{nameof(FormArrayValidatableObject<ObservableCollection<string>, string>.FormsCollectionDisplayTemplate)}: {{13F30447-45BD-4AFE-82CE-93987301BBAD}}")
                )
                .GetValue(this.formArrayValidatable) ?? throw new ArgumentException($"{nameof(formArrayValidatable)}: {{785E923F-6CCC-4A8C-B058-856641366E01}}")

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
                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.FormArrayPopupViewStyle),
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
                                        }.AddBinding(Label.TextProperty, new Binding(nameof(FormArrayValidatableObject<ObservableCollection<string>, string>.Title)))
                                    }
                                },
                                new CollectionView
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.FormArrayPopupCollectionViewStyle),
                                    ItemTemplate = LayoutHelpers.GetCollectionViewItemTemplate
                                    (
                                        this.formsCollectionDisplayTemplateDescriptor.TemplateName,
                                        this.formsCollectionDisplayTemplateDescriptor.Bindings
                                    )
                                }
                                .AddBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(FormArrayValidatableObject<ObservableCollection<string>, string>.Items)))
                                .AddBinding(SelectableItemsView.SelectionChangedCommandProperty, new Binding(nameof(FormArrayValidatableObject<ObservableCollection<string>, string>.SelectionChangedCommand)))
                                .AddBinding(SelectableItemsView.SelectedItemProperty, new Binding(nameof(FormArrayValidatableObject<ObservableCollection<string>, string>.SelectedItem))),
                                new BoxView { Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupFooterSeparatorStyle) },
                                new Grid
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupFooterStyle),
                                    ColumnDefinitions =
                                    {
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) },
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) },
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) },
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) },
                                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) }
                                    },
                                    Children =
                                    {
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupAddButtonStyle)
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding(nameof(FormArrayValidatableObject<ObservableCollection<string>, string>.AddCommand))),
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupEditButtonStyle)
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding(nameof(FormArrayValidatableObject<ObservableCollection<string>, string>.EditCommand)))
                                        .SetGridColumn(1),
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupDeleteButtonStyle)
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding(nameof(FormArrayValidatableObject<ObservableCollection<string>, string>.DeleteCommand)))
                                        .SetGridColumn(2),
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupCancelButtonStyle)
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding(nameof(FormArrayValidatableObject<ObservableCollection<string>, string>.CancelCommand)))
                                        .SetGridColumn(3),
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupAcceptButtonStyle)
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding(nameof(FormArrayValidatableObject<ObservableCollection<string>, string>.SubmitCommand)))
                                        .SetGridColumn(4)
                                    }
                                }
                            }
                        }
                    }
                }
            };

            this.BackgroundColor = Color.Transparent;
            Visual = VisualMarker.Material;
            this.BindingContext = this.formArrayValidatable;
        }

        private readonly IValidatable formArrayValidatable;
        private readonly FormsCollectionDisplayTemplateDescriptor formsCollectionDisplayTemplateDescriptor;
    }
}