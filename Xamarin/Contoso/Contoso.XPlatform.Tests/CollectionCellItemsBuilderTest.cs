using AutoMapper;
using Contoso.Domain.Entities;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.Bindings;
using Contoso.XPlatform.Flow;
using Contoso.XPlatform.Flow.Cache;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Tests.Mocks;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using Contoso.XPlatform.ViewModels.Validatables;
using LogicBuilder.RulesDirector;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace Contoso.XPlatform.Tests
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
        public void CreateReadOnlyPropertiesForInstructorModel()
        {
            ICollection<IReadOnly> properties = serviceProvider.GetRequiredService<ICollectionCellItemsBuilder>().CreateCellsCollection
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
            );

            Assert.Equal(2, properties.Count);
        }

        [Fact]
        public void CreateReadOnlyPropertiesForInstructorModelWithNavigationPrpertu()
        {
            ICollection<IReadOnly> properties = serviceProvider.GetRequiredService<ICollectionCellItemsBuilder>().CreateCellsCollection
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
