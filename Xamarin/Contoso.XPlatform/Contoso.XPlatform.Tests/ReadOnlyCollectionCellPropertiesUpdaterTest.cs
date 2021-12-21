using AutoMapper;
using Contoso.Domain.Entities;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.Bindings;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Flow;
using Contoso.XPlatform.Flow.Cache;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Tests.Mocks;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using Contoso.XPlatform.ViewModels.Validatables;
using LogicBuilder.RulesDirector;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace Contoso.XPlatform.Tests
{
    public class ReadOnlyCollectionCellPropertiesUpdaterTest
    {
        public ReadOnlyCollectionCellPropertiesUpdaterTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void MapInstructorModelToIReadOnlyList()
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

            List<ItemBindingDescriptor> itemBindings = new List<ItemBindingDescriptor>
            {
                new TextItemBindingDescriptor
                {
                    Name = "Text",
                    Property = "HireDate",
                    StringFormat = "{0:MMMM dd, yyyy}",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "DateTemplate" }
                },
                new MultiSelectItemBindingDescriptor
                {
                    Name = "Detail",
                    Property = "Courses",
                    StringFormat = "{0}",
                    MultiSelectTemplate = Descriptors.InstructorFormWithPopupOfficeAssignment.FieldSettings.OfType<MultiSelectFormControlSettingsDescriptor>().Single(f => f.Field == "Courses").MultiSelectTemplate
                }
            };

            ICollection<IReadOnly> properties = serviceProvider.GetRequiredService<ICollectionCellItemsBuilder>().CreateCellsCollection
            (
                itemBindings,
                typeof(InstructorModel)
            );

            //act
            serviceProvider.GetRequiredService<IReadOnlyCollectionCellPropertiesUpdater>().UpdateProperties
            (
                properties,
                typeof(InstructorModel),
                instructor,
                itemBindings
            );

            //assert
            IDictionary<string, object> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);
            Assert.Equal(new DateTime(2021, 5, 20), propertiesDictionary["HireDate"]);
            Assert.Equal(1, ((IEnumerable<CourseAssignmentModel>)propertiesDictionary["Courses"]).First().CourseID);
        }

        [Fact]
        public void MapCourseModelToIReadOnlyList()
        {
            //arrange
            CourseModel course = new CourseModel
            {
                CourseID = 3,
                Title = "Chemistry",
                Credits = 5
            };

            List<ItemBindingDescriptor> itemBindings = new List<ItemBindingDescriptor>
            {
                new TextItemBindingDescriptor
                {
                    Name = "Header",
                    Property = "CourseID",
                    StringFormat = "{}",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "DateTemplate" }
                },
                new TextItemBindingDescriptor
                {
                    Name = "Detail",
                    Property = "Title",
                    StringFormat = "{0",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "DateTemplate" }
                },
                new DropDownItemBindingDescriptor
                {
                    Name = "Detail",
                    Property = "Credits",
                    StringFormat = "{0}",
                    DropDownTemplate = Descriptors.CourseForm.FieldSettings.OfType<FormControlSettingsDescriptor>().Single(f => f.Field == "Credits").DropDownTemplate
                }
            };

            ICollection<IReadOnly> properties = serviceProvider.GetRequiredService<ICollectionCellItemsBuilder>().CreateCellsCollection
            (
                itemBindings,
                typeof(CourseModel)
            );

            //act
            serviceProvider.GetRequiredService<IReadOnlyCollectionCellPropertiesUpdater>().UpdateProperties
            (
                properties,
                typeof(CourseModel),
                course,
                itemBindings
            );

            //assert
            IDictionary<string, object> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);
            Assert.Equal(3, propertiesDictionary["CourseID"]);
            Assert.Equal("Chemistry", propertiesDictionary["Title"]);
            Assert.Equal(5, propertiesDictionary["Credits"]);
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
