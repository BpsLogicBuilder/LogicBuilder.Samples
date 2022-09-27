using Enrollment.Domain.Entities;
using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.Bindings;
using Enrollment.XPlatform.Tests.Helpers;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.ViewModels.Factories;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace Enrollment.XPlatform.Tests
{
    public class CollectionCellItemsBuilderTest
    {
        public CollectionCellItemsBuilderTest()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void CreateReadOnlyPropertiesForUserModel()
        {
            ICollection<IReadOnly> properties = GetCollectionCellItemsBuilder
            (
                new List<ItemBindingDescriptor>
                {
                    new TextItemBindingDescriptor
                    {
                        Name = "Text",
                        Property = "UserId",
                        StringFormat = "{0}",
                        TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                    },
                    new TextItemBindingDescriptor
                    {
                        Name = "Detail",
                        Property = "UserName",
                        StringFormat = "{0}",
                        TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                    }
                },
                typeof(UserModel)
            ).CreateFields();

            Assert.Equal(2, properties.Count);
        }

        [Fact]
        public void CreateReadOnlyPropertiesForUserMddelWithNavigationProperty()
        {
            ICollection<IReadOnly> properties = GetCollectionCellItemsBuilder
            (
                new List<ItemBindingDescriptor>
                {
                    new TextItemBindingDescriptor
                    {
                        Name = "Text",
                        Property = "UserId",
                        StringFormat = "{0}",
                        TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                    },
                    new TextItemBindingDescriptor
                    {
                        Name = "Detail",
                        Property = "UserName",
                        StringFormat = "{0}",
                        TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                    },
                    new TextItemBindingDescriptor
                    {
                        Name = "Detail",
                        Property = "Personal.FirstName",
                        StringFormat = "{0}",
                        TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                    }
                },
                typeof(UserModel)
            ).CreateFields();

            Assert.Equal(3, properties.Count);
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
