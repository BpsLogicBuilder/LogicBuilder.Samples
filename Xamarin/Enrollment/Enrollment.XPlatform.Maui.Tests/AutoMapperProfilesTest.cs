using AutoMapper;
using Enrollment.AutoMapperProfiles;
using Enrollment.XPlatform.AutoMapperProfiles;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Enrollment.XPlatform.Maui.Tests
{
    public class AutoMapperProfilesTest
    {
        static AutoMapperProfilesTest()
        {
            SetupAutoMapper();
        }

        [Fact]
        public void TestConfigurationIsValid()
        {
            config.AssertConfigurationIsValid();
        }

        static MapperConfiguration config;
        [MemberNotNull(nameof(config))]
        private static void SetupAutoMapper()
        {
            config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(DescriptorToOperatorMappingProfile), typeof(CommandButtonProfile));//AddMaps adds all other profiles
            });
        }
    }
}
