using AutoMapper;
using Contoso.AutoMapperProfiles;
using Contoso.XPlatform.AutoMapperProfiles;
using Xunit;

namespace Contoso.XPlatform.Tests
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
