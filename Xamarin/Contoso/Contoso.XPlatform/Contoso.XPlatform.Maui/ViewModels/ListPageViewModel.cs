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

        public ListPageViewModel(IContextProvider contextProvider, ScreenSettingsBase screenSettings)
        {
            this.contextProvider = contextProvider;
            ListPageCollectionViewModel = CreateSearchPageListViewModel((ScreenSettings<ListFormSettingsDescriptor>)screenSettings);
        }

        public ListPageCollectionViewModelBase ListPageCollectionViewModel { get; set; }

        private ListPageCollectionViewModelBase CreateSearchPageListViewModel(ScreenSettings<ListFormSettingsDescriptor> screenSettings)
        {
            return (ListPageCollectionViewModelBase)(
                Activator.CreateInstance
                (
                    typeof(ListPageCollectionViewModel<>).MakeGenericType
                    (
                        Type.GetType
                        (
                            screenSettings.Settings.ModelType,
                            AssemblyResolver,
                            TypeResolver
                        ) ?? throw new ArgumentException($"{nameof(screenSettings.Settings.ModelType)}: {{0220CF6A-B18A-485D-8B4C-C889B13B4D06}}")
                    ),
                    new object[]
                    {
                        screenSettings,
                        this.contextProvider
                    }
                ) ?? throw new ArgumentException($"{nameof(screenSettings.Settings.ModelType)}: {{88E40B36-62E1-415D-B5D4-A17312E400A2}}")
            );

            Type? TypeResolver(Assembly? assembly, string typeName, bool matchCase)
                => assembly?.GetType(typeName);

            Assembly? AssemblyResolver(AssemblyName assemblyName)
                => typeof(Domain.BaseModelClass).Assembly;
        }
    }
}
