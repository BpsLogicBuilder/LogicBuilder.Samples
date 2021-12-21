using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.ViewModels;
using System;

namespace Enrollment.XPlatform.Services
{
    public interface IReadOnlyFieldsCollectionBuilder
    {
        DetailFormLayout CreateFieldsCollection(IFormGroupSettings formSettings, Type modelType);
    }
}
