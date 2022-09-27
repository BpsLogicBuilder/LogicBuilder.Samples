using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.Bindings;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Contoso.XPlatform.Utils
{
    internal static class LayoutHelpers
    {
        public static T AddBinding<T>(this T bindable, BindableProperty targetProperty, BindingBase binding) where T : BindableObject
        {
            bindable.SetBinding
            (
                targetProperty,
                binding
            );

            return bindable;
        }

        public static T SetAutomationPropertiesName<T>(this T bindable, string propertyName) where T : BindableObject
        {
            AutomationProperties.SetName(bindable, propertyName);

            return bindable;
        }

        public static T AssignDynamicResource<T>(this T bindable, BindableProperty property, string key) where T : Element
        {
            bindable.SetDynamicResource
            (
                property, key
            );

            return bindable;
        }

        public static T SetGridColumn<T>(this T bindable, int column) where T : BindableObject
        {
            Grid.SetColumn(bindable, column);
            return bindable;
        }

        public static T SetGridColumnSpan<T>(this T bindable, int columnSpan) where T : BindableObject
        {
            Grid.SetColumnSpan(bindable, columnSpan);
            return bindable;
        }

        public static T SetGridRow<T>(this T bindable, int row) where T : BindableObject
        {
            Grid.SetRow(bindable, row);
            return bindable;
        }

        public static T SetGridRowSpan<T>(this T bindable, int rowSpan) where T : BindableObject
        {
            Grid.SetRowSpan(bindable, rowSpan);
            return bindable;
        }

        public static T SetDataTemplateSelector<T>(this T bindable, DataTemplateSelector dataTemplateSelector) where T : BindableObject
        {
            BindableLayout.SetItemTemplateSelector(bindable, dataTemplateSelector);
            return bindable;
        }

        public static T SetAbsoluteLayoutBounds<T>(this T bindable, Rect rectangle) where T : BindableObject
        {
            AbsoluteLayout.SetLayoutBounds(bindable, rectangle);
            return bindable;
        }

        public static T SetAbsoluteLayoutFlags<T>(this T bindable, AbsoluteLayoutFlags absoluteLayoutFlags) where T : BindableObject
        {
            AbsoluteLayout.SetLayoutFlags(bindable, absoluteLayoutFlags);
            return bindable;
        }

        public static Style GetStaticStyleResource(string styleName)
        {
            if (Application.Current == null)
                throw new ArgumentException($"{nameof(Application.Current)}: {{E118848A-872E-48DA-A6D4-A1E5A0D57070}}");
            if (Application.Current.Resources.TryGetValue(styleName, out object resource)
                && resource is Style style)
                return style;

            throw new ArgumentException($"{styleName}: DF65BD5C-E8A5-409C-A736-F6DF1B29D5E7");
        }

        internal static DataTemplate GetCollectionViewItemTemplate(string templateName,
            Dictionary<string, ItemBindingDescriptor> bindings)
        {
            return templateName switch
            {
                TemplateNames.HeaderTextDetailTemplate => new DataTemplate
                (
                    () => new Grid
                    {
                        Style = GetStaticStyleResource(StyleKeys.HeaderTextDetailListItemStyle),
                        Children =
                        {
                            new StackLayout
                            {
                                Style = GetStaticStyleResource(StyleKeys.HeaderTextDetailItemLayout),
                                Children =
                                {
                                    CollectionCellIViewHelpers.GetCollectionViewItemTemplateItem
                                    (
                                        bindings[BindingNames.Header].TemplateName,
                                        bindings[BindingNames.Header].Property,
                                        FontAttributes.Bold
                                    ),
                                    CollectionCellIViewHelpers.GetCollectionViewItemTemplateItem
                                    (
                                        bindings[BindingNames.Text].TemplateName,
                                        bindings[BindingNames.Text].Property,
                                        FontAttributes.None
                                    ),
                                    CollectionCellIViewHelpers.GetCollectionViewItemTemplateItem
                                    (
                                        bindings[BindingNames.Detail].TemplateName,
                                        bindings[BindingNames.Detail].Property,
                                        FontAttributes.Italic
                                    )
                                }
                            }
                        }
                    }
                ),
                TemplateNames.TextDetailTemplate => new DataTemplate
                (
                    () => new Grid
                    {
                        Style = GetStaticStyleResource(StyleKeys.TextDetailListItemStyle),
                        Children =
                        {
                            new StackLayout
                            {
                                Style = GetStaticStyleResource(StyleKeys.TextDetailItemLayout),
                                Children =
                                {
                                    CollectionCellIViewHelpers.GetCollectionViewItemTemplateItem
                                    (
                                        bindings[BindingNames.Text].TemplateName,
                                        bindings[BindingNames.Text].Property,
                                        FontAttributes.Bold
                                    ),
                                    CollectionCellIViewHelpers.GetCollectionViewItemTemplateItem
                                    (
                                        bindings[BindingNames.Detail].TemplateName,
                                        bindings[BindingNames.Detail].Property,
                                        FontAttributes.Italic
                                    )
                                }
                            }
                        }
                    }
                ),
                _ => throw new ArgumentException
                (
                    $"{nameof(templateName)}: 83C55FEE-9A93-45D3-A972-2335BA0F16AE"
                ),
            };
        }

        private struct TemplateNames
        {
            public const string TextDetailTemplate = "TextDetailTemplate";
            public const string HeaderTextDetailTemplate = "HeaderTextDetailTemplate";
        }

        private struct BindingNames
        {
            public const string Header = "Header";
            public const string Text = "Text";
            public const string Detail = "Detail";
        }

        internal static void AddToolBarItems(IList<ToolbarItem> toolbarItems, ICollection<CommandButtonDescriptor> buttons)
        {
            foreach (var button in buttons)
                toolbarItems.Add(BuildToolbarItem(button));
        }

        static ToolbarItem BuildToolbarItem(CommandButtonDescriptor button)
            => new ToolbarItem
            {
                AutomationId = button.ShortString,
                //Text = button.LongString,
                IconImageSource = new FontImageSource
                {
                    FontFamily = EditFormViewHelpers.GetFontAwesomeFontFamily(),
                    Glyph = FontAwesomeIcons.Solid[button.ButtonIcon],
                    Size = 20
                },
                Order = ToolbarItemOrder.Primary,
                Priority = 0,
                CommandParameter = button
            }
            .AddBinding(MenuItem.CommandProperty, new Binding(button.Command))
            .SetAutomationPropertiesName(button.ShortString);

        /// <summary>
        /// For inline child forms the key includes a period which does not work for binding dictionaries.
        /// i.e. new Binding(@"[Property.ChildProperty].Value") does not work.
        /// We're using new Binding(@"[Property_ChildProperty].Value") instead
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        internal static string ToBindingDictionaryKey(this string key)
            => key.Replace(".", "_");

        /// <summary>
        /// If every form field has a FormGroupBoxSettingsDescriptor parent then we don't need to create a
        /// default group box.  Otherwise we create a default group box using IFormGroupSettings.Title as the
        /// group header.
        /// </summary>
        /// <param name="descriptors"></param>
        /// <returns></returns>
        internal static bool ShouldCreateDefaultControlGroupBox(this List<FormItemSettingsDescriptor> descriptors)
        {
            return descriptors.Aggregate(false, DoAggregate);

            static bool DoAggregate(bool shouldAdd, FormItemSettingsDescriptor next)
            {
                if (shouldAdd) return shouldAdd;

                if (next is FormGroupSettingsDescriptor inlineFormGroupSettingsDescriptor
                    && inlineFormGroupSettingsDescriptor.FormGroupTemplate.TemplateName == FromGroupTemplateNames.InlineFormGroupTemplate)
                {
                    if (inlineFormGroupSettingsDescriptor.FieldSettings.Aggregate(false, DoAggregate))
                        return true;
                }
                else if ((next is FormGroupBoxSettingsDescriptor) == false)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
