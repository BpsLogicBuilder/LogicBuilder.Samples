using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Directives.Factories;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.Validators;
using Enrollment.XPlatform.ViewModels.Factories;
using Enrollment.XPlatform.Views.Factories;
using System;
using System.Collections.Generic;

namespace Enrollment.XPlatform.ViewModels.Validatables
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
