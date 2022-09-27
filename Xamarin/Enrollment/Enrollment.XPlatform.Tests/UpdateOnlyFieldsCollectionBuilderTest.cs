using Enrollment.Domain.Entities;
using Enrollment.XPlatform.Tests.Helpers;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.ViewModels.Factories;
using Enrollment.XPlatform.ViewModels.Validatables;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace Enrollment.XPlatform.Tests
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
                typeof(ResidencyModel),
                Descriptors.ResidencyForm.FieldSettings,
                Descriptors.ResidencyForm,
                Descriptors.ResidencyForm.ValidationMessages,
                null,
                null
            );

            //act
            ObservableCollection<IValidatable> properties = updateOnlyFieldsCollectionBuilder.CreateFields().Properties;
            //assert
            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            Assert.Equal(typeof(LabelValidatableObject<string>), propertiesDictionary["CitizenshipStatus"].GetType());
        }
    }
}
