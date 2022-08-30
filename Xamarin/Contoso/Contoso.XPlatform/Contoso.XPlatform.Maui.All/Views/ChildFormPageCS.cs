using Contoso.Forms.Configuration;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.Validatables;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using System;
using System.Linq;

namespace Contoso.XPlatform.Views
{
    public class ChildFormPageCS : ContentPage
    {
        public ChildFormPageCS(IValidatable formValidatable)
        {
            this.formValidatable = formValidatable;
            this.formLayout = (EditFormLayout)
            (
                (
                    this.formValidatable.GetType()
                    .GetProperty(nameof(FormValidatableObject<string>.FormLayout))
                         ?? throw new ArgumentException($"{nameof(FormValidatableObject<string>.FormLayout)}: {{0A475AAA-9FC8-422A-8A87-A9E814920241}}")
                )
                .GetValue(this.formValidatable) ?? throw new ArgumentException($"{nameof(formValidatable)}: {{37FE80AE-770B-4875-8944-EF4FFAF84500}}")
            );

            Content = new AbsoluteLayout
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Children = 
                {
                    new ContentView 
                    { 
                        Content = new StackLayout
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
                                        new StackLayout(),
                                        (stackLayout, controlBox) =>
                                        {
                                            if (controlBox.IsVisible == false)
                                                return stackLayout;

                                            stackLayout.Children.Add
                                            (
                                                new Label
                                                {
                                                    Style = LayoutHelpers.GetStaticStyleResource("EditFormGroupHeaderStyle"),
                                                    BindingContext = controlBox
                                                }
                                                .AddBinding
                                                (
                                                    Label.TextProperty,
                                                    GetHeaderBinding(controlBox.HeaderBindings, $"{nameof(ControlGroupBox.GroupHeader)}")
                                                )
                                            );
                                            stackLayout.Children.Add
                                            (
                                                new StackLayout
                                                {
                                                    VerticalOptions = LayoutOptions.Start,
                                                    BindingContext = controlBox
                                                }
                                                .AddBinding(BindableLayout.ItemsSourceProperty, new Binding("."))
                                                .SetDataTemplateSelector(EditFormViewHelpers.QuestionTemplateSelector)
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
                                        .SetGridColumn(1),
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource("PopupAcceptButtonStyle")
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding("SubmitCommand"))
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
            this.BindingContext = this.formValidatable;

            BindingBase GetHeaderBinding(MultiBindingDescriptor multiBindingDescriptor, string bindingName)
            {
                if (multiBindingDescriptor == null)
                    return new Binding(bindingName);

                return new MultiBinding
                {
                    StringFormat = multiBindingDescriptor.StringFormat,
                    Bindings = multiBindingDescriptor.Fields.Select
                    (
                        field => new Binding($"{nameof(ControlGroupBox.BindingPropertiesDictionary)}[{field.ToBindingDictionaryKey()}].{nameof(IValidatable.Value)}")
                    )
                    .Cast<BindingBase>()
                    .ToList()
                };
            }
        }

        private IValidatable formValidatable;
        private EditFormLayout formLayout;
    }
}