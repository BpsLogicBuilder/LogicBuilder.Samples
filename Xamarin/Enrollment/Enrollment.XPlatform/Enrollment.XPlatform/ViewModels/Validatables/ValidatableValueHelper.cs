using Enrollment.Forms.Configuration.DataForm;
using LogicBuilder.Expressions.Utils;
using LogicBuilder.RulesDirector;
using System;

namespace Enrollment.XPlatform.ViewModels.Validatables
{
    internal class ValidatableValueHelper : IValidatableValueHelper
    {
        private static readonly DateTime DefaultDateTime = new(1900, 1, 1);

        public object? GetDefaultValue(FormControlSettingsDescriptor setting, Type type)
        {
            if (setting.ValidationSetting?.DefaultValue != null)
            {
                if (!type.AssignableFrom(setting.ValidationSetting.DefaultValue.GetType()))
                    throw new ArgumentException($"{nameof(setting.ValidationSetting.DefaultValue)}: {{974B3DA6-C626-4CE4-AB46-A5EDACBD2CFC}}");

                return setting.ValidationSetting?.DefaultValue;
            }

            if (type == typeof(DateTime))
                return DefaultDateTime;

            return type.IsValueType == false || type.IsNullableType()
                ? null
                : Activator.CreateInstance(type);
        }
    }
}
