using AutoMapper;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.Factories;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using Contoso.XPlatform.ViewModels.Validatables;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Directives
{
    internal static class DirectiveServices
    {
        internal static IServiceCollection AddDirectiveServices(this IServiceCollection services)
            => services
                .AddTransient<Func<Type, IEnumerable<IFormField>, object, IClearIfManager>>
                (
                    provider =>
                    (modelType, currentProperties, conditions) =>
                    {
                        return (IClearIfManager)ActivatorUtilities.CreateInstance
                        (
                            provider,
                            typeof(ClearIfManager<>).MakeGenericType(modelType),
                            provider.GetRequiredService<IMapper>(),
                            provider.GetRequiredService<UiNotificationService>(),
                            currentProperties,
                            conditions
                        );
                    }
                )
                .AddTransient<Func<Type, ObservableCollection<IValidatable>, IFormGroupSettings, IDirectiveManagers>>
                (
                    provider =>
                    (modelType, properties, formSettings) =>
                    {
                        return (IDirectiveManagers)ActivatorUtilities.CreateInstance
                        (
                            provider,
                            typeof(DirectiveManagers<>).MakeGenericType(modelType),
                            provider.GetRequiredService<IDirectiveManagersFactory>(),
                            properties,
                            formSettings
                        );
                    }
                )
                .AddTransient<IDirectiveManagersFactory, DirectiveManagersFactory>()
                .AddTransient<Func<Type, IEnumerable<IFormField>, object, IHideIfManager>>
                (
                    provider =>
                    (modelType, currentProperties, conditions) =>
                    {
                        return (IHideIfManager)ActivatorUtilities.CreateInstance
                        (
                            provider,
                            typeof(HideIfManager<>).MakeGenericType(modelType),
                            provider.GetRequiredService<IMapper>(),
                            provider.GetRequiredService<UiNotificationService>(),
                            currentProperties,
                            conditions
                        );
                    }
                )
                .AddTransient<Func<Type, ObservableCollection<IReadOnly>, IFormGroupSettings, IReadOnlyDirectiveManagers>>
                (
                    provider =>
                    (modelType, properties, formSettings) =>
                    {
                        return (IReadOnlyDirectiveManagers)ActivatorUtilities.CreateInstance
                        (
                            provider,
                            typeof(ReadOnlyDirectiveManagers<>).MakeGenericType(modelType),
                            provider.GetRequiredService<IDirectiveManagersFactory>(),
                            properties,
                            formSettings
                        );
                    }
                )
                .AddTransient<Func<Type, IEnumerable<IFormField>, object, IReloadIfManager>>
                (
                    provider =>
                    (modelType, currentProperties, conditions) =>
                    {
                        return (IReloadIfManager)ActivatorUtilities.CreateInstance
                        (
                            provider,
                            typeof(ReloadIfManager<>).MakeGenericType(modelType),
                            provider.GetRequiredService<IMapper>(),
                            provider.GetRequiredService<UiNotificationService>(),
                            currentProperties,
                            conditions
                        );
                    }
                )
                .AddTransient<Func<Type, Type, IFormGroupSettings, IEnumerable<IFormField>, object?, string?, object>>
                (
                    provider =>
                    (modelType, directiveType, formGroupSettings, properties, parentList, parentName) =>
                    {
                        return Activator.CreateInstance
                        (
                            GetBuilderGenericTypeDefinition().MakeGenericType(modelType),
                            provider.GetRequiredService<IDirectiveManagersFactory>(),
                            provider.GetRequiredService<IMapper>(),
                            formGroupSettings,
                            properties,
                            parentList,
                            parentName
                        ) ?? throw new ArgumentException($"{modelType}: {{7CC0018D-3143-417B-B116-6E6FC9E7E6BC}}");

                        Type GetBuilderGenericTypeDefinition()
                        {
                            if (directiveType == typeof(ClearIf<>))
                                return typeof(ClearIfConditionalDirectiveBuilder<>);
                            else if (directiveType == typeof(HideIf<>))
                                return typeof(HideIfConditionalDirectiveBuilder<>);
                            else if (directiveType == typeof(ReloadIf<>))
                                return typeof(ReloadIfConditionalDirectiveBuilder<>);
                            else if (directiveType == typeof(ValidateIf<>))
                                return typeof(ValidateIfConditionalDirectiveBuilder<>);
                            else
                                throw new ArgumentException($"{directiveType}: {{6238A9BB-B079-4720-9CFE-B7ECC6A06C6E}}");
                        }
                    }
                )
                .AddTransient<Func<Type, IEnumerable<IFormField>, object, IValidateIfManager>>
                (
                    provider =>
                    (modelType, currentProperties, conditions) =>
                    {
                        return (IValidateIfManager)ActivatorUtilities.CreateInstance
                        (
                            provider,
                            typeof(ValidateIfManager<>).MakeGenericType(modelType),
                            provider.GetRequiredService<IMapper>(),
                            provider.GetRequiredService<UiNotificationService>(),
                            currentProperties,
                            conditions
                        );
                    }
                );
    }
}
