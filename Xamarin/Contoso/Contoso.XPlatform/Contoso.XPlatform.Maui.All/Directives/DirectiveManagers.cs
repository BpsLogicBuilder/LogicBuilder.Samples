using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.ViewModels.Factories;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Directives
{
    internal class DirectiveManagers<TModel> : IDirectiveManagers
    {
        public DirectiveManagers(IDirectiveManagersFactory directiveManagersFactory, ObservableCollection<IValidatable> properties, IFormGroupSettings formSettings)
        {
            this.properties = properties;
            this.formSettings = formSettings;

            validateIfManager = directiveManagersFactory.GetValidateIfManager
            (
                this.properties,
                GetConditions<ValidateIf<TModel>>()
            );

            hideIfManager = directiveManagersFactory.GetHideIfManager
            (
                this.properties,
                GetConditions<HideIf<TModel>>()
            );

            clearIfManager = directiveManagersFactory.GetClearIfManager
            (
                this.properties,
                GetConditions<ClearIf<TModel>>()
            );

            reloadIfManager = directiveManagersFactory.GetReloadIfManager
            (
                this.properties,
                GetConditions<ReloadIf<TModel>>()
            );

            List<TConditionBase> GetConditions<TConditionBase>() where TConditionBase : ConditionBase<TModel>, new()
                => directiveManagersFactory.GetDirectiveConditionsBuilder<TConditionBase, TModel>
                (
                    this.formSettings,
                    this.properties
                ).GetConditions();
        }

        private readonly ObservableCollection<IValidatable> properties;
        private readonly IFormGroupSettings formSettings;

        private readonly IValidateIfManager validateIfManager;
        private readonly IHideIfManager hideIfManager;
        private readonly IClearIfManager clearIfManager;
        private readonly IReloadIfManager reloadIfManager;

        public void Dispose()
        {
            Dispose(validateIfManager);
            Dispose(hideIfManager);
            Dispose(clearIfManager);
            Dispose(reloadIfManager);
        }

        private static void Dispose(IDisposable disposable)
        {
            if (disposable != null)
                disposable.Dispose();
        }
    }
}
