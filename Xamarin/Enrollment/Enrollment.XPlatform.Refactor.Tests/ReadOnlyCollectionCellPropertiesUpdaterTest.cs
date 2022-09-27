using Enrollment.Domain.Entities;
using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.Bindings;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Tests.Helpers;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.ViewModels.Factories;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Enrollment.XPlatform.Tests
{
    public class ReadOnlyCollectionCellPropertiesUpdaterTest
    {
        public ReadOnlyCollectionCellPropertiesUpdaterTest()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void MapResidencyModelToIReadOnlyList()
        {
            //arrange
            ResidencyModel residency = new()
            {
                UserId = 3,
                CitizenshipStatus = "US",
                ResidentState = "OH",
                HasValidDriversLicense = true,
                StatesLivedIn = new List<StateLivedInModel>
                {
                    new StateLivedInModel
                    {
                        StateLivedInId = 1,
                        UserId = 3,
                        State = "GA"
                    },
                    new StateLivedInModel
                    {
                        StateLivedInId = 2,
                        UserId = 3,
                        State = "MI"
                    },
                    new StateLivedInModel
                    {
                        StateLivedInId = 3,
                        UserId = 3,
                        State = "OH"
                    }
                }
            };

            List<ItemBindingDescriptor> itemBindings = new()
            {
                new TextItemBindingDescriptor
                {
                    Name = "Text",
                    Property = "UserId",
                    StringFormat = "{0}",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                },
                new DropDownItemBindingDescriptor
                {
                    Name = "Detail",
                    Property = "CitizenshipStatus",
                    StringFormat = "{0}",
                    DropDownTemplate = Descriptors.ResidencyForm.FieldSettings.OfType<FormControlSettingsDescriptor>().Single(f => f.Field == "CitizenshipStatus").DropDownTemplate
                },
                new MultiSelectItemBindingDescriptor
                {
                    Name = "Detail",
                    Property = "StatesLivedIn",
                    StringFormat = "{0}",
                    MultiSelectTemplate = Descriptors.ResidencyForm.FieldSettings.OfType<MultiSelectFormControlSettingsDescriptor>().Single(f => f.Field == "StatesLivedIn").MultiSelectTemplate
                }
            };

            ICollection<IReadOnly> properties = GetCollectionCellItemsBuilder
            (
                itemBindings,
                typeof(ResidencyModel)
            )
            .CreateFields();

            //act
            serviceProvider.GetRequiredService<IReadOnlyCollectionCellPropertiesUpdater>().UpdateProperties
            (
                properties,
                typeof(ResidencyModel),
                residency,
                itemBindings
            );

            //assert
            IDictionary<string, object?> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);
            Assert.Equal(3, propertiesDictionary["UserId"]);
            Assert.Equal("US", propertiesDictionary["CitizenshipStatus"]);
            Assert.Equal("GA", ((IEnumerable<StateLivedInModel>)propertiesDictionary["StatesLivedIn"]!).First().State);
        }

        private ICollectionCellItemsBuilder GetCollectionCellItemsBuilder(List<ItemBindingDescriptor> bindingDescriptors, Type modelType)
        {
            return serviceProvider.GetRequiredService<ICollectionBuilderFactory>().GetCollectionCellItemsBuilder
            (
                modelType,
                bindingDescriptors
            );
        }
    }
}
