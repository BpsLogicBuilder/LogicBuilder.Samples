using Enrollment.Forms.Configuration.DataForm;
using System;

namespace Enrollment.XPlatform.ViewModels.Validatables
{
    public interface IValidatableValueHelper
    {
        object? GetDefaultValue(FormControlSettingsDescriptor setting, Type type);
    }
}
