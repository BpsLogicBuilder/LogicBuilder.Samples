using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Maui.Tests.Helpers;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.EditForm;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;
using Xunit;

namespace Contoso.XPlatform.Maui.Tests
{
    public class DependencyResolverTests
    {
        public DependencyResolverTests()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void CanResolveEditFormViewModel()
        {
            ScreenSettingsBase settings = new ScreenSettings<DataFormSettingsDescriptor>(Descriptors.DepartmentForm, Descriptors.ButtonDescriptors, ViewType.EditForm);
            Func<ScreenSettingsBase, EditFormViewModelBase> factoryFunc = serviceProvider.GetRequiredService<Func<ScreenSettingsBase, EditFormViewModelBase>>();
            EditFormViewModelBase editFormViewModel = factoryFunc(settings);
            Assert.NotNull(editFormViewModel);
        }

        [Fact]
        public void CanResolveEditFormViewModelWithNonGenericConstructor()
        {
            ScreenSettingsBase settings = new ScreenSettings<DataFormSettingsDescriptor>(Descriptors.DepartmentForm, Descriptors.ButtonDescriptors, ViewType.EditForm);
            Func<ScreenSettingsBase, EditFormViewModelBase> factoryFunc = (Func<ScreenSettingsBase, EditFormViewModelBase>)serviceProvider.GetRequiredService(typeof(Func<ScreenSettingsBase, EditFormViewModelBase>));
            EditFormViewModelBase editFormViewModel = factoryFunc(settings);
            Assert.NotNull(editFormViewModel);
        }
    }
}
