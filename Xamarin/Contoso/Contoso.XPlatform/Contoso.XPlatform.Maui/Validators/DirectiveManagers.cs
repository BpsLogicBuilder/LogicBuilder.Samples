using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Validators
{
    internal class DirectiveManagers<TModel> : IDisposable
    {
        public DirectiveManagers(ObservableCollection<IValidatable> properties, IFormGroupSettings formSettings, IContextProvider contextProvider)
        {
            this.properties = properties;
            this.formSettings = formSettings;

            this.validateIfManager = new ValidateIfManager<TModel>
            (
                this.properties,
                contextProvider.ConditionalValidationConditionsBuilder.GetConditions<TModel>
                (
                    this.formSettings,
                    this.properties
                ),
                contextProvider.Mapper,
                contextProvider.UiNotificationService
            );

            this.hideIfManager = new HideIfManager<TModel>
            (
                this.properties,
                contextProvider.HideIfConditionalDirectiveBuilder.GetConditions<TModel>
                (
                    this.formSettings,
                    this.properties
                ),
                contextProvider.Mapper,
                contextProvider.UiNotificationService
            );

            this.clearIfManager = new ClearIfManager<TModel>
            (
                this.properties,
                contextProvider.ClearIfConditionalDirectiveBuilder.GetConditions<TModel>
                (
                    this.formSettings,
                    this.properties
                ),
                contextProvider.Mapper,
                contextProvider.UiNotificationService
            );

            this.reloadIfManager = new ReloadIfManager<TModel>
            (
                this.properties,
                contextProvider.ReloadIfConditionalDirectiveBuilder.GetConditions<TModel>
                (
                    this.formSettings,
                    this.properties
                ),
                contextProvider.Mapper,
                contextProvider.UiNotificationService
            );
        }

        private readonly ObservableCollection<IValidatable> properties;
        private readonly IFormGroupSettings formSettings;

        private readonly ValidateIfManager<TModel> validateIfManager;
        private readonly HideIfManager<TModel> hideIfManager;
        private readonly ClearIfManager<TModel> clearIfManager;
        private readonly ReloadIfManager<TModel> reloadIfManager;

        public void Dispose()
        {
            Dispose(this.validateIfManager);
            Dispose(this.hideIfManager);
            Dispose(this.clearIfManager);
            Dispose(this.reloadIfManager);
        }

        private void Dispose(IDisposable disposable)
        {
            if (disposable != null)
                disposable.Dispose();
        }
    }
}
