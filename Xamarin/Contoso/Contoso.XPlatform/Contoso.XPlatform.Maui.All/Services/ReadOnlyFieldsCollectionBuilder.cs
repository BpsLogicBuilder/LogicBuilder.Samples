using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using System;

namespace Contoso.XPlatform.Services
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
