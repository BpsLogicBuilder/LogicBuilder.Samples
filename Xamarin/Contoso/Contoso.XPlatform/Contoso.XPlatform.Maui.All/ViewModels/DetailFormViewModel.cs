using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels.DetailForm;
using System;
using System.Reflection;

namespace Contoso.XPlatform.ViewModels
{
    public class DetailFormViewModel : FlyoutDetailViewModelBase
    {
        private readonly IContextProvider contextProvider;

        public DetailFormViewModel(IContextProvider contextProvider, ScreenSettingsBase screenSettings)
        {
            this.contextProvider = contextProvider;
            DetailFormEntityViewModel = CreateDetailFormViewModel((ScreenSettings<DataFormSettingsDescriptor>)screenSettings);
        }

        public DetailFormEntityViewModelBase DetailFormEntityViewModel { get; set; }

        private DetailFormEntityViewModelBase CreateDetailFormViewModel(ScreenSettings<DataFormSettingsDescriptor> screenSettings)
        {
            return (DetailFormEntityViewModelBase)(
                Activator.CreateInstance
                (
                    typeof(DetailFormEntityViewModel<>).MakeGenericType
                    (
                        Type.GetType
                        (
                            screenSettings.Settings.ModelType,
                            AssemblyResolver,
                            TypeResolver
                        ) ?? throw new ArgumentException($"{nameof(screenSettings.Settings.ModelType)}: {{15389B98-631E-48B2-BDF3-461C4F74C79D}}")
                    ),
                    new object[]
                    {
                        screenSettings,
                        this.contextProvider
                    }
                ) ?? throw new ArgumentException($"{nameof(screenSettings.Settings.ModelType)}: {{C2A6533B-8C32-429A-B88E-56A1BB735105}}")
            );

            Type? TypeResolver(Assembly? assembly, string typeName, bool matchCase)
                => assembly?.GetType(typeName);

            Assembly? AssemblyResolver(AssemblyName assemblyName)
                => typeof(Domain.BaseModelClass).Assembly;
        }
    }
}
