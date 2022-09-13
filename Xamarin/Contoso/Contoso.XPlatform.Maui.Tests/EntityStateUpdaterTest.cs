using AutoMapper;
using Contoso.Domain.Entities;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Maui.Tests.Helpers;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Validatables;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace Contoso.XPlatform.Maui.Tests
{
    public class EntityStateUpdaterTest
    {
        public EntityStateUpdaterTest()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void ShouldCorrectlySetEntityStatesForMultiSelects()
        {
            //arrange
            DataFormSettingsDescriptor formDescriptor = Descriptors.InstructorFormWithInlineOfficeAssignment;
            InstructorModel instructorModel = new()
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
                    new CourseAssignmentModel
                    {
                        CourseID = 1,
                        InstructorID = 3,
                        CourseTitle = "Chemistry"
                    },
                    new CourseAssignmentModel
                    {
                        CourseID = 2,
                        InstructorID = 3,
                        CourseTitle = "Physics"
                    },
                    new CourseAssignmentModel
                    {
                        CourseID = 3,
                        InstructorID = 3,
                        CourseTitle = "Mathematics"
                    }
                }
            };

            ObservableCollection<IValidatable> modifiedProperties = CreateValidatablesFormSettings(formDescriptor, typeof(InstructorModel));
            IDictionary<string, IValidatable> propertiesDictionary = modifiedProperties.ToDictionary(property => property.Name);
            propertiesDictionary["ID"].Value = 3;
            propertiesDictionary["FirstName"].Value = "John";
            propertiesDictionary["LastName"].Value = "Smith";
            propertiesDictionary["HireDate"].Value = new DateTime(2021, 5, 20);
            propertiesDictionary["OfficeAssignment.Location"].Value = "Location1";
            propertiesDictionary["Courses"].Value = new ObservableCollection<CourseAssignmentModel>
            (
                new List<CourseAssignmentModel>
                {
                    new CourseAssignmentModel
                    {
                        CourseID = 1,
                        InstructorID = 3,
                        CourseTitle = "Chemistry"
                    },
                    new CourseAssignmentModel
                    {
                        CourseID = 2,
                        InstructorID = 3,
                        CourseTitle = "Physics"
                    },
                    new CourseAssignmentModel
                    {
                        CourseID = 4,
                        InstructorID = 3,
                        CourseTitle = "Algebra"
                    }
                }
            );

            InstructorModel currentInstructor = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                instructorModel,
                instructorModel.EntityToObjectDictionary
                (
                   serviceProvider.GetRequiredService<IMapper>(),
                   formDescriptor.FieldSettings
                ),
                modifiedProperties,
                formDescriptor.FieldSettings
            );

            Assert.Equal(LogicBuilder.Domain.EntityStateType.Modified, currentInstructor.EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Unchanged, currentInstructor.Courses.Single(c => c.CourseID == 1).EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Unchanged, currentInstructor.Courses.Single(c => c.CourseID == 2).EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Deleted, currentInstructor.Courses.Single(c => c.CourseID == 3).EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Added, currentInstructor.Courses.Single(c => c.CourseID == 4).EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Unchanged, currentInstructor.OfficeAssignment.EntityState);
            Assert.Equal(4, currentInstructor.Courses.Count);
        }

        [Fact]
        public void ShouldCorrectlySetEntityStatesForChildFormGroupArray()
        {
            //arrange
            DataFormSettingsDescriptor formDescriptor = Descriptors.DepartmentForm;
            DepartmentModel departmentModel = new()
            {
                DepartmentID = 1,
                Name = "Mathematics",
                Budget = 100000m,
                StartDate = new DateTime(2021, 5, 20),
                InstructorID = 1,
                Courses = new List<CourseModel>
                {
                    new CourseModel
                    {
                        CourseID = 1,
                        Credits = 3,
                        Title = "Trigonometry"
                    },
                    new CourseModel
                    {
                        CourseID = 2,
                        Credits = 4,
                        Title = "Physics"
                    },
                    new CourseModel
                    {
                        CourseID = 3,
                        Credits = 5,
                        Title = "Calculus"
                    }
                }
            };

            ObservableCollection<IValidatable> modifiedProperties = CreateValidatablesFormSettings(formDescriptor, typeof(DepartmentModel));
            IDictionary<string, IValidatable> propertiesDictionary = modifiedProperties.ToDictionary(property => property.Name);
            propertiesDictionary["DepartmentID"].Value = 1;
            propertiesDictionary["Name"].Value = "Mathematics";
            propertiesDictionary["Budget"].Value = 100000m;
            propertiesDictionary["StartDate"].Value = new DateTime(2021, 5, 20);
            propertiesDictionary["InstructorID"].Value = 1;
            propertiesDictionary["Courses"].Value = new ObservableCollection<CourseModel>
            (
                new List<CourseModel>
                {
                    new CourseModel
                    {
                        CourseID = 1,
                        Credits = 3,
                        Title = "Trigonometry"
                    },
                    new CourseModel
                    {
                        CourseID = 2,
                        Credits = 4,
                        Title = "Physics"
                    },
                    new CourseModel
                    {
                        CourseID = 4,
                        Credits = 5,
                        Title = "Algebra"
                    }
                }
            );

            DepartmentModel currentDepartment = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                departmentModel,
                departmentModel.EntityToObjectDictionary
                (
                   serviceProvider.GetRequiredService<IMapper>(),
                   formDescriptor.FieldSettings
                ),
                modifiedProperties,
                formDescriptor.FieldSettings
            );

            Assert.Equal(LogicBuilder.Domain.EntityStateType.Modified, currentDepartment.EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Unchanged, currentDepartment.Courses.Single(c => c.CourseID == 1).EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Unchanged, currentDepartment.Courses.Single(c => c.CourseID == 2).EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Deleted, currentDepartment.Courses.Single(c => c.CourseID == 3).EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Added, currentDepartment.Courses.Single(c => c.CourseID == 4).EntityState);
            Assert.Equal(4, currentDepartment.Courses.Count);
        }

        [Fact]
        public void ShouldCorrectlySetEntityStatesForAddedChildEntity()
        {
            //arrange
            DataFormSettingsDescriptor formDescriptor = Descriptors.InstructorFormWithInlineOfficeAssignment;
            InstructorModel instructorModel = new()
            {
                ID = 3,
                FirstName = "John",
                LastName = "Smith",
                HireDate = new DateTime(2021, 5, 20)
            };

            ObservableCollection<IValidatable> modifiedProperties = CreateValidatablesFormSettings(formDescriptor, typeof(InstructorModel));
            IDictionary<string, IValidatable> propertiesDictionary = modifiedProperties.ToDictionary(property => property.Name);
            propertiesDictionary["ID"].Value = 3;
            propertiesDictionary["FirstName"].Value = "John";
            propertiesDictionary["LastName"].Value = "Smith";
            propertiesDictionary["HireDate"].Value = new DateTime(2021, 5, 20);
            propertiesDictionary["OfficeAssignment.Location"].Value = "Location1";

            InstructorModel currentInstructor = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                instructorModel,
                instructorModel.EntityToObjectDictionary
                (
                   serviceProvider.GetRequiredService<IMapper>(),
                   formDescriptor.FieldSettings
                ),
                modifiedProperties,
                formDescriptor.FieldSettings
            );

            Assert.Equal(LogicBuilder.Domain.EntityStateType.Modified, currentInstructor.EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Added, currentInstructor.OfficeAssignment.EntityState);
        }

        [Fact]
        public void ShouldCorrectlySetEntityStatesForExistingChildEntity()
        {
            //arrange
            DataFormSettingsDescriptor formDescriptor = Descriptors.InstructorFormWithInlineOfficeAssignment;
            InstructorModel instructorModel = new()
            {
                ID = 3,
                FirstName = "John",
                LastName = "Smith",
                HireDate = new DateTime(2021, 5, 20),
                OfficeAssignment = new OfficeAssignmentModel
                {
                    Location = "Location1"
                }
            };

            ObservableCollection<IValidatable> modifiedProperties = CreateValidatablesFormSettings(formDescriptor, typeof(InstructorModel));
            IDictionary<string, IValidatable> propertiesDictionary = modifiedProperties.ToDictionary(property => property.Name);
            propertiesDictionary["ID"].Value = 3;
            propertiesDictionary["FirstName"].Value = "John";
            propertiesDictionary["LastName"].Value = "Smith";
            propertiesDictionary["HireDate"].Value = new DateTime(2021, 5, 20);
            propertiesDictionary["OfficeAssignment.Location"].Value = "Location2";

            InstructorModel currentInstructor = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                instructorModel,
                instructorModel.EntityToObjectDictionary
                (
                   serviceProvider.GetRequiredService<IMapper>(),
                   formDescriptor.FieldSettings
                ),
                modifiedProperties,
                formDescriptor.FieldSettings
            );

            // Assert.Equal(LogicBuilder.Domain.EntityStateType.Unchanged, currentInstructor.EntityState);
            //Assert.Equal(LogicBuilder.Domain.EntityStateType.Unchanged, currentInstructor.OfficeAssignment.EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Modified, currentInstructor.EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Modified, currentInstructor.OfficeAssignment.EntityState);
        }

        [Fact]
        public void ShouldCorrectlySetEntityStatesForAddedObjectGraph()
        {
            //arrange
            DataFormSettingsDescriptor formDescriptor = Descriptors.DepartmentForm;
            DepartmentModel? departmentModel = null;

            ObservableCollection<IValidatable> modifiedProperties = CreateValidatablesFormSettings(formDescriptor, typeof(DepartmentModel));
            IDictionary<string, IValidatable> propertiesDictionary = modifiedProperties.ToDictionary(property => property.Name);
            propertiesDictionary["DepartmentID"].Value = 1;
            propertiesDictionary["Name"].Value = "Mathematics";
            propertiesDictionary["Budget"].Value = 100000m;
            propertiesDictionary["StartDate"].Value = new DateTime(2021, 5, 20);
            propertiesDictionary["InstructorID"].Value = 1;
            propertiesDictionary["Courses"].Value = new ObservableCollection<CourseModel>
            (
                new List<CourseModel>
                {
                    new CourseModel
                    {
                        CourseID = 1,
                        Credits = 3,
                        Title = "Trigonometry"
                    },
                    new CourseModel
                    {
                        CourseID = 2,
                        Credits = 4,
                        Title = "Physics"
                    },
                    new CourseModel
                    {
                        CourseID = 4,
                        Credits = 5,
                        Title = "Algebra"
                    }
                }
            );

            DepartmentModel currentDepartment = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                departmentModel,
                departmentModel.EntityToObjectDictionary
                (
                   serviceProvider.GetRequiredService<IMapper>(),
                   formDescriptor.FieldSettings
                )!,/*does not return null*/
                modifiedProperties,
                formDescriptor.FieldSettings
            );

            Assert.Equal(LogicBuilder.Domain.EntityStateType.Added, currentDepartment.EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Added, currentDepartment.Courses.Single(c => c.CourseID == 1).EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Added, currentDepartment.Courses.Single(c => c.CourseID == 2).EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Added, currentDepartment.Courses.Single(c => c.CourseID == 4).EntityState);
            Assert.Equal(3, currentDepartment.Courses.Count);
        }

        [Fact]
        public void ShouldCorrectlySetEntityStatesForAddedObjectGraphWithChildEntity()
        {
            //arrange
            DataFormSettingsDescriptor formDescriptor = Descriptors.InstructorFormWithInlineOfficeAssignment;
            InstructorModel? instructorModel = null;

            ObservableCollection<IValidatable> modifiedProperties = CreateValidatablesFormSettings(formDescriptor, typeof(InstructorModel));
            IDictionary<string, IValidatable> propertiesDictionary = modifiedProperties.ToDictionary(property => property.Name);
            propertiesDictionary["ID"].Value = 3;
            propertiesDictionary["FirstName"].Value = "John";
            propertiesDictionary["LastName"].Value = "Smith";
            propertiesDictionary["HireDate"].Value = new DateTime(2021, 5, 20);
            propertiesDictionary["OfficeAssignment.Location"].Value = "Location1";

            InstructorModel currentInstructor = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                instructorModel,
                instructorModel.EntityToObjectDictionary
                (
                   serviceProvider.GetRequiredService<IMapper>(),
                   formDescriptor.FieldSettings
                )!,/*does not return null*/
                modifiedProperties,
                formDescriptor.FieldSettings
            );

            Assert.Equal(LogicBuilder.Domain.EntityStateType.Added, currentInstructor?.EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Added, currentInstructor?.OfficeAssignment.EntityState);
        }

        private ObservableCollection<IValidatable> CreateValidatablesFormSettings(IFormGroupSettings formSettings, Type modelType)
        {
            return GetFieldsCollectionBuilder
            (
                formSettings,
                modelType
            )
            .CreateFields().Properties;
        }

        private IFieldsCollectionBuilder GetFieldsCollectionBuilder(IFormGroupSettings formSettings, Type modelType)
        {
            return serviceProvider.GetRequiredService<IContextProvider>().GetFieldsCollectionBuilder
            (
                modelType,
                formSettings.FieldSettings,
                formSettings,
                formSettings.ValidationMessages,
                null,
                null
            );
        }
    }
}
