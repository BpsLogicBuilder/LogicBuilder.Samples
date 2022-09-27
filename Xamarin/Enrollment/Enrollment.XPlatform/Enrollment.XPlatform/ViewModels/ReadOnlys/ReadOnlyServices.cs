using AutoMapper;
using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.Bindings;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform;
using Enrollment.XPlatform.Directives.Factories;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels;
using Enrollment.XPlatform.ViewModels.Factories;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using Enrollment.XPlatform.ViewModels.ReadOnlys.Factories;
using Enrollment.XPlatform.Views.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class ReadOnlyServices
    {
        internal static IServiceCollection AddReadOnlyServices(this IServiceCollection services)
        {
            return services
                .AddTransient<Func<Type, List<ItemBindingDescriptor>, ICollectionCellItemsBuilder>>
                (
                    provider =>
                    (modelType, itemBindings) =>
                    {
                        return new CollectionCellItemsBuilder
                        (
                            provider.GetRequiredService<IReadOnlyFactory>(),
                            itemBindings,
                            modelType
                        );
                    }
                )
                .AddTransient<Func<string, string, string, IReadOnly>>
                (//switch and checkbox read-onlys have the same signature
                    provider =>
                    (name, templateName, label) =>
                    {
                        if (templateName == nameof(ReadOnlyControlTemplateSelector.CheckboxTemplate))
                        {
                            return new CheckboxReadOnlyObject
                            (
                                provider.GetRequiredService<UiNotificationService>(),
                                name,
                                templateName,
                                label
                            );
                        }
                        else if (templateName == nameof(ReadOnlyControlTemplateSelector.SwitchTemplate))
                        {
                            return new SwitchReadOnlyObject
                            (
                                provider.GetRequiredService<UiNotificationService>(),
                                name,
                                templateName,
                                label
                            );
                        }

                        throw new ArgumentException($"{nameof(templateName)}: {{B3EE00C4-130A-4C00-843B-A21CD41241DC}}");
                    }
                )
                .AddTransient<Func<Type, string, FormGroupArraySettingsDescriptor, IReadOnly>>
                (
                    provider =>
                    (elementType, name, setting) =>
                    {
                        if (setting.FormGroupTemplate.TemplateName != nameof(ReadOnlyControlTemplateSelector.FormGroupArrayTemplate))
                            throw new ArgumentException($"{nameof(setting.FormGroupTemplate.TemplateName)}: {{0F3DD1E6-F5C2-4208-9007-418E4503FF63}}");

                        return (IReadOnly)(
                            Activator.CreateInstance
                            (
                                typeof(FormArrayReadOnlyObject<,>).MakeGenericType
                                (
                                    typeof(ObservableCollection<>).MakeGenericType(elementType),
                                    elementType
                                ),
                                provider.GetRequiredService<ICollectionCellManager>(),
                                provider.GetRequiredService<IPopupFormFactory>(),
                                provider.GetRequiredService<IReadOnlyFactory>(),
                                provider.GetRequiredService<UiNotificationService>(),
                                name,
                                setting
                            ) ?? throw new ArgumentException($"{elementType}: {{F0633351-B095-4AEE-BF7B-A9E6D74201E9}}")
                        );
                    }
                )
                .AddTransient<Func<Type, string, IChildFormGroupSettings, IReadOnly>>
                (
                    provider =>
                    (fieldType, name, setting) =>
                    {
                        if (setting.FormGroupTemplate.TemplateName != nameof(ReadOnlyControlTemplateSelector.PopupFormGroupTemplate)
                            && setting.FormGroupTemplate.TemplateName != nameof(ReadOnlyControlTemplateSelector.FormGroupArrayTemplate))
                        {
                            throw new ArgumentException($"{nameof(setting.FormGroupTemplate.TemplateName)}: {{E8F5BC05-4696-456F-AD02-66A484394A9E}}");
                        }

                        return (IReadOnly)(
                            Activator.CreateInstance
                            (
                                typeof(FormReadOnlyObject<>).MakeGenericType(fieldType),
                                provider.GetRequiredService<ICollectionBuilderFactory>(),
                                provider.GetRequiredService<IDirectiveManagersFactory>(),
                                provider.GetRequiredService<IPopupFormFactory>(),
                                provider.GetRequiredService<IReadOnlyPropertiesUpdater>(),
                                provider.GetRequiredService<UiNotificationService>(),
                                name,
                                setting
                            ) ?? throw new ArgumentException($"{fieldType}: {{C61BFCD8-C88D-4E00-AD96-18587A603F57}}")
                        );
                    }
                )
                .AddTransient<Func<Type, string, string, IReadOnly>>
                (
                    provider =>
                    (fieldType, name, templateName) =>
                    {
                        if (templateName != nameof(ReadOnlyControlTemplateSelector.HiddenTemplate))
                            throw new ArgumentException($"{nameof(templateName)}: {{6468B98E-E937-4536-AB39-3A0D6A390603}}");

                        return (IReadOnly)(
                            Activator.CreateInstance
                            (
                                typeof(HiddenReadOnlyObject<>).MakeGenericType(fieldType),
                                provider.GetRequiredService<UiNotificationService>(),
                                name,
                                templateName
                            ) ?? throw new ArgumentException($"{fieldType}: {{7A00C8A1-0BB5-41AA-8857-082D81B0A7C3}}")
                        );
                    }
                )
                .AddTransient<Func<Type, string, List<string>, string, string, MultiSelectTemplateDescriptor, IReadOnly>>
                (
                    provider =>
                    (elementType, name, keyFields, title, stringFormat, multiSelectTemplate) =>
                    {
                        if (multiSelectTemplate.TemplateName != nameof(ReadOnlyControlTemplateSelector.MultiSelectTemplate))
                            throw new ArgumentException($"{nameof(multiSelectTemplate.TemplateName)}: {{085E7940-E203-42A2-975C-A21CBA0D7D97}}");

                        return (IReadOnly)(
                            Activator.CreateInstance
                            (
                                typeof(MultiSelectReadOnlyObject<,>).MakeGenericType
                                (
                                    typeof(ObservableCollection<>).MakeGenericType(elementType),
                                    elementType
                                ),
                                provider.GetRequiredService<IHttpService>(),
                                provider.GetRequiredService<IPopupFormFactory>(),
                                provider.GetRequiredService<UiNotificationService>(),
                                name, 
                                keyFields, 
                                title, 
                                stringFormat, 
                                multiSelectTemplate
                            ) ?? throw new ArgumentException($"{elementType}: {{A3652A4E-A2F2-437C-AD6B-517EF0F14A67}}")
                        );
                    }
                )
                .AddTransient<Func<Type, string, string, string, DropDownTemplateDescriptor, IReadOnly>>
                (
                    provider =>
                    (fieldType, name, title, stringFormat, dropDownTemplate) =>
                    {
                        if (dropDownTemplate.TemplateName != nameof(ReadOnlyControlTemplateSelector.PickerTemplate))
                            throw new ArgumentException($"{nameof(dropDownTemplate.TemplateName)}: {{51404CE9-710F-44DD-A7BB-691055BB6BA7}}");

                        return (IReadOnly)(
                            Activator.CreateInstance
                            (
                                typeof(PickerReadOnlyObject<>).MakeGenericType(fieldType),
                                provider.GetRequiredService<IHttpService>(),
                                provider.GetRequiredService<IMapper>(),
                                provider.GetRequiredService<UiNotificationService>(),
                                name,
                                title,
                                stringFormat,
                                dropDownTemplate
                            ) ?? throw new ArgumentException($"{fieldType}: {{547A766E-039E-498E-AFA0-3131A03376A3}}")
                        );
                    }
                )
                .AddTransient<IReadOnlyFactory, ReadOnlyFactory>()
                .AddTransient<Func<Type, string, string, string, string, IReadOnly>>
                (
                    provider =>
                    (fieldType, name, templateName, title, stringFormat) =>
                    {
                        if (templateName != nameof(ReadOnlyControlTemplateSelector.DateTemplate)
                            && templateName != nameof(ReadOnlyControlTemplateSelector.TextTemplate))
                            throw new ArgumentException($"{nameof(templateName)}: {{73A3F1FE-2B6B-4AF5-A0E8-FDF32C05FF67}}");

                        return (IReadOnly)(
                            Activator.CreateInstance
                            (
                                typeof(TextFieldReadOnlyObject<>).MakeGenericType(fieldType),
                                provider.GetRequiredService<UiNotificationService>(),
                                name,
                                templateName, 
                                title, 
                                stringFormat
                            ) ?? throw new ArgumentException($"{fieldType}: {{BAA4ED68-BF23-44F5-9760-C2E19C45C376}}")
                        );
                    }
                )
                .AddTransient<Func<Type, List<FormItemSettingsDescriptor>, IFormGroupBoxSettings, DetailFormLayout?, string?, IReadOnlyFieldsCollectionBuilder>>
                (
                    provider =>
                    (modelType, fieldSettings, groupBoxSettings, formLayout, parentName) =>
                    {
                        return new ReadOnlyFieldsCollectionBuilder
                        (
                            provider.GetRequiredService<ICollectionBuilderFactory>(),
                            provider.GetRequiredService<IReadOnlyFactory>(),
                            fieldSettings,
                            groupBoxSettings,
                            modelType,
                            formLayout,
                            parentName
                        );
                    }
                );
        }
    }
}
