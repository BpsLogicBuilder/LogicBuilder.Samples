using Enrollment.Forms.Configuration;
using Enrollment.XPlatform.Constants;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Enrollment.XPlatform.Views
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
                                        }.AddBinding(Label.TextProperty, new Binding(nameof(FormArrayReadOnlyObject<ObservableCollection<string>, string>.Title)))
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
                                .AddBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(FormArrayReadOnlyObject<ObservableCollection<string>, string>.Items)))
                                .AddBinding(SelectableItemsView.SelectionChangedCommandProperty, new Binding(nameof(FormArrayReadOnlyObject<ObservableCollection<string>, string>.SelectionChangedCommand)))
                                .AddBinding(SelectableItemsView.SelectedItemProperty, new Binding(nameof(FormArrayReadOnlyObject<ObservableCollection<string>, string>.SelectedItem))),
                                new BoxView
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupFooterSeparatorStyle)
                                },
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
                                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupDetailButtonStyle)
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding(nameof(FormArrayReadOnlyObject<ObservableCollection<string>, string>.DetailCommand)))
                                        .SetGridColumn(1),
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupCancelButtonStyle)
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding(nameof(FormArrayReadOnlyObject<ObservableCollection<string>, string>.CancelCommand)))
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
            this.BindingContext = this.formArrayReadOnly;
        }

        private readonly IReadOnly formArrayReadOnly;
        private readonly FormsCollectionDisplayTemplateDescriptor formsCollectionDisplayTemplateDescriptor;
    }
}