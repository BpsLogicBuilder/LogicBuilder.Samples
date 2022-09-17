using AutoMapper;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels.Factories;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Validators
{
    internal class DirectiveManagers<TModel> : IDirectiveManagers
    {
        public DirectiveManagers(IDirectiveManagersFactory directiveManagersFactory,  IContextProvider contextProvider, IMapper mapper, ObservableCollection<IValidatable> properties, IFormGroupSettings formSettings)
        {
            this.properties = properties;
            this.formSettings = formSettings;

            this.validateIfManager = directiveManagersFactory.GetValidateIfManager
            (
                this.properties,
                contextProvider.ConditionalValidationConditionsBuilder.GetConditions<TModel>
                (
                    this.formSettings,
                    this.properties
                )
            );

            this.hideIfManager = directiveManagersFactory.GetHideIfManager
            (
                this.properties,
                contextProvider.HideIfConditionalDirectiveBuilder.GetConditions<TModel>
                (
                    this.formSettings,
                    this.properties
                )
            );

            this.clearIfManager = directiveManagersFactory.GetClearIfManager
            (
                this.properties,
                contextProvider.ClearIfConditionalDirectiveBuilder.GetConditions<TModel>
                (
                    this.formSettings,
                    this.properties
                )
            );

            this.reloadIfManager = directiveManagersFactory.GetReloadIfManager
            (
                this.properties,
                contextProvider.ReloadIfConditionalDirectiveBuilder.GetConditions<TModel>
                (
                    this.formSettings,
                    this.properties
                )
            );
        }

        private readonly ObservableCollection<IValidatable> properties;
        private readonly IFormGroupSettings formSettings;

        private readonly IValidateIfManager validateIfManager;
        private readonly IHideIfManager hideIfManager;
        private readonly IClearIfManager clearIfManager;
        private readonly IReloadIfManager reloadIfManager;

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
