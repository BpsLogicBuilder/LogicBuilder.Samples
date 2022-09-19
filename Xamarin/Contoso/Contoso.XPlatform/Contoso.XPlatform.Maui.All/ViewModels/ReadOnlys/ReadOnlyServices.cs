using AutoMapper;
using Contoso.Forms.Configuration.Bindings;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Directives.Factories;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.Factories;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using Contoso.XPlatform.ViewModels.ReadOnlys.Factories;
using System;
using System.Collections.Generic;

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
                            provider.GetRequiredService<IContextProvider>(),
                            provider.GetRequiredService<IMapper>(),
                            provider.GetRequiredService<IReadOnlyFactory>(),
                            itemBindings,
                            modelType
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
                                provider.GetRequiredService<IContextProvider>(),
                                provider.GetRequiredService<IDirectiveManagersFactory>(),
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
                                provider.GetRequiredService<IContextProvider>(),
                                name,
                                templateName
                            ) ?? throw new ArgumentException($"{fieldType}: {{7A00C8A1-0BB5-41AA-8857-082D81B0A7C3}}")
                        );
                    }
                )
                .AddTransient<IReadOnlyFactory, ReadOnlyFactory>()
                .AddTransient<Func<Type, List<FormItemSettingsDescriptor>, IFormGroupBoxSettings, DetailFormLayout?, string?, IReadOnlyFieldsCollectionBuilder>>
                (
                    provider =>
                    (modelType, fieldSettings, groupBoxSettings, formLayout, parentName) =>
                    {
                        return new ReadOnlyFieldsCollectionBuilder
                        (
                            provider.GetRequiredService<ICollectionCellManager>(),
                            provider.GetRequiredService<ICollectionBuilderFactory>(),
                            provider.GetRequiredService<IContextProvider>(),
                            provider.GetRequiredService<IDirectiveManagersFactory>(),
                            provider.GetRequiredService<IMapper>(),
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
