using AutoMapper;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Directives.Factories;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.ViewModels;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using Enrollment.XPlatform.ViewModels.Validatables;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Enrollment.XPlatform.Directives
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
                        return (IClearIfManager)(
                            Activator.CreateInstance
                            (
                                typeof(ClearIfManager<>).MakeGenericType(modelType),
                                provider.GetRequiredService<IMapper>(),
                                provider.GetRequiredService<IUiNotificationService>(),
                                currentProperties,
                                conditions
                            ) ?? throw new ArgumentException($"{modelType}: {{5290B2FB-5FE0-4A1C-BE83-62FDD75B6457}}")
                        );
                    }
                )
                .AddTransient<Func<Type, ObservableCollection<IValidatable>, IFormGroupSettings, IDirectiveManagers>>
                (
                    provider =>
                    (modelType, properties, formSettings) =>
                    {
                        return (IDirectiveManagers)(
                            Activator.CreateInstance
                            (
                                typeof(DirectiveManagers<>).MakeGenericType(modelType),
                                provider.GetRequiredService<IDirectiveManagersFactory>(),
                                properties,
                                formSettings
                            ) ?? throw new ArgumentException($"{modelType}: {{47406905-5EF2-4081-AC6C-F3016F3853BB}}")
                        );
                    }
                )
                .AddTransient<IDirectiveManagersFactory, DirectiveManagersFactory>()
                .AddTransient<Func<Type, IEnumerable<IFormField>, object, IHideIfManager>>
                (
                    provider =>
                    (modelType, currentProperties, conditions) =>
                    {
                        return (IHideIfManager)(
                            Activator.CreateInstance
                            (
                                typeof(HideIfManager<>).MakeGenericType(modelType),
                                provider.GetRequiredService<IMapper>(),
                                provider.GetRequiredService<IUiNotificationService>(),
                                currentProperties,
                                conditions
                            ) ?? throw new ArgumentException($"{modelType}: {{A0C36BDC-17D9-4860-870E-C57CD3B0E476}}")
                        );
                    }
                )
                .AddTransient<Func<Type, ObservableCollection<IReadOnly>, IFormGroupSettings, IReadOnlyDirectiveManagers>>
                (
                    provider =>
                    (modelType, properties, formSettings) =>
                    {
                        return (IReadOnlyDirectiveManagers)(
                            Activator.CreateInstance
                            (
                                typeof(ReadOnlyDirectiveManagers<>).MakeGenericType(modelType),
                                provider.GetRequiredService<IDirectiveManagersFactory>(),
                                properties,
                                formSettings
                            ) ?? throw new ArgumentException($"{modelType}: {{80EE6859-8C06-4263-AFA8-0BF3A51E97C6}}")
                        );
                    }
                )
                .AddTransient<Func<Type, IEnumerable<IFormField>, object, IReloadIfManager>>
                (
                    provider =>
                    (modelType, currentProperties, conditions) =>
                    {
                        return (IReloadIfManager)(
                            Activator.CreateInstance
                            (
                                typeof(ReloadIfManager<>).MakeGenericType(modelType),
                                provider.GetRequiredService<IMapper>(),
                                provider.GetRequiredService<IUiNotificationService>(),
                                currentProperties,
                                conditions
                            ) ?? throw new ArgumentException($"{modelType}: {{5A5873D7-EDC7-464B-953A-F8BBF068C569}}")
                        );
                    }
                )
                .AddTransient<Func<Type, Type, IFormGroupSettings, IEnumerable<IFormField>, object?, string?, IConditionalDirectiveBuilder>>
                (
                    provider =>
                    (modelType, directiveType, formGroupSettings, properties, parentList, parentName) =>
                    {
                        return (IConditionalDirectiveBuilder)(
                            Activator.CreateInstance
                            (
                                GetBuilderGenericTypeDefinition().MakeGenericType(modelType),
                                provider.GetRequiredService<IDirectiveManagersFactory>(),
                                provider.GetRequiredService<IMapper>(),
                                formGroupSettings,
                                properties,
                                parentList,
                                parentName
                            ) ?? throw new ArgumentException($"{modelType}: {{7CC0018D-3143-417B-B116-6E6FC9E7E6BC}}")
                        );

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
                        return (IValidateIfManager)(
                            Activator.CreateInstance
                            (
                                typeof(ValidateIfManager<>).MakeGenericType(modelType),
                                provider.GetRequiredService<IMapper>(),
                                provider.GetRequiredService<IUiNotificationService>(),
                                currentProperties,
                                conditions
                            ) ?? throw new ArgumentException($"{modelType}: {{EC839E00-CB19-4BDF-B9A9-E68CEC9B300E}}")
                        );
                    }
                );
    }
}
