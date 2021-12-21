using AutoMapper;
using Contoso.Domain.Entities;
using Contoso.XPlatform.Flow;
using Contoso.XPlatform.Flow.Cache;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Tests.Mocks;
using Contoso.XPlatform.ViewModels;
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
    public class FieldsCollectionBuilderTest
    {
        public FieldsCollectionBuilderTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void MapCourseModelToIValidatableList()
        {
            //act
            ObservableCollection<IValidatable> properties = serviceProvider.GetRequiredService<IFieldsCollectionBuilder>().CreateFieldsCollection
            (
                Descriptors.CourseForm,
                typeof(CourseModel)
            ).Properties;

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
            EditFormLayout formLayout = serviceProvider.GetRequiredService<IFieldsCollectionBuilder>().CreateFieldsCollection
            (
                Descriptors.DepartmentForm,
                typeof(DepartmentModel)
            );

            Assert.Single(formLayout.ControlGroupBoxList);
            Assert.Equal(6, formLayout.ControlGroupBoxList.Single().Count);
            Assert.Equal("Department", formLayout.ControlGroupBoxList.Single().GroupHeader);
        }

        [Fact]
        public void CreateEditFormLayoutForDepartment_AllFieldsGrouped()
        {
            EditFormLayout formLayout = serviceProvider.GetRequiredService<IFieldsCollectionBuilder>().CreateFieldsCollection
            (
                Descriptors.DepartmentFormWithAllItemsGrouped,
                typeof(DepartmentModel)
            );

            Assert.Equal(2, formLayout.ControlGroupBoxList.Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "GroupOne").Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "GroupTwo").Count);
        }

        [Fact]
        public void CreateEditFormLayoutForDepartment_SomeFieldsGrouped()
        {
            EditFormLayout formLayout = serviceProvider.GetRequiredService<IFieldsCollectionBuilder>().CreateFieldsCollection
            (
                Descriptors.DepartmentFormWithSomesItemsGrouped,
                typeof(DepartmentModel)
            );


            Assert.Equal(2, formLayout.ControlGroupBoxList.Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "GroupOne").Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Department").Count);
            Assert.Equal("Department", formLayout.ControlGroupBoxList.First().GroupHeader);
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
                .AddSingleton<IUpdateOnlyFieldsCollectionBuilder, UpdateOnlyFieldsCollectionBuilder>()
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
