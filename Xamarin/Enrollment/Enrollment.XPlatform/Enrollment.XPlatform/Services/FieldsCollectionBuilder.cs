using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels;
using System;

namespace Enrollment.XPlatform.Services
{
    public class FieldsCollectionBuilder : IFieldsCollectionBuilder
    {
        private readonly IContextProvider contextProvider;

        public FieldsCollectionBuilder(IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public EditFormLayout CreateFieldsCollection(IFormGroupSettings formSettings, Type modelType) 
            => new FieldsCollectionHelper(formSettings.FieldSettings, formSettings, formSettings.ValidationMessages, this.contextProvider, modelType).CreateFields();
    }
}
