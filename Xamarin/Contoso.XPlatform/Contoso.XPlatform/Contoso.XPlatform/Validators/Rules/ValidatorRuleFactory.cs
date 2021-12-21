using Contoso.Forms.Configuration.DataForm;
using Contoso.Forms.Configuration.Validation;
using Contoso.XPlatform.ViewModels.Validatables;
using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Contoso.XPlatform.Validators.Rules
{
    internal class ValidatorRuleFactory
    {
        public ValidatorRuleFactory(string parentName)
        {
            this.parentName = parentName;
        }

        private readonly string parentName;

        public IValidationRule GetValidatorRule(ValidatorDefinitionDescriptor validator, FormControlSettingsDescriptor setting, Dictionary<string, List<ValidationRuleDescriptor>> validationMessages, ObservableCollection<IValidatable> fields)
            => (IValidationRule)typeof(ValidatorRuleFactory).GetMethod
            (
                "_GetValidatorRule",
                1,
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                new Type[]
                {
                    typeof(ValidatorDefinitionDescriptor),
                    typeof(FormControlSettingsDescriptor),
                    typeof(Dictionary<string, List<ValidationRuleDescriptor>>),
                    typeof(ObservableCollection<IValidatable>)
                },
                null
            )
            .MakeGenericMethod(Type.GetType(setting.Type)).Invoke
            (
                this,
                new object[]
                {
                    validator,
                    setting,
                    validationMessages,
                    fields
                }
            );

        private IValidationRule _GetValidatorRule<T>(ValidatorDefinitionDescriptor validator, FormControlSettingsDescriptor setting, Dictionary<string, List<ValidationRuleDescriptor>> validationMessages, ObservableCollection<IValidatable> fields)
        {
            if (validationMessages == null)
                throw new ArgumentException($"{nameof(validationMessages)}: C1BDA4F7-B684-438F-B5BB-B61F01B625CE");

            if (!validationMessages.TryGetValue(setting.Field, out List<ValidationRuleDescriptor> methodList))
                throw new ArgumentException($"{nameof(setting.Field)}: 4FF12AAC-DF7F-4346-8747-52413FCA808F");

            Dictionary<string, string> methodDictionary = methodList.ToDictionary(vr => vr.ClassName, vr => vr.Message);

            if (!methodDictionary.TryGetValue(validator.ClassName, out string validationMessage))
                throw new ArgumentException($"{nameof(validator.ClassName)}: 8A45F637-347D-4578-9F9C-72E9026FBCEB");

            if (validator.ClassName == nameof(RequiredRule<T>))
                return GetRequiredRule();
            else if (validator.ClassName == nameof(IsMatchRule<T>))
                return GetIsMatchRule();
            else if (validator.ClassName == nameof(RangeRule<int>))
                return GetRangeRule();
            else if (validator.ClassName == nameof(MustBeNumberRule<T>))
                return GetMustBeNumberRule();
            else if (validator.ClassName == nameof(MustBePositiveNumberRule<T>))
                return GetMustBePositiveNumberRule();
            else if (validator.ClassName == nameof(MustBeIntegerRule<T>))
                return GetMustBeIntegerRule();
            else if (validator.ClassName == nameof(IsLengthValidRule))
                return GetIsLengthValidRule();
            else if (validator.ClassName == nameof(IsValidEmailRule))
                return GetIsValidEmailRule();
            else if (validator.ClassName == nameof(IsValidPasswordRule))
                return GetIsValidPasswordRule();
            else if (validator.ClassName == nameof(IsPatternMatchRule))
                return GetIsPatternMatchRule();
            else if (validator.ClassName == nameof(IsValueTrueRule))
                return GetIsValueTrueRule();
            else if (validator.ClassName == nameof(AtLeastOneRequiredRule<List<string>>))
                return GetAtLeastOneRequiredRule();
            else
                throw new ArgumentException($"{nameof(validator.ClassName)}: CF4FDB4D-F135-40E0-BB31-14DBA624FC25");

            IValidationRule GetIsValueTrueRule()
                => new IsValueTrueRule
                (
                    GetFieldName(setting.Field),
                    validationMessage,
                    fields
                );

            IValidationRule GetIsValidPasswordRule()
                => new IsValidEmailRule
                (
                    GetFieldName(setting.Field),
                    validationMessage,
                    fields
                );

            IValidationRule GetIsValidEmailRule()
                => new IsValidEmailRule
                (
                    GetFieldName(setting.Field),
                    validationMessage,
                    fields
                );

            IValidationRule GetIsPatternMatchRule()
            {
                const string pattern = "pattern";
                ValidatorArgumentDescriptor patternDescriptor = null;
                if (validator.Arguments?.TryGetValue(pattern, out patternDescriptor) != true)
                    throw new ArgumentException($"{pattern}: 086E280E-03C7-4900-A8DB-2C570CEEC91A");

                return new IsPatternMatchRule
                (
                    GetFieldName(setting.Field),
                    validationMessage,
                    fields,
                    (string)patternDescriptor?.Value
                );
            }

            IValidationRule GetIsLengthValidRule()
            {
                const string argumentMin = "minimunLength";
                const string argumentMax = "maximunLength";
                if (!validator.Arguments.TryGetValue(argumentMin, out ValidatorArgumentDescriptor minDescriptor))
                    throw new ArgumentException($"{argumentMin}: 521CBE54-0677-4633-AB4F-35A355490D89");
                if (!validator.Arguments.TryGetValue(argumentMax, out ValidatorArgumentDescriptor maxDescriptor))
                    throw new ArgumentException($"{argumentMax}: EEB2EC10-42B9-49EC-A7A3-86530D11C679");

                return new IsLengthValidRule
                (
                    GetFieldName(setting.Field),
                    validationMessage,
                    fields,
                    (int)minDescriptor.Value,
                    (int)maxDescriptor.Value
                );
            }

            IValidationRule GetMustBePositiveNumberRule()
                => new MustBePositiveNumberRule<T>
                (
                    GetFieldName(setting.Field),
                    validationMessage,
                    fields
                );

            IValidationRule GetMustBeNumberRule()
                => new MustBeNumberRule<T>
                (
                    GetFieldName(setting.Field),
                    validationMessage,
                    fields
                );

            IValidationRule GetMustBeIntegerRule()
                => new MustBeIntegerRule<T>
                (
                    GetFieldName(setting.Field),
                    validationMessage,
                    fields
                );

            IValidationRule GetRequiredRule()
            {
                if (setting.ValidationSetting?.DefaultValue != null
                    && !typeof(T).AssignableFrom(setting.ValidationSetting.DefaultValue.GetType()))
                    throw new ArgumentException($"{nameof(setting.ValidationSetting.DefaultValue)}: C96394B8-B26B-45B2-8C34-B9BA3FF95088");

                return new RequiredRule<T>
                (
                    GetFieldName(setting.Field),
                    validationMessage,
                    fields,
                    setting.ValidationSetting?.DefaultValue == null ? default : (T)setting.ValidationSetting.DefaultValue
                );
            }

            IValidationRule GetIsMatchRule()
            {
                const string argumentName = "otherFieldName";
                if (!validator.Arguments.TryGetValue(argumentName, out ValidatorArgumentDescriptor validatorArgumentDescriptor))
                    throw new ArgumentException($"{argumentName}: ADB88D64-F9DA-4FC0-B9C0-CB910F86B735");

                return new IsMatchRule<T>
                (
                    GetFieldName(setting.Field),
                    validationMessage,
                    fields,
                    GetFieldName((string)validatorArgumentDescriptor.Value)
                );
            }

            IValidationRule GetRangeRule()
            {
                return (IValidationRule)typeof(ValidatorRuleFactory).GetMethod
                (
                    "GetRangeRule",
                    1,
                    BindingFlags.NonPublic | BindingFlags.Instance,
                    null,
                    new Type[]
                    {
                        typeof(ValidatorDefinitionDescriptor),
                        typeof(FormControlSettingsDescriptor),
                        typeof(string),
                        typeof(ObservableCollection<IValidatable>)
                    },
                    null
                )
                .MakeGenericMethod(Type.GetType(setting.Type)).Invoke
                (
                    this,
                    new object[]
                    {
                        validator,
                        setting,
                        validationMessage,
                        fields
                    }
                );
            }

            IValidationRule GetAtLeastOneRequiredRule()
            {
                return (IValidationRule)typeof(ValidatorRuleFactory).GetMethod
                (
                    "GetAtLeastOneRequiredRule",
                    1,
                    BindingFlags.NonPublic | BindingFlags.Instance,
                    null,
                    new Type[]
                    {
                        typeof(FormControlSettingsDescriptor),
                        typeof(string),
                        typeof(ObservableCollection<IValidatable>)
                    },
                    null
                )
                .MakeGenericMethod(Type.GetType(setting.Type)).Invoke
                (
                    this,
                    new object[]
                    {
                        setting,
                        validationMessage,
                        fields
                    }
                );
            }
        }

        private IValidationRule GetRangeRule<T>(ValidatorDefinitionDescriptor validator, FormControlSettingsDescriptor setting, string validationMessage, ObservableCollection<IValidatable> fields) where T : IComparable<T>
        {
            const string argumentMin = "min";
            const string argumentMax = "max";
            if (!validator.Arguments.TryGetValue(argumentMin, out ValidatorArgumentDescriptor minDescriptor))
                throw new ArgumentException($"{argumentMin}: 34965468-76E0-4FA0-A3EC-16F2BCCB2CE0");
            if (!validator.Arguments.TryGetValue(argumentMax, out ValidatorArgumentDescriptor maxDescriptor))
                throw new ArgumentException($"{argumentMax}: 6AA3A056-3ECA-4F48-B79A-A326B2188D14");

            return new RangeRule<T>
            (
                GetFieldName(setting.Field),
                validationMessage,
                fields,
                (T)minDescriptor.Value,
                (T)maxDescriptor.Value
            );
        }

        private IValidationRule GetAtLeastOneRequiredRule<T>(FormControlSettingsDescriptor setting, string validationMessage, ObservableCollection<IValidatable> fields) where T : IEnumerable<object>
        {
            return (IValidationRule)Activator.CreateInstance
            (
                typeof(AtLeastOneRequiredRule<>).MakeGenericType
                (
                    typeof(ObservableCollection<>).MakeGenericType
                    (
                        typeof(T).GetGenericArguments()[0]
                    )
                ),
                new object[]
                {
                    GetFieldName(setting.Field),
                    validationMessage,
                    fields
                }
            );
        }

        string GetFieldName(string field)
                => parentName == null ? field : $"{parentName}.{field}";
    }
}
