using Contoso.Forms.Configuration.SearchForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels.SearchPage;
using System;
using System.Reflection;

namespace Contoso.XPlatform.ViewModels
{
    public class SearchPageViewModel : FlyoutDetailViewModelBase
    {
        private readonly IContextProvider contextProvider;

        public SearchPageViewModel(IContextProvider contextProvider, ScreenSettingsBase screenSettings)
        {
            this.contextProvider = contextProvider;
            SearchPageEntityViewModel = CreateSearchPageListViewModel((ScreenSettings<SearchFormSettingsDescriptor>)screenSettings);
        }

        public SearchPageCollectionViewModelBase SearchPageEntityViewModel { get; set; }

        private SearchPageCollectionViewModelBase CreateSearchPageListViewModel(ScreenSettings<SearchFormSettingsDescriptor> screenSettings)
        {
            return (SearchPageCollectionViewModelBase)(
                Activator.CreateInstance
                (
                    typeof(SearchPageCollectionViewModel<>).MakeGenericType
                    (
                        Type.GetType
                        (
                            screenSettings.Settings.ModelType,
                            AssemblyResolver,
                            TypeResolver
                        ) ?? throw new ArgumentException($"{nameof(screenSettings.Settings.ModelType)}: {{17A85D69-F54A-4746-8A6E-A2BB32F6439C}}")
                    ),
                    new object[]
                    {
                        screenSettings,
                        this.contextProvider
                    }
                ) ?? throw new ArgumentException($"{nameof(screenSettings.Settings.ModelType)}: {{23E376AA-8B40-40BE-8E52-0C712E6D2AD0}}")
            );

            Type? TypeResolver(Assembly? assembly, string typeName, bool matchCase)
                => assembly?.GetType(typeName);

            Assembly? AssemblyResolver(AssemblyName assemblyName)
                => typeof(Domain.BaseModelClass).Assembly;
        }
    }
}
