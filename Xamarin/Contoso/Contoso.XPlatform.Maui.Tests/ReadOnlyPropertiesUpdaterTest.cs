using Contoso.Domain.Entities;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Maui.Tests.Helpers;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels.Factories;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace Contoso.XPlatform.Maui.Tests
{
    public class ReadOnlyPropertiesUpdaterTest
    {
        public ReadOnlyPropertiesUpdaterTest()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void MapInstructorModelToIReadOnlyListWithInlineOfficeAssignment()
        {
            //arrange
            InstructorModel instructor = new()
            {
                ID = 3,
                FirstName = "John",
                LastName = "Smith",
                HireDate = new DateTime(2021, 5, 20),
                OfficeAssignment = new OfficeAssignmentModel
                {
                    Location = "Location1"
                },
                Courses = new List<CourseAssignmentModel>
                {
                    new() {
                        CourseID = 1,
                        InstructorID = 2,
                        CourseTitle = "Chemistry"
                    },
                    new() {
                        CourseID = 2,
                        InstructorID = 3,
                        CourseTitle = "Physics"
                    },
                    new() {
                        CourseID = 3,
                        InstructorID = 4,
                        CourseTitle = "Mathematics"
                    }
                }
            };
            ObservableCollection<IReadOnly> properties = GetReadOnlyFieldsCollectionBuilder
            (
                ReadOnlyDescriptors.InstructorFormWithInlineOfficeAssignment,
                typeof(InstructorModel)
            )
            .CreateFields()
            .Properties;

            //act
            serviceProvider.GetRequiredService<IReadOnlyPropertiesUpdater>().UpdateProperties
            (
                properties,
                instructor,
                ReadOnlyDescriptors.InstructorFormWithInlineOfficeAssignment.FieldSettings
            );

            //assert
            IDictionary<string, object?> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);
            Assert.Equal(3, propertiesDictionary["ID"]);
            Assert.Equal("John", propertiesDictionary["FirstName"]);
            Assert.Equal("Smith", propertiesDictionary["LastName"]);
            Assert.Equal(new DateTime(2021, 5, 20), propertiesDictionary["HireDate"]);
            Assert.Equal("Location1", propertiesDictionary["OfficeAssignment.Location"]);
            Assert.Equal("Chemistry", ((IEnumerable<CourseAssignmentModel>)propertiesDictionary["Courses"]!).First().CourseTitle);
        }

        [Fact]
        public void MapInstructorModelToIReadOnlyeListWithPopupOfficeAssignment()
        {
            //arrange
            InstructorModel instructor = new()
            {
                ID = 3,
                FirstName = "John",
                LastName = "Smith",
                HireDate = new DateTime(2021, 5, 20),
                OfficeAssignment = new OfficeAssignmentModel
                {
                    Location = "Location1"
                },
                Courses = new List<CourseAssignmentModel>
                {
                    new() {
                        CourseID = 1,
                        InstructorID = 2,
                        CourseTitle = "Chemistry"
                    },
                    new() {
                        CourseID = 2,
                        InstructorID = 3,
                        CourseTitle = "Physics"
                    },
                    new() {
                        CourseID = 3,
                        InstructorID = 4,
                        CourseTitle = "Mathematics"
                    }
                }
            };
            ObservableCollection<IReadOnly> properties = GetReadOnlyFieldsCollectionBuilder
            (
                ReadOnlyDescriptors.InstructorFormWithPopupOfficeAssignment,
                typeof(InstructorModel)
            )
            .CreateFields()
            .Properties;

            //act
            serviceProvider.GetRequiredService<IReadOnlyPropertiesUpdater>().UpdateProperties
            (
                properties,
                instructor,
                ReadOnlyDescriptors.InstructorFormWithPopupOfficeAssignment.FieldSettings
            );

            //assert
            IDictionary<string, object?> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);
            Assert.Equal(3, propertiesDictionary["ID"]);
            Assert.Equal("John", propertiesDictionary["FirstName"]);
            Assert.Equal("Smith", propertiesDictionary["LastName"]);
            Assert.Equal(new DateTime(2021, 5, 20), propertiesDictionary["HireDate"]);
            Assert.Equal("Location1", ((OfficeAssignmentModel)propertiesDictionary["OfficeAssignment"]!).Location);
            Assert.Equal("Chemistry", ((IEnumerable<CourseAssignmentModel>)propertiesDictionary["Courses"]!).First().CourseTitle);
        }

        [Fact]
        public void MapDepartmentModelToIReadOnlyList()
        {
            //arrange
            DepartmentModel department = new()
            {
                DepartmentID = 1,
                Name = "Mathematics",
                Budget = 100000m,
                StartDate = new DateTime(2021, 5, 20),
                InstructorID = 1,
                Courses = new List<CourseModel>
                {
                    new() {
                        CourseID = 1,
                        Credits = 3,
                        Title = "Trigonometry"
                    },
                    new() {
                        CourseID = 2,
                        Credits = 4,
                        Title = "Physics"
                    },
                    new() {
                        CourseID = 3,
                        Credits = 5,
                        Title = "Calculus"
                    }
                }
            };
            ObservableCollection<IReadOnly> properties = GetReadOnlyFieldsCollectionBuilder
            (
                ReadOnlyDescriptors.DepartmentForm,
                typeof(DepartmentModel)
            )
            .CreateFields()
            .Properties;

            //act
            serviceProvider.GetRequiredService<IReadOnlyPropertiesUpdater>().UpdateProperties
            (
                properties,
                department,
                ReadOnlyDescriptors.DepartmentForm.FieldSettings
            );

            //assert
            IDictionary<string, object?> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);

            Assert.Equal(1, propertiesDictionary["DepartmentID"]);
            Assert.Equal("Mathematics", propertiesDictionary["Name"]);
            Assert.Equal(100000m, propertiesDictionary["Budget"]);
            Assert.Equal(new DateTime(2021, 5, 20), propertiesDictionary["StartDate"]);
            Assert.Equal(1, propertiesDictionary["InstructorID"]);
            Assert.Equal("Trigonometry", ((IEnumerable<CourseModel>)propertiesDictionary["Courses"]!).First().Title);
        }

        [Fact]
        public void MapCourseModel_WithMultipleGroupBoxSettingsDescriptorFields_ToIReadOnlyList()
        {
            //arrange
            CourseModel courseModel = new()
            {
                CourseID = 1,
                Credits = 3,
                Title = "Trigonometry",
                DepartmentID = 2
            };

            ObservableCollection<IReadOnly> properties = GetReadOnlyFieldsCollectionBuilder
            (
                ReadOnlyDescriptors.CourseForm,
                typeof(CourseModel)
            )
            .CreateFields()
            .Properties;

            //act
            serviceProvider.GetRequiredService<IReadOnlyPropertiesUpdater>().UpdateProperties
            (
                properties,
                courseModel,
                ReadOnlyDescriptors.CourseForm.FieldSettings
            );

            IDictionary<string, object?> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);
            Assert.Equal(1, propertiesDictionary["CourseID"]);
            Assert.Equal(3, propertiesDictionary["Credits"]);
            Assert.Equal("Trigonometry", propertiesDictionary["Title"]);
            Assert.Equal(2, propertiesDictionary["DepartmentID"]);
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
