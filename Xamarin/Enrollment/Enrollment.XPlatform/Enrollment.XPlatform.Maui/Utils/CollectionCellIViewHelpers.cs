﻿using Enrollment.XPlatform.ViewModels.ReadOnlys;
using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using Enrollment.XPlatform.Constants;

namespace Enrollment.XPlatform.Utils
{
    internal class CollectionCellIViewHelpers
    {
        public static View GetCollectionViewItemTemplateItem(string templateName, string field, FontAttributes fontAttributes)
        {
            return templateName switch
            {
                nameof(ReadOnlyControlTemplateSelector.CheckboxTemplate) => GetCheckboxControl(field, fontAttributes),
                nameof(ReadOnlyControlTemplateSelector.DateTemplate)
                    or nameof(ReadOnlyControlTemplateSelector.TextTemplate) => GetTextFieldControl(field, fontAttributes),
                nameof(ReadOnlyControlTemplateSelector.PasswordTemplate) => GetPasswordTextFieldControl(field, fontAttributes),
                nameof(ReadOnlyControlTemplateSelector.PickerTemplate) => GetPickerFieldControl(field, fontAttributes),
                nameof(ReadOnlyControlTemplateSelector.SwitchTemplate) => GetSwitchFieldControl(field, fontAttributes),
                nameof(ReadOnlyControlTemplateSelector.MultiSelectTemplate) => GetMultiSelectFieldControl(field, fontAttributes),
                _ => throw new ArgumentException($"{nameof(templateName)}: 65783D5D-D80C-46BC-AD6B-0419CE37D987"),
            };
        }

        private static HorizontalStackLayout GetCheckboxControl(string field, FontAttributes fontAttributes)
            => new()
            {
                IsEnabled = false,
                Children =
                {
                    new CheckBox
                    {
                        IsEnabled = false
                    }
                    .AddBinding(CheckBox.IsCheckedProperty, new Binding($"[{field}].{nameof(CheckboxReadOnlyObject.Value)}")),
                    new Label
                    {
                        FontAttributes = fontAttributes,
                        VerticalOptions = LayoutOptions.Center
                    }
                    .AddBinding(Label.TextProperty, new Binding($"[{field}].{nameof(CheckboxReadOnlyObject.CheckboxLabel)}"))
                }
            };

        private static HorizontalStackLayout GetSwitchFieldControl(string field, FontAttributes fontAttributes)
            => new()
            {
                Children =
                {
                    new Switch
                    {
                        Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.DetailSwitchStyle),
                    }
                    .AddBinding(Switch.IsToggledProperty, new Binding($"[{field}].{nameof(SwitchReadOnlyObject.Value)}")),
                    new Label
                    {
                        Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.SwitchCollectionCellLabelStyle),
                        FontAttributes = fontAttributes
                    }
                    .AddBinding(Label.TextProperty, new Binding($"[{field}].{nameof(SwitchReadOnlyObject.SwitchLabel)}"))
                }
            };

        private static Grid GetMultiSelectFieldControl(string field, FontAttributes fontAttributes)
            => new()
            {
                Children =
                {
                    GetTextField
                    (
                        $"[{field}].{nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.Title)}",
                        $"[{field}].{nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.DisplayText)}",
                        fontAttributes
                    )
                    .AddBinding(Entry.PlaceholderProperty, new Binding(nameof(MultiSelectReadOnlyObject<ObservableCollection<string>, string>.Placeholder))),
                    new BoxView()
                }
            };

        private static View GetPickerFieldControl(string field, FontAttributes fontAttributes)
            => GetTextField
            (
                $"[{field}].{nameof(PickerReadOnlyObject<string>.Title)}",
                $"[{field}].{nameof(PickerReadOnlyObject<string>.DisplayText)}",
                fontAttributes
            );

        private static View GetTextFieldControl(string field, FontAttributes fontAttributes)
            => GetTextField
            (
                $"[{field}].{nameof(TextFieldReadOnlyObject<string>.Title)}",
                $"[{field}].{nameof(TextFieldReadOnlyObject<string>.DisplayText)}",
                fontAttributes
            );

        private static Grid GetPasswordTextFieldControl(string field, FontAttributes fontAttributes)
            => new()
            {
                Children =
                {
                    GetPasswordTextField
                    (
                        $"[{field}].{nameof(TextFieldReadOnlyObject<string>.Title)}",
                        $"[{field}].{nameof(TextFieldReadOnlyObject<string>.DisplayText)}",
                        fontAttributes
                    ),
                    new BoxView()
                }
            };

        private static View GetTextField(string titleBinding, string valueBinding, FontAttributes fontAttributes, bool isPassword = false)
            => new Label
            {
                FontAttributes = fontAttributes,
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
            };

        private static View GetPasswordTextField(string titleBinding, string valueBinding, FontAttributes fontAttributes)
            => GetTextField(titleBinding, valueBinding, fontAttributes, isPassword: true);
    }
}
