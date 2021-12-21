using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels;
using System;

namespace Enrollment.XPlatform.Services
{
    public class ReadOnlyFieldsCollectionBuilder : IReadOnlyFieldsCollectionBuilder
    {
        private readonly IContextProvider contextProvider;

        public ReadOnlyFieldsCollectionBuilder(IContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public DetailFormLayout CreateFieldsCollection(IFormGroupSettings formSettings, Type modelType)
            => new ReadOnlyFieldsCollectionHelper(formSettings.FieldSettings, formSettings, this.contextProvider, modelType).CreateFields();
    }
}
