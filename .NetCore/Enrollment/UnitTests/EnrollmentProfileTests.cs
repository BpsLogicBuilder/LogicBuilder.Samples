using AutoMapper;
using Enrollment.AutoMapperProfiles;
using Enrollment.Data.Entities;
using Enrollment.Domain.Entities;
using Enrollment.Forms.Parameters.Common;
using LogicBuilder.Expressions.Utils.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class EnrollmentProfileTests
    {
        public EnrollmentProfileTests()
        {
            SetupAutoMapper();
        }
        #region Fields
        static IMapper mapper;
        #endregion Fields

        #region Methods
        [Fact]
        public void Map_Residency_model_with_location_to_residency()
        {
            ResidencyModel residency = new ResidencyModel
            {
                CitizenshipStatus = "US",
                CountryOfCitizenship ="AA",
                DriversLicenseNumber = "NC12345",
                DriversLicenseState = "NC",
                EntityState = LogicBuilder.Domain.EntityStateType.Added,
                HasValidDriversLicense = true,
                ImmigrationStatus = "BB",
                ResidentState = "AR",
                UserId = 7,
                StatesLivedIn = new List<StateLivedInModel>
                {
                    new StateLivedInModel{ EntityState = LogicBuilder.Domain.EntityStateType.Added, State = "OH"  },
                    new StateLivedInModel{ EntityState = LogicBuilder.Domain.EntityStateType.Deleted, State = "TN", UserId = 7, StateLivedInId = 41  }
                }
            };

            Residency mapped = mapper.Map<Residency>(residency);

            Assert.Equal("US", mapped.CitizenshipStatus);
            Assert.Equal("OH", mapped.StatesLivedIn.First().State);
            Assert.Equal(LogicBuilder.Data.EntityStateType.Added, mapped.StatesLivedIn.First().EntityState);
        }

        [Fact]
        public void Map_Enrollment_FilterGroup_To_LogicBuilder_FilterGroup()
        {
            FilterGroupParameters filterGroup = new FilterGroupParameters
            {
                Logic = "and",
                Filters = new List<FilterDefinitionParameters>
                {
                    new FilterDefinitionParameters { Field = "FirstName", Operator = "contains", Value = "ja" },
                    new FilterDefinitionParameters { Field = "ZipCaode", Operator = "eq", Value = "28202" }
                }
            };

            FilterGroup mapped = mapper.Map<FilterGroup>(filterGroup);
            Assert.Equal("and", mapped.Logic);
            Assert.Equal("FirstName", mapped.Filters.First().Field);
            Assert.Equal("contains", mapped.Filters.First().Operator);
        }

        private static void SetupAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(EnrollmentProfile));
            });
            config.AssertConfigurationIsValid<EnrollmentProfile>();
            config.AssertConfigurationIsValid<FilterGroupProfile>();
            mapper = config.CreateMapper();
        }
        #endregion Methods
    }
}
