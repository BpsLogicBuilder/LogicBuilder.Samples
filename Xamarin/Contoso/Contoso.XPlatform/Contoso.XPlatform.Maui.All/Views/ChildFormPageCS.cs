using Contoso.Forms.Configuration;
using Contoso.XPlatform.Constants;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.Validatables;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
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
                Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupDialogAbsoluteLayoutStyle),
                Children = 
                {
                    new ContentView 
                    {
                        Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupDialogContentViewStyle),
                        Content = new VerticalStackLayout
                        {
                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.ChildFormPopupViewStyle),
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
                                        }.AddBinding(Label.TextProperty, new Binding(nameof(FormValidatableObject<string>.Title)))
                                    }
                                },
                                new ScrollView
                                {
                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.ChildFormPopupScrollViewStyle),
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
                                                    Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.EditFormGroupHeaderStyle),
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
                                                new VerticalStackLayout
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
                                        .AddBinding(Button.CommandProperty, new Binding(nameof(FormValidatableObject<string>.CancelCommand)))
                                        .SetGridColumn(1),
                                        new Button
                                        {
                                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PopupAcceptButtonStyle)
                                        }
                                        .AddBinding(Button.CommandProperty, new Binding(nameof(FormValidatableObject<string>.SubmitCommand)))
                                        .SetGridColumn(2)
                                    }
                                }
                            }
                        }
                    }
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

        private readonly IValidatable formValidatable;
        private readonly EditFormLayout formLayout;
    }
}