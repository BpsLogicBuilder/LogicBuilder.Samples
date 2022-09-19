using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.ViewModels.Factories;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Validators
{
    internal class ReadOnlyDirectiveManagers<TModel> : IReadOnlyDirectiveManagers
    {
        public ReadOnlyDirectiveManagers(IDirectiveManagersFactory directiveManagersFactory, ObservableCollection<IReadOnly> properties, IFormGroupSettings formSettings)
        {
            this.properties = properties;
            this.formSettings = formSettings;

            this.hideIfManager = directiveManagersFactory.GetHideIfManager
            (
                this.properties,
                GetConditions<HideIf<TModel>>()
            );

            this.clearIfManager = directiveManagersFactory.GetClearIfManager
            (
                this.properties,
                GetConditions<ClearIf<TModel>>()
            );

            this.reloadIfManager = directiveManagersFactory.GetReloadIfManager
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

        private readonly ObservableCollection<IReadOnly> properties;
        private readonly IFormGroupSettings formSettings;

        private readonly IHideIfManager hideIfManager;
        private readonly IClearIfManager clearIfManager;
        private readonly IReloadIfManager reloadIfManager;

        public void Dispose()
        {
            Dispose(this.hideIfManager);
            Dispose(this.clearIfManager);
            Dispose(this.reloadIfManager);
        }

        private static void Dispose(IDisposable disposable)
        {
            if (disposable != null)
                disposable.Dispose();
        }
    }
}
