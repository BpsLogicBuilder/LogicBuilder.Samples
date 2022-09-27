using Enrollment.Forms.Configuration.DataForm;
using Enrollment.Forms.Configuration.ListForm;
using Enrollment.Forms.Configuration.SearchForm;
using Enrollment.Forms.Configuration.TextForm;
using Enrollment.XPlatform.Flow.Settings.Screen;
using Enrollment.XPlatform.Tests.Helpers;
using Enrollment.XPlatform.ViewModels.DetailForm;
using Enrollment.XPlatform.ViewModels.EditForm;
using Enrollment.XPlatform.ViewModels.ListPage;
using Enrollment.XPlatform.ViewModels.SearchPage;
using Enrollment.XPlatform.ViewModels.TextPage;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Enrollment.XPlatform.Tests
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
            ScreenSettingsBase settings = new ScreenSettings<DataFormSettingsDescriptor>(Descriptors.ResidencyForm, Descriptors.ButtonDescriptors, ViewType.EditForm);
            Func<ScreenSettingsBase, EditFormViewModelBase> factoryFunc = serviceProvider.GetRequiredService<Func<ScreenSettingsBase, EditFormViewModelBase>>();
            EditFormViewModelBase editFormViewModel = factoryFunc(settings);
            Assert.NotNull(editFormViewModel);
        }

        [Fact]
        public void CanResolveEditFormViewModelWithNonGenericConstructor()
        {
            ScreenSettingsBase settings = new ScreenSettings<DataFormSettingsDescriptor>(Descriptors.ResidencyForm, Descriptors.ButtonDescriptors, ViewType.EditForm);
            Func<ScreenSettingsBase, EditFormViewModelBase> factoryFunc = (Func<ScreenSettingsBase, EditFormViewModelBase>)serviceProvider.GetRequiredService(typeof(Func<ScreenSettingsBase, EditFormViewModelBase>));
            EditFormViewModelBase editFormViewModel = factoryFunc(settings);
            Assert.NotNull(editFormViewModel);
        }

        [Fact]
        public void CanCreateDetailViewMode()
        {
            ScreenSettingsBase settings = new ScreenSettings<DataFormSettingsDescriptor>(ReadOnlyDescriptors.ResidencyForm, Descriptors.ButtonDescriptors, ViewType.DetailForm);
            Func<ScreenSettingsBase, DetailFormViewModelBase> factoryFunc = serviceProvider.GetRequiredService<Func<ScreenSettingsBase, DetailFormViewModelBase>>();
            DetailFormViewModelBase viewModel = factoryFunc(settings);
            Assert.NotNull(viewModel);
        }

        [Fact]
        public void CanCreateListPageViewMode()
        {
            ScreenSettingsBase settings = new ScreenSettings<ListFormSettingsDescriptor>(ListFormDescriptors.AboutForm, Descriptors.ButtonDescriptors, ViewType.ListPage);
            Func<ScreenSettingsBase, ListPageViewModelBase> factoryFunc = serviceProvider.GetRequiredService<Func<ScreenSettingsBase, ListPageViewModelBase>>();
            ListPageViewModelBase viewModel = factoryFunc(settings);
            Assert.NotNull(viewModel);
        }

        [Fact]
        public void CanCreateSearchPageViewMode()
        {
            ScreenSettingsBase settings = new ScreenSettings<SearchFormSettingsDescriptor>(SearchFormDescriptors.SudentsForm, Descriptors.ButtonDescriptors, ViewType.SearchPage);
            Func<ScreenSettingsBase, SearchPageViewModelBase> factoryFunc = serviceProvider.GetRequiredService<Func<ScreenSettingsBase, SearchPageViewModelBase>>();
            SearchPageViewModelBase viewModel = factoryFunc(settings);
            Assert.NotNull(viewModel);
        }

        [Fact]
        public void CanCreateTextPageViewMode()
        {
            ScreenSettingsBase settings = new ScreenSettings<TextFormSettingsDescriptor>(TextFormDescriptors.HomePage, Descriptors.ButtonDescriptors, ViewType.TextPage);
            Func<ScreenSettingsBase, TextPageViewModel> factoryFunc = serviceProvider.GetRequiredService<Func<ScreenSettingsBase, TextPageViewModel>>();
            TextPageViewModel viewModel = factoryFunc(settings);
            Assert.NotNull(viewModel);
        }
    }
}
