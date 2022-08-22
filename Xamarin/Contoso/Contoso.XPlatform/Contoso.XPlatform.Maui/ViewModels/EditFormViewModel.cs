using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels.EditForm;
using System;
using System.Reflection;

namespace Contoso.XPlatform.ViewModels
{
    public class EditFormViewModel : FlyoutDetailViewModelBase
    {
        private readonly IContextProvider contextProvider;

        public EditFormViewModel(IContextProvider contextProvider, ScreenSettingsBase screenSettings)
        {
            this.contextProvider = contextProvider;
            EditFormEntityViewModel = CreateEditFormViewModel((ScreenSettings<DataFormSettingsDescriptor>)screenSettings);
        }

        public EditFormEntityViewModelBase EditFormEntityViewModel { get; set; }

        private EditFormEntityViewModelBase CreateEditFormViewModel(ScreenSettings<DataFormSettingsDescriptor> screenSettings)
        {
            return (EditFormEntityViewModelBase)(
                Activator.CreateInstance
                (
                    typeof(EditFormEntityViewModel<>).MakeGenericType
                    (
                        Type.GetType
                        (
                            screenSettings.Settings.ModelType,
                            AssemblyResolver,
                            TypeResolver
                        ) ?? throw new ArgumentException($"{nameof(screenSettings.Settings.ModelType)}: {{C2A6533B-8C32-429A-B88E-56A1BB735105}}")
                    ),
                    new object[]
                    {
                        screenSettings,
                        this.contextProvider
                    }
                ) ?? throw new ArgumentException($"{nameof(screenSettings.Settings.ModelType)}: {{1FEC340E-FDEF-4FE4-9BDA-26A9F497A059}}")
            );

            Type? TypeResolver(Assembly? assembly, string typeName, bool matchCase)
                => assembly?.GetType(typeName);

            Assembly? AssemblyResolver(AssemblyName assemblyName)
                => typeof(Domain.BaseModelClass).Assembly;
        }
    }
}
