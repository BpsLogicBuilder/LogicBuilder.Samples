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

        public SearchPageViewModel(IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public override void Initialize(ScreenSettingsBase screenSettings)
        {
            SearchPageEntityViewModel = CreateSearchPageListViewModel((ScreenSettings<SearchFormSettingsDescriptor>)screenSettings);
        }

        public SearchPageCollectionViewModelBase SearchPageEntityViewModel { get; set; }

        private SearchPageCollectionViewModelBase CreateSearchPageListViewModel(ScreenSettings<SearchFormSettingsDescriptor> screenSettings)
        {
            return (SearchPageCollectionViewModelBase)Activator.CreateInstance
            (
                typeof(SearchPageCollectionViewModel<>).MakeGenericType
                (
                    Type.GetType
                    (
                        screenSettings.Settings.ModelType,
                        AssemblyResolver,
                        TypeResolver
                    )
                ),
                new object[]
                {
                    screenSettings,
                    this.contextProvider
                }
            );

            Type TypeResolver(Assembly assembly, string typeName, bool matchCase)
                => assembly.GetType(typeName);

            Assembly AssemblyResolver(AssemblyName assemblyName)
                => typeof(Domain.BaseModelClass).Assembly;
        }
    }
}
