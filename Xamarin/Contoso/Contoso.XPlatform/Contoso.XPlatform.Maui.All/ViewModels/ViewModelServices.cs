﻿using Contoso.Forms.Configuration.DataForm;
using Contoso.Forms.Configuration.ListForm;
using Contoso.Forms.Configuration.SearchForm;
using Contoso.Forms.Configuration.TextForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.DetailForm;
using Contoso.XPlatform.ViewModels.EditForm;
using Contoso.XPlatform.ViewModels.ListPage;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using Contoso.XPlatform.ViewModels.SearchPage;
using Contoso.XPlatform.ViewModels.TextPage;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class ViewModelServices
    {
        internal static IServiceCollection AddViewModels(this IServiceCollection services) 
            => services
                .AddTransient<MainPageViewModel>()
                .AddTransient<Func<ScreenSettingsBase, DetailFormViewModelBase>>
                (
                    provider =>
                    (screenSettings) =>
                    {
                        if (screenSettings is not ScreenSettings<DataFormSettingsDescriptor> dataFormSettings)
                            throw new ArgumentException($"{nameof(screenSettings)}: {{40DAEE65-72D5-4213-8243-07F54D5494B0}}");

                        return (DetailFormViewModelBase)ActivatorUtilities.CreateInstance
                        (
                            provider,
                            typeof(DetailFormViewModel<>).MakeGenericType(GetEntityType(dataFormSettings.Settings.ModelType)),
                            provider.GetRequiredService<IContextProvider>(),
                            provider.GetRequiredService<Func<Type, ObservableCollection<IReadOnly>, IFormGroupSettings, IReadOnlyDirectiveManagers>>(),
                            dataFormSettings
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
                            provider.GetRequiredService<IContextProvider>(),
                            properties,
                            formSettings
                        );
                    }
                )
                .AddTransient<Func<ScreenSettingsBase, EditFormViewModelBase>>
                (
                    provider =>
                    screenSettings =>
                    {
                        if (screenSettings is not ScreenSettings<DataFormSettingsDescriptor> dataFormSettings)
                            throw new ArgumentException($"{nameof(screenSettings)}: {{0BA43238-B845-4E6E-8952-F213CF74B755}}");

                        return (EditFormViewModelBase)ActivatorUtilities.CreateInstance
                        (
                            provider,
                            typeof(EditFormViewModel<>).MakeGenericType(GetEntityType(dataFormSettings.Settings.ModelType)),
                            provider.GetRequiredService<IContextProvider>(),
                            provider.GetRequiredService<Func<Type, ObservableCollection<IValidatable>, IFormGroupSettings, IDirectiveManagers>>(),
                            dataFormSettings
                        );
                    }
                )
                .AddTransient<Func<ScreenSettingsBase, ListPageViewModelBase>>
                (
                    provider =>
                    screenSettings =>
                    {
                        if (screenSettings is not ScreenSettings<ListFormSettingsDescriptor> listFormSettings)
                            throw new ArgumentException($"{nameof(screenSettings)}: {{E418F32B-6D8B-44AF-BBC5-1BCBC14642C6}}");

                        return (ListPageViewModelBase)ActivatorUtilities.CreateInstance
                        (
                            provider,
                            typeof(ListPageViewModel<>).MakeGenericType(GetEntityType(listFormSettings.Settings.ModelType)),
                            provider.GetRequiredService<IContextProvider>(),
                            listFormSettings
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
                            provider.GetRequiredService<IContextProvider>(),
                            properties,
                            formSettings
                        );
                    }
                )
                .AddTransient<Func<ScreenSettingsBase, SearchPageViewModelBase>>
                (
                    provider =>
                    screenSettings =>
                    {
                        if (screenSettings is not ScreenSettings<SearchFormSettingsDescriptor> searchFormSettings)
                            throw new ArgumentException($"{nameof(screenSettings)}: {{48040E4A-EDDD-4724-8B5C-F7D0B8FF11E9}}");

                        return (SearchPageViewModelBase)ActivatorUtilities.CreateInstance
                        (
                            provider,
                            typeof(SearchPageViewModel<>).MakeGenericType(GetEntityType(searchFormSettings.Settings.ModelType)),
                            provider.GetRequiredService<IContextProvider>(),
                            searchFormSettings
                        );
                    }
                )
                .AddTransient<Func<ScreenSettingsBase, TextPageViewModel>>
                (
                    provider =>
                    screenSettings =>
                    {
                        if (screenSettings is not ScreenSettings<TextFormSettingsDescriptor> textFormSettings)
                            throw new ArgumentException($"{nameof(screenSettings)}: {{3F3E8EE3-5E51-4786-A018-8A2EEF247581}}");

                        return ActivatorUtilities.CreateInstance<TextPageViewModel>
                        (
                            provider,
                            textFormSettings
                        );
                    }
                )
                .AddTransient<ExtendedSplashViewModel>();


        static Type? TypeResolver(Assembly? assembly, string typeName, bool matchCase)
                => assembly?.GetType(typeName);

        static Assembly? AssemblyResolver(AssemblyName assemblyName)
            => typeof(Contoso.Domain.BaseModelClass).Assembly;

        static Type GetEntityType(string typeFullName) 
            => Type.GetType
            (
                typeFullName,
                AssemblyResolver,
                TypeResolver
            ) ?? throw new ArgumentException($"{nameof(typeFullName)}: {{41354F62-FF24-45DA-821A-B4B995F9D859}}");
    }
}
