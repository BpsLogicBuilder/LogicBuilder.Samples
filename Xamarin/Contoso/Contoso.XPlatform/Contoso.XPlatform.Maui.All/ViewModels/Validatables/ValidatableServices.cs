using AutoMapper;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.DataForm;
using Contoso.Forms.Configuration.Validation;
using Contoso.XPlatform;
using Contoso.XPlatform.Directives.Factories;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.Factories;
using Contoso.XPlatform.ViewModels.Validatables;
using Contoso.XPlatform.ViewModels.Validatables.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
                .AddTransient<Func<string, string, string, IEnumerable<IValidationRule>?, IValidatable>>
                (//switch and checkbox validatables have the same signature
                    provider =>
                    (name, templateName, label, validations) =>
                    {
                        if (templateName == nameof(QuestionTemplateSelector.CheckboxTemplate))
                        {
                            return new CheckboxValidatableObject
                            (
                                provider.GetRequiredService<UiNotificationService>(),
                                name,
                                templateName,
                                label,
                                validations ?? Array.Empty<IValidationRule>()
                            );
                        }
                        else if (templateName == nameof(QuestionTemplateSelector.SwitchTemplate))
                        {
                            return new SwitchValidatableObject
                            (
                                provider.GetRequiredService<UiNotificationService>(),
                                name,
                                templateName,
                                label,
                                validations ?? Array.Empty<IValidationRule>()
                            );
                        }

                        throw new ArgumentException($"{nameof(templateName)}: {{704DE899-ABD4-4EC4-BB6B-56F6B3F1F289}}");
                    }
                )
                .AddTransient<Func<Type, string, string, IEnumerable<IValidationRule>?, IValidatable>>
                (//DatePicker and Hidden have the same signature
                    provider =>
                    (fieldType, name, templateName, validations) =>
                    {
                        if (templateName == nameof(QuestionTemplateSelector.DateTemplate))
                        {
                            return (IValidatable)(
                                Activator.CreateInstance
                                (
                                    typeof(DatePickerValidatableObject<>).MakeGenericType(fieldType),
                                    new object[]
                                    {
                                        provider.GetRequiredService<UiNotificationService>(),
                                        name,
                                        templateName,
                                        validations ?? Array.Empty<IValidationRule>()
                                    }
                                ) ?? throw new ArgumentException($"{fieldType}: {{966C7238-5435-4A7E-BF50-57284004AE61}}")
                            );
                        }
                        else if (templateName == nameof(QuestionTemplateSelector.HiddenTemplate))
                        {
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

                        throw new ArgumentException($"{nameof(templateName)}: {{08714810-6632-437D-9F5F-0A4EC5A3D1E2}}");
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
                .AddTransient<Func<Type, string, FormGroupArraySettingsDescriptor, IEnumerable<IValidationRule>?, IValidatable>>
                (
                    provider =>
                    (elementType, name, setting, validations) =>
                    {
                        if (setting.FormGroupTemplate.TemplateName != nameof(QuestionTemplateSelector.FormGroupArrayTemplate))
                            throw new ArgumentException($"{nameof(setting.FormGroupTemplate.TemplateName)}: {{A35D2359-809D-42DF-95E1-B29DA2E1B962}}");

                        return (IValidatable)(
                            Activator.CreateInstance
                            (
                                typeof(FormArrayValidatableObject<,>).MakeGenericType
                                (
                                    typeof(ObservableCollection<>).MakeGenericType(elementType),
                                    elementType
                                ),
                                provider.GetRequiredService<ICollectionCellManager>(),
                                provider.GetRequiredService<ICollectionBuilderFactory>(),
                                provider.GetRequiredService<IContextProvider>(),
                                provider.GetRequiredService<IValidatableFactory>(),
                                name,
                                setting,
                                validations ?? Array.Empty<IValidationRule>()
                            ) ?? throw new ArgumentException($"{setting.FormGroupTemplate.TemplateName}: {{3E474C92-1061-4222-929D-4C9A2C6B358F}}")
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
                                        provider.GetRequiredService<IDirectiveManagersFactory>(),
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
                                        provider.GetRequiredService<IDirectiveManagersFactory>(),
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
                                provider.GetRequiredService<UiNotificationService>(),
                                name,
                                templateName,
                                title,
                                placeholder,
                                stringFormat,
                                validations ?? Array.Empty<IValidationRule>()
                            ) ?? throw new ArgumentException($"{fieldType}: {{891D329A-2CF5-4C7D-8AA9-924D060AA881}}")
                        );
                    }
                )
                .AddTransient<Func<Type, string, MultiSelectFormControlSettingsDescriptor, IEnumerable<IValidationRule>?, IValidatable>>
                (
                    provider =>
                    (elementType, name, setting, validations) =>
                    {
                        if (setting.MultiSelectTemplate.TemplateName != nameof(QuestionTemplateSelector.MultiSelectTemplate))
                            throw new ArgumentException($"{nameof(setting.MultiSelectTemplate.TemplateName)}: {{77223A01-5D5B-4F7D-8642-60D9E48F46C4}}");

                        return (IValidatable)(
                            Activator.CreateInstance
                            (
                                typeof(MultiSelectValidatableObject<,>).MakeGenericType
                                (
                                    typeof(ObservableCollection<>).MakeGenericType(elementType),
                                    elementType
                                ),
                                provider.GetRequiredService<IContextProvider>(),
                                name,
                                setting,
                                validations ?? Array.Empty<IValidationRule>()
                            ) ?? throw new ArgumentException($"{setting.MultiSelectTemplate.ModelType}: {{225867FD-C54E-423D-BD87-A6A2954C824B}}")
                        );
                    }
                )
                .AddTransient<Func<Type, string, object?, DropDownTemplateDescriptor, IEnumerable<IValidationRule>?, IValidatable>>
                (
                    provider =>
                    (fieldType, name, defaultValue, dropDownTemplate, validations) =>
                    {
                        if (dropDownTemplate.TemplateName != nameof(QuestionTemplateSelector.PickerTemplate))
                            throw new ArgumentException($"{nameof(dropDownTemplate.TemplateName)}: {{981EC142-52D2-4CA8-A16A-E62405DD85EF}}");

                        return (IValidatable)(
                            Activator.CreateInstance
                            (
                                typeof(PickerValidatableObject<>).MakeGenericType(fieldType),
                                new object?[]
                                {
                                        provider.GetRequiredService<IContextProvider>(),
                                        provider.GetRequiredService<IMapper>(),
                                        name,
                                        defaultValue,
                                        dropDownTemplate,
                                        validations ?? Array.Empty<IValidationRule>()
                                }
                            ) ?? throw new ArgumentException($"{fieldType}: {{79B782E6-21DD-4579-93DD-DC901D0D3CD8}}")
                        );
                    }
                )
                .AddTransient<IValidatableFactory, ValidatableFactory>()
                .AddTransient<IValidatableValueHelper, ValidatableValueHelper>();
    }
}
