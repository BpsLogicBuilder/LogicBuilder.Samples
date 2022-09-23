using Contoso.Domain.Entities;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Maui.Tests.Helpers;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace Contoso.XPlatform.Maui.Tests
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
        public void CreateDetailFormLayoutForDepartment_NoGroups()
        {
            DetailFormLayout formLayout = GetReadOnlyFieldsCollectionBuilder
            (
                ReadOnlyDescriptors.DepartmentForm,
                typeof(DepartmentModel)
            )
            .CreateFields();

            Assert.Single(formLayout.ControlGroupBoxList);
            Assert.Equal(6, formLayout.ControlGroupBoxList.Single().Count);
            Assert.Equal("Department", formLayout.ControlGroupBoxList.Single().GroupHeader);
        }

        [Fact]
        public void CreateDetailFormLayoutForDepartment_AllFieldsGrouped()
        {
            DetailFormLayout formLayout = GetReadOnlyFieldsCollectionBuilder
            (
                ReadOnlyDescriptors.DepartmentFormWithAllItemsGrouped,
                typeof(DepartmentModel)
            )
            .CreateFields();

            Assert.Equal(2, formLayout.ControlGroupBoxList.Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "GroupOne").Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "GroupTwo").Count);
        }

        [Fact]
        public void CreateDetailFormLayoutForDepartment_SomeFieldsGrouped()
        {
            DetailFormLayout formLayout = GetReadOnlyFieldsCollectionBuilder
            (
                ReadOnlyDescriptors.DepartmentFormWithSomeItemsGrouped,
                typeof(DepartmentModel)
            )
            .CreateFields();


            Assert.Equal(2, formLayout.ControlGroupBoxList.Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "GroupOne").Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Department").Count);
            Assert.Equal("Department", formLayout.ControlGroupBoxList.First().GroupHeader);
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
