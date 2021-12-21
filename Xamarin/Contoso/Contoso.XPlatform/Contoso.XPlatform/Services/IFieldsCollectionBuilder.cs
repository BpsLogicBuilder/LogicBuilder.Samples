using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.ViewModels;
using System;

namespace Contoso.XPlatform.Services
{
    public interface IFieldsCollectionBuilder
    {
        EditFormLayout CreateFieldsCollection(IFormGroupSettings formSettings, Type modelType);
    }
}
