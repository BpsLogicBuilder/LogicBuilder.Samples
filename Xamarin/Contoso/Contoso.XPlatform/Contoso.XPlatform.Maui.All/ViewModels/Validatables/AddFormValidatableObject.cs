using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.Factories;
using System;
using System.Collections.Generic;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class AddFormValidatableObject<T> : FormValidatableObject<T> where T : class
    {
        public AddFormValidatableObject(
            ICollectionBuilderFactory collectionBuilderFactory,
            IContextProvider contextProvider,
            IDirectiveManagersFactory directiveManagersFactory,
            string name,
            IChildFormGroupSettings setting,
            IEnumerable<IValidationRule> validations) : base(collectionBuilderFactory, contextProvider, directiveManagersFactory, name, setting, validations)
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
