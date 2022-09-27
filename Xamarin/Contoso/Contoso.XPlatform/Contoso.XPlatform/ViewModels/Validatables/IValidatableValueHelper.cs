using Contoso.Forms.Configuration.DataForm;
using System;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public interface IValidatableValueHelper
    {
        object? GetDefaultValue(FormControlSettingsDescriptor setting, Type type);
    }
}
