using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Directives.Factories;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.Factories;
using Contoso.XPlatform.Views.Factories;
using System;
using System.Collections.Generic;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class AddFormValidatableObject<T> : FormValidatableObject<T> where T : class
    {
        public AddFormValidatableObject(
            ICollectionBuilderFactory collectionBuilderFactory,
            IDirectiveManagersFactory directiveManagersFactory,
            IEntityUpdater entityUpdater,
            IPopupFormFactory popupFormFactory,
            IPropertiesUpdater propertiesUpdater,
            UiNotificationService uiNotificationService,
            string name,
            IChildFormGroupSettings setting,
            IEnumerable<IValidationRule> validations) : base(
                collectionBuilderFactory,
                directiveManagersFactory,
                entityUpdater,
                popupFormFactory,
                propertiesUpdater,
                uiNotificationService,
                name,
                setting,
                validations)
        {
        }

        protected override void CreateFieldsCollection()
        {
            FormLayout = this.fieldsCollectionBuilder.CreateFields();
        }

        public event EventHandler? AddCancelled;

        protected override void Cancel()
        {
            AddCancelled?.Invoke(this, new EventArgs());
            base.Cancel();
        }
    }
}
