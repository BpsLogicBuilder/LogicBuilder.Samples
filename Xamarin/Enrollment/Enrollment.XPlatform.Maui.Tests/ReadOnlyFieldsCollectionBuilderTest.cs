using Enrollment.Domain.Entities;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Maui.Tests.Helpers;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.ViewModels;
using Enrollment.XPlatform.ViewModels.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace Enrollment.XPlatform.Maui.Tests
{
    public class ReadOnlyFieldsCollectionBuilderTest
    {
        public ReadOnlyFieldsCollectionBuilderTest()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void CreateDetailFormLayoutForUserModelPersonal()
        {
            DetailFormLayout formLayout = GetReadOnlyFieldsCollectionBuilder
            (
                ReadOnlyDescriptors.PersonalFrom,
                typeof(UserModel)
            )
            .CreateFields();

            Assert.Equal(3, formLayout.ControlGroupBoxList.Count);
            Assert.Equal(5, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Name").Count);
            Assert.Equal(6, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Address").Count);
            Assert.Equal(2, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Phone Numbers").Count);
        }

        [Fact]
        public void CreateDetailFormLayoutForUserModelPersonalWithDefaultGroupForSomeFields()
        {
            DetailFormLayout formLayout = GetReadOnlyFieldsCollectionBuilder
            (
                ReadOnlyDescriptors.PersonalFromWithDefaultGroupForSomeFields,
                typeof(UserModel)
            )
            .CreateFields();

            Assert.Equal(3, formLayout.ControlGroupBoxList.Count);
            Assert.Equal(5, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "PersonalRoot").Count);
            Assert.Equal(6, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Address").Count);
            Assert.Equal(2, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Phone Numbers").Count);
        }

        private IReadOnlyFieldsCollectionBuilder GetReadOnlyFieldsCollectionBuilder(DataFormSettingsDescriptor dataFormSettingsDescriptor, Type modelType)
        {
            return serviceProvider.GetRequiredService<ICollectionBuilderFactory>().GetReadOnlyFieldsCollectionBuilder
            (
                modelType,
                dataFormSettingsDescriptor.FieldSettings,
                dataFormSettingsDescriptor,
                null,
                null
            );
        }
    }
}
