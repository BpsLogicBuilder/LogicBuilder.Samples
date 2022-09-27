using Contoso.Domain.Entities;
using Contoso.XPlatform.Tests.Helpers;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels.Factories;
using Contoso.XPlatform.ViewModels.Validatables;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace Contoso.XPlatform.Tests
{
    public class UpdateOnlyFieldsCollectionBuilderTest
    {
        public UpdateOnlyFieldsCollectionBuilderTest()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void MapCourseModelToIValidatableList()
        {
            //arrange
            IUpdateOnlyFieldsCollectionBuilder updateOnlyFieldsCollectionBuilder = serviceProvider.GetRequiredService<ICollectionBuilderFactory>().GetUpdateOnlyFieldsCollectionBuilder
            (
                typeof(CourseModel),
                Descriptors.CourseForm.FieldSettings,
                Descriptors.CourseForm,
                Descriptors.CourseForm.ValidationMessages,
                null,
                null
            );

            //act
            ObservableCollection<IValidatable> properties = updateOnlyFieldsCollectionBuilder.CreateFields().Properties;

            //assert
            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            Assert.Equal(typeof(LabelValidatableObject<int>), propertiesDictionary["CourseID"].GetType());
        }
    }
}
