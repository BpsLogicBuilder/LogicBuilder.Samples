using Contoso.XPlatform.Constants;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Utils
{
    public static class DetailFormViewHelpers
    {
        public static ReadOnlyControlTemplateSelector ReadOnlyControlTemplateSelector => new ReadOnlyControlTemplateSelector
        {
            CheckboxTemplate = new DataTemplate
            (
                () => new VerticalStackLayout
                {
                    Children =
                    {
                        GetCheckboxControl()
                    }
                }
                .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)))
            ),
            DateTemplate = new DataTemplate
            (
                () => new VerticalStackLayout
                {
                    Children =
                    {
                        GetTextFieldControl()
                    }
                }
                .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)))
            ),
            FormGroupArrayTemplate = new DataTemplate
            (
                () => new VerticalStackLayout
                {
                    Children =
                    {
                        GetPopupFormArrayFieldControl()
                    }
                }
                .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)))
            ),
            HiddenTemplate = new DataTemplate
            (
                () => new VerticalStackLayout
                {
                    IsVisible = false,
                    HeightRequest = 1
                }
            ),
            MultiSelectTemplate = new DataTemplate
            (
                () => new VerticalStackLayout
                {
                    Children =
                    {
                        GetMultiSelectFieldControl()
                    }
                }
                .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)))
            ),
            PasswordTemplate = new DataTemplate
            (
                () => new VerticalStackLayout
                {
                    Children =
                    {
                        GetPasswordTextFieldControl()
                    }
                }
                .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)))
            ),
            PopupFormGroupTemplate = new DataTemplate
            (
                () => new VerticalStackLayout
                {
                    Children =
                    {
                        GetPopupFormFieldControl()
                    }
                }
                .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)))
            ),
            PickerTemplate = new DataTemplate
            (
                () => new VerticalStackLayout
                {
                    Children =
                    {
                        GetTextFieldControl()
                    }
                }
                .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)))
            ),
            SwitchTemplate = new DataTemplate
            (
                () => new VerticalStackLayout
                {
                    Children =
                    {
                        GetSwitchFieldControl()
                    }
                }
                .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)))
            ),
            TextTemplate = new DataTemplate
            (
                () => new VerticalStackLayout
                {
                    Children =
                    {
                        GetTextFieldControl()
                    }
                }
                .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)))
            )
        };

        private static View GetTextFieldControl() 
            => GetTextField
            (
                nameof(TextFieldReadOnlyObject<string>.Title),
                nameof(TextFieldReadOnlyObject<string>.DisplayText)
            );

        private static HorizontalStackLayout GetCheckboxControl()
            => new HorizontalStackLayout()
            {
                IsEnabled = false,
                Children =
                {
                    new CheckBox
                    {
                        IsEnabled = false
                    }
                    .AddBinding(CheckBox.IsCheckedProperty, new Binding(nameof(CheckboxReadOnlyObject.Value))),
                    new Label
                    {
                        VerticalOptions = LayoutOptions.Center
                    }
                    .AddBinding(Label.TextProperty, new Binding(nameof(CheckboxReadOnlyObject.CheckboxLabel)))
                    .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)))
                }
            };

        private static HorizontalStackLayout GetSwitchFieldControl()
            => new HorizontalStackLayout()
            {
                Children =
                {
                    new Switch
                    {
                        Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.DetailSwitchStyle)
                    }
                    .AddBinding(Switch.IsToggledProperty, new Binding(nameof(SwitchReadOnlyObject.Value))),
                    new Label
                    {
                        Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.SwitchLabelStyle)
                    }
                    .AddBinding(Label.TextProperty, new Binding(nameof(SwitchReadOnlyObject.SwitchLabel)))
                    .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)))
                }
            };

        private static Grid GetPasswordTextFieldControl()
            => new()
            {
                Children =
                {
                    GetPasswordTextField
                    (
                        nameof(TextFieldReadOnlyObject<string>.Title),
                        nameof(TextFieldReadOnlyObject<string>.DisplayText)
                    ),
                    new BoxView()
                }
            };

        private static Grid GetMultiSelectFieldControl()
            => new()
            {
                Children =
                {
                    GetTextField
                    (
                        nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.Title),
                        nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.DisplayText)
                    )
                    .AddBinding(Entry.PlaceholderProperty, new Binding(nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.Placeholder))),
                    new BoxView()
                },
                GestureRecognizers =
                {
                    new TapGestureRecognizer().AddBinding
                    (
                        TapGestureRecognizer.CommandProperty,
                        new Binding(path: nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.OpenCommand))
                    )
                }
            };

        private static Grid GetPopupFormFieldControl() 
            => new()
            {
                Children =
                {
                    GetEntry()
                    .AddBinding(Entry.PlaceholderProperty, new Binding(nameof(FormReadOnlyObject<string>.Placeholder))),
                    new BoxView()
                    {
                        GestureRecognizers =
                        {
                            new TapGestureRecognizer().AddBinding
                            (
                                TapGestureRecognizer.CommandProperty,
                                new Binding(path: nameof(FormReadOnlyObject<string>.OpenCommand))
                            )
                        }
                    }
                }
            };

        private static Grid GetPopupFormArrayFieldControl() 
            => new()
            {
                Children =
                {
                    GetEntry()
                        .AddBinding(Entry.PlaceholderProperty, new Binding(nameof(FormArrayReadOnlyObject<ObservableCollection<string>, string>.Placeholder))),
                    new BoxView()
                    {
                        GestureRecognizers =
                        {
                            new TapGestureRecognizer().AddBinding
                            (
                                TapGestureRecognizer.CommandProperty,
                                new Binding(path: nameof(FormArrayReadOnlyObject<ObservableCollection<string>, string>.OpenCommand))
                            )
                        }
                    }
                }
            };

        private static View GetPasswordTextField(string titleBinding, string valueBinding)
            => GetTextField(titleBinding, valueBinding, isPassword: true);

        private static View GetTextField(string titleBinding, string valueBinding, bool isPassword = false)
            => new Label
            {
                Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.DetailFormLabel),
                FormattedText = new FormattedString
                {
                    Spans =
                    {
                        new Span{ FontAttributes = FontAttributes.Italic }.AddBinding(Span.TextProperty, new Binding(titleBinding)),
                        new Span { Text = ":  " },
                        isPassword
                            ? new Span{ FontAttributes = FontAttributes.Bold, Text = "*****" }
                            : new Span{ FontAttributes = FontAttributes.Bold }.AddBinding(Span.TextProperty, new Binding(valueBinding))
                    }
                }
            }.AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)));

        static Entry GetEntry()
            => new Entry() { Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.DetailFormEntryStyle) }
            .AddBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IFormField.IsVisible)));
    }
}
