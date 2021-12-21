using Contoso.Forms.Configuration.DataForm;
using LogicBuilder.RulesDirector;
using System;
using System.Reflection;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    internal static class ValidatableObjectFactory
    {
        private static readonly DateTime DefaultDateTime = new DateTime(1900, 1, 1);

        public static IValidatable GetValidatable(object validatable, FormControlSettingsDescriptor setting)
        {
            ((IValidatable)validatable).Value = GetValue(setting);
            return (IValidatable)validatable;
        }

        public static object GetValue(FormControlSettingsDescriptor setting)
            => typeof(ValidatableObjectFactory)
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
                )
                .MakeGenericMethod(Type.GetType(setting.Type))
                .Invoke(null, new object[] { setting });

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
                typeof(T) == typeof(DateTime) ? DefaultDateTime : default(T)
            );
    }
}
