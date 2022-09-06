using Contoso.Forms.Configuration;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using System.Linq;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using System;

namespace Contoso.XPlatform.Views
{
    public class ReadOnlyChildFormPageCS : ContentPage
    {
        public ReadOnlyChildFormPageCS(IReadOnly formReadOnly)
        {
            this.formReadOnly = formReadOnly;
            this.formLayout = (DetailFormLayout)
            (
                (
                    this.formReadOnly.GetType()
                    .GetProperty(nameof(FormReadOnlyObject<string>.FormLayout))
                         ?? throw new ArgumentException($"{nameof(FormReadOnlyObject<string>.FormLayout)}: {{16836217-4D19-479F-9281-CBF604C62302}}")
                )
                .GetValue(this.formReadOnly) ?? throw new ArgumentException($"{nameof(formReadOnly)}: {{9C6FA403-A084-462A-9591-0BEFEA3EC831}}")
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
                            Style = LayoutHelpers.GetStaticStyleResource("ChildFormPopupViewStyle"),
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
                                },
                                new ScrollView
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource("ChildFormPopupScrollViewStyle"),
                                    Content = this.formLayout.ControlGroupBoxList.Aggregate
                                    (
                                        new VerticalStackLayout(),
                                        (stackLayout, controlBox) =>
                                        {
                                            if (controlBox.IsVisible == false)
                                                return stackLayout;

                                            stackLayout.Children.Add
                                            (
                                                new Label
                                                {
                                                    Style = LayoutHelpers.GetStaticStyleResource("DetailFormGroupHeaderStyle"),
                                                    BindingContext = controlBox
                                                }
                                                .AddBinding
                                                (
                                                    Label.TextProperty,
                                                    GetLabelBinding(controlBox.HeaderBindings, $"{nameof(ReadOnlyControlGroupBox.GroupHeader)}")
                                                )
                                            );
                                            stackLayout.Children.Add
                                            (
                                                new VerticalStackLayout
                                                {
                                                    VerticalOptions = LayoutOptions.Start,
                                                    BindingContext = controlBox
                                                }
                                                .AddBinding(BindableLayout.ItemsSourceProperty, new Binding("."))
                                                .SetDataTemplateSelector(DetailFormViewHelpers.ReadOnlyControlTemplateSelector)
                                            );

                                            return stackLayout;
                                        }
                                    )
                                },
                                new BoxView { Style = LayoutHelpers.GetStaticStyleResource("PopupFooterSeparatorStyle") },
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
                                        .AddBinding(Button.CommandProperty, new Binding("CancelCommand"))
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
            this.BindingContext = this.formReadOnly;

            BindingBase GetLabelBinding(MultiBindingDescriptor multiBindingDescriptor, string bindingName)
            {
                if (multiBindingDescriptor == null)
                    return new Binding(bindingName);

                return new MultiBinding
                {
                    StringFormat = multiBindingDescriptor.StringFormat,
                    Bindings = multiBindingDescriptor.Fields.Select
                    (
                        field => new Binding($"{nameof(ReadOnlyControlGroupBox.BindingPropertiesDictionary)}[{field.ToBindingDictionaryKey()}].{nameof(IReadOnly.Value)}")
                    )
                    .Cast<BindingBase>()
                    .ToList()
                };
            }
        }

        private IReadOnly formReadOnly;
        private DetailFormLayout formLayout;
    }
}