using AutoMapper;
using Contoso.AutoMapperProfiles;
using Contoso.XPlatform.AutoMapperProfiles;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Contoso.XPlatform.Maui.Tests
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
