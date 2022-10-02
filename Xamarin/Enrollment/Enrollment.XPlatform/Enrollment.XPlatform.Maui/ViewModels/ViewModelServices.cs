using AutoMapper;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.Forms.Configuration.ListForm;
using Enrollment.Forms.Configuration.SearchForm;
using Enrollment.Forms.Configuration.TextForm;
using Enrollment.XPlatform;
using Enrollment.XPlatform.Directives.Factories;
using Enrollment.XPlatform.Flow.Settings.Screen;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.ViewModels;
using Enrollment.XPlatform.ViewModels.DetailForm;
using Enrollment.XPlatform.ViewModels.EditForm;
using Enrollment.XPlatform.ViewModels.Factories;
using Enrollment.XPlatform.ViewModels.ListPage;
using Enrollment.XPlatform.ViewModels.SearchPage;
using Enrollment.XPlatform.ViewModels.TextPage;
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

                        return (DetailFormViewModelBase)(
                            Activator.CreateInstance
                            (
                                typeof(DetailFormViewModel<>).MakeGenericType(GetEntityType(dataFormSettings.Settings.ModelType)),
                                provider.GetRequiredService<ICollectionBuilderFactory>(),
                                provider.GetRequiredService<IDirectiveManagersFactory>(),
                                provider.GetRequiredService<IHttpService>(),
                                provider.GetRequiredService<IReadOnlyPropertiesUpdater>(),
                                provider.GetRequiredService<IUiNotificationService>(),
                                dataFormSettings
                            ) ?? throw new ArgumentException($"{dataFormSettings.Settings.ModelType}: {{BBCA800D-5A92-40F2-9BCA-6FCBE9D71B28}}")
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

                        return (EditFormViewModelBase)(
                            Activator.CreateInstance
                            (
                                typeof(EditFormViewModel<>).MakeGenericType(GetEntityType(dataFormSettings.Settings.ModelType)),
                                provider.GetRequiredService<ICollectionBuilderFactory>(),
                                provider.GetRequiredService<IDirectiveManagersFactory>(),
                                provider.GetRequiredService<IEntityStateUpdater>(),
                                provider.GetRequiredService<IHttpService>(),
                                provider.GetRequiredService<IMapper>(),
                                provider.GetRequiredService<IPropertiesUpdater>(),
                                provider.GetRequiredService<IUiNotificationService>(),
                                dataFormSettings
                            ) ?? throw new ArgumentException($"{dataFormSettings.Settings.ModelType}: {{0960F651-92CD-4D84-A76D-D5CADA2220B7}}")
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

                        return (ListPageViewModelBase)(
                            Activator.CreateInstance
                            (
                                typeof(ListPageViewModel<>).MakeGenericType(GetEntityType(listFormSettings.Settings.ModelType)),
                                provider.GetRequiredService<ICollectionCellManager>(),
                                provider.GetRequiredService<IHttpService>(),
                                listFormSettings
                            ) ?? throw new ArgumentException($"{listFormSettings.Settings.ModelType}: {{AD49AB82-1CF6-426C-93BD-939045F7817B}}")
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

                        return (SearchPageViewModelBase)(
                            Activator.CreateInstance
                            (
                                typeof(SearchPageViewModel<>).MakeGenericType(GetEntityType(searchFormSettings.Settings.ModelType)),
                                provider.GetRequiredService<ICollectionCellManager>(),
                                provider.GetRequiredService<IHttpService>(),
                                provider.GetRequiredService<IMapper>(),
                                searchFormSettings
                            ) ?? throw new ArgumentException($"{searchFormSettings.Settings.ModelType}: {{DBBCF268-565B-4F23-BEB0-418626AB5475}}")
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

                        return new TextPageViewModel
                        (
                            textFormSettings
                        );
                    }
                )
                .AddTransient<ExtendedSplashViewModel>()
                .AddValidatableServices();


        static Type? TypeResolver(Assembly? assembly, string typeName, bool matchCase)
                => assembly?.GetType(typeName);

        static Assembly? AssemblyResolver(AssemblyName assemblyName)
            => typeof(Enrollment.Domain.BaseModelClass).Assembly;

        static Type GetEntityType(string typeFullName) 
            => Type.GetType
            (
                typeFullName,
                AssemblyResolver,
                TypeResolver
            ) ?? throw new ArgumentException($"{nameof(typeFullName)}: {{41354F62-FF24-45DA-821A-B4B995F9D859}}");
    }
}
