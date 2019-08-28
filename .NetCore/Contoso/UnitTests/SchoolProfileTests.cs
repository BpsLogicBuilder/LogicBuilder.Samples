using AutoMapper;
using Contoso.AutoMapperProfiles;
using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using Contoso.Forms.Parameters.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class SchoolProfileTests
    {
        public SchoolProfileTests()
        {
            SetupAutoMapper();
        }
        #region Fields
        static IMapper mapper;
        #endregion Fields

        #region Methods
        [Fact]
        public void Map_Instructor_model_with_location_to_instructor()
        {
            InstructorModel ins = new InstructorModel
            {
                FirstName = "Jack",
                OfficeAssignment = new OfficeAssignmentModel
                {
                    Location = "Staff Room 1"
                }
            };

            Instructor mapped = mapper.Map<Instructor>(ins);

            Assert.Equal("Jack", mapped.FirstName);
            Assert.Equal("Staff Room 1", mapped.OfficeAssignment.Location);
        }

        [Fact]
        public void Map_Contoso_FilterGroup_To_LogicBuilder_FilterGroup()
        {
            FilterGroupParameters filterGroup = new FilterGroupParameters
            {
                Logic = "and",
                Filters = new List<FilterDefinitionParameters>
                {
                    new FilterDefinitionParameters{ Field = "FirstName", Operator = "contains", Value = "ja" },
                    new FilterDefinitionParameters{ Field = "ZipCaode", Operator = "eq", Value = "28202" }
                }
            };

            LogicBuilder.Expressions.Utils.DataSource.FilterGroup mapped = mapper.Map<LogicBuilder.Expressions.Utils.DataSource.FilterGroup>(filterGroup);
            Assert.Equal("and", mapped.Logic);
            Assert.Equal("FirstName", mapped.Filters.First().Field);
            Assert.Equal("contains", mapped.Filters.First().Operator);
        }

        private static void SetupAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(SchoolProfile));
            });
            config.AssertConfigurationIsValid<SchoolProfile>();
            config.AssertConfigurationIsValid<FilterGroupProfile>();
            mapper = config.CreateMapper();
        }
        #endregion Methods
    }
}
