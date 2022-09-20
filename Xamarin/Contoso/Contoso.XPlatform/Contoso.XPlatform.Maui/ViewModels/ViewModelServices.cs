using AutoMapper;
using Contoso.Forms.Configuration.DataForm;
using Contoso.Forms.Configuration.ListForm;
using Contoso.Forms.Configuration.SearchForm;
using Contoso.Forms.Configuration.TextForm;
using Contoso.XPlatform.Directives.Factories;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.DetailForm;
using Contoso.XPlatform.ViewModels.EditForm;
using Contoso.XPlatform.ViewModels.Factories;
using Contoso.XPlatform.ViewModels.ListPage;
using Contoso.XPlatform.ViewModels.SearchPage;
using Contoso.XPlatform.ViewModels.TextPage;
using System;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class ViewModelServices
    {
        internal static IServiceCollection AddViewModels(this IServiceCollection services) 
            => services
                .AddTransient<MainPageViewModel>()
                .AddTransient<ICollectionBuilderFactory, CollectionBuilderFactory>()
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
                            provider.GetRequiredService<ICollectionBuilderFactory>(),
                            provider.GetRequiredService<IContextProvider>(),
                            provider.GetRequiredService<IDirectiveManagersFactory>(),
                            dataFormSettings
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
                            provider.GetRequiredService<ICollectionBuilderFactory>(),
                            provider.GetRequiredService<IContextProvider>(),
                            provider.GetRequiredService<IDirectiveManagersFactory>(),
                            provider.GetRequiredService<IMapper>(),
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
                            provider.GetRequiredService<ICollectionCellManager>(),
                            provider.GetRequiredService<IContextProvider>(),
                            listFormSettings
                        );
                    }
                )
                .AddReadOnlyServices()
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
                            provider.GetRequiredService<ICollectionCellManager>(),
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
                .AddTransient<ExtendedSplashViewModel>()
                .AddValidatableServices();


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
