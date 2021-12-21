using AutoMapper;
using Enrollment.Domain.Entities;
using Enrollment.XPlatform.Flow;
using Enrollment.XPlatform.Flow.Cache;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.Tests.Mocks;
using Enrollment.XPlatform.Utils;
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
    public class EntityUpdaterTest
    {
        public EntityUpdaterTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void MapIValidatableListToResidency()
        {
            //arrange
            ObservableCollection<IValidatable> properties = serviceProvider.GetRequiredService<IFieldsCollectionBuilder>().CreateFieldsCollection
            (
                Descriptors.ResidencyForm, 
                typeof(ResidencyModel)
            ).Properties;
            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            propertiesDictionary["UserId"].Value = 3;
            propertiesDictionary["CitizenshipStatus"].Value = "US";
            propertiesDictionary["ResidentState"].Value = "OH";
            propertiesDictionary["HasValidDriversLicense"].Value = true;
            propertiesDictionary["StatesLivedIn"].Value = new ObservableCollection<StateLivedInModel>
            (
                new List<StateLivedInModel>
                {
                    new StateLivedInModel
                    {
                        StateLivedInId = 1,
                        UserId = 3,
                        State = "GA"
                    },
                    new StateLivedInModel
                    {
                        StateLivedInId = 2,
                        UserId = 3,
                        State = "MI"
                    },
                    new StateLivedInModel
                    {
                        StateLivedInId = 4,
                        UserId = 3,
                        State = "IN"
                    }
                }
            );

            //act
            ResidencyModel residencyModel = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                (ResidencyModel) null,
                new Dictionary<string, object>(),
                properties,
                Descriptors.ResidencyForm.FieldSettings
            );

            //assert
            Assert.Equal(3, residencyModel.UserId);
            Assert.Equal("US", residencyModel.CitizenshipStatus);
            Assert.Equal("OH", residencyModel.ResidentState);
            Assert.True(residencyModel.HasValidDriversLicense);
            Assert.Equal("GA", residencyModel.StatesLivedIn.First().State);
        }

        [Fact]
        public void MapIValidatableListModelToAcademic()
        {
            //arrange
            ObservableCollection<IValidatable> properties = serviceProvider.GetRequiredService<IFieldsCollectionBuilder>().CreateFieldsCollection
            (
                Descriptors.AcademicForm,
                typeof(AcademicModel)
            ).Properties;
            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            propertiesDictionary["UserId"].Value = 1;
            propertiesDictionary["LastHighSchoolLocation"].Value = "NC";
            propertiesDictionary["NcHighSchoolName"].Value = "NCSCHOOL1";
            propertiesDictionary["FromDate"].Value = new DateTime(2019, 5, 20);
            propertiesDictionary["ToDate"].Value = new DateTime(2021, 5, 20);
            propertiesDictionary["GraduationStatus"].Value = "DP";
            propertiesDictionary["EarnedCreditAtCmc"].Value = true;
            propertiesDictionary["Institutions"].Value = new ObservableCollection<InstitutionModel>
            (
                new List<InstitutionModel>
                {
                    new InstitutionModel
                    {
                        InstitutionId = 1,
                        InstitutionState = "FL",
                        InstitutionName = "I1",
                        StartYear = "2011",
                        EndYear = "2013",
                        HighestDegreeEarned = "CT"
                    },
                    new InstitutionModel
                    {
                        InstitutionId = 2,
                        InstitutionState = "GA",
                        InstitutionName = "I1",
                        StartYear = "2012",
                        EndYear = "2014",
                        HighestDegreeEarned = "DP"
                    },
                    new InstitutionModel
                    {
                        InstitutionId = 4,
                        InstitutionState = "AL",
                        InstitutionName = "I2",
                        StartYear = "2016",
                        EndYear = "2019",
                        HighestDegreeEarned = "MA"
                    }
                }
            );

            //act
            AcademicModel academicModel = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                (AcademicModel)null,
                new Dictionary<string, object>(),
                properties,
                Descriptors.AcademicForm.FieldSettings
            );

            //assert
            Assert.Equal(1, academicModel.UserId);
            Assert.Equal("NC", academicModel.LastHighSchoolLocation);
            Assert.Equal("NCSCHOOL1", academicModel.NcHighSchoolName);
            Assert.Equal(new DateTime(2019, 5, 20), academicModel.FromDate);
            Assert.Equal(new DateTime(2021, 5, 20), academicModel.ToDate);
            Assert.Equal("DP", academicModel.GraduationStatus);
            Assert.True(academicModel.EarnedCreditAtCmc);
            Assert.Equal("FL", academicModel.Institutions.First().InstitutionState);
        }

        [Fact]
        public void MapIValidatableListModelToAcademic_WhenSingleValueFieldIsNotRequired()
        {
            //arrange
            ObservableCollection<IValidatable> properties = serviceProvider.GetRequiredService<IFieldsCollectionBuilder>().CreateFieldsCollection
            (
                Descriptors.AcademicForm,
                typeof(AcademicModel)
            ).Properties;
            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            propertiesDictionary["UserId"].Value = 1;
            propertiesDictionary["LastHighSchoolLocation"].Value = "NC";
            propertiesDictionary["FromDate"].Value = new DateTime(2019, 5, 20);
            propertiesDictionary["ToDate"].Value = new DateTime(2021, 5, 20);
            propertiesDictionary["GraduationStatus"].Value = "DP";
            propertiesDictionary["EarnedCreditAtCmc"].Value = true;
            propertiesDictionary["Institutions"].Value = new ObservableCollection<InstitutionModel>
            (
                new List<InstitutionModel>
                {
                    new InstitutionModel
                    {
                        InstitutionId = 1,
                        InstitutionState = "FL",
                        InstitutionName = "I1",
                        StartYear = "2011",
                        EndYear = "2013",
                        HighestDegreeEarned = "CT"
                    },
                    new InstitutionModel
                    {
                        InstitutionId = 2,
                        InstitutionState = "GA",
                        InstitutionName = "I1",
                        StartYear = "2012",
                        EndYear = "2014",
                        HighestDegreeEarned = "DP"
                    },
                    new InstitutionModel
                    {
                        InstitutionId = 4,
                        InstitutionState = "AL",
                        InstitutionName = "I2",
                        StartYear = "2016",
                        EndYear = "2019",
                        HighestDegreeEarned = "MA"
                    }
                }
            );

            //act
            AcademicModel academicModel = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                (AcademicModel)null,
                new Dictionary<string, object>(),
                properties,
                Descriptors.AcademicForm.FieldSettings
            );

            //assert
            Assert.Equal(1, academicModel.UserId);
            Assert.Equal("NC", academicModel.LastHighSchoolLocation);
            Assert.Equal("", academicModel.NcHighSchoolName);
            Assert.Equal(new DateTime(2019, 5, 20), academicModel.FromDate);
            Assert.Equal(new DateTime(2021, 5, 20), academicModel.ToDate);
            Assert.Equal("DP", academicModel.GraduationStatus);
            Assert.True(academicModel.EarnedCreditAtCmc);
            Assert.Equal("FL", academicModel.Institutions.First().InstitutionState);
        }

        [Fact]
        public void MapIValidatableListModelToAcademic_WhenSingleValueFieldIsNotDefauly()
        {
            //arrange
            ObservableCollection<IValidatable> properties = serviceProvider.GetRequiredService<IFieldsCollectionBuilder>().CreateFieldsCollection
            (
                Descriptors.AcademicForm,
                typeof(AcademicModel)
            ).Properties;
            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            propertiesDictionary["NcHighSchoolName"].Value = null;

            //act
            AcademicModel academicModel = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                (AcademicModel)null,
                new Dictionary<string, object>(),
                properties,
                Descriptors.AcademicForm.FieldSettings
            );

            //assert
            Assert.Null(academicModel.NcHighSchoolName);
        }

        [Fact]
        public void MapIValidatableListToResidency_WnenMultiSelectFieldNotRequired()
        {
            //arrange
            ObservableCollection<IValidatable> properties = serviceProvider.GetRequiredService<IFieldsCollectionBuilder>().CreateFieldsCollection
            (
                Descriptors.ResidencyForm,
                typeof(ResidencyModel)
            ).Properties;
            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            propertiesDictionary["UserId"].Value = 3;
            propertiesDictionary["CitizenshipStatus"].Value = "US";
            propertiesDictionary["ResidentState"].Value = "OH";
            propertiesDictionary["HasValidDriversLicense"].Value = true;

            //act
            ResidencyModel residencyModel = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                (ResidencyModel)null,
                new Dictionary<string, object>(),
                properties,
                Descriptors.ResidencyForm.FieldSettings
            );

            //assert
            Assert.Equal(3, residencyModel.UserId);
            Assert.Equal("US", residencyModel.CitizenshipStatus);
            Assert.Equal("OH", residencyModel.ResidentState);
            Assert.True(residencyModel.HasValidDriversLicense);
            Assert.Null(residencyModel.StatesLivedIn);
        }

        [Fact]
        public void MapIValidatableListModelToAcademic_WhenFormArrayIsNotRequired()
        {
            //arrange
            ObservableCollection<IValidatable> properties = serviceProvider.GetRequiredService<IFieldsCollectionBuilder>().CreateFieldsCollection
            (
                Descriptors.AcademicForm,
                typeof(AcademicModel)
            ).Properties;
            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            propertiesDictionary["UserId"].Value = 1;
            propertiesDictionary["LastHighSchoolLocation"].Value = "NC";
            propertiesDictionary["NcHighSchoolName"].Value = "NCSCHOOL1";
            propertiesDictionary["FromDate"].Value = new DateTime(2019, 5, 20);
            propertiesDictionary["ToDate"].Value = new DateTime(2021, 5, 20);
            propertiesDictionary["GraduationStatus"].Value = "DP";
            propertiesDictionary["EarnedCreditAtCmc"].Value = true;

            //act
            AcademicModel academicModel = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                (AcademicModel)null,
                new Dictionary<string, object>(),
                properties,
                Descriptors.AcademicForm.FieldSettings
            );

            //assert
            Assert.Equal(1, academicModel.UserId);
            Assert.Equal("NC", academicModel.LastHighSchoolLocation);
            Assert.Equal("NCSCHOOL1", academicModel.NcHighSchoolName);
            Assert.Equal(new DateTime(2019, 5, 20), academicModel.FromDate);
            Assert.Equal(new DateTime(2021, 5, 20), academicModel.ToDate);
            Assert.Equal("DP", academicModel.GraduationStatus);
            Assert.True(academicModel.EarnedCreditAtCmc);
            Assert.Empty(academicModel.Institutions);
        }

        [Fact]
        public void MapIValidatableListToPersonal_withMultipleGroupBoxSettingsDescriptorFields()
        {
            //arrange
            ObservableCollection<IValidatable> properties = serviceProvider.GetRequiredService<IFieldsCollectionBuilder>().CreateFieldsCollection
            (
                Descriptors.PersonalFrom,
                typeof(UserModel)
            ).Properties;
            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            propertiesDictionary["Personal.FirstName"].Value = "John";
            propertiesDictionary["Personal.MiddleName"].Value = "Michael";
            propertiesDictionary["Personal.LastName"].Value = "Jackson";
            propertiesDictionary["Personal.PrimaryEmail"].Value = "mj@hotmail.com";
            propertiesDictionary["Personal.Suffix"].Value = "jr";
            propertiesDictionary["Personal.Address1"].Value = "820 Jackson Street";
            propertiesDictionary["Personal.Address2"].Value = "UNIT 1975";
            propertiesDictionary["Personal.City"].Value = "Detroit";
            propertiesDictionary["Personal.County"].Value = "Dekalb";
            propertiesDictionary["Personal.State"].Value = "MI";
            propertiesDictionary["Personal.ZipCode"].Value = "23456";
            propertiesDictionary["Personal.CellPhone"].Value = "770-888-9999";
            propertiesDictionary["Personal.OtherPhone"].Value = "770-333-4444";

            //act
            UserModel userModel = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                (UserModel)null,
                new Dictionary<string, object>(),
                properties,
                Descriptors.PersonalFrom.FieldSettings
            );

            //assert
            Assert.Equal("John", userModel.Personal.FirstName);
            Assert.Equal("Michael", userModel.Personal.MiddleName);
            Assert.Equal("Jackson", userModel.Personal.LastName);
            Assert.Equal("mj@hotmail.com", userModel.Personal.PrimaryEmail);
            Assert.Equal("jr", userModel.Personal.Suffix);
            Assert.Equal("820 Jackson Street", userModel.Personal.Address1);
            Assert.Equal("UNIT 1975", userModel.Personal.Address2);
            Assert.Equal("Detroit", userModel.Personal.City);
            Assert.Equal("Dekalb", userModel.Personal.County);
            Assert.Equal("MI", userModel.Personal.State);
            Assert.Equal("23456", userModel.Personal.ZipCode);
            Assert.Equal("770-888-9999", userModel.Personal.CellPhone);
            Assert.Equal("770-333-4444", userModel.Personal.OtherPhone);
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
