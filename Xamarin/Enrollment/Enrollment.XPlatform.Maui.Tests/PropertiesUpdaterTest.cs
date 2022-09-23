using Enrollment.Domain.Entities;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Maui.Tests.Helpers;
using Enrollment.XPlatform.Services;
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
    public class PropertiesUpdaterTest
    {
        public PropertiesUpdaterTest()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void MapResidencyModelToIValidatableList()
        {
            //arrange
            ResidencyModel residency = new()
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

            ObservableCollection<IValidatable> properties = GetFieldsCollectionBuilder
            (
                Descriptors.ResidencyForm,
                typeof(ResidencyModel)
            )
            .CreateFields()
            .Properties;

            //act
            serviceProvider.GetRequiredService<IPropertiesUpdater>().UpdateProperties
            (
                properties,
                residency,
                Descriptors.ResidencyForm.FieldSettings
            );

            //assert
            IDictionary<string, object?> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);
            Assert.Equal(3, propertiesDictionary["UserId"]);
            Assert.Equal("US", propertiesDictionary["CitizenshipStatus"]);
            Assert.Equal("OH", propertiesDictionary["ResidentState"]);
            Assert.Equal(true, propertiesDictionary["HasValidDriversLicense"]);
            Assert.Equal("GA", ((IEnumerable<StateLivedInModel>)propertiesDictionary["StatesLivedIn"]!).First().State);
        }

        [Fact]
        public void MapAcademicModelToIValidatableList()
        {
            //arrange
            AcademicModel academic = new()
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

            ObservableCollection<IValidatable> properties = GetFieldsCollectionBuilder
            (
                Descriptors.AcademicForm,
                typeof(AcademicModel)
            )
            .CreateFields()
            .Properties;

            //act
            serviceProvider.GetRequiredService<IPropertiesUpdater>().UpdateProperties
            (
                properties,
                academic,
                Descriptors.AcademicForm.FieldSettings
            );

            //assert
            IDictionary<string, object?> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);

            Assert.Equal(1, propertiesDictionary["UserId"]);
            Assert.Equal("NC", propertiesDictionary["LastHighSchoolLocation"]);
            Assert.Equal("NCSCHOOL1", propertiesDictionary["NcHighSchoolName"]);
            Assert.Equal(new DateTime(2021, 5, 20), propertiesDictionary["FromDate"]);
            Assert.Equal(new DateTime(2021, 5, 20), propertiesDictionary["ToDate"]);
            Assert.Equal("2011", ((IEnumerable<InstitutionModel>)propertiesDictionary["Institutions"]!).First().StartYear);
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
