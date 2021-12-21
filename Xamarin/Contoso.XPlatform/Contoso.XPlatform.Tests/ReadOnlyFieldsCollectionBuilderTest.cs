using AutoMapper;
using Contoso.Domain.Entities;
using Contoso.XPlatform.Flow;
using Contoso.XPlatform.Flow.Cache;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Tests.Mocks;
using Contoso.XPlatform.ViewModels;
using LogicBuilder.RulesDirector;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace Contoso.XPlatform.Tests
{
    public class ReadOnlyFieldsCollectionBuilderTest
    {
        public ReadOnlyFieldsCollectionBuilderTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void CreateDetailFormLayoutForDepartment_NoGroups()
        {
            DetailFormLayout formLayout = serviceProvider.GetRequiredService<IReadOnlyFieldsCollectionBuilder>().CreateFieldsCollection
            (
                ReadOnlyDescriptors.DepartmentForm,
                typeof(DepartmentModel)
            );

            Assert.Single(formLayout.ControlGroupBoxList);
            Assert.Equal(6, formLayout.ControlGroupBoxList.Single().Count);
            Assert.Equal("Department", formLayout.ControlGroupBoxList.Single().GroupHeader);
        }

        [Fact]
        public void CreateDetailFormLayoutForDepartment_AllFieldsGrouped()
        {
            DetailFormLayout formLayout = serviceProvider.GetRequiredService<IReadOnlyFieldsCollectionBuilder>().CreateFieldsCollection
            (
                ReadOnlyDescriptors.DepartmentFormWithAllItemsGrouped,
                typeof(DepartmentModel)
            );

            Assert.Equal(2, formLayout.ControlGroupBoxList.Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "GroupOne").Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "GroupTwo").Count);
        }

        [Fact]
        public void CreateDetailFormLayoutForDepartment_SomeFieldsGrouped()
        {
            DetailFormLayout formLayout = serviceProvider.GetRequiredService<IReadOnlyFieldsCollectionBuilder>().CreateFieldsCollection
            (
                ReadOnlyDescriptors.DepartmentFormWithSomeItemsGrouped,
                typeof(DepartmentModel)
            );


            Assert.Equal(2, formLayout.ControlGroupBoxList.Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "GroupOne").Count);
            Assert.Equal(3, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Department").Count);
            Assert.Equal("Department", formLayout.ControlGroupBoxList.First().GroupHeader);
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
