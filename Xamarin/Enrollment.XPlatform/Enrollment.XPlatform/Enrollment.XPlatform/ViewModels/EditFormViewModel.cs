using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Flow.Settings.Screen;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.ViewModels.EditForm;
using System;
using System.Reflection;

namespace Enrollment.XPlatform.ViewModels
{
    public class EditFormViewModel : FlyoutDetailViewModelBase
    {
        private readonly IContextProvider contextProvider;

        public EditFormViewModel(IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public override void Initialize(ScreenSettingsBase screenSettings)
        {
            EditFormEntityViewModel = CreateEditFormViewModel((ScreenSettings<DataFormSettingsDescriptor>)screenSettings);
        }

        public EditFormEntityViewModelBase EditFormEntityViewModel { get; set; }

        private EditFormEntityViewModelBase CreateEditFormViewModel(ScreenSettings<DataFormSettingsDescriptor> screenSettings)
        {
            return (EditFormEntityViewModelBase)Activator.CreateInstance
            (
                typeof(EditFormEntityViewModel<>).MakeGenericType
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
