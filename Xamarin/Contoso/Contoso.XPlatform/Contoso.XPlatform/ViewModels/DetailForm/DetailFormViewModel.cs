using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Directives;
using Contoso.XPlatform.Directives.Factories;
using Contoso.XPlatform.Flow.Requests;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.DetailForm
{
    public class DetailFormViewModel<TModel> : DetailFormViewModelBase, IDisposable where TModel : Domain.EntityModelBase
    {
        public DetailFormViewModel(
            ICollectionBuilderFactory collectionBuilderFactory,
            IDirectiveManagersFactory directiveManagersFactory,
            IHttpService httpService,
            IReadOnlyPropertiesUpdater readOnlyPropertiesUpdater,
            IUiNotificationService uiNotificationService,
            ScreenSettings<DataFormSettingsDescriptor> screenSettings) 
            : base(screenSettings, uiNotificationService)
        {
            FormLayout = collectionBuilderFactory.GetReadOnlyFieldsCollectionBuilder
            (
                typeof(TModel),
                this.FormSettings.FieldSettings,
                this.FormSettings,
                null,
                null
            ).CreateFields();

            this.httpService = httpService;
            this.propertiesUpdater = readOnlyPropertiesUpdater;

            this.directiveManagers = (ReadOnlyDirectiveManagers<TModel>)directiveManagersFactory.GetReadOnlyDirectiveManagers
            (
                typeof(TModel), 
                FormLayout.Properties, 
                FormSettings
            );
            
            GetEntity();
        }

        private readonly IHttpService httpService;
        private readonly IReadOnlyPropertiesUpdater propertiesUpdater;
        private readonly ReadOnlyDirectiveManagers<TModel> directiveManagers;
        private TModel? entity;

        private ICommand? _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand != null)
                    return _deleteCommand;

                _deleteCommand = new Command<CommandButtonDescriptor>
                (
                     async (button) => 
                     {
                         BaseResponse response = await BusyIndicatorHelpers.ExecuteRequestWithBusyIndicator
                        (
                            () => this.httpService.DeleteEntity
                            (
                                new DeleteEntityRequest
                                {
                                    Entity = entity
                                },
                                this.FormSettings.RequestDetails.DeleteUrl
                            )
                        );

                         if (response.Success == false)
                         {
                             await App.Current!.MainPage!.DisplayAlert/*App.Current.MainPage is not null at this point*/
                             (
                                 "Errors",
                                 string.Join(Environment.NewLine, response.ErrorMessages),
                                 "Ok"
                             );
                         }

                         Next(button);
                     }
                );

                return _deleteCommand;
            }
        }

        private ICommand? _editCommand;
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand != null)
                    return _editCommand;

                _editCommand = new Command<CommandButtonDescriptor>
                (
                    Edit,
                    (button) => this.entity != null
                );

                return _editCommand;
            }
        }

        public override DetailFormLayout FormLayout { get; set; }

        public void Dispose()
        {
            Dispose(this.directiveManagers);
            foreach (var property in FormLayout.Properties)
            {
                if (property is IDisposable disposable)
                    Dispose(disposable);
            }
            GC.SuppressFinalize(this);
        }

        protected void Dispose(IDisposable disposable)
        {
            if (disposable != null)
                disposable.Dispose();
        }

        private void Edit(CommandButtonDescriptor button)
        {
            SetEntityAndNavigateNext(button);
        }

        private async void GetEntity()
        {
            if (this.FormSettings.RequestDetails.Filter == null)
                throw new ArgumentException($"{nameof(this.FormSettings.RequestDetails.Filter)}: 51755FE3-099A-44EB-A59B-3ED312EDD8D1");

            BaseResponse baseResponse = await BusyIndicatorHelpers.ExecuteRequestWithBusyIndicator
            (
                () => this.httpService.GetEntity
                (
                    new GetEntityRequest
                    {
                        Filter = this.FormSettings.RequestDetails.Filter,
                        SelectExpandDefinition = this.FormSettings.RequestDetails.SelectExpandDefinition,
                        ModelType = this.FormSettings.RequestDetails.ModelType,
                        DataType = this.FormSettings.RequestDetails.DataType
                    }
                )
            );

            if (baseResponse.Success == false)
            {
                await App.Current!.MainPage!.DisplayAlert/*App.Current.MainPage is not null at this point*/
                (
                    "Errors",
                    string.Join(Environment.NewLine, baseResponse.ErrorMessages),
                    "Ok"
                );
                return;
            }

            GetEntityResponse getEntityResponse = (GetEntityResponse)baseResponse;
            this.entity = (TModel)getEntityResponse.Entity;
            ((Command)EditCommand).ChangeCanExecute();

            this.propertiesUpdater.UpdateProperties
            (
                FormLayout.Properties,
                getEntityResponse.Entity,
                this.FormSettings.FieldSettings
            );
        }

        private void SetEntityAndNavigateNext(CommandButtonDescriptor button)
        {
            if (entity == null)
                throw new ArgumentException($"{nameof(entity)}: {{97F661A5-1AE3-4A85-8083-438B665A58B7}}");

            using IScopedFlowManagerService flowManagerService = App.ServiceProvider.GetRequiredService<IScopedFlowManagerService>();
            flowManagerService.CopyFlowItems();

            flowManagerService.SetFlowDataCacheItem
            (
                typeof(TModel).FullName!,
                this.entity
            );

            flowManagerService.Next
            (
                new CommandButtonRequest
                {
                    NewSelection = button.ShortString
                }
            );
        }
    }
}
