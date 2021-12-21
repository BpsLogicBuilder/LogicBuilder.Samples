using AutoMapper;
using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.Validators;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.EditForm
{
    public class EditFormEntityViewModel<TModel> : EditFormEntityViewModelBase where TModel : Domain.EntityModelBase
    {
        public EditFormEntityViewModel(ScreenSettings<DataFormSettingsDescriptor> screenSettings, IContextProvider contextProvider)
            : base(screenSettings, contextProvider)
        {
            FormLayout = contextProvider.FieldsCollectionBuilder.CreateFieldsCollection(this.FormSettings, typeof(TModel));
            this.entityStateUpdater = contextProvider.EntityStateUpdater;
            this.httpService = contextProvider.HttpService;
            this.propertiesUpdater = contextProvider.PropertiesUpdater;
            this.mapper = contextProvider.Mapper;
            this.directiveManagers = new DirectiveManagers<TModel>
            (
                FormLayout.Properties,
                FormSettings,
                contextProvider
            );

            propertyChangedSubscription = this.UiNotificationService.ValueChanged.Subscribe(FieldChanged);

            if (this.FormSettings.FormType == FormType.Update)
                GetEntity();
        }

        private readonly IEntityStateUpdater entityStateUpdater;
        private readonly IHttpService httpService;
        private readonly IPropertiesUpdater propertiesUpdater;
        private readonly IMapper mapper;
        private readonly DirectiveManagers<TModel> directiveManagers;
        private TModel entity;
        private Dictionary<string, object> originalEntityDictionary = new Dictionary<string, object>();
        private readonly IDisposable propertyChangedSubscription;

        public override void Dispose()
        {
            base.Dispose();
            Dispose(this.directiveManagers);
            Dispose(this.propertyChangedSubscription);
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

        private void FieldChanged(string fieldName)
        {
            (SubmitCommand as Command).ChangeCanExecute();
        }

        private async void GetEntity()
        {
            if (this.FormSettings.RequestDetails.Filter == null)
                throw new ArgumentException($"{nameof(this.FormSettings.RequestDetails.Filter)}: 883E834F-98A6-4DF7-9D07-F1BB0D6639E1");

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

            this.originalEntityDictionary = this.entity.EntityToObjectDictionary
            (
               mapper,
               this.FormSettings.FieldSettings
            );

            this.propertiesUpdater.UpdateProperties
            (
                FormLayout.Properties,
                getEntityResponse.Entity,
                this.FormSettings.FieldSettings
            );
        }

        private ICommand _submitCommand;
        public ICommand SubmitCommand => _submitCommand ??= new Command<CommandButtonDescriptor>
        (
            execute: async (button) =>
            {
                TModel toSave = this.entityStateUpdater.GetUpdatedModel
                (
                    entity,
                    this.originalEntityDictionary,
                    FormLayout.Properties,
                    FormSettings.FieldSettings
                );

                if (toSave.EntityState == LogicBuilder.Domain.EntityStateType.Unchanged)
                {
                    Next(button);
                    return;
                }

                BaseResponse response = await BusyIndicatorHelpers.ExecuteRequestWithBusyIndicator
                (
                    () => this.httpService.SaveEntity
                    (
                        new SaveEntityRequest
                        {
                            Entity = toSave
                        },
                        this.FormSettings.FormType == FormType.Add
                            ? this.FormSettings.RequestDetails.AddUrl
                            : this.FormSettings.RequestDetails.UpdateUrl
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
            },
            canExecute: (button) => AreFieldsValid()
        );
    }
}
