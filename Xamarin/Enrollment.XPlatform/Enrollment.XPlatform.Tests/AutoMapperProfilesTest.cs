using AutoMapper;
using Enrollment.AutoMapperProfiles;
using Enrollment.XPlatform.AutoMapperProfiles;
using System;
using Xunit;

namespace Enrollment.XPlatform.Tests
{
    public class AutoMapperProfilesTest
    {
        public AutoMapperProfilesTest()
        {
            SetupAutoMapper();
        }

        [Fact]
        public void TestConfigurationIsValid()
        {
            config.AssertConfigurationIsValid();
        }

        static MapperConfiguration config;
        private static void SetupAutoMapper()
        {
            config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(DescriptorToOperatorMappingProfile), typeof(CommandButtonProfile));//AddMaps adds all other profiles
            });
        }
    }
}
