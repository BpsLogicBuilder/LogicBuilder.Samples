using AutoMapper;
using Contoso.AutoMapperProfiles;
using Contoso.Domain.Entities;
using Contoso.Forms.Configuration.ListForm;
using Contoso.Forms.Parameters;
using Contoso.Forms.Parameters.Bindings;
using Contoso.Forms.Parameters.ListForm;
using Contoso.XPlatform.AutoMapperProfiles;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Contoso.XPlatform.Tests
{
    public class ListFormSettingsTests
    {
        public ListFormSettingsTests()
        {
            SetupAutoMapper();
        }

        #region Fields
        IMapper mapper;
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
                        "DateTimeValue",
                        "Enrollment Date: {0:MM/dd/yyyy}",
                        new TextFieldTemplateParameters("TextTemplate") { TemplateName = "TextTemplate" }
                    )
                },
                null,
                null
            );
            ListFormSettingsDescriptor settings = mapper.Map<ListFormSettingsDescriptor>(parameters);
            Assert.Equal("TextDetailTemplate", settings.ItemTemplateName);
        }

        [MemberNotNull(nameof(mapper))]
        private void SetupAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FormsParameterToFormsDescriptorMappingProfile>();
                cfg.AddProfile<ParameterToDescriptorMappingProfile>();
                cfg.AddProfile<DescriptorToOperatorMappingProfile>();
                cfg.AddProfile<ExpansionParameterToDescriptorMappingProfile>();
                cfg.AddProfile<ExpansionDescriptorToOperatorMappingProfile>();
            });
            config.AssertConfigurationIsValid();
            mapper = config.CreateMapper();
        }
    }
}
