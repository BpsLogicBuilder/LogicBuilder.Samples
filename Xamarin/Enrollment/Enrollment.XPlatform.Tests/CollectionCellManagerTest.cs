using Enrollment.Domain.Entities;
using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.Bindings;
using Enrollment.XPlatform.Tests.Helpers;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Enrollment.XPlatform.Tests
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
            AssertValuesMatch("EnergencyContactFirstName", "John", updatedItems[0].Key);
            AssertValuesMatch("DateOfBirth", "May 20, 2021", updatedItems[0].Key);
            AssertValuesMatch("User_UserName", "User1", updatedItems[0].Key);

            AssertValuesMatch("EnergencyContactFirstName", "Kasper", updatedItems[1].Key);
            AssertValuesMatch("DateOfBirth", "June 22, 2020", updatedItems[1].Key);
            AssertValuesMatch("User_UserName", "User2", updatedItems[1].Key);

            AssertValuesMatch("EnergencyContactFirstName", "Mary", updatedItems[2].Key);
            AssertValuesMatch("DateOfBirth", "April 20, 2019", updatedItems[2].Key);
            AssertValuesMatch("User_UserName", "User3", updatedItems[2].Key);

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
            var contactInfoModel = new ContactInfoModel
            {
                UserId = 1,
                EnergencyContactFirstName = "The Rock",
                DateOfBirth = new DateTime(2015, 5, 15),
                User = new UserModel
                {
                    UserName = "User4"
                }
            };

            //act
            collectionCellManager.UpdateCollectionCellProperties(contactInfoModel, dictionary.Values, ItemBindings);

            //assert
            Assert.Equal("The Rock", GetDisplayText(dictionary["EnergencyContactFirstName"]));
            Assert.Equal("May 15, 2015", GetDisplayText(dictionary["DateOfBirth"]));
            Assert.Equal("User4", GetDisplayText(dictionary["User_UserName"]));
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        static string GetDisplayText(IReadOnly readOnly)
        {
            return (string)Enrollment.Utils.TypeHelpers.GetPropertyValue(readOnly, "DisplayText");
        }

        private readonly List<ContactInfoModel> Items = new()
        {
            new ContactInfoModel
            {
                UserId = 1,
                EnergencyContactFirstName = "John",
                DateOfBirth = new DateTime(2021, 5, 20),
                User = new UserModel
                {
                    UserName = "User1"
                }
            },
            new ContactInfoModel
            {
                UserId = 2,
                EnergencyContactFirstName = "Kasper",
                DateOfBirth = new DateTime(2020, 6, 22),
                User = new UserModel
                {
                    UserName = "User2"
                }
            },
            new ContactInfoModel
            {
                UserId = 2,
                EnergencyContactFirstName = "Mary",
                DateOfBirth = new DateTime(2019, 4, 20),
                User = new UserModel
                {
                    UserName = "User3"
                }
            }
        };

        private readonly List<ItemBindingDescriptor> ItemBindings = new()
        {
            new TextItemBindingDescriptor
            {
                Name = "Text",
                Property = "DateOfBirth",
                StringFormat = "{0:MMMM dd, yyyy}",
                TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "DateTemplate" }
            },
            new TextItemBindingDescriptor
            {
                Name = "Header",
                Property = "EnergencyContactFirstName",
                StringFormat = "{0}",
                TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
            },
            new TextItemBindingDescriptor
            {
                Name = "Detail",
                Property = "User.UserName",
                StringFormat = "{0}",
                TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
            }
        };

    }
}
