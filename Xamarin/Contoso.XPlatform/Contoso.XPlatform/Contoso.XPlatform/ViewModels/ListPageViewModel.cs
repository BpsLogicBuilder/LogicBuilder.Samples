using Contoso.Forms.Configuration.ListForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels.ListPage;
using System;
using System.Reflection;

namespace Contoso.XPlatform.ViewModels
{
    public class ListPageViewModel : FlyoutDetailViewModelBase
    {
        private readonly IContextProvider contextProvider;

        public ListPageViewModel(IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public override void Initialize(ScreenSettingsBase screenSettings)
        {
            ListPageCollectionViewModel = CreateSearchPageListViewModel((ScreenSettings<ListFormSettingsDescriptor>)screenSettings);
        }

        public ListPageCollectionViewModelBase ListPageCollectionViewModel { get; set; }

        private ListPageCollectionViewModelBase CreateSearchPageListViewModel(ScreenSettings<ListFormSettingsDescriptor> screenSettings)
        {
            return (ListPageCollectionViewModelBase)Activator.CreateInstance
            (
                typeof(ListPageCollectionViewModel<>).MakeGenericType
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
