using Contoso.Domain.Entities;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.Bindings;
using Contoso.XPlatform.Tests.Helpers;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Contoso.XPlatform.Tests
{
    public class CollectionCellManagerTest
    {
        public CollectionCellManagerTest()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        [Fact]
        public void CanGetCollectionCellDictionaryModelPair()
        {
            //arrange
            ICollectionCellManager collectionCellManager = serviceProvider.GetRequiredService<ICollectionCellManager>();

            //act
            var updatedItems = Items.Select
            (
                i => collectionCellManager.GetCollectionCellDictionaryModelPair
                (
                    i,
                    ItemBindings
                )
            )
            .ToList();

            //assert
            AssertValuesMatch("FullName", "John Smith", updatedItems[0].Key);
            AssertValuesMatch("HireDate", "May 20, 2021", updatedItems[0].Key);
            AssertValuesMatch("OfficeAssignment_Location", "Location1", updatedItems[0].Key);

            AssertValuesMatch("FullName", "Kasper Klein", updatedItems[1].Key);
            AssertValuesMatch("HireDate", "June 22, 2020", updatedItems[1].Key);
            AssertValuesMatch("OfficeAssignment_Location", "Location2", updatedItems[1].Key);

            AssertValuesMatch("FullName", "Mary Hartson", updatedItems[2].Key);
            AssertValuesMatch("HireDate", "April 20, 2019", updatedItems[2].Key);
            AssertValuesMatch("OfficeAssignment_Location", "Location3", updatedItems[2].Key);

            static void AssertValuesMatch(string itemKey, string expecttedDisplayText, Dictionary<string, IReadOnly> item)
            {
                Assert.Equal(expecttedDisplayText, GetDisplayText(item[itemKey]));
            }
        }

        [Fact]
        public void CanUpateCollectionCellDictionaryModelPair()
        {
            //arrange
            ICollectionCellManager collectionCellManager = serviceProvider.GetRequiredService<ICollectionCellManager>();
            var updatedItems = Items.Select
            (
                i => collectionCellManager.GetCollectionCellDictionaryItem
                (
                    i,
                    ItemBindings
                )
            )
            .ToList();
            Dictionary<string, IReadOnly> dictionary = updatedItems[0];
            var instructorModel = new InstructorModel
            {
                ID = 1,
                FullName = "The Rock",
                HireDate = new DateTime(2015, 5, 15),
                OfficeAssignment = new OfficeAssignmentModel
                {
                    Location = "Location4"
                }
            };

            //act
            collectionCellManager.UpdateCollectionCellProperties(instructorModel, dictionary.Values, ItemBindings);

            //assert
            Assert.Equal("The Rock", GetDisplayText(dictionary["FullName"]));
            Assert.Equal("May 15, 2015", GetDisplayText(dictionary["HireDate"]));
            Assert.Equal("Location4", GetDisplayText(dictionary["OfficeAssignment_Location"]));
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        static string GetDisplayText(IReadOnly readOnly)
        {
            return (string)Contoso.Utils.TypeHelpers.GetPropertyValue(readOnly, "DisplayText");
        }

        private readonly List<InstructorModel> Items = new()
        {
            new InstructorModel
            {
                ID = 1,
                FullName = "John Smith",
                HireDate = new DateTime(2021, 5, 20),
                OfficeAssignment = new OfficeAssignmentModel
                {
                    Location = "Location1"
                }
            },
            new InstructorModel
            {
                ID = 2,
                FullName = "Kasper Klein",
                HireDate = new DateTime(2020, 6, 22),
                OfficeAssignment = new OfficeAssignmentModel
                {
                    Location = "Location2"
                }
            },
            new InstructorModel
            {
                ID = 3,
                FullName = "Mary Hartson",
                HireDate = new DateTime(2019, 4, 20),
                OfficeAssignment = new OfficeAssignmentModel
                {
                    Location = "Location3"
                }
            }
        };

        private readonly List<ItemBindingDescriptor> ItemBindings = new()
        {
            new TextItemBindingDescriptor
            {
                Name = "Text",
                Property = "HireDate",
                StringFormat = "{0:MMMM dd, yyyy}",
                TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "DateTemplate" }
            },
            new TextItemBindingDescriptor
            {
                Name = "Header",
                Property = "FullName",
                StringFormat = "{0}",
                TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
            },
            new TextItemBindingDescriptor
            {
                Name = "Detail",
                Property = "OfficeAssignment.Location",
                StringFormat = "{0}",
                TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
            }
        };

    }
}
