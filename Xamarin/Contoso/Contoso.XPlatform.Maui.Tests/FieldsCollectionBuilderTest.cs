using Contoso.Domain.Entities;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Maui.Tests.Helpers;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.Factories;
using Contoso.XPlatform.ViewModels.Validatables;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace Contoso.XPlatform.Maui.Tests
{
    public class FieldsCollectionBuilderTest
    {
        public FieldsCollectionBuilderTest()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void MapCourseModelToIValidatableList()
        {
            //act
            ObservableCollection<IValidatable> properties = GetFieldsCollectionBuilder
            (
                Descriptors.CourseForm,
                typeof(CourseModel)
            )
            .CreateFields()
            .Properties;

            //assert
            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            Assert.Equal(typeof(EntryValidatableObject<int>), propertiesDictionary["CourseID"].GetType());
            Assert.Equal(typeof(EntryValidatableObject<string>), propertiesDictionary["Title"].GetType());
            Assert.Equal(typeof(PickerValidatableObject<int>), propertiesDictionary["Credits"].GetType());
            Assert.Equal(typeof(PickerValidatableObject<int>), propertiesDictionary["DepartmentID"].GetType());
        }

        [Fact]
        public void CreateEditFormLayoutForDepartment_NoGroups()
        {
            EditFormLayout formLayout = GetFieldsCollectionBuilder
            (
                Descriptors.DepartmentForm,
                typeof(DepartmentModel)
            ).CreateFields();

            Assert.Single(formLayout.ControlGroupBoxList);
            Assert.Equal(6, formLayout.ControlGroupBoxList.Single().Count);
            Assert.Equal("Department", formLayout.ControlGroupBoxList.Single().GroupHeader);
        }

        [Fact]
        public void CreateEditFormLayoutForDepartment_AllFieldsGrouped()
        {
            EditFormLayout formLayout = GetFieldsCollectionBuilder
            (
                Descriptors.DepartmentFormWithAllItemsGrouped,
                typeof(DepartmentModel)
            ).CreateFields();

            Assert.Equal(2, formLayout.ControlGroupBoxList.Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "GroupOne").Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "GroupTwo").Count);
        }

        [Fact]
        public void CreateEditFormLayoutForDepartment_SomeFieldsGrouped()
        {
            EditFormLayout formLayout = GetFieldsCollectionBuilder
            (
                Descriptors.DepartmentFormWithSomesItemsGrouped,
                typeof(DepartmentModel)
            ).CreateFields();


            Assert.Equal(2, formLayout.ControlGroupBoxList.Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "GroupOne").Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Department").Count);
            Assert.Equal("Department", formLayout.ControlGroupBoxList.First().GroupHeader);
        }

        private IFieldsCollectionBuilder GetFieldsCollectionBuilder(DataFormSettingsDescriptor dataFormSettingsDescriptor, Type modelType)
        {
            return serviceProvider.GetRequiredService<ICollectionBuilderFactory>().GetFieldsCollectionBuilder
            (
                modelType,
                dataFormSettingsDescriptor.FieldSettings,
                dataFormSettingsDescriptor,
                dataFormSettingsDescriptor.ValidationMessages,
                null,
                null
            );
        }
    }
}
