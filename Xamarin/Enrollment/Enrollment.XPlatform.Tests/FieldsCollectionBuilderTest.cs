using AutoMapper;
using Enrollment.Domain.Entities;
using Enrollment.XPlatform.Flow;
using Enrollment.XPlatform.Flow.Cache;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.Tests.Mocks;
using Enrollment.XPlatform.ViewModels;
using Enrollment.XPlatform.ViewModels.Validatables;
using LogicBuilder.RulesDirector;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace Enrollment.XPlatform.Tests
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
                Descriptors.AcademicForm,
                typeof(AcademicModel)
            ).Properties;

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
            EditFormLayout formLayout = serviceProvider.GetRequiredService<IFieldsCollectionBuilder>().CreateFieldsCollection
            (
                Descriptors.PersonalFrom,
                typeof(UserModel)
            );

            Assert.Equal(3, formLayout.ControlGroupBoxList.Count);
            Assert.Equal(5, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Name").Count);
            Assert.Equal(6, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Address").Count);
            Assert.Equal(2, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Phone Numbers").Count);
        }

        [Fact]
        public void CreateEditFormLayoutForUserModelPersonalWithDefaultGroupForSomeFields()
        {
            EditFormLayout formLayout = serviceProvider.GetRequiredService<IFieldsCollectionBuilder>().CreateFieldsCollection
            (
                Descriptors.PersonalFromWithDefaultGroupForSomeFields,
                typeof(UserModel)
            );

            Assert.Equal(3, formLayout.ControlGroupBoxList.Count);
            Assert.Equal(5, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "PersonalRoot").Count);
            Assert.Equal(6, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Address").Count);
            Assert.Equal(2, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Phone Numbers").Count);
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
