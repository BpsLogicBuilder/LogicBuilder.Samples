using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Flow.Settings.Screen;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.ViewModels.DetailForm;
using System;
using System.Reflection;

namespace Enrollment.XPlatform.ViewModels
{
    public class DetailFormViewModel : FlyoutDetailViewModelBase
    {
        private readonly IContextProvider contextProvider;

        public DetailFormViewModel(IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public override void Initialize(ScreenSettingsBase screenSettings)
        {
            DetailFormEntityViewModel = CreateDetailFormViewModel((ScreenSettings<DataFormSettingsDescriptor>)screenSettings);
        }

        public DetailFormEntityViewModelBase DetailFormEntityViewModel { get; set; }

        private DetailFormEntityViewModelBase CreateDetailFormViewModel(ScreenSettings<DataFormSettingsDescriptor> screenSettings)
        {
            return (DetailFormEntityViewModelBase)Activator.CreateInstance
            (
                typeof(DetailFormEntityViewModel<>).MakeGenericType
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
