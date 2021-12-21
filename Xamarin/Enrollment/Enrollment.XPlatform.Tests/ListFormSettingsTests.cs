using AutoMapper;
using Enrollment.AutoMapperProfiles;
using Enrollment.Domain.Entities;
using Enrollment.Forms.Configuration.ListForm;
using Enrollment.Forms.Parameters;
using Enrollment.Forms.Parameters.Bindings;
using Enrollment.Forms.Parameters.ListForm;
using Enrollment.XPlatform.AutoMapperProfiles;
using System.Collections.Generic;
using Xunit;

namespace Enrollment.XPlatform.Tests
{
    public class ListFormSettingsTests
    {
        public ListFormSettingsTests()
        {
            SetupAutoMapper();
        }

        #region Fields
        static IMapper mapper;
        #endregion Fields

        [Fact]
        public void Map_ConnectorParameters_To_CommandButtonDescriptor()
        {
            ListFormSettingsParameters parameters = new ListFormSettingsParameters
            (
                "About",
                typeof(LookUpsModel),
                "Loading ...",
                "TextDetailTemplate",
                new List<ItemBindingParameters>
                {
                    new TextItemBindingParameters
                    (
                        "Text",
                        "DateTimeValue",
                        "Enrollment Date",
                        "Enrollment Date: {0:MM/dd/yyyy}",
                        new TextFieldTemplateParameters("TextTemplate")
                    )
                },
                null,
                null
            );
            ListFormSettingsDescriptor settings = mapper.Map<ListFormSettingsDescriptor>(parameters);
            Assert.Equal("TextDetailTemplate", settings.ItemTemplateName);
        }

        private static void SetupAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FormsParameterToFormsDescriptorMappingProfile>();
                cfg.AddProfile<ParameterToDescriptorMappingProfile>();
                cfg.AddProfile<DescriptorToOperatorMappingProfile>();
                cfg.AddProfile<ExpansionParameterToDescriptorMappingProfile>();
                cfg.AddProfile<ExpansionDescriptorToOperatorMappingProfile>();
                cfg.AddProfile<ItemFilterParameterToDescriptorMappingProfile>();
            });
            config.AssertConfigurationIsValid<FormsParameterToFormsDescriptorMappingProfile>();
            mapper = config.CreateMapper();
        }
    }
}
