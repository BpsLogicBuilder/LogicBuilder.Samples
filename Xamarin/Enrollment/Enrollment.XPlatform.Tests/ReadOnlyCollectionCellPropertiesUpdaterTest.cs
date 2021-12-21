using AutoMapper;
using Enrollment.Domain.Entities;
using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.Bindings;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Flow;
using Enrollment.XPlatform.Flow.Cache;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.Tests.Mocks;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using LogicBuilder.RulesDirector;
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
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void MapResidencyModelToIReadOnlyList()
        {
            //arrange
            ResidencyModel residency = new ResidencyModel
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

            List<ItemBindingDescriptor> itemBindings = new List<ItemBindingDescriptor>
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

            ICollection<IReadOnly> properties = serviceProvider.GetRequiredService<ICollectionCellItemsBuilder>().CreateCellsCollection
            (
                itemBindings,
                typeof(ResidencyModel)
            );

            //act
            serviceProvider.GetRequiredService<IReadOnlyCollectionCellPropertiesUpdater>().UpdateProperties
            (
                properties,
                typeof(ResidencyModel),
                residency,
                itemBindings
            );

            //assert
            IDictionary<string, object> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);
            Assert.Equal(3, propertiesDictionary["UserId"]);
            Assert.Equal("US", propertiesDictionary["CitizenshipStatus"]);
            Assert.Equal("GA", ((IEnumerable<StateLivedInModel>)propertiesDictionary["StatesLivedIn"]).First().State);
        }

        static MapperConfiguration MapperConfiguration;
        private void Initialize()
        {
            if (MapperConfiguration == null)
            {
                MapperConfiguration = new MapperConfiguration(cfg =>
                {
                });
            }
            MapperConfiguration.AssertConfigurationIsValid();
            serviceProvider = new ServiceCollection()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    MapperConfiguration
                )
                .AddTransient<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .AddSingleton<UiNotificationService, UiNotificationService>()
                .AddSingleton<IFlowManager, FlowManager>()
                .AddSingleton<FlowActivityFactory, FlowActivityFactory>()
                .AddSingleton<DirectorFactory, DirectorFactory>()
                .AddSingleton<FlowDataCache, FlowDataCache>()
                .AddSingleton<ScreenData, ScreenData>()
                .AddSingleton<IAppLogger, AppLoggerMock>()
                .AddSingleton<IRulesCache, RulesCacheMock>()
                .AddSingleton<IDialogFunctions, DialogFunctions>()
                .AddSingleton<IActions, Actions>()
                .AddSingleton<IFieldsCollectionBuilder, FieldsCollectionBuilder>()
                .AddSingleton<ICollectionCellItemsBuilder, CollectionCellItemsBuilder>()
                .AddSingleton<IConditionalValidationConditionsBuilder, ConditionalValidationConditionsBuilder>()
                .AddSingleton<IHideIfConditionalDirectiveBuilder, HideIfConditionalDirectiveBuilder>()
                .AddSingleton<IClearIfConditionalDirectiveBuilder, ClearIfConditionalDirectiveBuilder>()
                .AddSingleton<IReloadIfConditionalDirectiveBuilder, ReloadIfConditionalDirectiveBuilder>()
                .AddSingleton<IGetItemFilterBuilder, GetItemFilterBuilder>()
                .AddSingleton<ISearchSelectorBuilder, SearchSelectorBuilder>()
                .AddSingleton<IEntityStateUpdater, EntityStateUpdater>()
                .AddSingleton<IEntityUpdater, EntityUpdater>()
                .AddSingleton<IPropertiesUpdater, PropertiesUpdater>()
                .AddSingleton<IReadOnlyPropertiesUpdater, ReadOnlyPropertiesUpdater>()
                .AddSingleton<IReadOnlyCollectionCellPropertiesUpdater, ReadOnlyCollectionCellPropertiesUpdater>()
                .AddSingleton<IContextProvider, ContextProvider>()
                .AddHttpClient()
                .AddSingleton<IHttpService, HttpServiceMock>()
                .BuildServiceProvider();
        }
    }
}
