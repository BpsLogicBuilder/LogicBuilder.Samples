using AutoMapper;
using Contoso.AutoMapperProfiles;
using Contoso.Forms.Parameters;
using Contoso.Forms.View;
using Contoso.Spa.AutoMapperProfiles;
using LogicBuilder.Forms.Parameters;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace MapperTests
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

        #region Methods
        [Fact]
        public void Map_ConnectorParameters_To_CommandButtonView()
        {
            ConnectorParameters parameters = new()
            {
                Id = 1,
                ShortString = "EDT",
                LongString = "Edit",
                ConnectorData = new CommandButtonParameters
                {
                    ButtonIcon = "fa-step-forward",
                    Cancel = false,
                    ClassString = "btn-secondary",
                    GridCommandButton = true,
                    GridId = 1
                }
            };

            CommandButtonView button = mapper.Map<CommandButtonView>(parameters);
            Assert.Equal(1, button.Id);
            Assert.Equal("EDT", button.ShortString);
            Assert.Equal("Edit", button.LongString);
            Assert.Equal("fa-step-forward", button.ButtonIcon);
            Assert.False(button.Cancel);
            Assert.Equal("btn-secondary", button.ClassString);
            Assert.True(button.GridCommandButton);
            Assert.Equal(1, button.GridId);
        }

        [Fact]
        public void Map_CommandButtonView_To_ConnectorParameters()
        {
            CommandButtonView button = new()
            {
                Id = 1,
                ShortString = "EDT",
                LongString = "Edit",
                ButtonIcon = "fa-step-forward",
                Cancel = false,
                ClassString = "btn-secondary",
                GridCommandButton = true,
                GridId = 1
            };

            ConnectorParameters parameters = mapper.Map<ConnectorParameters>(button);
            CommandButtonParameters data = (CommandButtonParameters)parameters.ConnectorData;
            Assert.Equal(1, parameters.Id);
            Assert.Equal("EDT", parameters.ShortString);
            Assert.Equal("Edit", parameters.LongString);
            Assert.Equal("fa-step-forward", data.ButtonIcon);
            Assert.False(data.Cancel);
            Assert.Equal("btn-secondary", data.ClassString);
            Assert.True(data.GridCommandButton);
            Assert.Equal(1, data.GridId);
        }

        [MemberNotNull(nameof(mapper))]
        private static void SetupAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(ConnectorProfile));
                cfg.AddMaps(typeof(DescriptorToOperatorMappingProfile));
            });
            config.AssertConfigurationIsValid();
            mapper = config.CreateMapper();
        }
        #endregion Methods
    }
}
