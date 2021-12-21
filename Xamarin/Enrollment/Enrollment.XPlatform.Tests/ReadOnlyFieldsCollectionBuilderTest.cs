using AutoMapper;
using Enrollment.Domain.Entities;
using Enrollment.XPlatform.Flow;
using Enrollment.XPlatform.Flow.Cache;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.Tests.Mocks;
using Enrollment.XPlatform.ViewModels;
using LogicBuilder.RulesDirector;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace Enrollment.XPlatform.Tests
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
        public void CreateDetailFormLayoutForUserModelPersonal()
        {
            DetailFormLayout formLayout = serviceProvider.GetRequiredService<IReadOnlyFieldsCollectionBuilder>().CreateFieldsCollection
            (
                ReadOnlyDescriptors.PersonalFrom,
                typeof(UserModel)
            );

            Assert.Equal(3, formLayout.ControlGroupBoxList.Count);
            Assert.Equal(5, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Name").Count);
            Assert.Equal(6, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Address").Count);
            Assert.Equal(2, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Phone Numbers").Count);
        }

        [Fact]
        public void CreateDetailFormLayoutForUserModelPersonalWithDefaultGroupForSomeFields()
        {
            DetailFormLayout formLayout = serviceProvider.GetRequiredService<IReadOnlyFieldsCollectionBuilder>().CreateFieldsCollection
            (
                ReadOnlyDescriptors.PersonalFromWithDefaultGroupForSomeFields,
                typeof(UserModel)
            );

            Assert.Equal(3, formLayout.ControlGroupBoxList.Count);
            Assert.Equal(5, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "PersonalRoot").Count);
            Assert.Equal(6, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Address").Count);
            Assert.Equal(2, formLayout.ControlGroupBoxList.Single(cg => cg.GroupHeader == "Phone Numbers").Count);
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
