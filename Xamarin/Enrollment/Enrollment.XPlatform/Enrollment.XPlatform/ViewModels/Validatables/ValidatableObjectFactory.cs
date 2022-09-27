using Enrollment.Forms.Configuration.DataForm;
using LogicBuilder.RulesDirector;
using System;
using System.Reflection;

namespace Enrollment.XPlatform.ViewModels.Validatables
{
    internal static class ValidatableObjectFactory
    {
        private static readonly DateTime DefaultDateTime = new DateTime(1900, 1, 1);

        public static IValidatable GetValidatable(object validatable, FormControlSettingsDescriptor setting)
        {
            ((IValidatable)validatable).Value = GetValue(setting);
            return (IValidatable)validatable;
        }

        public static object? GetValue(FormControlSettingsDescriptor setting)
        {
            return
            (
                typeof(ValidatableObjectFactory)
                .GetMethod
                (
                    "_GetValue",
                    1,
                    BindingFlags.NonPublic | BindingFlags.Static,
                    null,
                    new Type[]
                    {
                typeof(FormControlSettingsDescriptor)
                    },
                    null
                ) ?? throw new ArgumentException($"{nameof(_GetValue)}: {{3A81E2A1-3431-4B49-A319-741C431AB7F2}}")
            )
            .MakeGenericMethod
            (
                Type.GetType(setting.Type) ?? throw new ArgumentException($"{nameof(_GetValue)}: {{0A43E0B0-8BC6-4DF1-A159-E5E0F30521CB}}")
            )
            .Invoke(null, new object[] { setting });
        }

        private static T _GetValue<T>(FormControlSettingsDescriptor setting, object defaultValue)
        {
            if (setting.ValidationSetting?.DefaultValue != null
                && !typeof(T).AssignableFrom(setting.ValidationSetting.DefaultValue.GetType()))
                throw new ArgumentException($"{nameof(setting.ValidationSetting.DefaultValue)}: 323DA51E-BCA1-4017-A32F-A9FEF6477393");

            return (T)(setting.ValidationSetting?.DefaultValue ?? defaultValue);
        }

        private static T _GetValue<T>(FormControlSettingsDescriptor setting) 
            => _GetValue<T>
            (
                setting, 
                typeof(T) == typeof(DateTime) ? DefaultDateTime : default(T)!
            );
    }
}
