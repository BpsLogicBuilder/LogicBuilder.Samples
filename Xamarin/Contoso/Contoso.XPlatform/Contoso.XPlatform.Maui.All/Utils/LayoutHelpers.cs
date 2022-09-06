using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.Bindings;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using Contoso.XPlatform.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using Microsoft.Maui;

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

        public static T SetGridRow<T>(this T bindable, int row) where T : BindableObject
        {
            Grid.SetRow(bindable, row);
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

            throw new ArgumentException($"{nameof(styleName)}: DF65BD5C-E8A5-409C-A736-F6DF1B29D5E7");
        }

        internal static Page CreatePage(this ScreenSettingsBase screenSettings)
        {
            if (!Enum.IsDefined(typeof(ViewType), screenSettings.ViewType))
                throw new ArgumentException($"{nameof(screenSettings.ViewType)}: {{11439C04-24F7-48A2-8775-142B7C362BF3}}");

            return CreatePage(Enum.GetName(typeof(ViewType), screenSettings.ViewType)!);

            Page CreatePage(string viewName)
            {
                Type factoryFuncType = typeof(Func<,>).MakeGenericType
                (
                    typeof(ScreenSettingsBase),
                    typeof(FlyoutDetailViewModelBase).Assembly.GetType
                    (
                        $"Contoso.XPlatform.ViewModels.{viewName}ViewModel"
                    ) ?? throw new ArgumentException($"{viewName}: {{A00D3DF7-FC4C-40F5-8FBC-2CF3B3C7695D}}")
                );

                Func<ScreenSettingsBase, FlyoutDetailViewModelBase> factoryFunc = (Func<ScreenSettingsBase, FlyoutDetailViewModelBase>)App.ServiceProvider.GetRequiredService(factoryFuncType);
                FlyoutDetailViewModelBase viewModel = factoryFunc(screenSettings);
                //Delegate factoryFunc = (Delegate)App.ServiceProvider.GetRequiredService(factoryFuncType);
                //FlyoutDetailViewModelBase viewModel = (FlyoutDetailViewModelBase)factoryFunc.DynamicInvoke(screenSettings);

                return (Page)(
                    Activator.CreateInstance
                    (
                        typeof(MainPageView).Assembly.GetType
                        (
                            $"Contoso.XPlatform.Views.{viewName}ViewCS"
                        ) ?? throw new ArgumentException($"{viewName}: {{782AEB97-488F-46DE-AA1C-9B0B26A40C2E}}"),
                        viewModel
                    ) ?? throw new ArgumentException($"{viewName}: {{4074835A-2C4E-4488-84C2-8F39CA4C9720}}")
                );
            }
        }

        internal static DataTemplate GetCollectionViewItemTemplate(string templateName,
            Dictionary<string, ItemBindingDescriptor> bindings)
        {
            switch (templateName)
            {
                case TemplateNames.HeaderTextDetailTemplate:
                    return new DataTemplate
                    (
                        () => new Grid
                        {
                            Style = GetStaticStyleResource(ItemStyleNames.HeaderTextDetailListItemStyle),
                            Children =
                            {
                                new VerticalStackLayout
                                {
                                    Margin = new Thickness(2),
                                    Padding = new Thickness(7),
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
                                .AssignDynamicResource(VisualElement.BackgroundColorProperty, "ResultListBackgroundColor")
                            }
                        }
                    );
                case TemplateNames.TextDetailTemplate:
                    return new DataTemplate
                    (
                        () => new Grid
                        {
                            Style = GetStaticStyleResource(ItemStyleNames.TextDetailListItemStyle),
                            Children =
                            {
                                new VerticalStackLayout
                                {
                                    Margin = new Thickness(2),
                                    Padding = new Thickness(7),
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
                                .AssignDynamicResource(VisualElement.BackgroundColorProperty, "ResultListBackgroundColor")
                            }
                        }
                    );
                default:
                    throw new ArgumentException
                    (
                        $"{nameof(templateName)}: 83C55FEE-9A93-45D3-A972-2335BA0F16AE"
                    );
            }
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

        private struct ItemStyleNames
        {
            public const string TextDetailListItemStyle = "TextDetailListItemStyle";
            public const string HeaderTextDetailListItemStyle = "HeaderTextDetailListItemStyle";
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

            bool DoAggregate(bool shouldAdd, FormItemSettingsDescriptor next)
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

        /// <summary>
        /// Returns a key value pair where the key is a dictionary of the entity's properties and the value is the entity itself.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="entity"></param>
        /// <param name="contextProvider"></param>
        /// <param name="itemBindings"></param>
        /// <returns></returns>
        internal static KeyValuePair<Dictionary<string, IReadOnly>, TModel> GetCollectionCellDictionaryModelPair<TModel>(this TModel entity, IContextProvider contextProvider, List<ItemBindingDescriptor> itemBindings)
            => new KeyValuePair<Dictionary<string, IReadOnly>, TModel>
            (
                GetCollectionCellDictionaryItem(entity, contextProvider, itemBindings),
                entity
            );

        /// <summary>
        /// Returns a dictionary of the entity's properties where the key is the property name and the value is an IReadOnly implementation for the property.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="entity"></param>
        /// <param name="contextProvider"></param>
        /// <param name="itemBindings"></param>
        /// <returns></returns>
        internal static Dictionary<string, IReadOnly> GetCollectionCellDictionaryItem<TModel>(this TModel entity, IContextProvider contextProvider, List<ItemBindingDescriptor> itemBindings)
        {
            ICollection<IReadOnly> properties = contextProvider.CollectionCellItemsBuilder.CreateCellsCollection(itemBindings, typeof(TModel));

            UpdateCollectionCellProperties
            (
                entity,
                properties,
                contextProvider,
                itemBindings
            );

            return properties.ToDictionary(p => p.Name.ToBindingDictionaryKey());
        }

        /// <summary>
        /// Updates the IReadOnly objects to reflect the entity.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        /// <param name="contextProvider"></param>
        /// <param name="itemBindings"></param>
        /// <exception cref="ArgumentException"></exception>
        internal static void UpdateCollectionCellProperties<TModel>(this TModel entity, ICollection<IReadOnly> properties, IContextProvider contextProvider, List<ItemBindingDescriptor> itemBindings)
        {
            if (entity == null)
                return;

            Dictionary<string, IReadOnly> propertiesDictionary = properties.ToDictionary(p => p.Name);

            contextProvider.ReadOnlyCollectionCellPropertiesUpdater.UpdateProperties
            (
                properties,
                typeof(TModel),
                entity,
                itemBindings
            );

            itemBindings.ForEach
            (
                binding =>
                {
                    if (binding is DropDownItemBindingDescriptor dropDownItemBinding && dropDownItemBinding.RequiresReload)
                    {
                        if (string.IsNullOrEmpty(dropDownItemBinding.DropDownTemplate.ReloadItemsFlowName))
                            throw new ArgumentException($"{nameof(dropDownItemBinding.DropDownTemplate.ReloadItemsFlowName)}: F8304FC1-ABB9-4F2B-9668-4955A6D36F3B");

                        GetHasItemsSourceReadOnly().Reload(entity, typeof(TModel));
                    }

                    IHasItemsSource GetHasItemsSourceReadOnly()
                        => (IHasItemsSource)propertiesDictionary[binding.Property];
                }
            );
        }
    }
}
