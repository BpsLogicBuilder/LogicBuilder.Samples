using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.DataForm;
using Contoso.Parameters.Expressions;
using Contoso.XPlatform.Flow.Requests;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.DetailForm
{
    public class DetailFormEntityViewModel<TModel> : DetailFormEntityViewModelBase, IDisposable where TModel : Domain.EntityModelBase
    {
        public DetailFormEntityViewModel(ScreenSettings<DataFormSettingsDescriptor> screenSettings, IContextProvider contextProvider) 
            : base(screenSettings, contextProvider)
        {
            FormLayout = contextProvider.ReadOnlyFieldsCollectionBuilder.CreateFieldsCollection(this.FormSettings, typeof(TModel));
            this.httpService = contextProvider.HttpService;
            this.propertiesUpdater = contextProvider.ReadOnlyPropertiesUpdater;
            this.uiNotificationService = contextProvider.UiNotificationService;
            this.getItemFilterBuilder = contextProvider.GetItemFilterBuilder;

            this.directiveManagers = new ReadOnlyDirectiveManagers<TModel>
            (
                FormLayout.Properties,
                FormSettings,
                contextProvider
            );

            GetEntity();
        }

        private readonly IHttpService httpService;
        private readonly IReadOnlyPropertiesUpdater propertiesUpdater;
        private readonly IGetItemFilterBuilder getItemFilterBuilder;
        private readonly UiNotificationService uiNotificationService;
        private readonly ReadOnlyDirectiveManagers<TModel> directiveManagers;
        private TModel entity;

        private ICommand _deleteCommand;
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
                             await App.Current.MainPage.DisplayAlert
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

        private ICommand _editCommand;
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

        public void Dispose()
        {
            Dispose(this.directiveManagers);
            foreach (var property in FormLayout.Properties)
            {
                if (property is IDisposable disposable)
                    Dispose(disposable);
            }
        }

        protected void Dispose(IDisposable disposable)
        {
            if (disposable != null)
                disposable.Dispose();
        }

        private void Edit(CommandButtonDescriptor button)
        {
            SetItemFilterAndNavigateNext(button);
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
                await App.Current.MainPage.DisplayAlert
                (
                    "Errors",
                    string.Join(Environment.NewLine, baseResponse.ErrorMessages),
                    "Ok"
                );
                return;
            }

            GetEntityResponse getEntityResponse = (GetEntityResponse)baseResponse;
            this.entity = (TModel)getEntityResponse.Entity;
            (EditCommand as Command).ChangeCanExecute();

            this.propertiesUpdater.UpdateProperties
            (
                FormLayout.Properties,
                getEntityResponse.Entity,
                this.FormSettings.FieldSettings
            );
        }

        private void SetItemFilterAndNavigateNext(CommandButtonDescriptor button)
        {
            using (IScopedFlowManagerService flowManagerService = App.ServiceProvider.GetRequiredService<IScopedFlowManagerService>())
            {
                flowManagerService.CopyFlowItems();

                flowManagerService.SetFlowDataCacheItem
                (
                    typeof(FilterLambdaOperatorParameters).FullName,
                    this.getItemFilterBuilder.CreateFilter
                    (
                        this.FormSettings.ItemFilterGroup,
                        typeof(TModel),
                        this.entity
                    )
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
}
