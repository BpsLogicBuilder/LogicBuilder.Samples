using Contoso.Domain.Entities;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.Bindings;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Maui.Tests.Helpers;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace Contoso.XPlatform.Maui.Tests
{
    public class CollectionCellItemsBuilderTest
    {
        public CollectionCellItemsBuilderTest()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields
        [Fact]
        public void CreateReadOnlyPropertiesForInstructorModel()
        {
            ICollection<IReadOnly> properties = GetCollectionCellItemsBuilder
            (
                new List<ItemBindingDescriptor>
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
                        Name = "Detail",
                        Property = "FullName",
                        StringFormat = "{0}",
                        TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                    }
                },
                typeof(InstructorModel)
            ).CreateFields();

            Assert.Equal(2, properties.Count);
        }

        [Fact]
        public void CreateReadOnlyPropertiesForInstructorModelWithNavigationPrpertu()
        {
            ICollection<IReadOnly> properties = GetCollectionCellItemsBuilder
            (
                new List<ItemBindingDescriptor>
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
                        Name = "Detail",
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
                },
                typeof(InstructorModel)
            )
            .CreateFields();

            Assert.Equal(3, properties.Count);
        }

        private ICollectionCellItemsBuilder GetCollectionCellItemsBuilder(List<ItemBindingDescriptor> bindingDescriptors, Type modelType)
        {
            return serviceProvider.GetRequiredService<IContextProvider>().GetCollectionCellItemsBuilder
            (
                modelType,
                bindingDescriptors
            );
        }
    }
}
