using Contoso.Forms.Configuration.DataForm;
using Contoso.Forms.Configuration.Validation;
using Contoso.XPlatform;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.Factories;
using Contoso.XPlatform.ViewModels.Validatables;
using Contoso.XPlatform.ViewModels.Validatables.Factories;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class ValidatableServices
    {
        internal static IServiceCollection AddValidatableServices(this IServiceCollection services) 
            => services
                .AddTransient<Func<Type, List<FormItemSettingsDescriptor>, IFormGroupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>>, EditFormLayout?, string?, IFieldsCollectionBuilder>>
                (
                    provider =>
                    (modelType, fieldSettings, groupBoxSettings, validationMessages, formLayout, parentName) =>
                    {
                        return new FieldsCollectionBuilder
                        (
                            provider.GetRequiredService<ICollectionBuilderFactory>(),
                            provider.GetRequiredService<IContextProvider>(),
                            provider.GetRequiredService<IValidatableFactory>(),
                            provider.GetRequiredService<IValidatableValueHelper>(),
                            fieldSettings,
                            groupBoxSettings,
                            validationMessages,
                            modelType,
                            formLayout,
                            parentName
                        );
                    }
                )
                .AddTransient<Func<Type, List<FormItemSettingsDescriptor>, IFormGroupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>>, EditFormLayout?, string?, IUpdateOnlyFieldsCollectionBuilder>>
                (
                    provider =>
                    (modelType, fieldSettings, groupBoxSettings, validationMessages, formLayout, parentName) =>
                    {
                        return new UpdateOnlyFieldsCollectionBuilder
                        (
                            provider.GetRequiredService<ICollectionBuilderFactory>(),
                            provider.GetRequiredService<IContextProvider>(),
                            provider.GetRequiredService<IValidatableFactory>(),
                            provider.GetRequiredService<IValidatableValueHelper>(),
                            fieldSettings,
                            groupBoxSettings,
                            validationMessages,
                            modelType,
                            formLayout,
                            parentName
                        );
                    }
                )
                .AddTransient<Func<Type, string, string, string, string, IEnumerable<IValidationRule>?, IValidatable>>
                (
                    provider =>
                    (fieldType, name, templateName, placeholder, stringFormat, validations) =>
                    {
                        if (templateName != nameof(QuestionTemplateSelector.TextTemplate))
                            throw new ArgumentException($"{nameof(templateName)}: {{F06BF86E-6C14-44F1-8C3D-AB68453D71D2}}");

                        return (IValidatable)(
                            Activator.CreateInstance
                            (
                                typeof(EntryValidatableObject<>).MakeGenericType(fieldType),
                                new object[]
                                {
                                        provider.GetRequiredService<UiNotificationService>(),
                                        name,
                                        templateName,
                                        placeholder,
                                        stringFormat,
                                        validations ?? Array.Empty<IValidationRule>()
                                }
                            ) ?? throw new ArgumentException($"{fieldType}: {{D2EEDDEE-0124-4B83-B4D4-520F37626570}}")
                        );
                    }
                )
                .AddTransient<Func<Type, string, string, IChildFormGroupSettings, IEnumerable<IValidationRule>?, IValidatable>>
                (
                    provider =>
                    (fieldType, name, validatableObjectName, setting, validations) =>
                    {
                        if (setting.FormGroupTemplate.TemplateName == nameof(QuestionTemplateSelector.PopupFormGroupTemplate)
                            || setting.FormGroupTemplate.TemplateName == nameof(QuestionTemplateSelector.FormGroupArrayTemplate))
                        {
                            if (validatableObjectName == nameof(FormValidatableObject<string>))
                            {
                                return (IValidatable)(
                                    Activator.CreateInstance
                                    (
                                        typeof(FormValidatableObject<>).MakeGenericType(fieldType),
                                        provider.GetRequiredService<ICollectionBuilderFactory>(),
                                        provider.GetRequiredService<IContextProvider>(),
                                        name,
                                        setting,
                                        Array.Empty<IValidationRule>()
                                    ) ?? throw new ArgumentException($"{setting.ModelType}: {{C9248FC9-AE9F-4B22-91D3-451664FFC4F8}}")
                                );
                            }
                            else if (validatableObjectName == nameof(AddFormValidatableObject<string>))
                            {
                                return (IValidatable)(
                                    Activator.CreateInstance
                                    (
                                        typeof(AddFormValidatableObject<>).MakeGenericType(fieldType),
                                        provider.GetRequiredService<ICollectionBuilderFactory>(),
                                        provider.GetRequiredService<IContextProvider>(),
                                        name,
                                        setting,
                                        Array.Empty<IValidationRule>()
                                    ) ?? throw new ArgumentException($"{setting.ModelType}: {{C9248FC9-AE9F-4B22-91D3-451664FFC4F8}}")
                                );
                            }
                            else
                            {
                                throw new ArgumentException($"{validatableObjectName}: {{124D7AA3-37D9-4AD3-B693-0E9FB98E8B8E}}");
                            }
                        }

                        throw new ArgumentException($"{nameof(setting.FormGroupTemplate.TemplateName)}: {{E8F5BC05-4696-456F-AD02-66A484394A9E}}");
                    }
                )
                .AddTransient<Func<Type, string, string, IEnumerable<IValidationRule>?, IValidatable>>
                (
                    provider =>
                    (fieldType, name, templateName, validations) =>
                    {
                        if (templateName != nameof(QuestionTemplateSelector.HiddenTemplate))
                            throw new ArgumentException($"{nameof(templateName)}: {{08714810-6632-437D-9F5F-0A4EC5A3D1E2}}");

                        return (IValidatable)(
                            Activator.CreateInstance
                            (
                                typeof(HiddenValidatableObject<>).MakeGenericType(fieldType),
                                new object[]
                                {
                                        provider.GetRequiredService<UiNotificationService>(),
                                        name,
                                        templateName,
                                        validations ?? Array.Empty<IValidationRule>()
                                }
                            ) ?? throw new ArgumentException($"{fieldType}: {{EB232E08-F38C-49F8-B137-1C771316DA13}}")
                        );
                    }
                )
                .AddTransient<Func<Type, string, string, string, string, string, IEnumerable<IValidationRule>?, IValidatable>>
                (
                    provider =>
                    (fieldType, name, templateName, title, placeholder, stringFormat, validations) =>
                    {
                        if (templateName != nameof(QuestionTemplateSelector.LabelTemplate))
                            throw new ArgumentException($"{nameof(templateName)}: {{A4279A15-DE89-4903-B051-35DEB040D767}}");

                        return (IValidatable)(
                            Activator.CreateInstance
                            (
                                typeof(LabelValidatableObject<>).MakeGenericType(fieldType),
                                new object[]
                                {
                                        provider.GetRequiredService<UiNotificationService>(),
                                        name,
                                        templateName,
                                        title,
                                        placeholder,
                                        stringFormat,
                                        validations ?? Array.Empty<IValidationRule>()
                                }
                            ) ?? throw new ArgumentException($"{fieldType}: {{891D329A-2CF5-4C7D-8AA9-924D060AA881}}")
                        );
                    }
                )
                .AddTransient<IValidatableFactory, ValidatableFactory>()
                .AddTransient<IValidatableValueHelper, ValidatableValueHelper>();
    }
}
