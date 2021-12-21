using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels;
using System;

namespace Enrollment.XPlatform.Services
{
    public class UpdateOnlyFieldsCollectionBuilder : IUpdateOnlyFieldsCollectionBuilder
    {
        private readonly IContextProvider contextProvider;

        public UpdateOnlyFieldsCollectionBuilder(IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public EditFormLayout CreateFieldsCollection(IFormGroupSettings formSettings, Type modelType)
            => new UpdateOnlyFieldsCollectionHelper(formSettings.FieldSettings, formSettings, formSettings.ValidationMessages, this.contextProvider, modelType).CreateFields();
    }
}
