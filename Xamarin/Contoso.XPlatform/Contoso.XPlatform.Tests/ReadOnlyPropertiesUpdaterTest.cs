using AutoMapper;
using Contoso.Domain.Entities;
using Contoso.XPlatform.Flow;
using Contoso.XPlatform.Flow.Cache;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Tests.Mocks;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using LogicBuilder.RulesDirector;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace Contoso.XPlatform.Tests
{
    public class ReadOnlyPropertiesUpdaterTest
    {
        public ReadOnlyPropertiesUpdaterTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void MapInstructorModelToIReadOnlyListWithInlineOfficeAssignment()
        {
            //arrange
            InstructorModel instructor = new InstructorModel
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
                        InstructorID = 2,
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
                        InstructorID = 4,
                        CourseTitle = "Mathematics"
                    }
                }
            };
            ObservableCollection<IReadOnly> properties = serviceProvider.GetRequiredService<IReadOnlyFieldsCollectionBuilder>().CreateFieldsCollection
            (
                ReadOnlyDescriptors.InstructorFormWithInlineOfficeAssignment,
                typeof(InstructorModel)
            ).Properties;

            //act
            serviceProvider.GetRequiredService<IReadOnlyPropertiesUpdater>().UpdateProperties
            (
                properties,
                instructor,
                ReadOnlyDescriptors.InstructorFormWithInlineOfficeAssignment.FieldSettings
            );

            //assert
            IDictionary<string, object> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);
            Assert.Equal(3, propertiesDictionary["ID"]);
            Assert.Equal("John", propertiesDictionary["FirstName"]);
            Assert.Equal("Smith", propertiesDictionary["LastName"]);
            Assert.Equal(new DateTime(2021, 5, 20), propertiesDictionary["HireDate"]);
            Assert.Equal("Location1", propertiesDictionary["OfficeAssignment.Location"]);
            Assert.Equal("Chemistry", ((IEnumerable<CourseAssignmentModel>)propertiesDictionary["Courses"]).First().CourseTitle);
        }

        [Fact]
        public void MapInstructorModelToIReadOnlyeListWithPopupOfficeAssignment()
        {
            //arrange
            InstructorModel instructor = new InstructorModel
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
                        InstructorID = 2,
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
                        InstructorID = 4,
                        CourseTitle = "Mathematics"
                    }
                }
            };
            ObservableCollection<IReadOnly> properties = serviceProvider.GetRequiredService<IReadOnlyFieldsCollectionBuilder>().CreateFieldsCollection
            (
                ReadOnlyDescriptors.InstructorFormWithPopupOfficeAssignment,
                typeof(InstructorModel)
            ).Properties;

            //act
            serviceProvider.GetRequiredService<IReadOnlyPropertiesUpdater>().UpdateProperties
            (
                properties,
                instructor,
                ReadOnlyDescriptors.InstructorFormWithPopupOfficeAssignment.FieldSettings
            );

            //assert
            IDictionary<string, object> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);
            Assert.Equal(3, propertiesDictionary["ID"]);
            Assert.Equal("John", propertiesDictionary["FirstName"]);
            Assert.Equal("Smith", propertiesDictionary["LastName"]);
            Assert.Equal(new DateTime(2021, 5, 20), propertiesDictionary["HireDate"]);
            Assert.Equal("Location1", ((OfficeAssignmentModel)propertiesDictionary["OfficeAssignment"]).Location);
            Assert.Equal("Chemistry", ((IEnumerable<CourseAssignmentModel>)propertiesDictionary["Courses"]).First().CourseTitle);
        }

        [Fact]
        public void MapDepartmentModelToIReadOnlyList()
        {
            //arrange
            DepartmentModel department = new DepartmentModel
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
            ObservableCollection<IReadOnly> properties = serviceProvider.GetRequiredService<IReadOnlyFieldsCollectionBuilder>().CreateFieldsCollection
            (
                ReadOnlyDescriptors.DepartmentForm,
                typeof(DepartmentModel)
            ).Properties;

            //act
            serviceProvider.GetRequiredService<IReadOnlyPropertiesUpdater>().UpdateProperties
            (
                properties,
                department,
                ReadOnlyDescriptors.DepartmentForm.FieldSettings
            );

            //assert
            IDictionary<string, object> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);

            Assert.Equal(1, propertiesDictionary["DepartmentID"]);
            Assert.Equal("Mathematics", propertiesDictionary["Name"]);
            Assert.Equal(100000m, propertiesDictionary["Budget"]);
            Assert.Equal(new DateTime(2021, 5, 20), propertiesDictionary["StartDate"]);
            Assert.Equal(1, propertiesDictionary["InstructorID"]);
            Assert.Equal("Trigonometry", ((IEnumerable<CourseModel>)propertiesDictionary["Courses"]).First().Title);
        }

        [Fact]
        public void MapCourseModel_WithMultipleGroupBoxSettingsDescriptorFields_ToIReadOnlyList()
        {
            //arrange
            CourseModel courseModel = new CourseModel
            {
                CourseID = 1,
                Credits = 3,
                Title = "Trigonometry",
                DepartmentID = 2
            };

            ObservableCollection<IReadOnly> properties = serviceProvider.GetRequiredService<IReadOnlyFieldsCollectionBuilder>().CreateFieldsCollection
            (
                ReadOnlyDescriptors.CourseForm,
                typeof(CourseModel)
            ).Properties;

            //act
            serviceProvider.GetRequiredService<IReadOnlyPropertiesUpdater>().UpdateProperties
            (
                properties,
                courseModel,
                ReadOnlyDescriptors.CourseForm.FieldSettings
            );

            IDictionary<string, object> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);
            Assert.Equal(1, propertiesDictionary["CourseID"]);
            Assert.Equal(3, propertiesDictionary["Credits"]);
            Assert.Equal("Trigonometry", propertiesDictionary["Title"]);
            Assert.Equal(2, propertiesDictionary["DepartmentID"]);
        }

        static MapperConfiguration MapperConfiguration;
        private void Initialize()
        {
            if (MapperConfiguration == null)
            {
                MapperConfiguration = new MapperConfiguration(cfg =>
                {
                });
            }
            MapperConfiguration.AssertConfigurationIsValid();
            serviceProvider = new ServiceCollection()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    MapperConfiguration
                )
                .AddTransient<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .AddSingleton<UiNotificationService, UiNotificationService>()
                .AddSingleton<IFlowManager, FlowManager>()
                .AddSingleton<FlowActivityFactory, FlowActivityFactory>()
                .AddSingleton<DirectorFactory, DirectorFactory>()
                .AddSingleton<FlowDataCache, FlowDataCache>()
                .AddSingleton<ScreenData, ScreenData>()
                .AddSingleton<IAppLogger, AppLoggerMock>()
                .AddSingleton<IRulesCache, RulesCacheMock>()
                .AddSingleton<IDialogFunctions, DialogFunctions>()
                .AddSingleton<IActions, Actions>()
                .AddSingleton<IFieldsCollectionBuilder, FieldsCollectionBuilder>()
                .AddSingleton<ICollectionCellItemsBuilder, CollectionCellItemsBuilder>()
                .AddSingleton<IReadOnlyFieldsCollectionBuilder, ReadOnlyFieldsCollectionBuilder>()
                .AddSingleton<IConditionalValidationConditionsBuilder, ConditionalValidationConditionsBuilder>()
                .AddSingleton<IHideIfConditionalDirectiveBuilder, HideIfConditionalDirectiveBuilder>()
                .AddSingleton<IClearIfConditionalDirectiveBuilder, ClearIfConditionalDirectiveBuilder>()
                .AddSingleton<IReloadIfConditionalDirectiveBuilder, ReloadIfConditionalDirectiveBuilder>()
                .AddSingleton<IGetItemFilterBuilder, GetItemFilterBuilder>()
                .AddSingleton<ISearchSelectorBuilder, SearchSelectorBuilder>()
                .AddSingleton<IEntityStateUpdater, EntityStateUpdater>()
                .AddSingleton<IEntityUpdater, EntityUpdater>()
                .AddSingleton<IPropertiesUpdater, PropertiesUpdater>()
                .AddSingleton<IReadOnlyPropertiesUpdater, ReadOnlyPropertiesUpdater>()
                .AddSingleton<IReadOnlyCollectionCellPropertiesUpdater, ReadOnlyCollectionCellPropertiesUpdater>()
                .AddSingleton<IContextProvider, ContextProvider>()
                .AddHttpClient()
                .AddSingleton<IHttpService, HttpServiceMock>()
                .BuildServiceProvider();
        }
    }
}
