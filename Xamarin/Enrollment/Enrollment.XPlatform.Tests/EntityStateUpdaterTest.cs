using AutoMapper;
using Enrollment.Domain.Entities;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Flow.Cache;
using Enrollment.XPlatform.Flow;
using LogicBuilder.RulesDirector;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.ViewModels.Factories;
using Enrollment.XPlatform.Tests.Helpers;
using Enrollment.XPlatform.ViewModels.Validatables;
using Enrollment.XPlatform.Utils;

namespace Enrollment.XPlatform.Tests
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
            DataFormSettingsDescriptor formDescriptor = Descriptors.ResidencyForm;
            ResidencyModel residencyModel = new()
            {
                UserId = 3,
                CitizenshipStatus = "US",
                ResidentState = "OH",
                HasValidDriversLicense = true,
                StatesLivedIn = new List<StateLivedInModel>
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
                        StateLivedInId = 3,
                        UserId = 3,
                        State = "OH"
                    }
                }
            };

            ObservableCollection<IValidatable> modifiedProperties = CreateValidatablesFormSettings(formDescriptor, typeof(ResidencyModel));
            IDictionary<string, IValidatable> propertiesDictionary = modifiedProperties.ToDictionary(property => property.Name);
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

            ResidencyModel currentResidency = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                residencyModel,
                residencyModel.EntityToObjectDictionary
                (
                   serviceProvider.GetRequiredService<IMapper>(),
                   formDescriptor.FieldSettings
                ),
                modifiedProperties,
                formDescriptor.FieldSettings
            );

            Assert.Equal(LogicBuilder.Domain.EntityStateType.Modified, currentResidency.EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Unchanged, currentResidency.StatesLivedIn.Single(c => c.State == "GA").EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Unchanged, currentResidency.StatesLivedIn.Single(c => c.State == "MI").EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Deleted, currentResidency.StatesLivedIn.Single(c => c.State == "OH").EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Added, currentResidency.StatesLivedIn.Single(c => c.State == "IN").EntityState);

            Assert.Equal(4, currentResidency.StatesLivedIn.Count);
        }

        [Fact]
        public void ShouldCorrectlySetEntityStatesForChildFormGroupArray()
        {
            //arrange
            DataFormSettingsDescriptor formDescriptor = Descriptors.AcademicForm;
            AcademicModel academicModel = new()
            {
                UserId = 1,
                LastHighSchoolLocation = "NC",
                NcHighSchoolName = "NCSCHOOL1",
                FromDate = new DateTime(2021, 5, 20),
                ToDate = new DateTime(2021, 5, 20),
                GraduationStatus = "DP",
                EarnedCreditAtCmc = true,
                Institutions = new List<InstitutionModel>
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
                        InstitutionId = 3,
                        InstitutionState = "FL",
                        InstitutionName = "I2",
                        StartYear = "2014",
                        EndYear = "2015",
                        HighestDegreeEarned = "MA"
                    }
                }
            };

            ObservableCollection<IValidatable> modifiedProperties = CreateValidatablesFormSettings(formDescriptor, typeof(AcademicModel));
            IDictionary<string, IValidatable> propertiesDictionary = modifiedProperties.ToDictionary(property => property.Name);

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

            AcademicModel currentAcademic = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                academicModel,
                academicModel.EntityToObjectDictionary
                (
                   serviceProvider.GetRequiredService<IMapper>(),
                   formDescriptor.FieldSettings
                ),
                modifiedProperties,
                formDescriptor.FieldSettings
            );

            Assert.Equal(LogicBuilder.Domain.EntityStateType.Modified, currentAcademic.EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Unchanged, currentAcademic.Institutions.Single(c => c.InstitutionId == 1).EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Unchanged, currentAcademic.Institutions.Single(c => c.InstitutionId == 2).EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Deleted, currentAcademic.Institutions.Single(c => c.InstitutionId == 3).EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Added, currentAcademic.Institutions.Single(c => c.InstitutionId == 4).EntityState);
            Assert.Equal(4, currentAcademic.Institutions.Count);
        }


        [Fact]
        public void ShouldCorrectlySetEntityStatesForAddedObjectGraph()
        {
            //arrange
            DataFormSettingsDescriptor formDescriptor = Descriptors.AcademicForm;
            AcademicModel? academicModel = null;

            ObservableCollection<IValidatable> modifiedProperties = CreateValidatablesFormSettings(formDescriptor, typeof(AcademicModel));
            IDictionary<string, IValidatable> propertiesDictionary = modifiedProperties.ToDictionary(property => property.Name);
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

            AcademicModel currentAcademic = serviceProvider.GetRequiredService<IEntityStateUpdater>().GetUpdatedModel
            (
                academicModel,
                academicModel.EntityToObjectDictionary
                (
                   serviceProvider.GetRequiredService<IMapper>(),
                   formDescriptor.FieldSettings
                ),
                modifiedProperties,
                formDescriptor.FieldSettings
            );

            Assert.Equal(LogicBuilder.Domain.EntityStateType.Added, currentAcademic.EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Added, currentAcademic.Institutions.Single(c => c.InstitutionId == 1).EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Added, currentAcademic.Institutions.Single(c => c.InstitutionId == 2).EntityState);
            Assert.Equal(LogicBuilder.Domain.EntityStateType.Added, currentAcademic.Institutions.Single(c => c.InstitutionId == 4).EntityState);
            Assert.Equal(3, currentAcademic.Institutions.Count);
        }

        private ObservableCollection<IValidatable> CreateValidatablesFormSettings(IFormGroupSettings formSettings, Type modelType)
        {
            return GetFieldsCollectionBuilder
            (
                formSettings,
                modelType
            )
            .CreateFields()
            .Properties;
        }

        private IFieldsCollectionBuilder GetFieldsCollectionBuilder(IFormGroupSettings formSettings, Type modelType)
        {
            return serviceProvider.GetRequiredService<ICollectionBuilderFactory>().GetFieldsCollectionBuilder
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
