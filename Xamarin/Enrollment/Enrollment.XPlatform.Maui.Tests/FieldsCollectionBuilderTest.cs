using Enrollment.Domain.Entities;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Maui.Tests.Helpers;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.ViewModels;
using Enrollment.XPlatform.ViewModels.Factories;
using Enrollment.XPlatform.ViewModels.Validatables;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace Enrollment.XPlatform.Maui.Tests
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
                Descriptors.AcademicForm,
                typeof(AcademicModel)
            )
            .CreateFields()
            .Properties;

            //assert
            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            Assert.Equal(typeof(HiddenValidatableObject<int>), propertiesDictionary["UserId"].GetType());
            Assert.Equal(typeof(PickerValidatableObject<string>), propertiesDictionary["LastHighSchoolLocation"].GetType());
            Assert.Equal(typeof(PickerValidatableObject<string>), propertiesDictionary["NcHighSchoolName"].GetType());
            Assert.Equal(typeof(DatePickerValidatableObject<DateTime>), propertiesDictionary["FromDate"].GetType());
            Assert.Equal(typeof(DatePickerValidatableObject<DateTime>), propertiesDictionary["ToDate"].GetType());
            Assert.Equal(typeof(PickerValidatableObject<string>), propertiesDictionary["GraduationStatus"].GetType());
            Assert.Equal(typeof(SwitchValidatableObject), propertiesDictionary["EarnedCreditAtCmc"].GetType());
            Assert.Equal(typeof(FormArrayValidatableObject<ObservableCollection<InstitutionModel>, InstitutionModel>), propertiesDictionary["Institutions"].GetType());
        }

        [Fact]
        public void CreateEditFormLayoutForUserModelPersonal()
        {
            EditFormLayout formLayout = GetFieldsCollectionBuilder
            (
                Descriptors.PersonalFrom,
                typeof(UserModel)
            ).CreateFields();

            Assert.Equal(3, formLayout.ControlGroupBoxList.Count);
            Assert.Equal(5, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Name").Count);
            Assert.Equal(6, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Address").Count);
            Assert.Equal(2, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Phone Numbers").Count);
        }

        [Fact]
        public void CreateEditFormLayoutForUserModelPersonalWithDefaultGroupForSomeFields()
        {
            EditFormLayout formLayout = GetFieldsCollectionBuilder
            (
                Descriptors.PersonalFromWithDefaultGroupForSomeFields,
                typeof(UserModel)
            ).CreateFields();

            Assert.Equal(3, formLayout.ControlGroupBoxList.Count);
            Assert.Equal(5, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "PersonalRoot").Count);
            Assert.Equal(6, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Address").Count);
            Assert.Equal(2, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Phone Numbers").Count);
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
