﻿using AutoMapper;
using Enrollment.Forms.Configuration;
using Enrollment.Forms.Parameters;
using Enrollment.XPlatform.AutoMapperProfiles;
using LogicBuilder.Forms.Parameters;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Enrollment.XPlatform.Tests
{
    public class CommandButtonTests
    {
        static CommandButtonTests()
        {
            SetupAutoMapper();
        }
        #region Fields
        static IMapper mapper;
        #endregion Fields

        [Fact]
        public void Map_ConnectorParameters_To_CommandButtonDescriptor()
        {
            ConnectorParameters parameters = new()
            {
                Id = 1,
                ShortString = "EDT",
                LongString = "Edit",
                ConnectorData = new CommandButtonParameters("SubmitCommand", "Save")
            };

            CommandButtonDescriptor button = mapper.Map<CommandButtonDescriptor>(parameters);
            Assert.Equal(1, button.Id);
            Assert.Equal("EDT", button.ShortString);
            Assert.Equal("Edit", button.LongString);
            Assert.Equal("Save", button.ButtonIcon);
            Assert.Equal("SubmitCommand", button.Command);
        }

        [MemberNotNull(nameof(mapper))]
        private static void SetupAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CommandButtonProfile>();
            });
            config.AssertConfigurationIsValid();
            mapper = config.CreateMapper();
        }
    }
}
