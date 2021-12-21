using AutoMapper;
using Enrollment.Domain.Entities;
using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.Bindings;
using Enrollment.XPlatform.Flow;
using Enrollment.XPlatform.Flow.Cache;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.Tests.Mocks;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using LogicBuilder.RulesDirector;
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
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void CreateReadOnlyPropertiesForUserModel()
        {
            ICollection<IReadOnly> properties = serviceProvider.GetRequiredService<ICollectionCellItemsBuilder>().CreateCellsCollection
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
            );

            Assert.Equal(2, properties.Count);
        }

        [Fact]
        public void CreateReadOnlyPropertiesForUserMddelWithNavigationProperty()
        {
            ICollection<IReadOnly> properties = serviceProvider.GetRequiredService<ICollectionCellItemsBuilder>().CreateCellsCollection
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
            );

            Assert.Equal(3, properties.Count);
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
                .AddSingleton<IUpdateOnlyFieldsCollectionBuilder, UpdateOnlyFieldsCollectionBuilder>()
                .AddSingleton<IReadOnlyFieldsCollectionBuilder, ReadOnlyFieldsCollectionBuilder>()
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
